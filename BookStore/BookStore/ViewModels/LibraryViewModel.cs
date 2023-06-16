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

namespace BookStore.ViewModels;
class LibraryViewModel : ViewModelBase
{

    private readonly IUsersRepository<User> usersRepository;
    private readonly IMessenger messenger;

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

    public LibraryViewModel(IUsersRepository<User> usersRepository, IMessenger messenger)
    {
        this.usersRepository = usersRepository;
        this.messenger = messenger;

        this.messenger.Subscribe<SendLoginedUserMessage>(obj =>
        {
            if (obj is SendLoginedUserMessage message)
            {
                this.CurrentUser = message.LoginedUser;
            }
        });
    }
}