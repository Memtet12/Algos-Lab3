using DynamicDataStruct.Model.DataStruct;
using DynamicDataStruct.View;
using DynamicDataStruct.ViewModel.Services;
using Model;
using System;
using System.Collections;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using View;

namespace DynamicDataStruct.ViewModel.ViewModels.WindowViewModels
{
    public class StackWindowViewModel : INotifyPropertyChanged, INavigationService
    {
        public ICommand SetStackModeCommand { get; }
        public ICommand SetQueueModeCommand { get; }
        public ICommand PushCommand { get; }
        public ICommand PopCommand { get; }
        public ICommand TopCommand { get; }
        public ICommand IsEmptyCommand { get; }
        public ICommand PrintCommand { get; }
        public string Value
        {
            get => valueForPush;
            set
            {
                valueForPush = value;
            }
        }

        private string valueForPush = "";
        private MyICollection<string> collection;
        private bool collectionTypeIsStack;
        private StackWindow stackWindow;
        public StackWindowViewModel(StackWindow window)
        {
            stackWindow = window;
            collection = new MyStack<string>();
            collectionTypeIsStack = true;

            SetStackModeCommand = new RelayCommand(SetStackMode);
            SetQueueModeCommand = new RelayCommand(SetQueueMode);
            PushCommand = new RelayCommand(Push);
            PopCommand = new RelayCommand(Pop);
            TopCommand = new RelayCommand(Top);
            IsEmptyCommand = new RelayCommand(IsEmpty);
            PrintCommand = new RelayCommand(Print);

           
        }
        private void SetStackMode()
        {
            stackWindow.UniformGridForVisualElements.Children.Clear();
            valueForPush = "";
            OnPropertyChanged(nameof(Value));
            stackWindow.ResultTextBlock.Text = "";
            collection = new MyStack<string>();
            collectionTypeIsStack = true;

            stackWindow.PushButton.Content = "Push";
            stackWindow.PopButton.Content = "Pop";
            stackWindow.PopButton.IsEnabled = false;
            stackWindow.PrintButton.IsEnabled = false;
            stackWindow.TopButton.IsEnabled = false;

            stackWindow.ButtonSetStackMode.Background = new SolidColorBrush(Color.FromRgb(255, 199, 199));
            stackWindow.ButtonSetQueueMode.Background = new SolidColorBrush(Color.FromRgb(221, 221, 221));
        }

        private void SetQueueMode()
        {
            stackWindow.UniformGridForVisualElements.Children.Clear();
            valueForPush = "";
            OnPropertyChanged(nameof(Value));
            stackWindow.ResultTextBlock.Text = "";
            collection = new MyQueue<string>();
            collectionTypeIsStack = false;

            stackWindow.PushButton.Content = "Enqueue";
            stackWindow.PopButton.Content = "Dequeue";
            stackWindow.PopButton.IsEnabled = false;
            stackWindow.PrintButton.IsEnabled = false;
            stackWindow.TopButton.IsEnabled = false;

            stackWindow.ButtonSetQueueMode.Background = new SolidColorBrush(Color.FromRgb(255, 199, 199));
            stackWindow.ButtonSetStackMode.Background = new SolidColorBrush(Color.FromRgb(221, 221, 221));
        }
        private void Push()
        {
            if (valueForPush != "")
            {
                stackWindow.PopButton.IsEnabled = true;
                stackWindow.TopButton.IsEnabled = true;
                stackWindow.PrintButton.IsEnabled = true;

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

                stackWindow.UniformGridForVisualElements.Children.Insert(0, border);

                collection.Push(valueForPush);
            }
            else
            {
                MessageBox.Show("Вводимым значением не может быть пустая строка.");
            }

        }
        private void Pop()
        {
            if (collectionTypeIsStack)
            {
                stackWindow.UniformGridForVisualElements.Children.RemoveAt(0);
            }
            else
            {
                stackWindow.UniformGridForVisualElements.Children.RemoveAt(stackWindow.UniformGridForVisualElements.Children.Count - 1);
            }

            collection.Pop();

            if (stackWindow.UniformGridForVisualElements.Children.Count == 0)
            {
                stackWindow.PopButton.IsEnabled = false;
                stackWindow.TopButton.IsEnabled = false;
                stackWindow.PrintButton.IsEnabled = false;
            }
        }
        private void Top()
        {
            stackWindow.ResultTextBlock.Text = collection.Top();
        }
        private void IsEmpty()
        {
            stackWindow.ResultTextBlock.Text = collection.IsEmpty().ToString();
        }
        private void Print()
        {
            StringBuilder result = new StringBuilder("( ");

            foreach (string el in collection.Print())
            {
                result.AppendFormat(el + ", ");
            }

            result.Remove(result.Length - 2, 2);

            result.Append(" )");

            stackWindow.ResultTextBlock.Text = result.ToString();
        }

       

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public IEnumerable GetErrors(string propertyName)
        {
            throw new NotImplementedException();
        }

    }
}
