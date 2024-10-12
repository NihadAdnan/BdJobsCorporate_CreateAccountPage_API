using BdJobsCorporate_CreateAccountPage.AggregateRoot.Validation;
using BdJobsCorporate_CreateAccountPage.DTO.DTOs;
using BdJobsCorporate_CreateAccountPage.Handler.Abstraction;
using BdJobsCorporate_CreateAccountPage.Handler.Service;
using BdJobsCorporate_CreateAccountPage.Repository.Data;
using BdJobsCorporate_CreateAccountPage.Repository.Repository.Abstraction;
using BdJobsCorporate_CreateAccountPage.Repository.Repository;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddTransient<DapperDbContext>();



builder.Services.AddScoped<IGenericRepository, GenericRepository>();

builder.Services.AddScoped<CheckNamesHandler>();
builder.Services.AddScoped<ICheckNamesHandler, CheckNamesHandler>();
builder.Services.AddScoped<IValidator<CheckNamesRequestDTO>, CheckNamesRequestDTOValidator>();

builder.Services.AddScoped<IIndustryTypeHandler, IndustryTypeHandler>();
builder.Services.AddScoped<IValidator<IndustryTypeRequestDTO>, IndustryTypeRequestDTOValidator>();

builder.Services.AddScoped<IRLNoHandler, RLNoHandler>();
builder.Services.AddScoped<IValidator<RLNoRequestDTO>, RLDTOValidator>();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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
