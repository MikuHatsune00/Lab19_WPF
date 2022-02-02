using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab19_WPF
{
    public class CalculatorModel
    {
        #region Private members
        private string result;
        #endregion

        #region Constructors
        public CalculatorModel (string firstOperator, string secondOperator, string operation)
        {
            ValidateOperator(firstOperator);
            ValidateOperator(secondOperator);
            ValidateOperation(operation);

            FirstOperator = firstOperator;
            SecondOperator = secondOperator;
            Operation = operation;
            result = string.Empty;
        }
        public CalculatorModel(string firstOperator, string operation)
        {
            ValidateOperator(firstOperator);
            ValidateOperator(operation);

            FirstOperator = firstOperator;
            SecondOperator = string.Empty;
            Operation = operation;
            result = string.Empty;
        }
        public CalculatorModel()
        { FirstOperator = string.Empty;
            SecondOperator = string.Empty;
            Operation = string.Empty;
            result = string.Empty;
        }
        #endregion

        #region Public properties and methods

        public string FirstOperator { get; set; }
        public string SecondOperator { get; set; }
        public string Operation { get; set; }
        public string Result { get { return result; } }

        public void CalculateResult()
        {
            ValidateData();

            try
            {
                switch (Operation)
                {
                    case ("+"):
                        result = (Convert.ToDouble(FirstOperator) + Convert.ToDouble(SecondOperator)).ToString();
                        break;

                    case ("-"):
                        result = (Convert.ToDouble(FirstOperator) - Convert.ToDouble(SecondOperator)).ToString();
                        break;

                    case ("*"):
                        result = (Convert.ToDouble(FirstOperator) * Convert.ToDouble(SecondOperator)).ToString();
                        break;

                    case ("/"):
                        result = (Convert.ToDouble(FirstOperator) / Convert.ToDouble(SecondOperator)).ToString();
                        break;

                    case ("sin"):
                        result = Math.Sin(DegreeToRadian(Convert.ToDouble(FirstOperator))).ToString();
                        break;

                    case ("cos"):
                        result = Math.Cos(DegreeToRadian(Convert.ToDouble(FirstOperator))).ToString();
                        break;

                    case ("tan"):
                        result = Math.Tan(DegreeToRadian(Convert.ToDouble(FirstOperator))).ToString();
                        break;
                }
            }
            catch (Exception)
            {
                result = "Error";
                throw;
            }
        }

        private double DegreeToRadian(double angle)
        {
            return Math.PI * angle / 180.0; 
        }
        private void ValidateOperator(string operand)
        {
            try
            {
                Convert.ToDouble(operand);
            }
            catch (Exception)
            {
                result = "Invalid number: " + operand;
                throw;
            }
        }
        private void ValidateOperation(string operation)
        {
            switch (operation)
            {
                case "/":
                case "*":
                case "-":
                case "+":
                case "tan":
                case "cos":
                case "sin":
                    break;
                default:
                    result = "Unknown operation: " + operation;
                    throw new ArgumentException("Unknown Operation: " + operation, "operation");
            }
        }
        private void ValidateData()
        {
            switch (Operation)
            {
                case "/":
                case "*":
                case "-":
                case "+":
                    ValidateOperator(FirstOperator);
                    ValidateOperator(SecondOperator);
                    break;
                case "tan":
                case "cos":
                case "sin":
                    ValidateOperator(FirstOperator);
                    break;
                default:
                    result = "Unknown operation: " + Operation;
                    throw new ArgumentException("Unknown Operation: " + Operation, "operation");
            }
        }

        #endregion
    }
}
