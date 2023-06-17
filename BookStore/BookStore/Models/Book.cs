using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client.Extensions.Msal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string? CoverUrl { get; set; }
        public string? Content { get; set; }
        public double Price { get; set; }
        public List<Comment> Comments { get; set; }
        public List<UserBook> UserBooks { get; set; }

        private Book() { }
        public Book(string title, string author)
        {
            Title = title;
            Author = author;
        }
    }
}
