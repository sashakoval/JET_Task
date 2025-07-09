using JET_Task.Application.Mapping;
using JET_Task.Application.Services;
using JET_Task.Domain.Interfaces;
using JET_Task.Infrastructure.Repositories;
using JET_Task.Infrastructure.Services;
using JET_Task.Api.Endpoints;
using JET_Task.Infrastructure.Mapping;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Configure to use HTTP only for Docker environment
builder.WebHost.UseUrls("http://+:80");

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Movie Search API", Version = "v1" });
});

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorApp", policy =>
    {
        policy.WithOrigins("http://localhost:7000", "http://localhost:3000")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Configure HttpClient
builder.Services.AddHttpClient();

// Configure Redis
builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
{
    var configuration = sp.GetRequiredService<IConfiguration>();
    var redisConnection = configuration.GetConnectionString("Redis") ?? "localhost:6379";
    return ConnectionMultiplexer.Connect(redisConnection);
});

// Configure AutoMapper
builder.Services.AddAutoMapper(typeof(MovieMappingProfile), typeof(ApplicationMappingProfile));

// Register Application Services
builder.Services.AddScoped<IMovieService, MovieService>();

// Register Infrastructure Services
builder.Services.AddScoped<IOmdbApiService, OmdbApiService>();
builder.Services.AddScoped<IMovieRepository, MovieRepository>();
builder.Services.AddScoped<ISearchQueryRepository, RedisSearchQueryRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Movie Search API v1");
    });
}

// Remove HTTPS redirection for Docker environment
// app.UseHttpsRedirection();
app.UseCors("AllowBlazorApp");

// Map Minimal API endpoints
app.MapMovieEndpoints();

app.Run();
