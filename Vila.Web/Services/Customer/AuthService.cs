using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Vila.Web.Services.Customer
{
    public class AuthService : IAuthService
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public AuthService(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public string GetJwtToken()
        {
            var claims = _contextAccessor.HttpContext.User.Claims.ToList();

            if (claims.Count<1)
            {
                return "";
            }
            return _contextAccessor.HttpContext.User.Claims.FirstOrDefault(x=>x.Type == "JWTsecret").Value;
        }

        public void SignOut()
        {
            _contextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}