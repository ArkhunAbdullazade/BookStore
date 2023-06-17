using BookStore.Models;
using BookStore.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Repositories.ForComments.Base
{
    public interface ICommentsRepository<TComment> : IRepository<TComment> { }
}
