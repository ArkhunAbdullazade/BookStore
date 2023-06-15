using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class Book
    {
        public int Id { get; private set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public ICollection<UserBook> UserBooks { get; set; }
        public ICollection<User> Users { get; set; }

        private Book() { }
        public Book(string title, string content)
        {
            Title = title;
            Content = content;
        }
    }
}
