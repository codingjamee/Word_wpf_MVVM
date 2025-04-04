using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace wpf_inf.Models
{
    public class ViewModelBase :INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged (string propertyName)
            => PropertyChanged?.Invoke (this, new PropertyChangedEventArgs (propertyName));

        protected void SetProperty<T> (ref  T field, T value, [CallerMemberName] string propertyName ="")
        {
            if(!EqualityComparer<T>.Default.Equals (field, value))
            {
                field = value;
                OnPropertyChanged (propertyName);
            }
        }

        public ViewModelBase()
        { 

        }

    }
}
