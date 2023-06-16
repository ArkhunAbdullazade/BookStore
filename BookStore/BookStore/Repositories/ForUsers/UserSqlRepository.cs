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

namespace BookStore.Repositories
{
    public class UserSqlRepository : IUsersRepository<User>
    {
        private BookStoreDBContext context;
        public UserSqlRepository()
        {
            context = new BookStoreDBContext();
        }

        public IEnumerable<User> GetAll() => context.Users;

        public void Add(User user) => context.Users.Add(user);

        public void Delete(User user) => context.Users.Remove(user);

        public User? FindById(int id) => context.Users.Find(id);

        public User FindByNameAndPassword(string name, string password) => context.Users.First(user => user.Name == name && user.Password == password);

        public void Update(User changedUser)
        {
            User? user = context.Users.FirstOrDefault(user => user.Id == changedUser.Id);
            if (user == null) return;
            user.Name = changedUser.Name;
            user.Password = changedUser.Password;
        }
        public User? Login(string name, string password)
        {
            ArgumentNullException.ThrowIfNull(name, nameof(name));
            ArgumentNullException.ThrowIfNull(password, nameof(password));

            return this.FindByNameAndPassword(name, password);
        }

        public void Signup(string name, string password)
        {
            ArgumentNullException.ThrowIfNull(name, nameof(name));
            ArgumentNullException.ThrowIfNull(password, nameof(password));

            User user = new(name, password);
            this.Add(user);
        }

        void IRepository<User>.SaveChanges()
        {
            context.SaveChanges();
        }
    }
}
