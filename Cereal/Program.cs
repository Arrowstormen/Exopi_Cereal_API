using Cereal.Data;
using Cereal.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<CerealContext>();
using (var context = new CerealContext())
{
    context.Database.EnsureCreated();
}

//Register services
var services = builder.Services;

services.AddTransient<ICerealContext, CerealContext>();
services.AddTransient<ICerealService, CerealService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
