using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Password { get; set; }
        public string? AvatarUrl { get; set; }
        public double Amount { get; set; } = 0;
        public List<Comment> Comments { get; set; }
        public List<UserBook> UserBooks { get; set; }

        private User() { }
        public User(string name, string password)
        {
            Name = name;
            Password = password;
        }
        public User(string name, string password, string avatarUrl, double amount) : this(name, password)
        {
            AvatarUrl = avatarUrl;
            Amount = amount;
        }
    }
}
