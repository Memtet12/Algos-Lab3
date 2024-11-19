using DynamicDataStruct.Model.DataStruct;
using DynamicDataStruct.View;
using DynamicDataStruct.ViewModel.Services;
using Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using View;
using static System.Net.Mime.MediaTypeNames;

namespace DynamicDataStruct.ViewModel.ViewModels.WindowViewModels
{
    public class CalculatorWindowViewModel : INotifyPropertyChanged, INavigationService
    {
        public ICommand DeleteCommand { get; }
        public ICommand DeleteAllCommand { get; }
        public ICommand EquallyCommand { get; }
        public ICommand ChooseInfixCommand { get; }
        public ICommand ChoosePostfixCommand { get; }

        public ICommand StepForwardCommand { get; }

        private int stepIndex;
        private List<MyStack<string>> steps;
        private Stack<int> SingAndNumberLengths = new Stack<int>();
        private CalculatorWindow calculatorWindow;
        
        private string ChoiceInfixOrPostfix = "Infix";
        public CalculatorWindowViewModel(CalculatorWindow window)
        {
            calculatorWindow = window;
            stepIndex = 0;

            DeleteCommand = new RelayCommand(DeleteOneSign);
            DeleteAllCommand = new RelayCommand(DeleteAll);

            EquallyCommand = new RelayCommand(Equally);

            StepForwardCommand = new RelayCommand(StepForward);

            ChooseInfixCommand = new RelayCommand(ChooseInfix);
            ChoosePostfixCommand = new RelayCommand(ChoosePostfix);
        }
        public void SignAndNumberButtonClick(string signOrNumber)
        {
            calculatorWindow.ButtonEqually.IsEnabled = true;
            calculatorWindow.ButtonDeleteAll.IsEnabled = true;

            if (ChoiceInfixOrPostfix == "Postfix") 
            {
                if (signOrNumber == "Space")
                {
                    signOrNumber = " ";
                }
                SingAndNumberLengths.Push(signOrNumber.Length);
                calculatorWindow.TextBoxPostfixEntry.Text = calculatorWindow.TextBoxPostfixEntry.Text + signOrNumber;
            }
            else 
            {
                if (signOrNumber == "Space")
                {
                    signOrNumber = " ";
                }
                SingAndNumberLengths.Push(signOrNumber.Length);
                calculatorWindow.TextBoxInfixEntry.Text = calculatorWindow.TextBoxInfixEntry.Text + signOrNumber;
            }
        }

        private void DeleteOneSign()
        {
            if (ChoiceInfixOrPostfix == "Infix")
            {
                if (calculatorWindow.TextBoxInfixEntry.Text != "")
                {
                    calculatorWindow.TextBoxInfixEntry.Text = calculatorWindow.TextBoxInfixEntry.Text.Substring(0, calculatorWindow.TextBoxInfixEntry.Text.Length - SingAndNumberLengths.Pop());
                }
            }

            if (ChoiceInfixOrPostfix == "Postfix")
            {
                if (calculatorWindow.TextBoxPostfixEntry.Text != "")
                {
                    calculatorWindow.TextBoxPostfixEntry.Text = calculatorWindow.TextBoxPostfixEntry.Text.Substring(0, calculatorWindow.TextBoxPostfixEntry.Text.Length - SingAndNumberLengths.Pop());
                }
            }

            if ((calculatorWindow.TextBoxInfixEntry.Text == "") && (calculatorWindow.TextBoxPostfixEntry.Text == "") && (calculatorWindow.TextBoxResult.Text == ""))
            {

                calculatorWindow.ButtonDeleteAll.IsEnabled = false;
            }
            else
            {
                calculatorWindow.ButtonDeleteAll.IsEnabled = true;
            }
        }
        private void DeleteAll()
        {
            calculatorWindow.TextBoxInfixEntry.Text = "";
            calculatorWindow.TextBoxPostfixEntry.Text = "";
            calculatorWindow.TextBoxResult.Text = "";
            EndProgram();


            calculatorWindow.ButtonEqually.IsEnabled = false;
            calculatorWindow.ButtonDeleteAll.IsEnabled = false;
        }

