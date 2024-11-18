using DynamicDataStruct.View;
using DynamicDataStruct.View.Pages;
using DynamicDataStruct.ViewModel.Services;
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

namespace DynamicDataStruct.ViewModel.ViewModels.PageViewModels
{
    internal class SeventhAlgorithmPageViewModel : INotifyPropertyChanged
    {
        public ICommand GenerateListCommand { get; }
        public ICommand SolveCommand { get; }


        private SeventhAlgorithmPage7 seventhAlgorithmPage;
        private MyList<string> list;

        public SeventhAlgorithmPageViewModel(SeventhAlgorithmPage7 page)
        {
            seventhAlgorithmPage = page;
            seventhAlgorithmPage.TextBlockWithTask.Text = "7. Написать функцию, которая удаляет из списка L все элементы Е,\nесли таковые имеются. ";
            GenerateListCommand = new RelayCommand(GenerateMyList);
            SolveCommand = new RelayCommand(Solve);
        }

        private void GenerateMyList()
        {
            seventhAlgorithmPage.UniformGridForInitialList.Children.Clear();
            seventhAlgorithmPage.UniformGridForFinalList.Children.Clear();

            list = new MyList<string>(TestSetGenerator.GenerateStrings(12, 10, 2).ToArray());

            seventhAlgorithmPage.ButtonSolve.IsEnabled = true;
            FillUniformGrid(seventhAlgorithmPage.UniformGridForInitialList);

        }
        private void Solve() 
        {
            seventhAlgorithmPage.UniformGridForFinalList.Children.Clear();
            list.RemoveSameElements(new Node<string> (seventhAlgorithmPage.TextBoxSelectingElement.Text, null));
            FillUniformGrid(seventhAlgorithmPage.UniformGridForFinalList);
        }

        private void FillUniformGrid(UniformGrid uniformGrid)
        {
            for (int i = 0; i < list.Count; i++)
            {
                Border border = new Border();
                border.BorderBrush = new SolidColorBrush(Colors.Black);
                border.BorderThickness = new Thickness(1);
                border.Margin = new Thickness(1, 0, 1, 0);

                TextBlock element = new TextBlock();

                element.Text = list[i].Value;
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
