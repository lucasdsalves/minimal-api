using demo_minimal_api.Data;
using demo_minimal_api.Models;
using Microsoft.EntityFrameworkCore;

#region ConfigureServices
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<MinimalContextDb>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();
#endregion

#region Environment
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseHttpsRedirection();
}
#endregion

#region Actions
app.MapGet("/products", async (MinimalContextDb context) =>
    await context.Products.ToListAsync())
    .Produces<IEnumerable<Product>>(StatusCodes.Status200OK)
    .WithName("get-products").WithTags("products");

app.MapGet("/products/{id}", async (Guid id, MinimalContextDb context) =>
    await context.Products.FindAsync(id)
        is Product product ? Results.Ok(product) : Results.NotFound())
    .Produces<Product>(StatusCodes.Status200OK)
    .Produces(StatusCodes.Status404NotFound)
    .WithName("get-products-byId").WithTags("products");

app.MapPost("/products", async (Product product, MinimalContextDb context) =>
{
    context.Products.Add(product);
    var result = await context.SaveChangesAsync();

    return result > 0 ? Results.CreatedAtRoute("get-products-byId", new { id = product.Id }, product)
        : Results.BadRequest("Houve um problema ao salvar o registro");
})
   .Produces<Product>(StatusCodes.Status201Created)
   .Produces(StatusCodes.Status400BadRequest)
   .WithName("post-products").WithTags("products");

app.MapPut("/products/id", async (MinimalContextDb context, Product product) =>
{
    if (await context.Products.FindAsync(product.Id) == null)
        return Results.NotFound();

    context.Products.Update(product);
    var result = await context.SaveChangesAsync();

    return result > 0 ? Results.NoContent() : Results.BadRequest("It's not possible to save this product.");
})
   .Produces(StatusCodes.Status204NoContent)
   .Produces(StatusCodes.Status400BadRequest)
   .Produces(StatusCodes.Status404NotFound)
   .WithName("put-products").WithTags("products");

app.MapDelete("/products/id", async (Guid id, MinimalContextDb context) =>
{
    var product = await context.Products.FindAsync(id);

    if (product == null) return Results.NotFound();

    context.Products.Remove(product);
    var result = await context.SaveChangesAsync();

    return result > 0 ? Results.NoContent() : Results.BadRequest("It's not possible to delete this product.");
})
   .Produces(StatusCodes.Status204NoContent)
   .Produces(StatusCodes.Status400BadRequest)
   .Produces(StatusCodes.Status404NotFound)
   .WithName("delete-products").WithTags("products");

#endregion

app.Run();

