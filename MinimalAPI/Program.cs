using Microsoft.Extensions.Caching.Distributed;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = "localhost:6379";
    options.InstanceName = "RedisServer";
});

var app = builder.Build();

app.UseSwagger();

var cache = app.Services.GetService<IDistributedCache>();

await cache.SetStringAsync("value_one", "First mesage test!");

app.MapGet("/", async () => await cache.GetStringAsync("value_one"));

app.UseSwaggerUI();

app.Run();
