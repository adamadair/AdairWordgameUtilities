using System;
using System.Windows.Input;

namespace WordGameLib
{
    /// <summary>
    /// <example>
    ///         public ICommand ButtonCommand { get; set; }
    ///
    ///         public MainWindowViewModel()
    ///         {
    ///             ButtonCommand = new RelayCommand(o => MainButtonClick("MainButton"));
    ///         }
    ///
    ///         private void MainButtonClick(object sender)
    ///         {
    ///             MessageBox.Show(sender.ToString());
    ///         }
    /// </example>
    /// </summary>
    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Predicate<object> _canExecute;

        public RelayCommand(Action<object> execute, Predicate<object> canExecute = null)
        {
            if (execute == null) throw new ArgumentNullException("execute");

            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            _execute(parameter ?? "<N/A>");
        }

    }
}
