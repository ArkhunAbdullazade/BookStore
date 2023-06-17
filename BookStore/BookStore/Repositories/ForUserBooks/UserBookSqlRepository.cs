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
using Microsoft.EntityFrameworkCore;
using BookStore;

namespace CarShop.Repositories
{
    public class UserBookSqlRepository : IUserBooksRepository<UserBook>
    {
        private BookStoreDBContext context;
        public UserBookSqlRepository()
        {
            context = App.ServiceContainer.GetInstance<BookStoreDBContext>();
        }

        public IEnumerable<UserBook> GetAll() => context.UserBooks;

        public UserBook Add(UserBook userBook)
        {
            var result = context.UserBooks.Add(userBook);
            context.SaveChanges();

            return result.Entity;
        }

        public bool Delete(UserBook userBook)
        {
            if (userBook is null) return false;

            context.UserBooks.Remove(userBook);
            context.SaveChanges();

            return true;
        }

        public UserBook? FindById(int id) => context.UserBooks.Find(id);

        public UserBook Update(UserBook userBook)
        {
            throw new NotImplementedException();
        }
    }
}
