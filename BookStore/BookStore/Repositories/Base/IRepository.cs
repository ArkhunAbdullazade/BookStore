using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Repositories.Base
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        void Add(T obj);
        void Delete(T obj);
        void Update(T obj);
        T? FindById(int id);
        void SaveChanges();
    }
}
