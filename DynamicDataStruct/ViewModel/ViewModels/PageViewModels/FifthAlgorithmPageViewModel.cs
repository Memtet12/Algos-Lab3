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
using System.Windows.Media;
using System.Windows;
using Tests;
using System.Windows.Input;

namespace DynamicDataStruct.ViewModel.ViewModels.PageViewModels
{
    internal class FifthAlgorithmPageViewModel : INotifyPropertyChanged
    {
        public ICommand GenerateListCommand { get; }
        public ICommand SolveProblemCommand { get; }

        public string Value
        {
            get => valueX;
            set
            {   
                valueX = value;
            }
        }

        private MyList<string> l;
        private MyList<string> standartL;
        private string valueX = "";
        private FifthAlgorithmPage5 fifthAlgorithmPage;
        public FifthAlgorithmPageViewModel(FifthAlgorithmPage5 page)
        {
            fifthAlgorithmPage = page;

            GenerateListCommand = new RelayCommand(GenerateMyList);
            SolveProblemCommand = new RelayCommand(SolveProblem);
        }

        private void GenerateMyList()
        {
            fifthAlgorithmPage.UniformGridForArrayBeforeAdding.Children.Clear();
            fifthAlgorithmPage.UniformGridForArrayAfterAdding.Children.Clear();

            l = new MyList<string>(TestSetGenerator.GenerateStrings(12, 10, 2).ToArray());
            standartL = new MyList<string>(l.ToArrayList().ToArray());

            fifthAlgorithmPage.SolveButton.IsEnabled = true;
            FillUniformGrid(fifthAlgorithmPage.UniformGridForArrayBeforeAdding);
        }
        private void SolveProblem()
        {
            fifthAlgorithmPage.UniformGridForArrayAfterAdding.Children.Clear();

            l.AddListAfterElement(new Node<string>(valueX, null));

            FillUniformGrid(fifthAlgorithmPage.UniformGridForArrayAfterAdding);

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
