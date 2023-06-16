using BookStore.Models.Messages;
using BookStore.Services.Base;
using BookStore.Tools;
using BookStore.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.ViewModels;
public class MainViewModel : ViewModelBase
{
    private readonly IMessenger messenger;

    private ViewModelBase? activeViewModel;
    public ViewModelBase? ActiveViewModel
    {
        get => activeViewModel;
        set => base.PropertyChange(out this.activeViewModel, value);
    }

    private ViewModelBase? menuViewModel;
    public ViewModelBase? MenuViewModel
    {
        get => (activeViewModel is LogInViewModel || activeViewModel is SignUpViewModel) 
                ? null : menuViewModel;
        set => base.PropertyChange(out this.menuViewModel, value);
    }

    public MainViewModel(IMessenger messenger)
    {
        this.messenger = messenger;

        this.MenuViewModel = new MenuViewModel(messenger);

        this.messenger.Subscribe<NavigationMessage>((message) => {
            if (message is NavigationMessage navigationMessage)
            {
                var viewModelObj = App.ServiceContainer.GetInstance(navigationMessage.DestinationViewModelType);
                if (viewModelObj is ViewModelBase viewModel)
                {
                    this.ActiveViewModel = viewModel;
                    bool flag = (activeViewModel is not LogInViewModel && activeViewModel is not SignUpViewModel);
                    MenuViewModel = (activeViewModel is not LogInViewModel && activeViewModel is not SignUpViewModel)
                                    ? new MenuViewModel(messenger) : null;
                }
            }
        });
    }


}
