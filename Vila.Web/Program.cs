using Microsoft.Extensions.DependencyInjection;
using Vila.Web.Services.Customer;
using Vila.Web.Utility;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

#region ApiURL

var ApiUrlsSection = builder.Configuration.GetSection("ApiUrls");
services.Configure<ApiUrls>(ApiUrlsSection);

#endregion
#region IOC

services.AddTransient<ICustomerService, CustomerService>();


#endregion

services.AddHttpClient();

builder.Services.AddControllersWithViews();



var app = builder.Build();




if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
