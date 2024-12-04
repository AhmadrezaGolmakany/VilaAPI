using Vila.Web.Models.Vila;

namespace Vila.Web.Services.Vila
{
    public interface IVilaService
    {
        Task<VilaPaging> Search(int pageId , string fillter , int take);
    }
}
