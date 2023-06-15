using BookStore.Models.Messages;
using BookStore.Services.Base;
using BookStore.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.ViewModels;
public class MainViewModel : ViewModelBase
{
    private ViewModelBase? activeViewModel;
    private readonly IMessenger messenger;
    public ViewModelBase? ActiveViewModel
    {
        get => activeViewModel;
        set => base.PropertyChange(out this.activeViewModel, value);
    }

    public MainViewModel(IMessenger messenger)
    {
        this.messenger = messenger;

        this.messenger.Subscribe<NavigationMessage>((message) => {
            if (message is NavigationMessage navigationMessage)
            {
                var viewModelObj = App.ServiceContainer.GetInstance(navigationMessage.DestinationViewModelType);
                if (viewModelObj is ViewModelBase viewModel)
                {
                    this.ActiveViewModel = viewModel;
                }
            }
        });
    }
}
