using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;
using System;
using IMS.EntityFrameworkCore.Data;

var builder = WebApplication.CreateBuilder(args);

//Register DbContext with PostgreSQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default"))
);


// Add services to the container for API controllers
builder.Services.AddControllers()
    .AddJsonOptions(options =>
{
options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

// Register AutoMapper
//builder.Services.AddAutoMapper(typeof(MappingProfile));

//builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
//builder.Services.AddScoped<IProductAppService, ProductAppService>();



//builder.Services.AddScoped<IProductManager, ProductManager>();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
options.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
app.UseSwagger();
app.UseSwaggerUI();
}
// Add services to the container for API controllers
app.MapControllers();
app.UseHttpsRedirection();

app.Run();