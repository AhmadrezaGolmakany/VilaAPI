using Vila.Web.Models.Detail;
using Vila.Web.Services.Generic;

namespace Vila.Web.Services.Detail
{
    public class DetailService : Repository<DetailModel>, IDetailService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public DetailService(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }


    }
}
