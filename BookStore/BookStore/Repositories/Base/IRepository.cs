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
        T Add(T obj);
        bool Delete(T obj);
        T Update(T obj);
        T? FindById(int id);
    }
}
