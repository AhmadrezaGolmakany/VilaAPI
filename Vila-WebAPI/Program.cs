using Microsoft.EntityFrameworkCore;
using Vila_WebAPI.Context;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
// Add services to the container.
var Context = builder.Configuration.GetConnectionString("Vila_API");
services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

services.AddDbContext<VilaContext>(x =>
{
    x.UseSqlServer(Context);
});



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
