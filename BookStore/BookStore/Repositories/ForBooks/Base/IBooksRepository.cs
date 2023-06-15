using BookStore.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Repositories.Base
{
    public interface IBooksRepository<TBook> : IRepository<TBook> { }
}
