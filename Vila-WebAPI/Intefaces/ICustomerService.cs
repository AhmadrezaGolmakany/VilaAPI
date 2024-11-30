using Vila_WebAPI.CustomerModels;
using Vila_WebAPI.DTOs;
using Vila_WebAPI.Models;

namespace Vila_WebAPI.Intefaces
{
    public interface ICustomerService
    {
        bool Register(RegisterModel model );
        LoginResultDTO Login(string mobile, string pass);
        public bool ExistMobile(string mobile);
        public bool PasswordIsCorrect(string mobile, string pass);
    }
}
