using BookStore.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Repositories.Base
{
    public interface IUsersRepository<TUser> : IRepository<TUser>
    {
        TUser? Login(string name, string password);
        void Signup(string name, string password);
        TUser? FindByNameAndPassword(string name, string password);
    }
}
