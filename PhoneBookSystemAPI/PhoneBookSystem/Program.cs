using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PhoneBook.Application.Contract;
using PhoneBook.Application.Mapper;
using PhoneBook.Application.Service;
using PhoneBook.Application.Validator;
using PhoneBook.Context;
using PhoneBook.Infrastructure;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddValidatorsFromAssemblyContaining<ContactValidator>();
builder.Services.AddScoped<IContactRepository, ContactRepository>();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<PhoneBookContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddScoped<IContactService, ContactService>();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "PhoneBook API", Version = "v1" });
});

builder.Services.AddCors(P =>
{
    P.AddPolicy("Default",
    policy => policy.AllowAnyHeader()
    .AllowAnyOrigin()
    .AllowAnyMethod());
});

var app = builder.Build();

app.UseCors("Default");

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
