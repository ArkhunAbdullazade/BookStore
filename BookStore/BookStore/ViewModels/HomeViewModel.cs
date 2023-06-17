using BookStore.Models.Messages.Base;
using BookStore.Models;
using BookStore.Repositories.Base;
using BookStore.Services.Base;
using BookStore.ViewModels.Base;
using BookStore.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BookStore.Models.Messages;
using System.Collections.ObjectModel;
using static System.Reflection.Metadata.BlobBuilder;

namespace BookStore.ViewModels;
class HomeViewModel : ViewModelBase
{

    private readonly IBooksRepository<Book> booksRepository;
    private readonly IMessenger messenger;

    public ObservableCollection<Book> LibraryBooks { get; private set; }
    public ObservableCollection<Book> ShopBooks { get; private set; }

    private User? currentUser;
    public User? CurrentUser
    {
        get { return currentUser; }
        set => base.PropertyChange(out currentUser, value);
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

    private Command? seeMoreShopCommand;
    public Command SeeMoreShopCommand
    {
        get => this.seeMoreShopCommand ??= new Command(
            action: () =>
            {
                this.messenger.Send(new SendLoginedUserMessage(CurrentUser));
                this.messenger.Send(new UpdateBooksListMessage());
                this.messenger.Send(new NavigationMessage(typeof(ShopViewModel)));
            },
            predicate: () => true);
        set => base.PropertyChange(out this.seeMoreShopCommand, value);
    }

    private Command? seeMoreLibraryCommand;
    public Command SeeMoreLibraryCommand
    {
        get => this.seeMoreLibraryCommand ??= new Command(
            action: () =>
            {
                this.messenger.Send(new SendLoginedUserMessage(CurrentUser));
                this.messenger.Send(new UpdateBooksListMessage());
                this.messenger.Send(new NavigationMessage(typeof(LibraryViewModel)));
            },
            predicate: () => true);
        set => base.PropertyChange(out this.seeMoreLibraryCommand, value);
    }

    public HomeViewModel(IBooksRepository<Book> booksRepository, IMessenger messenger)
    {
        this.booksRepository = booksRepository;
        this.messenger = messenger;

        ShopBooks = new ObservableCollection<Book>();
        LibraryBooks = new ObservableCollection<Book>();

        this.messenger.Subscribe<SendLoginedUserMessage>(obj =>
        {
            if (obj is SendLoginedUserMessage message)
            {
                this.CurrentUser = message.LoginedUser;
                this.UpdateBooksList();
            }
        });

        this.messenger.Subscribe<UpdateBooksListMessage>(obj =>
        {
            if (obj is UpdateBooksListMessage message)
            {
                this.UpdateBooksList();
            }
        });
    }
    void UpdateBooksList()
    {
        var booksOfUser = this.booksRepository.GetBooksByUserId(CurrentUser.Id);

        ShopBooks.Clear();
        var temp = this.booksRepository.GetAll().Take(3).ToList();
        if (booksOfUser.Count() == 0)
            foreach (var book in this.booksRepository.GetAll().Take(3).ToList()) { ShopBooks.Add(book); }
        else
            foreach (var book in this.booksRepository.GetAll().ToList())
            {
                if (ShopBooks.Count() == 3) break;
                if (!booksOfUser.Contains(book)) ShopBooks.Add(book); 
            }

        LibraryBooks.Clear();
        foreach (var book in booksOfUser.Take(3).ToList()) { LibraryBooks.Add(book); }

    }
}