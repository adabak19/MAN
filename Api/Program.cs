using MAN.Api.Models;
using MAN.Api.Services;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationDbContext>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddScoped<BookService>();
builder.Services.AddScoped<AuthorService>();
builder.Services.AddScoped<GenreService>();
builder.Services.AddScoped<BindingTypeService>();
builder.Services.AddScoped<BookGenreService>();
builder.Services.AddScoped<BookReadService>();
builder.Services.AddScoped<CoAuthorService>();
builder.Services.AddScoped<ProfileService>();
builder.Services.AddScoped<PublisherService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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
