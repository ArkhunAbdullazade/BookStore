using BookStore.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using BookStore.Models;
using BookStore.Data;
using BookStore.Repositories.Base;

namespace CarShop.Repositories
{
    public class UserBookSqlRepository : IUserBooksRepository<UserBook>
    {
        private BookStoreDBContext context;
        public UserBookSqlRepository()
        {
            context = new BookStoreDBContext();
        }

        public IEnumerable<UserBook> GetAll() => context.UserBooks;

        public void Add(UserBook userBook) => context.UserBooks.Add(userBook);

        public void Delete(UserBook userBook) => context.UserBooks.Remove(userBook);

        public UserBook? FindById(int id) => context.UserBooks.Find(id);

        void IRepository<UserBook>.SaveChanges()
        {
            context.SaveChanges();
        }

        public void Update(UserBook userBook)
        {
            throw new NotImplementedException();
        }
    }
}
