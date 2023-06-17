using BookStore.Models.Messages.Base;
using BookStore.Models.Messages;
using BookStore.Models;
using BookStore.Repositories.Base;
using BookStore.Services.Base;
using BookStore.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Tools;

namespace BookStore.ViewModels
{
    public class BookViewModel : ViewModelBase
    {
        private readonly IMessenger messenger;

        private User? currentUser;
        public User? CurrentUser
        {
            get => this.currentUser;
            set => base.PropertyChange(out currentUser, value);
        }

        private Book? currentBook;
        public Book? CurrentBook
        {
            get => this.currentBook;
            set => base.PropertyChange(out currentBook, value);
        }

        private string? fontSizeText;
        public string? FontSizeText
        {
            get => this.fontSizeText;
            set => base.PropertyChange(out fontSizeText, value);
        }

        private int fontSize = 16;
        public int FontSize
        {
            get => this.fontSize;
            set => base.PropertyChange(out fontSize, value);
        }

        private Command? logoutCommand;
        public Command LogoutCommand
        {
            get => this.logoutCommand ??= new Command(
                action: () =>
                {
                    this.CurrentUser = null;
                    this.messenger.Send(new NavigationMessage(typeof(LogInViewModel)));
                },
                predicate: () => true);
            set => base.PropertyChange(out this.logoutCommand, value);
        }
        
        private Command? setFontSizeCommand;
        public Command SetFontSizeCommand
        {
            get => this.setFontSizeCommand ??= new Command(
                action: () =>
                {
                    if (int.TryParse(FontSizeText, out int result))
                    {
                        FontSize = result;
                    }
                },
                predicate: () => true);
            set => base.PropertyChange(out this.setFontSizeCommand, value);
        }

        public BookViewModel(IMessenger messenger)
        {
            this.messenger = messenger;

            this.messenger.Subscribe<SendLoginedUserMessage>(obj =>
            {
                if (obj is SendLoginedUserMessage message)
                {
                    this.CurrentUser = message.LoginedUser;
                }
            });

            this.messenger.Subscribe<SendSelectedBookMessage>(obj =>
            {
                if (obj is SendSelectedBookMessage message)
                {
                    this.CurrentBook = message.SelectedBook;
                }
            });
        }
    }
}
