using Amazon;
using Amazon.DynamoDBv2;
using FluentValidation;
using LetsGetChecked.IlabUsers.Api.Domain.Entities;
using LetsGetChecked.IlabUsers.Api.Repositories;
using LetsGetChecked.IlabUsers.Api.Services;
using LetsGetChecked.IlabUsers.Api.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var config = builder.Configuration;

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<DatabaseSettings>(config.GetSection(DatabaseSettings.KeyName));


var dbConfig = new AmazonDynamoDBConfig()
{
    ServiceURL = config.GetValue<string>("AWS:ServiceURL"),
    AuthenticationRegion = config.GetValue<string>("AWS:Region")
};
builder.Services.AddSingleton<IAmazonDynamoDB>(_ => new AmazonDynamoDBClient(dbConfig));

builder.Services.AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddSingleton<IUserService, UserService>();
builder.Services.AddSingleton<IUserRepository, UserRepository>();
builder.Services.AddValidatorsFromAssemblyContaining<IUserService>(ServiceLifetime.Singleton);


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