namespace BookStore.ViewModels;

using BookStore.Models.Messages.Base;
using BookStore.Models;
using BookStore.Models.Messages;
using BookStore.Services.Base;
using BookStore.ViewModels.Base;
using System.Windows;
using System;
using BookStore.Repositories.Base;
using BookStore.Tools;

public class SignUpViewModel : ViewModelBase
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
               action: () => this.messenger.Send(new NavigationMessage(typeof(LogInViewModel))),
               predicate: () => true);
        set => base.PropertyChange(out navigationCommand, value);
    }

    private Command? signupCommand;

    public Command SignupCommand
    {
        get => this.signupCommand ??= new Command(
               action: () => SignupUser(),
               predicate: () => !string.IsNullOrWhiteSpace(this.Name) && !string.IsNullOrWhiteSpace(this.Password));
        set => base.PropertyChange(out this.signupCommand, value);
    }
    public SignUpViewModel(IUsersRepository<User> usersRepository, IMessenger messenger)
    {
        this.usersRepository = usersRepository;
        this.messenger = messenger;
    }
    void SignupUser()
    {
        try
        {
            this.usersRepository.Signup(this.Name, this.Password);
            this.usersRepository.SaveChanges();

            this.Password = string.Empty;

            this.messenger.Send(new NavigationMessage(typeof(LogInViewModel)));
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                messageBoxText: ex.Message,
                caption: "User signup Error",
                button: MessageBoxButton.OK,
                icon: MessageBoxImage.Error);
        }
    }
}