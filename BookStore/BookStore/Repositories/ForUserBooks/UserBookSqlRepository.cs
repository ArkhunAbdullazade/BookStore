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

        public UserBook Add(UserBook userBook)
        {
            var result = context.UserBooks.Add(userBook);
            context.SaveChanges();

            return result.Entity;
        }

        public bool Delete(UserBook userBook)
        {
            if (userBook is null) return false;
            else if (userBook.BookId == 0 || userBook.UserId == 0) return false;

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
