using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PicpayChal.App.Repositories.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        T Create(T entity);
        T Update(T entity);
        bool Delete(T entity);

        Task<T> GetById(Guid Id);
        Task<List<T>> GetAll();
    }
}