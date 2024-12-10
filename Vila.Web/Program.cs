using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.DependencyInjection;
using Vila.Web.Services.Customer;
using Vila.Web.Services.Detail;
using Vila.Web.Services.Vila;
using Vila.Web.Utility;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
services.AddCors();
#region ApiURL

var ApiUrlsSection = builder.Configuration.GetSection("ApiUrls");
services.Configure<ApiUrls>(ApiUrlsSection);

#endregion

#region IOC

services.AddTransient<ICustomerService, CustomerService>();
services.AddTransient<IVilaService, VilaService>();
services.AddTransient<IAuthService, AuthService>();
services.AddTransient<IDetailService, DetailService>();


#endregion

#region Session


services.AddSession(x=>
{
    x.IdleTimeout = TimeSpan.FromDays(7);
    x.Cookie.HttpOnly = true;
    x.Cookie.IsEssential = true;

});

#endregion

#region Auth

services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(x =>
    {
        x.Cookie.HttpOnly = true;
        x.ExpireTimeSpan = TimeSpan.FromDays(7);
        x.LoginPath = "/Account/Login";
        x.LogoutPath = "/Account/LogOut";
        x.AccessDeniedPath = "/Account/NotAccess";
    });

services.AddHttpContextAccessor();


services.AddHttpClient();

builder.Services.AddControllersWithViews();



var app = builder.Build();


#endregion


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
app.UseCors(x=>x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
app.UseAuthentication();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
