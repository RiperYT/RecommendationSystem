using TagsMath.Services;
using TagsMath.Services.Abstractions;
using TagsMath.Data;
using TagsMath.Data.Repositories;
using TagsMath.Data.Abstractions;
using Microsoft.EntityFrameworkCore;
using TagsMath.Common;

var builder = WebApplication.CreateBuilder(args);

string likeRules = File.ReadAllText("TagLikeMathRules.json");
_ = TagLikeMathRules.getInstance(likeRules);

string connectionRules = File.ReadAllText("ConnectionRules.json");
_ = ConnectionRules.getInstance(connectionRules);

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseNpgsql(ConnectionRules.PostgreConncection);
});

//builder.Services.AddScoped<DataContext>();

builder.Services.AddScoped<IPost_standard_TagRepository, Post_standard_TagRepository>();
builder.Services.AddScoped<IPost_stat_TagRepository, Post_stat_TagRepository>();
builder.Services.AddScoped<IPostRepository, PostRepository>();
builder.Services.AddScoped<ITagRepository, TagRepository>();
builder.Services.AddScoped<IUser_standard_TagRepository, User_standard_TagRepository>();
builder.Services.AddScoped<IUser_stat_TagRepository, User_stat_TagRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

//builder.Services.AddScoped<IBackTestService, BackTestService>();
builder.Services.AddHostedService<BackServices>();

var app = builder.Build();

app.Services.GetService<IBackTestService>();

app.MapGet("/", () => "Hello World!");

app.Run();
