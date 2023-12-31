﻿using BookStore.Models.Messages.Base;
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
using BookStore.Data;
using System.Diagnostics;
using System.Reflection.Metadata;
using Microsoft.EntityFrameworkCore.Storage;

namespace BookStore.ViewModels;
class ShopViewModel : ViewModelBase
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

    private Command? buyCommand;
    public Command BuyCommand
    {
        get => this.buyCommand ??= new Command(
            action: () =>
            {
                if (SelectedBook is null) return;
                this.BuyBook();
                this.UpdateBooksList();
            },
            predicate: () => true);
        set => base.PropertyChange(out this.buyCommand, value);
    }

    private Command? aboutCommand;
    public Command AboutCommand
    {
        get => this.aboutCommand ??= new Command(
            action: () =>
            {
                if (SelectedBook is null) return;
                this.messenger.Send(new SendSelectedBookMessage(SelectedBook));
                this.messenger.Send(new NavigationMessage(typeof(AboutViewModel)));
            },
            predicate: () => true);
        set => base.PropertyChange(out this.aboutCommand, value);
    }

    public ShopViewModel(IUserBooksRepository<UserBook> userBooksRepository, IBooksRepository<Book> booksRepository, IMessenger messenger)
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

    void BuyBook()
    {
        try
        {
            if (CurrentUser.Amount < SelectedBook.Price) throw new Exception("You dont have enough money");

            CurrentUser.Amount -= SelectedBook.Price;
            UserBook userBookToAdd = new UserBook()
            {
                User = CurrentUser,
                Book = SelectedBook
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

    void UpdateBooksList()
    {
        var booksToExclude = this.booksRepository.GetBooksByUserId(CurrentUser.Id);
        Books.Clear();

        if (booksToExclude.Count() == 0)
            foreach (var book in this.booksRepository.GetAll()) { Books.Add(book); }
        else
            foreach (var book in this.booksRepository.GetAll())
            { if (!booksToExclude.Contains(book)) Books.Add(book); }
    }
}