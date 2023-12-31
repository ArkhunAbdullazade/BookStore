﻿using BookStore.Repositories.Base;
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
using BookStore;

namespace CarShop.Repositories
{
    public class BookSqlRepository : IBooksRepository<Book>
    {
        private BookStoreDBContext context;
        public BookSqlRepository()
        {
            context = App.ServiceContainer.GetInstance<BookStoreDBContext>();
        }

        public IEnumerable<Book> GetAll() => context.Books;

        public Book Add(Book book)
        {
            var result = context.Books.Add(book);
            context.SaveChanges();

            return result.Entity;
        }

        public bool Delete(Book book)
        {
            if (book is null) return false;

            context.Books.Remove(book);
            context.SaveChanges();

            return true;
        }

        public Book? FindById(int id) => context.Books.Find(id);

        public Book Update(Book changedBook)
        {
            Book? book = context.Books.FirstOrDefault(book => book.Id == changedBook.Id);
            ArgumentNullException.ThrowIfNull(book, nameof(book));

            book.Title = changedBook.Title;
            book.Content = changedBook.Content;
            book.CoverUrl = changedBook.CoverUrl;
            book.Price = changedBook.Price;
            context.SaveChanges();
            return book;

        }
        public IEnumerable<Book> GetBooksByUserId(int id)
        {
            var query = from ub in context.UserBooks
                        join b in context.Books on ub.BookId equals b.Id
                        where ub.UserId == id
                        select b;
            return query.ToList();
        }
    }
}
