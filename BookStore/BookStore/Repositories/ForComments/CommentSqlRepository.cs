using BookStore.Data;
using BookStore.Models;
using BookStore.Repositories.Base;
using BookStore.Repositories.ForComments.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Repositories.ForComments
{
    class CommentSqlRepository : ICommentsRepository<Comment>
    {
        private BookStoreDBContext context;
        public CommentSqlRepository()
        {
            context = App.ServiceContainer.GetInstance<BookStoreDBContext>();
        }

        public IEnumerable<Comment> GetAll() => context.Comments;

        public Comment Add(Comment comment)
        {
            var result = context.Comments.Add(comment);
            context.SaveChanges();

            return result.Entity;
        }

        public bool Delete(Comment comment)
        {
            if (comment is null) return false;

            context.Comments.Remove(comment);
            context.SaveChanges();

            return true;
        }

        public Comment? FindById(int id) => context.Comments.Find(id);

        public Comment Update(Comment obj)
        {
            throw new NotImplementedException();
        }
    }
}
