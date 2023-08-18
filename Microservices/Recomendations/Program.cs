using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Recomendations.Common;
using Recomendations.Data.Abstractions;
using Recomendations.Data.Repositories;
using Recomendations.Services;
using Recomendations.Services.Abstractions;
using System.Text.Json;

string rules = File.ReadAllText("D:\\MRIYA\\MRIYA\\MRIYA_git\\Microservices\\TagLikeMathRules.json");

_ = TagStartupMathRules.getInstance(rules);
_ = ConnectionRules.getInstance(rules);

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IPostRepository, PostRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IStartupRepository, StartupRepository>();
builder.Services.AddScoped<IPostRecomendationsService, PostRecomendationsService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(builder =>
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader()
           .WithExposedHeaders("Content-Disposition")
           .WithExposedHeaders("Content-Length")
);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//app.MapGet("/", () => "Hello World!");

app.Run();