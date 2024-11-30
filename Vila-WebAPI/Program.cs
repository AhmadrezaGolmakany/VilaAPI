using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text;
using Vila_WebAPI.Context;
using Vila_WebAPI.Intefaces;
using Vila_WebAPI.Mapper;
using Vila_WebAPI.Models;
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
services.AddTransient<ICustomerService, CustomerService>();

#endregion


#region Versioning

services.AddApiVersioning(option =>
{
    option.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1,0);
    option.AssumeDefaultVersionWhenUnspecified = true;
    option.ReportApiVersions = true;
    option.ReportApiVersions = true;
    //option.ApiVersionReader = new HeaderApiVersionReader("X-ApiVersion");

});


services.AddVersionedApiExplorer(x =>
{
    x.GroupNameFormat = "'v'VVVV";
});

#endregion

#region Swagger
services.AddTransient<IConfigureOptions<SwaggerGenOptions>, SwaggerVilaDocument>();
services.AddSwaggerGen();


#endregion

#region Mapper
services.AddAutoMapper(typeof(MapperDTO));
#endregion



#region JWT

var JwtSettingSection = builder.Configuration.GetSection("JwtSetting");

services.Configure<JwtSettings>(JwtSettingSection);

var Jwtsetting = JwtSettingSection.Get<JwtSettings>();


var key = Encoding.ASCII.GetBytes(Jwtsetting.Secret);



services.AddAuthentication(x =>
{
    x.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(option =>
{
    option.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuerSigningKey = true,
        ValidIssuer = Jwtsetting.Issure,
        ValidateIssuer = true,
        ValidAudience = Jwtsetting.Audience,
        ValidateAudience = true,
        ValidateLifetime = true

    };
});

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
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
