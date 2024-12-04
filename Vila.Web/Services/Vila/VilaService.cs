using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Reflection;
using System.Text;
using Vila.Web.Models.customer;
using Vila.Web.Models;
using Vila.Web.Models.Vila;
using Vila.Web.Utility;

namespace Vila.Web.Services.Vila
{
    public class VilaService : IVilaService
    {
        private readonly ApiUrls _urls;
        private readonly IHttpClientFactory _client;


        public VilaService(IOptions<ApiUrls> urls, IHttpClientFactory client)
        {
            _urls = urls.Value;
            _client = client;

        }


        public async Task<VilaPaging> Search(int pageId, string fillter, int take)
        {
            var url = $"{_urls.BaseAddress}{_urls.VilaV2Address}?pageid={pageId}&fillter={fillter}&take={take}";
            var request = new HttpRequestMessage(HttpMethod.Get, url);

           

            var myClient = _client.CreateClient();
            HttpResponseMessage responseMessage = await myClient.SendAsync(request);



            if (responseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var jsonstring = await responseMessage.Content.ReadAsStringAsync();

                var paging = JsonConvert.DeserializeObject<VilaPaging>(jsonstring);
                return paging;
            }


            return null;
        }
    }
}
