using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Telerik.Windows.Controls;

namespace wpf_inf
{
    public class CharButtonState : ViewModelBase
    {
        private char _character;
        private bool _isEnabled = true;
        private ICommand _onClickChar;

        public char Character
        {
            get => _character;
            set
            {
                _character = value;
                OnPropertyChanged(nameof(Character));
            }
        }

        public bool IsEnabled
        {
            get => _isEnabled;
            set
            {
                _isEnabled = value;
                OnPropertyChanged(nameof(IsEnabled));
            }
        }

        public ICommand OnClickChar
        {
            get => _onClickChar;
            set
            {
                _onClickChar = value;
                OnPropertyChanged(nameof(OnClickChar));
            }
        }


        public CharButtonState (char character)
        {
            _character = character;
        }

        // Simple implementation of ICommand for button clicks
        public class DelegateCommand : ICommand
        {
            private readonly Action<object> _execute;
            private readonly Func<object, bool> _canExecute;

            public event EventHandler CanExecuteChanged;

            public DelegateCommand(Action<object> execute, Func<object, bool> canExecute = null)
            {
                _execute = execute ?? throw new ArgumentNullException(nameof(execute));
                _canExecute = canExecute;
            }

            public bool CanExecute(object parameter) => _canExecute == null || _canExecute(parameter);

            public void Execute(object parameter) => _execute(parameter);

            public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
