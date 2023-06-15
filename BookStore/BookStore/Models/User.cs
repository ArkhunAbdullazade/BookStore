using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class User
    {
        public int Id { get; private set; }
        public string? Name { get; set; }
        public string? Password { get; set; }
        public ICollection<UserBook> UserBooks { get; set; }
        public ICollection<Book> Books { get; set; }

        private User() { }
        public User(string name, string password)
        {
            Name = name;
            Password = password;
        }
    }
}
