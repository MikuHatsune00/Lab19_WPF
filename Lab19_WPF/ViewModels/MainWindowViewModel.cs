using Lab19_WPF.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Lab19_WPF.ViewModels
{
    class MainWindowViewModel:INotifyPropertyChanged

    {
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName]string PropertyName=null)
        { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName)); }
       

        private int r;
        public int R
        { get => r;
        set
            {
                r = value;
                OnPropertyChanged();
            }
        }
        private int l;
        public int L
        {
            get => l;
            set
            {
                l = value;
                OnPropertyChanged();
            }
        }

        public ICommand GetLCommand { get; }
        private void OnGetLCommandExecute(object p)
        {
            L = Geometry.GetL(R);

        }
        private bool CanGetLCommandExecuted(object p)
        {
            if (r != 0)
                return true;
            else return false;

        }

        public MainWindowViewModel()
        {

            GetLCommand = new RelayCommand(OnGetLCommandExecute, CanGetLCommandExecuted);
        }

    }
}
