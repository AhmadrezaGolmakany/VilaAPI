using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json.Serialization;
using Vila.Web.Models;
using Vila.Web.Models.customer;
using Vila.Web.Utility;

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


        
      



        public async Task<OperationResult> Register(RegisterModel model)
        {
            var url = $"{_urls.BaseAddress}{_urls.CustomerAddress}/Register";

            var request = new HttpRequestMessage(HttpMethod.Post, url);



            request.Content = new
                StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

            var myClient = _client.CreateClient();

            HttpResponseMessage responseMessage = await myClient.SendAsync(request);

            OperationResult operationResult = new OperationResult();

            if (responseMessage.StatusCode == System.Net.HttpStatusCode.Created)
            {
                operationResult.Result = true;
                operationResult.Message = "";


            }
            else if (responseMessage.StatusCode == System.Net.HttpStatusCode.BadRequest)
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
