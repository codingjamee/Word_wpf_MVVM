using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Telerik.Windows.Controls;

namespace wpf_inf
{
    public partial class CharButtonState : ViewModelBase
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
    }
}
