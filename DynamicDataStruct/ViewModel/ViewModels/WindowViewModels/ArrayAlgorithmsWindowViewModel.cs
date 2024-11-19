using DynamicDataStruct.View;
using System;
using System.Collections;
using System.ComponentModel;
using System.Windows.Input;
using View;
using DynamicDataStruct.ViewModel.Services;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Media;

namespace DynamicDataStruct.ViewModel.ViewModels.WindowViewModels
{
    internal class ArrayAlgorithmsWindowViewModel : INotifyPropertyChanged
    {

        private ComboBox selectionBox;
        private Button lastButton;
        private List<Page> pages;
        private ArrayAlgorithmsWindow arrayAlgorithmsWindow;
        public ArrayAlgorithmsWindowViewModel(ArrayAlgorithmsWindow window)
        {
            arrayAlgorithmsWindow = window;

            pages = new List<Page>();

            foreach (Type type in Assembly.GetExecutingAssembly()
                    .GetTypes()
                    .Where(mytype => mytype.GetInterfaces().Contains(typeof(INavigatable))))
            {
                pages.Add((Page)Activator.CreateInstance(type));
            }

            pages.Sort((page1, page2) =>
            {
                string num1 = TypeDescriptor.GetClassName(page1).Split("Page")[TypeDescriptor.GetClassName(page1).Split("Page").Length - 1];
                string num2 = TypeDescriptor.GetClassName(page2).Split("Page")[TypeDescriptor.GetClassName(page2).Split("Page").Length - 1];

                int n1 = int.Parse(num1);
                int n2 = int.Parse(num2);
                return n1.CompareTo(n2);
            });
            selectionBox = window.SelectionBox;
    
            FillSelectionBox();
            selectionBox.SelectionChanged += SelectionBox_SelectionChanged;
        }

        private void SelectionBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            arrayAlgorithmsWindow.Frame.Navigate(pages[selectionBox.SelectedIndex]);
        }

        private void FillSelectionBox()
        {
            for (int i = 1; i< pages.Count+1; i++)
            {
                selectionBox.Items.Add("Задача " + i.ToString());
                //Button button = new Button();
                //button.Content = "Задача "+ i.ToString();
                //button.FontSize = 15;
                //button.Margin = new Thickness(0, 0, 1, 2);
                //button.Click += ShowPage;
                //arrayAlgorithmsWindow.StackPanelForAlgorithms.Children.Add(button);
            }
            //lastButton = (Button)arrayAlgorithmsWindow.StackPanelForAlgorithms.Children[0];
        }

        private void ShowPage(object sender, RoutedEventArgs e)
        {
            arrayAlgorithmsWindow.Frame.Navigate(pages[selectionBox.SelectedIndex]);
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
