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
using System.Windows;
using BookStore.Tools;
using BookStore.Models.Messages;

namespace BookStore.ViewModels;
public class MenuViewModel : ViewModelBase
{
    private readonly IMessenger messenger;

    private Command navigateToHomeCommand;
    public Command NavigateToHomeCommand
    {
        get => this.navigateToHomeCommand ??= new Command(
               action: () =>
               {
                   this.messenger.Send(new UpdateBooksListMessage());
                   this.messenger.Send(new NavigationMessage(typeof(HomeViewModel)));
               },
               predicate: () => true);
        set => base.PropertyChange(out navigateToHomeCommand, value);
    }

    private Command navigateToProfileCommand;
    public Command NavigateToProfileCommand
    {
        get => this.navigateToProfileCommand ??= new Command(
               action: () => this.messenger.Send(new NavigationMessage(typeof(ProfileViewModel))),
               predicate: () => true);
        set => base.PropertyChange(out navigateToProfileCommand, value);
    }

    private Command navigateToShopCommand;
    public Command NavigateToShopCommand
    {
        get => this.navigateToShopCommand ??= new Command(
               action: () =>
               {
                   this.messenger.Send(new UpdateBooksListMessage());
                   this.messenger.Send(new NavigationMessage(typeof(ShopViewModel)));
               },
               predicate: () => true)
        {

        };
        set => base.PropertyChange(out navigateToShopCommand, value);
    }

    private Command navigateToLibraryCommand;
    public Command NavigateToLibraryCommand
    {
        get => this.navigateToLibraryCommand ??= new Command(
               action: () =>
               {
                   this.messenger.Send(new UpdateBooksListMessage());
                   this.messenger.Send(new NavigationMessage(typeof(LibraryViewModel)));
               },
               predicate: () => true);
        set => base.PropertyChange(out navigateToLibraryCommand, value);
    }

    public MenuViewModel(IMessenger messenger)
    {
        this.messenger = messenger;
    }
}
