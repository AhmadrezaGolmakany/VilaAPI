using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Vila_WebAPI.Context;
using Vila_WebAPI.Intefaces;
using Vila_WebAPI.Mapper;
using Vila_WebAPI.Services;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
// Add services to the container.
var Context = builder.Configuration.GetConnectionString("Vila_API");
services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
//services.AddSwaggerGen();

#region SQl
services.AddDbContext<VilaContext>(x =>
{
    x.UseSqlServer(Context);
});
#endregion

#region IOC

services.AddTransient<IVilaServices, VilaServices>();
services.AddTransient<IDetailService, DetailService>();

#endregion

#region Swagger

services.AddSwaggerGen(x=> 
{
    x.SwaggerDoc("VilaOpenApi",
        new OpenApiInfo()
        {
            Title = "Vila Api",

        });

    x.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory , "comment.xml"));
});


#endregion

#region Mapper
services.AddAutoMapper(typeof(MapperDTO));
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(x =>
    {
        x.SwaggerEndpoint("/swagger/VilaOpenApi/swagger.json" , "Vila");
    });

}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
