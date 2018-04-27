using System;
using System.ComponentModel;
using System.Windows.Input;
using System.Runtime.CompilerServices;

namespace RevitTestFrame.Models
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged([CallerMemberName]string name = null)
        {
            if(PropertyChanged!=null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }

    public class RelayCommand : ICommand
    {
        private Action<object> _execute = null;
        private Func<object, bool> _canExecute = null;

        public RelayCommand(Action<object> execute,Func<object,bool> canExecute =null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }
        public event EventHandler CanExecuteChanged;
        public void RaiseCanExecuteChanged()
        {
            if(CanExecuteChanged!=null)
            {
                CanExecuteChanged(this, EventArgs.Empty);
            }
        }

        public bool CanExecute(object parameter)
        {
            if (_canExecute != null)
                return _canExecute(parameter);
            return true;
        }

        public void Execute(object parameter)
        {
            if (_execute != null)
                _execute(parameter);
        }
    }
}
