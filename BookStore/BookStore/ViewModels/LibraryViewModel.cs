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

namespace BookStore.ViewModels;
class LibraryViewModel : ViewModelBase
{

    private readonly IBooksRepository<Book> booksRepository;
    private readonly IUserBooksRepository<UserBook> userBooksRepository;
    private readonly IMessenger messenger;

    public ObservableCollection<Book> Books { get; private set; }

    private User? currentUser;
    public User? CurrentUser
    {
        get => this.currentUser;
        set => base.PropertyChange(out currentUser, value);
    }

    private Book? selectedBook;
    public Book? SelectedBook
    {
        get => this.selectedBook;
        set => base.PropertyChange(out selectedBook, value);
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

    private Command? readCommand;
    public Command ReadCommand
    {
        get => this.readCommand ??= new Command(
            action: () =>
            {
                this.messenger.Send(new SendSelectedBookMessage(SelectedBook));
                this.messenger.Send(new NavigationMessage(typeof(BookViewModel)));
            },
            predicate: () => true);
        set => base.PropertyChange(out this.readCommand, value);
    }

    public LibraryViewModel(IUserBooksRepository<UserBook> userBooksRepository, IBooksRepository<Book> booksRepository, IMessenger messenger)
    {
        this.userBooksRepository = userBooksRepository;
        this.booksRepository = booksRepository;
        this.messenger = messenger;
        Books = new ObservableCollection<Book>();

        this.messenger.Subscribe<SendLoginedUserMessage>(obj =>
        {
            if (obj is SendLoginedUserMessage message)
            {
                this.CurrentUser = message.LoginedUser;
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
        var booksToInclude = this.booksRepository.GetBooksByUserId(CurrentUser.Id);
        Books.Clear();

        foreach (var book in booksToInclude) { Books.Add(book); }
    }
}