using GraphQL_Api.Auth;
using GraphQL_Api.GraphQL;
using GraphQL_Api.Models;

using HotChocolate.AspNetCore;
using HotChocolate;
using HotChocolate.Types;
using HotChocolate.AspNetCore.Authorization;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

using MongoDB.Driver;

using System.Text;

var builder = WebApplication.CreateBuilder(args);

// ========== CONFIGURACIÓN DE VARIABLES DE ENTORNO ==========
var mongoUri = Environment.GetEnvironmentVariable("MONGO_URI")
               ?? "mongodb://admin:admin123@mongo:27017/";

var jwtSecret = Environment.GetEnvironmentVariable("JWT_SECRET")
                ?? "supersecreto123";

var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret));

// ========== MONGO ==========
builder.Services.AddSingleton<IMongoClient>(_ => new MongoClient(mongoUri));

builder.Services.AddSingleton<IMongoDatabase>(sp =>
{
    var client = sp.GetRequiredService<IMongoClient>();
    return client.GetDatabase("notesdb");
});

// ========== AUTENTICACIÓN JWT ==========
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = signingKey,
            ValidateLifetime = true
        };
    });

builder.Services.AddAuthorization();

// ========== GRAPHQL ==========
builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>()
    .AddAuthorization()
    .AddFiltering()
    .AddSorting();

// ========== SWAGGER ==========
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ========== APP ==========
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapGraphQL("/graphql");

app.MapGet("/", () => new { ok = true, service = "graphql-api" });

app.Run();