        private void Equally()
        {
            try
            {
                if (ChoiceInfixOrPostfix == "Postfix") 
                {
                    if (!string.IsNullOrEmpty(calculatorWindow.TextBoxPostfixEntry.Text) && char.IsWhiteSpace(calculatorWindow.TextBoxPostfixEntry.Text[calculatorWindow.TextBoxPostfixEntry.Text.Length - 1]))
                    {
                        calculatorWindow.TextBoxPostfixEntry.Text = calculatorWindow.TextBoxPostfixEntry.Text.Substring(0, calculatorWindow.TextBoxPostfixEntry.Text.Length - 1);
                    }

                    ReversePolishEntry reversePolishEntry = new ReversePolishEntry(calculatorWindow.TextBoxPostfixEntry.Text);

                    steps = reversePolishEntry.Solve();

                    calculatorWindow.ButtonStepForward.IsEnabled = true;
                    StepForward();

                }
                else 
                {
                    string resultInfixExpression = InfixEntry.InfixToPostfix(calculatorWindow.TextBoxInfixEntry.Text);
                    calculatorWindow.TextBoxPostfixEntry.Text = resultInfixExpression;

                    ReversePolishEntry reversePolishEntry = new ReversePolishEntry(resultInfixExpression);
                
                    steps = reversePolishEntry.Solve();
                
                    calculatorWindow.ButtonStepForward.IsEnabled = true;
                    StepForward();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("К сожалению что-то пошло не так:\n" + ex.Message);
            }
        }
        private void StepForward()
        {
            if (stepIndex == steps.Count - 1)
            {
                calculatorWindow.TextBoxResult.Text = steps[stepIndex].Top();
                EndProgram();
            }
            else
            {
                
                calculatorWindow.UniformGridForStack.Children.Clear();

                List<string> listValuesStack = steps[stepIndex].Print();
                for (int i = 0 ; i < listValuesStack.Count; i++)
                {
                    Push(listValuesStack[i]);
                }
                stepIndex++;
            }
        }
        private void Push(string valueForPush)
        {
            Border border = new Border();
            border.HorizontalAlignment = HorizontalAlignment.Center;
            border.VerticalAlignment = VerticalAlignment.Bottom;
            border.BorderBrush = new SolidColorBrush(Colors.Black);
            border.BorderThickness = new Thickness(1);
            border.Margin = new Thickness(0, 1, 0, 1);

            TextBlock element = new TextBlock();

            element.Text = valueForPush;
            element.FontSize = 18;
            element.Background = new SolidColorBrush(Colors.LightBlue);
            element.MinWidth = 40;
            element.Padding = new Thickness(2, 1, 2, 1);
            element.TextAlignment = TextAlignment.Center;

            border.Child = element;

            calculatorWindow.UniformGridForStack.Children.Insert(0, border);

        }
        private void EndProgram()
        {

            calculatorWindow.UniformGridForStack.Children.Clear();
            stepIndex = 0;

            calculatorWindow.ButtonStepForward.IsEnabled = false;
        }
        private void ChooseInfix()
        {
            ChoiceInfixOrPostfix = "Infix";
            calculatorWindow.TextBoxPostfixEntry.Text = "";
            calculatorWindow.TextBoxResult.Text = "";

            calculatorWindow.ButtonDeleteAll.IsEnabled = true;
            calculatorWindow.ButtonEqually.IsEnabled = true;

            calculatorWindow.ButtonChooseInfixEntry.Background = new SolidColorBrush(Color.FromRgb(255, 199, 199));
            calculatorWindow.ButtonChoosePostfixEntry.Background = new SolidColorBrush(Color.FromRgb(103, 58, 183));
        }

        private void ChoosePostfix()
        {
            ChoiceInfixOrPostfix = "Postfix";
            calculatorWindow.TextBoxInfixEntry.Text = "";
            calculatorWindow.TextBoxResult.Text = "";

            if (calculatorWindow.TextBoxPostfixEntry.Text != "")
            {
                calculatorWindow.ButtonDeleteAll.IsEnabled = true;
                calculatorWindow.ButtonEqually.IsEnabled = true;
            }
            else
            {
                calculatorWindow.ButtonDeleteAll.IsEnabled = true;
                calculatorWindow.ButtonEqually.IsEnabled = true;
            }

            
            calculatorWindow.ButtonChoosePostfixEntry.Background = new SolidColorBrush(Color.FromRgb(255, 199, 199));
            calculatorWindow.ButtonChooseInfixEntry.Background = new SolidColorBrush(Color.FromRgb(103, 58, 183));
        }

 

        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        private string InfixOrPostfixEntry = "Infix";

        public IEnumerable GetErrors(string propertyName)
            {
                throw new NotImplementedException();
            }
        }
}
