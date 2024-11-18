using DynamicDataStruct.View;
using DynamicDataStruct.ViewModel.Services;
using Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using Tests;
using View;

namespace DynamicDataStruct.ViewModel.ViewModels.PageViewModels
{
    internal class FirstAlgorithmPageViewModel : INotifyPropertyChanged
    {
        public ICommand GenerateListCommand { get; }
        public ICommand SolveProblemCommand { get; }

        private MyList<string> l;
        private MyList<string> standartL;
        private FirstAlgorithmPage1 firstAlgorithmPage;
        public FirstAlgorithmPageViewModel(FirstAlgorithmPage1 page)
        {
            firstAlgorithmPage = page;

            GenerateListCommand = new RelayCommand(GenerateMyList);
            SolveProblemCommand = new RelayCommand(SolveProblem);
        }

        private void GenerateMyList()
        {
            firstAlgorithmPage.UniformGridForArrayBeforeReverse.Children.Clear();
            firstAlgorithmPage.UniformGridForArrayAfterReverse.Children.Clear();

            l = new MyList<string>(TestSetGenerator.GenerateStrings(12, 10, 2).ToArray());
            standartL = new MyList<string>(l.ToArrayList().ToArray());

            firstAlgorithmPage.SolveButton.IsEnabled = true;
            FillUniformGrid(firstAlgorithmPage.UniformGridForArrayBeforeReverse);
        }
        private void SolveProblem()
        {
            firstAlgorithmPage.UniformGridForArrayAfterReverse.Children.Clear();

            l.Reverse();

            FillUniformGrid(firstAlgorithmPage.UniformGridForArrayAfterReverse);

            l = new MyList<string>(standartL.ToArrayList().ToArray());
        }
        private void FillUniformGrid(UniformGrid uniformGrid)
        {
            for (int i = 0; i < l.Count; i++)
            {
                Border border = new Border();
                border.BorderBrush = new SolidColorBrush(Colors.Black);
                border.BorderThickness = new Thickness(1);
                border.Margin = new Thickness(1, 0, 1, 0);

                TextBlock element = new TextBlock();

                element.Text = l[i].Value;
                element.FontSize = 18;
                element.Background = new SolidColorBrush(Colors.LightBlue);
                element.MinWidth = 40;
                element.Padding = new Thickness(2, 1, 2, 1);
                element.TextAlignment = TextAlignment.Center;
                border.Child = element;

                uniformGrid.Children.Add(border);
            }
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
