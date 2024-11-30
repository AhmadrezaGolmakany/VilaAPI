using AutoMapper;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Vila_WebAPI.Context;
using Vila_WebAPI.CustomerModels;
using Vila_WebAPI.DTOs;
using Vila_WebAPI.Intefaces;
using Vila_WebAPI.Models;
using Vila_WebAPI.Utility;

namespace Vila_WebAPI.Services
{
    public class CustomerService : ICustomerService
    {

        private readonly VilaContext _context;
        private readonly JwtSettings _Settings;
        private readonly IMapper _Mapper;


        public CustomerService(VilaContext context , IOptions<JwtSettings> setting , IMapper Mapper)
        {
            _context = context;   
            _Settings = setting.Value;
            _Mapper = Mapper;
        }

        public bool ExistMobile(string mobile)=>
             _context.customers.Any(c=>c.Mobile.Trim() == mobile.Trim());

        public LoginResultDTO Login(string mobile, string pass)
        {
            var hashPass = PasswordHelper.EncodeProSecurity(pass.Trim());
            var user = _context.customers.SingleOrDefault(c=>c.Mobile == mobile && c.Pass == hashPass);

            if (user == null) { return null; }

            var key = Encoding.ASCII.GetBytes(_Settings.Secret);

            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescription = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name , user.userId.ToString()),
                    new Claim(ClaimTypes.Role , user.Role.ToString()),
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key) , 
                SecurityAlgorithms.HmacSha256Signature
                ),
                Issuer = _Settings.Issure,
                Audience = _Settings.Audience
            };
            
            var token = tokenHandler.CreateToken(tokenDescription);

            user.JwtSecret = tokenHandler.WriteToken(token);

            return _Mapper.Map<LoginResultDTO>(user);


        }

        public bool PasswordIsCorrect(string mobile, string pass)
        {
            var hashPass = PasswordHelper.EncodeProSecurity(pass.Trim());
            return _context.customers.Any(c => c.Mobile.Trim() == mobile.Trim() && c.Pass == hashPass);
        }

        public bool Register(RegisterModel model)
        {
            var hashPass = PasswordHelper.EncodeProSecurity(model.Pass.Trim());
            Customer customer = new()
            {
                     Mobile=model.Mobile,
                     Pass=hashPass,
                     Role = "user"
            };
            try
            {
                _context.customers.Add(customer);
                _context.SaveChanges();
                return true;
            }
            catch 
            {

                return false;
            }
            
        }
    }
}
