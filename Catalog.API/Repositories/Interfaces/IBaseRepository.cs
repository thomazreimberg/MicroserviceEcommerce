namespace Catalog.API.Repositories.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(string id);
        Task Create(T product);
        Task<bool> Update(T product);
        Task<bool> Delete(string id);
    }
}
