using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerDiaryDataAccess.DataAccess
{
    public interface IGenericRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        Task<ICollection<T>> GetAllAsync();
        IEnumerable<T> ListAll();
        T Get(int id);
        Task<T> GetAsync(int id);
        T Insert(T t);
        Task<T> InsertAsync(T t);
        void Delete(T entity);
        Task<int> DeleteAsync(T entity);
        T Update(T t, object key);
        Task<T> UpdateAsync(T t, object key);
    }
}
