using DynamicDataStruct.View;
using DynamicDataStruct.View.Pages;
using Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Tests;
using DynamicDataStruct.ViewModel.Services;

namespace DynamicDataStruct.ViewModel.ViewModels.PageViewModels
{
    internal class SecondAlgorithmPageViewModel : INotifyPropertyChanged
    {
        public ICommand GenerateListCommand { get; }
        public ICommand ReplaceFirstToLastCommand { get; }
        public ICommand ReplaceLastToFirstCommand { get; }

        private MyList<string> l;
        private MyList<string> standartL;
        private SecondAlgorithmPage2 secondAlgorithmPage;
        public SecondAlgorithmPageViewModel(SecondAlgorithmPage2 page)
        {
            secondAlgorithmPage = page;

            GenerateListCommand = new RelayCommand(GenerateMyList);
            ReplaceFirstToLastCommand = new RelayCommand(ReplaceFirstToLast);
            ReplaceLastToFirstCommand = new RelayCommand(ReplaceLastToFirst);
        }

        private void GenerateMyList()
        {
            secondAlgorithmPage.UniformGridForArrayBeforeReplace.Children.Clear();
            secondAlgorithmPage.UniformGridForArrayAfterReplace.Children.Clear();

            l = new MyList<string>(TestSetGenerator.GenerateStrings(15, 11, 2).ToArray());
            standartL = new MyList<string>(l.ToArrayList().ToArray());

            secondAlgorithmPage.ReplaceFirstToLastButton.IsEnabled = true;
            secondAlgorithmPage.ReplaceLastToFirstButton.IsEnabled = true;
            FillUniformGrid(secondAlgorithmPage.UniformGridForArrayBeforeReplace);
        }
        private void ReplaceFirstToLast()
        {
            secondAlgorithmPage.UniformGridForArrayAfterReplace.Children.Clear();

            l.SwapHeadWithTail(true);

            FillUniformGrid(secondAlgorithmPage.UniformGridForArrayAfterReplace);

            l = new MyList<string>(standartL.ToArrayList().ToArray());
        }
        private void ReplaceLastToFirst()
        {
            secondAlgorithmPage.UniformGridForArrayAfterReplace.Children.Clear();

            l.SwapHeadWithTail(false);

            FillUniformGrid(secondAlgorithmPage.UniformGridForArrayAfterReplace);

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
