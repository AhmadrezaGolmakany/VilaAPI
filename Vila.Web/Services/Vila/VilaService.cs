using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Reflection;
using System.Text;
using Vila.Web.Models.customer;
using Vila.Web.Models;
using Vila.Web.Models.Vila;
using Vila.Web.Utility;
using System.Net.Http.Headers;

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


        public async Task<VilaPaging> Search(int pageId, string fillter, int take, string token)
        {
            var url = $"{_urls.BaseAddress}{_urls.VilaV2Address}?pageid={pageId}&filter={fillter}&take={take}";
            var request = new HttpRequestMessage(HttpMethod.Get, url);

           

            var myClient = _client.CreateClient();


            #region send Bearer Token Authorization
            //myClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            myClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            #endregion

            #region Sen Api Version

            //myClient.DefaultRequestHeaders.Add("X-ApiVersion", "2");

            #endregion



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
