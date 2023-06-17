﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }

        private Comment() { }
        public Comment(string Content)
        {
            this.Content = Content;
        }
    }
}
