namespace Vila.Web.Services.Generic
{
    public interface IRipository<T> where T : class
    {
        Task<List<T>> GetAll(string url , string token );
        Task<T> GetById(string url , string token );
        Task<bool> Create(string url , string token , T model );
        Task<bool> Delete(string url , string token  );
        Task<bool> UpDate(string url , string token , T model );
    }
}
