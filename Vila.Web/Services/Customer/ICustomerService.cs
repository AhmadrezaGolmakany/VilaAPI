using Vila.Web.Models;
using Vila.Web.Models.customer;

namespace Vila.Web.Services.Customer
{
    public interface ICustomerService
    {
        Task<OperationResult> Register(RegisterModel model);
        Task<LoginResultModel> Login(RegisterModel model);
    }
}
