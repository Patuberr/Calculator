using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace Calculator
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private string result;

        public string Result
        {
            get { return result; }
            set
            {
                result = value;
                OnPropertyChanged("Result");
            }
        }

        private string currentNumber;
        private string selectedOperator;
        private double storedNumber;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            Clear();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void NumberButton_Click(object sender, RoutedEventArgs e)
        {
            string number = (sender as Button).Content.ToString();

            if (number == "." && currentNumber.Contains("."))
                return;

            currentNumber += number;
            Result = currentNumber;
        }

        private void OperatorButton_Click(object sender, RoutedEventArgs e)
        {
            string operatorValue = (sender as Button).Content.ToString();

            if (!string.IsNullOrEmpty(currentNumber))
            {
                if (!string.IsNullOrEmpty(selectedOperator))
                    Calculate();

                storedNumber = double.Parse(currentNumber);
                selectedOperator = operatorValue;
                currentNumber = string.Empty;
            }
        }

        private void EqualsButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(currentNumber) && !string.IsNullOrEmpty(selectedOperator))
            {
                Calculate();
                selectedOperator = string.Empty;
            }
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            Clear();
        }

        private void Clear()
        {
            currentNumber = string.Empty;
            selectedOperator = string.Empty;
            storedNumber = 0;
            Result = "0";
        }

        private void Calculate()
        {
            double currentNumberValue = double.Parse(currentNumber);
            switch (selectedOperator)
            {
                case "+":
                    storedNumber += currentNumberValue;
                    break;
                case "-":
                    storedNumber -= currentNumberValue;
                    break;
                case "*":
                    storedNumber *= currentNumberValue;
                    break;
                case "/":
                    if (currentNumberValue == 0)
                    {
                        MessageBox.Show("Cannot divide by zero.");
                        Clear();
                        return;
                    }
                    storedNumber /= currentNumberValue;
                    break;
            }

            currentNumber = storedNumber.ToString();
            Result = currentNumber;
        }
    }
}
