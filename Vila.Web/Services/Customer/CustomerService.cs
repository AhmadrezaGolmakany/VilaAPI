using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json.Serialization;
using Vila.Web.Models;
using Vila.Web.Models.customer;
using Vila.Web.Utility;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Vila.Web.Services.Customer
{
    public class CustomerService : ICustomerService
    {
        private readonly ApiUrls _urls;
        private readonly IHttpClientFactory _client;


        public CustomerService(IOptions<ApiUrls> urls , IHttpClientFactory client)
        {
            _urls = urls.Value;
            _client = client;

        }

        public async Task<LoginResultModel> Login(RegisterModel model)
        {
            var url = $"{_urls.BaseAddress}{_urls.CustomerAddress}/Login";

            var request = new HttpRequestMessage(HttpMethod.Post, url);

            request.Content = new
               StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            var myClient = _client.CreateClient();

            HttpResponseMessage responseMessage = await myClient.SendAsync(request);

            OperationResult operationResult = new OperationResult();

            CustomerModel customer = new();

            if (responseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var jsonstring = await responseMessage.Content.ReadAsStringAsync();

                customer = JsonConvert.DeserializeObject<CustomerModel>(jsonstring);

                operationResult.Result = true;
                operationResult.Message = "ورود با موفقیت انجام شد";


            }
            else if(responseMessage.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                var jsonstring = await responseMessage.Content.ReadAsStringAsync();

                var error = JsonConvert.DeserializeObject<ErrorViewModel>(jsonstring);

                customer = null;

                operationResult.Result = false;
                operationResult.Message = error.error;
            }
            else
            {
                customer = null;

                operationResult.Result = false;
                operationResult.Message = "خطای سمت سرور";
            }


            return new()
            {
                customer = customer,
                Result = operationResult
            };



        }

        public async Task<OperationResult> Register(RegisterModel model)
        {
            var url = $"{_urls.BaseAddress}{_urls.CustomerAddress}/Register";

            var request = new HttpRequestMessage(HttpMethod.Post, url);



            request.Content = new
                StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            var myClient = _client.CreateClient();

            HttpResponseMessage responseMessage = await myClient.SendAsync(request);

            OperationResult operationResult = new OperationResult();

            if(responseMessage.StatusCode == System.Net.HttpStatusCode.Created)
            {
                operationResult.Result = true;
                operationResult.Message = "ثبت نام با موفقیت انجام شد";


            }
            else if(responseMessage.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                var jsonstring = await responseMessage.Content.ReadAsStringAsync();
                var res = JsonConvert.DeserializeObject<ErrorViewModel>(jsonstring);
                operationResult.Result = false;
                operationResult.Message = res.error;
            }
            else
            {
                operationResult.Result = false;
                operationResult.Message = "خطای سمت سرور";

            }

            return operationResult;
        }
    }
}
