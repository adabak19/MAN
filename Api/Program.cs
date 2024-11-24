using MAN.Api.Services;
using LibraryManagement.Shared.Models;
using LibraryManagement.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", policy =>
    {
        policy.WithOrigins("http://localhost:5189")  // Allow only your frontend's URL
              .WithMethods("GET", "POST", "PUT", "DELETE")  // Allow necessary HTTP methods
              .WithHeaders("Content-Type", "Authorization");  // Allow headers such as Content-Type and Authorization
    });
});

builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationDbContext>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<IGenreService, GenreService>();
builder.Services.AddScoped<IBindingTypeService, BindingTypeService>();
builder.Services.AddScoped<IBookGenreService, BookGenreService>();
builder.Services.AddScoped<IBookReadService, BookReadService>();
builder.Services.AddScoped<ICoAuthorService, CoAuthorService>();
builder.Services.AddScoped<IProfileService, ProfileService>();
builder.Services.AddScoped<IPublisherService, PublisherService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseCors("AllowSpecificOrigin");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
