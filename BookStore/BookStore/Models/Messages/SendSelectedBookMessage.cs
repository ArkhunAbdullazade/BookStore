using BookStore.Models.Messages.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Models.Messages
{
    public class SendSelectedBookMessage : IMessage
    {
        public Book SelectedBook { get; set; }
        public SendSelectedBookMessage(Book selectedBook)
        {
            this.SelectedBook = selectedBook;
        }
    }
}
