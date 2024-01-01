using Microsoft.EntityFrameworkCore;
using ProductService.Data;
using ProductService.Extensions;
using ProductService.Services;
using ProductService.Services.IServices;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//custom
builder.AddSwaggenGenExtension();
builder.AddAuth();
//db connection
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("myconnection"));
});
//Reg for DI
builder.Services.AddScoped<IProduct, ProductServices>();
builder.Services.AddScoped<IImage, ImageServices>();
//automapper 
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseMigrations();

app.UseAuthorization();

app.MapControllers();

app.Run();
