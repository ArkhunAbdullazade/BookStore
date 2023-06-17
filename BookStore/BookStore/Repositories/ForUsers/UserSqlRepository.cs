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
using static Azure.Core.HttpHeader;

namespace BookStore.Repositories
{
    public class UserSqlRepository : IUsersRepository<User>
    {
        private BookStoreDBContext context;
        public UserSqlRepository()
        {
            context = App.ServiceContainer.GetInstance<BookStoreDBContext>();
        }

        public IEnumerable<User> GetAll() => context.Users;

        public User Add(User user) 
        {
            var result = context.Users.Add(user);
            context.SaveChanges();

            return result.Entity;
        }

        public bool Delete(User user)
        {
            if (user is null) return false;

            context.Users.Remove(user);
            context.SaveChanges();

            return true;
        }

        public User? FindById(int id) => context.Users.Find(id);

        public User FindByNameAndPassword(string name, string password) => context.Users.First(user => user.Name == name && user.Password == password);

        public User Update(User changedUser)
        {
            User? user = context.Users.FirstOrDefault(user => user.Id == changedUser.Id);
            ArgumentNullException.ThrowIfNull(user, nameof(user));

            user.Name = changedUser.Name;
            user.Password = changedUser.Password;
            user.AvatarUrl = changedUser.AvatarUrl;
            user.Amount = changedUser.Amount;
            context.SaveChanges();
            return user;
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
    }
}
