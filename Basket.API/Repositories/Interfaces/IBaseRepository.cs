using System.Threading.Tasks;

namespace Basket.API.Repositories.Interfaces
{
    public interface IBaseRepository<T>
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> Update(T entity);
    }
}
