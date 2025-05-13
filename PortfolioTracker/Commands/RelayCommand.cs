using System.Windows.Input;

namespace PortfolioTracker.Commands
{
    // code from Lecture
    public class RelayCommand(Action<object?> executeAction, Predicate<object?> canExecutePredicate) : ICommand
    {
        public event EventHandler? CanExecuteChanged;
        private readonly Action<object?> executeAction = executeAction;
        private readonly Predicate<object?> canExecutePredicate = canExecutePredicate;

        public bool CanExecute(object? parameter) => canExecutePredicate(parameter);
        public void Execute(object? parameter) => executeAction(parameter);
        public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}
