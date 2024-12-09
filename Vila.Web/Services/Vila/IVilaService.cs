using Vila.Web.Models.Vila;
using Vila.Web.Services.Generic;

namespace Vila.Web.Services.Vila
{
    public interface IVilaService : IRipository<VilaModel>
    {
        Task<VilaPaging> Search(int pageId , string fillter , int take , string token);
    }
}
