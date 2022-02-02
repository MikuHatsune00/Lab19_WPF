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
    public class MainWindowViewModel:INotifyPropertyChanged

    {
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName]string PropertyName=null)
        { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName)); }


        #region Private members

        private CalculatorModel calculation;

      

        private string lastOperation;
        private bool newDisplayRequired = false;
        private string fullExpression;
        private string display;

        #endregion

        

            #region Public Properties

        public string FirstOperator
        {
            get { return calculation.FirstOperator; }
            set { calculation.FirstOperator = value; }
        }

        public string SecondOperator
        {
            get { return calculation.SecondOperator; }
            set { calculation.SecondOperator = value; }
        }

        public string Operation
        {
            get { return calculation.Operation; }
            set { calculation.Operation = value; }
        }

        public string LastOperation
        {
            get { return lastOperation; }
            set { lastOperation = value; }
        }

        public string Result
        {
            get { return calculation.Result; }
        }

        public string Display
        {
            get { return display; }
            set
            {
                display = value;
                OnPropertyChanged("Display");
            }
        }

        public string FullExpression
        {
            get { return fullExpression; }
            set
            {
                fullExpression = value;
                OnPropertyChanged("FullExpression");
            }
        }

        #endregion
        #region Commands
        public ICommand OperationButtonPressCommand { get; }

        public ICommand DigitButtonPressCommand { get; }

        public ICommand SingularOperationButtonPressCommand { get; }

        private static bool CanOperationButtonPress(object number)
        {
            return true;
        }
               

        private static bool CanSingularOperationButtonPress(object number)
        {
            return true;
        }
               

        private static bool CanDigitButtonPress(object button)
        {
            return true;
        }


        //метод для кнопок ввода
        public void DigitButtonPress(object button)
        {
            var textButton = (string)button;
            switch (textButton)
            {
                case "C":
                    Display = "0";
                    FirstOperator = string.Empty;
                    SecondOperator = string.Empty;
                    Operation = string.Empty;
                    LastOperation = string.Empty;
                    FullExpression = string.Empty;
                    break;
                               
                default:
                    if (display == "0" || newDisplayRequired)
                        Display = textButton;
                    else
                        Display = display + textButton;
                    break;
            }
            newDisplayRequired = false;
        }

        //метод для операций с 2 оператороами
        public void OperationButtonPress(object operation)
        {
            var textOperation = (string)operation;
            if (FirstOperator == string.Empty || LastOperation == "=")
                {
                    FirstOperator = display;
                    LastOperation = textOperation;
                }
                else
                {
                    SecondOperator = display;
                    Operation = lastOperation;
                    calculation.CalculateResult();
                    LastOperation = textOperation;
                    Display = Result;
                    FirstOperator = display;
                }
                newDisplayRequired = true;
            
            
        }

        //sin,cos,tan
        public void SingularOperationButtonPress(object operation)
        {
            var textOperation = (string)operation;
            FirstOperator = Display;
                Operation = textOperation;
                calculation.CalculateResult();
                LastOperation = "=";
                Display = Result;
                FirstOperator = display;
                newDisplayRequired = true;
            
        }




        #endregion
        #region Constructor
        
        public MainWindowViewModel()
        {
            OperationButtonPressCommand = new RelayCommand(OperationButtonPress, CanOperationButtonPress);
            DigitButtonPressCommand = new RelayCommand(DigitButtonPress, CanDigitButtonPress);
            SingularOperationButtonPressCommand = new RelayCommand(SingularOperationButtonPress, CanSingularOperationButtonPress);
            this.calculation = new CalculatorModel();
            this.display = "0";
            this.FirstOperator = string.Empty;
            this.SecondOperator = string.Empty;
            this.Operation = string.Empty;
            this.lastOperation = string.Empty;
            this.fullExpression = string.Empty;
        }
        #endregion
    }
}
