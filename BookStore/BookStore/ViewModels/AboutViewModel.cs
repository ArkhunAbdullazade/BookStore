using BookStore.Models.Messages.Base;
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
using BookStore.Repositories.ForComments.Base;
using BookStore.Tools;
using CarShop.Repositories;
using System.Windows;
using BookStore.Models.Messages;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore.Update.Internal;
using static System.Reflection.Metadata.BlobBuilder;

namespace BookStore.ViewModels;
class AboutViewModel : ViewModelBase
{
    private readonly IUserBooksRepository<UserBook> userBooksRepository;
    private readonly ICommentsRepository<Comment> commentsRepository;
    private readonly IMessenger messenger;

    public ObservableCollection<Comment> Comments { get; private set; }

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

    private string? commentText;
    public string? CommentText
    {
        get => this.commentText;
        set => base.PropertyChange(out commentText, value);
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

    private Command? buyCommand;
    public Command BuyCommand
    {
        get => this.buyCommand ??= new Command(
            action: () =>
            {
                if (CurrentBook is null) return;
                this.BuyBook();
                this.messenger.Send(new UpdateBooksListMessage());
                this.messenger.Send(new NavigationMessage(typeof(ShopViewModel)));
            },
            predicate: () => true);
        set => base.PropertyChange(out this.buyCommand, value);
    }

    private Command? typeCommentCommand;
    public Command TypeCommentCommand
    {
        get => this.typeCommentCommand ??= new Command(
            action: () =>
            {
                this.commentsRepository.Add(new Comment(CommentText) { Book = CurrentBook, User = CurrentUser });
                this.UpdateCommentsList();
            },
            predicate: () => true);
        set => base.PropertyChange(out this.typeCommentCommand, value);
    }

    public AboutViewModel(IUserBooksRepository<UserBook> userBooksRepository, ICommentsRepository<Comment> commentsRepository, IMessenger messenger)
    {
        this.userBooksRepository = userBooksRepository;
        this.commentsRepository = commentsRepository;
        this.messenger = messenger;

        Comments = new ObservableCollection<Comment>();

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
                UpdateCommentsList();
            }
        });
    }

    void BuyBook()
    {
        try
        {
            if (CurrentUser.Amount < CurrentBook.Price) throw new Exception("You dont have enough money");

            CurrentUser.Amount -= CurrentBook.Price;
            UserBook userBookToAdd = new UserBook()
            {
                User = CurrentUser,
                Book = CurrentBook
            };
            userBooksRepository.Add(userBookToAdd);
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                messageBoxText: ex.Message,
                caption: "Buy Error",
                button: MessageBoxButton.OK,
                icon: MessageBoxImage.Error);
        }
    }

    void UpdateCommentsList()
    {
        Comments.Clear();

        foreach (var comment in this.commentsRepository.GetAll())
        {
            if (comment.BookId == CurrentBook.Id) Comments.Add(comment);
        }
    }
}