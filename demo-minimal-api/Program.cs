using demo_minimal_api.Data;
using demo_minimal_api.Data.Repositories;
using demo_minimal_api.Interfaces;
using demo_minimal_api.Models;
using demo_minimal_api.Services;
using demo_minimal_api.ViewModels;
using Microsoft.EntityFrameworkCore;

#region ConfigureServices
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();

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

app.MapGet("/products", async (IProductService productService) =>
    await productService.GetAll())
    .Produces<IEnumerable<ProductViewModel>>(StatusCodes.Status200OK)
    .WithName("get-products").WithTags("products");

app.MapGet("/products/{id}", async (IProductService productService, Guid id) =>
    await productService.GetById(id)
        is ProductViewModel product ? Results.Ok(product) : Results.NotFound())
    .Produces<ProductViewModel>(StatusCodes.Status200OK)
    .Produces(StatusCodes.Status404NotFound)
    .WithName("get-products-byId").WithTags("products");

app.MapPost("/products", async (IProductService productService, ProductViewModel product) =>
{
    var result = await productService.NewProduct(product);

    return result > 0 ? Results.CreatedAtRoute("get-products-byId", new { id = product.Id }, product)
        : Results.BadRequest("It's not possible to save this product.");
})
   .Produces<ProductViewModel>(StatusCodes.Status201Created)
   .Produces(StatusCodes.Status400BadRequest)
   .WithName("post-products").WithTags("products");

app.MapPut("/products/id", async (IProductService productService, ProductViewModel product) =>
{
    var result = await productService.UpdateProduct(product);
    
    if (!result) return Results.NotFound();

    return result == true ? Results.NoContent() : Results.BadRequest("It's not possible to save this product.");
})
   .Produces(StatusCodes.Status204NoContent)
   .Produces(StatusCodes.Status400BadRequest)
   .Produces(StatusCodes.Status404NotFound)
   .WithName("put-products").WithTags("products");

app.MapDelete("/products/id", async (IProductService productService, Guid id) =>
{
    var result = await productService.DeleteProduct(id);

    return result > 0 ? Results.NoContent() : Results.BadRequest("It's not possible to delete this product.");
})
   .Produces(StatusCodes.Status204NoContent)
   .Produces(StatusCodes.Status400BadRequest)
   .Produces(StatusCodes.Status404NotFound)
   .WithName("delete-products").WithTags("products");

#endregion

app.Run();

