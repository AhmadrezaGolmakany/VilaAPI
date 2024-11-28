using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Vila_WebAPI.Context;
using Vila_WebAPI.Intefaces;
using Vila_WebAPI.Mapper;
using Vila_WebAPI.Services;
using Vila_WebAPI.Utility;

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


#region Versioning

services.AddApiVersioning(option =>
{
    option.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1,0);
    option.AssumeDefaultVersionWhenUnspecified = true;
    option.ReportApiVersions = true;


});


services.AddVersionedApiExplorer(x =>
{
    x.GroupNameFormat = "'v'VVV";
});

#endregion

#region Swagger
services.AddTransient<IConfigureOptions<SwaggerGenOptions>, SwaggerVilaDocument>();
services.AddSwaggerGen();


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
        var provider = app.Services.CreateScope().ServiceProvider
                .GetRequiredService<IApiVersionDescriptionProvider>();

        foreach (var item in provider.ApiVersionDescriptions)
        {
            x.SwaggerEndpoint($"swagger/{item.GroupName}/swagger.json", item.GroupName.ToString());

        }
        //x.SwaggerEndpoint("/swagger/VilaOpenApi/swagger.json" , "Vila");
        x.RoutePrefix = "";
    });

}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
