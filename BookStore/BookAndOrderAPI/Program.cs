global using Models;
global using DataLayer.DTO;
using DataLayer.Infrastructure;
using Microsoft.EntityFrameworkCore;
using BookAndOrderAPI.Services;
using BookAndOrderAPI.Services.IServices;
using AutoMapper;
using DataLayer.Mapping;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// builder.Services.AddAutoMapper(typeof(Program).Assembly);

var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MappingProfile());
});

IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IBookReviewService, BookReviewService>();



builder.Services.AddDbContext<StoreDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

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
