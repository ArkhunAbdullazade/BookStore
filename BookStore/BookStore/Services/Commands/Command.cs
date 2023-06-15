using System;
using System.Windows;
using System.Windows.Input;

namespace BookStore.Tools;

public class Command : ICommand {
    private readonly Action action;
    private readonly Func<bool> predicate;

    public event EventHandler? CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }

    public Command(Action action, Func<bool> predicate)
    {
        this.action = action;
        this.predicate = predicate;
    }

    public bool CanExecute(object? parameter)
    {
        return predicate.Invoke();
    }

    public void Execute(object? parameter)
    {
        action.Invoke();
    }
}
