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
        public int UserId { get; set; }
        public int BookId { get; set; }
        public User User { get; set; }  
        public Book Book { get; set; }
    }
}
