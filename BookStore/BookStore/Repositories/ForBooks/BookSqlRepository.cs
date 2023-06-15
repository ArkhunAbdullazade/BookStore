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
    public class BookSqlRepository : IBooksRepository<Book>
    {
        private BookStoreDBContext context;
        public BookSqlRepository()
        {
            context = new BookStoreDBContext();
        }

        public IEnumerable<Book> GetAll() => context.Books;

        public void Add(Book user) => context.Books.Add(user);

        public void Delete(Book user) => context.Books.Remove(user);

        public Book? FindById(int id) => context.Books.Find(id);

        public void Update(Book changedBook)
        {
            Book? book = context.Books.FirstOrDefault(book => book.Id == changedBook.Id);
            if (book == null) return;
            book.Title = changedBook.Title;
            book.Content = changedBook.Content;
        }

        void IRepository<Book>.SaveChanges()
        {
            context.SaveChanges();
        }
    }
}
