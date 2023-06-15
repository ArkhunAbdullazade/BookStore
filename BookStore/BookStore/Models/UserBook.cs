using Azure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class UserBook
    {
        public int UsersId { get; set; }
        public int BooksId { get; set; }
        public User? User { get; set; } = null;
        public Book? Book { get; set; } = null;
    }
}
