using BookStore.Models.Messages.Base;
using BookStore.Models;
using BookStore.Repositories.Base;
using BookStore.Services.Base;
using BookStore.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Tools;
using BookStore.Models.Messages;

namespace BookStore.ViewModels;
class ProfileViewModel : ViewModelBase
{

    private readonly IUsersRepository<User> usersRepository;
    private readonly IMessenger messenger;

    private User? currentUser;
    public User? CurrentUser
    {
        get { return currentUser; }
        set => base.PropertyChange(out currentUser, value);
    }

    private string? name;
    public string? Name
    {
        get => name;
        set => base.PropertyChange(out name, value);
    }

    private string? password;
    public string? Password
    {
        get => password;
        set => base.PropertyChange(out password, value);
    }

    private string? avatarUrl;
    public string? AvatarUrl
    {
        get => avatarUrl;
        set => base.PropertyChange(out avatarUrl, value);
    }

    private string amount;
    public string? Amount
    {
        get => amount;
        set => base.PropertyChange(out amount, value);
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

    private Command? changeNameCommand;
    public Command ChangeNameCommand
    {
        get => this.changeNameCommand ??= new Command(
            action: () =>
            {
                CurrentUser.Name = Name;
                CurrentUser = this.usersRepository.Update(CurrentUser);
            },
            predicate: () => Name is not null);
        set => base.PropertyChange(out this.changeNameCommand, value);
    }

    private Command? changePasswordCommand;
    public Command ChangePasswordCommand
    {
        get => this.changePasswordCommand ??= new Command(
            action: () =>
            {
                CurrentUser.Password = Password;
                CurrentUser = this.usersRepository.Update(CurrentUser);
            },
            predicate: () => Password is not null);
        set => base.PropertyChange(out this.changePasswordCommand, value);
    }

    private Command? changeAvatarCommand;
    public Command ChangeAvatarCommand
    {
        get => this.changeAvatarCommand ??= new Command(
            action: () =>
            {
                CurrentUser.AvatarUrl = AvatarUrl;
                CurrentUser = this.usersRepository.Update(CurrentUser);
            },
            predicate: () => AvatarUrl is not null);
        set => base.PropertyChange(out this.changeAvatarCommand, value);
    }

    private Command? changeAmountCommand;
    public Command ChangeAmountCommand
    {
        get => this.changeAmountCommand ??= new Command(
            action: () =>
            {
                if (!double.TryParse(Amount, out double result)) return;

                CurrentUser.Amount += result;
                CurrentUser = this.usersRepository.Update(CurrentUser);
            },
            predicate: () => Amount is not null);
        set => base.PropertyChange(out this.changeAmountCommand, value);
    }

    public ProfileViewModel(IUsersRepository<User> usersRepository, IMessenger messenger)
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