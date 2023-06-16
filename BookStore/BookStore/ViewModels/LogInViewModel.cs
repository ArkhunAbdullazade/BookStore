namespace BookStore.ViewModels;

using BookStore.Models.Messages.Base;
using BookStore.Models;
using BookStore.Models.Messages;
using BookStore.Services.Base;
using BookStore.ViewModels.Base;
using System.Windows;
using System;
using BookStore.Repositories.Base;
using BookStore.ViewModels;
using BookStore.Tools;

public class LogInViewModel : ViewModelBase
{

    private readonly IUsersRepository<User> usersRepository;
    private readonly IMessenger messenger;

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

    private Command navigationCommand;
    public Command NavigationCommand
    {
        get => this.navigationCommand ??= new Command(
               action: () => this.messenger.Send(new NavigationMessage(typeof(SignUpViewModel))),
               predicate: () => true);
        set => base.PropertyChange(out navigationCommand, value);
    }

    private Command? loginCommand;

    public Command LoginCommand
    {
        get => this.loginCommand ??= new Command(
               action: () => LoginUser(),
               predicate: () => !string.IsNullOrWhiteSpace(this.Name) && !string.IsNullOrWhiteSpace(this.Password));
        set => base.PropertyChange(out this.loginCommand, value);
    }

    public LogInViewModel(IUsersRepository<User> usersRepository, IMessenger messenger)
    {
        this.usersRepository = usersRepository;
        this.messenger = messenger;
    }

    void LoginUser()
    {
        try
        {
            var user = this.usersRepository.Login(this.Name, this.Password);

            this.Name = string.Empty;
            this.Password = string.Empty;

            this.messenger.Send(new SendLoginedUserMessage(user));
            this.messenger.Send(new NavigationMessage(typeof(HomeViewModel)));
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                messageBoxText: ex.Message,
                caption: "User login Error",
                button: MessageBoxButton.OK,
                icon: MessageBoxImage.Error);
        }
    }
}