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
    internal class SixthAlgorithmPageViewModel : INotifyPropertyChanged
    {
        public ICommand GenerateListCommand { get; }
        public ICommand SolveProblemCommand { get; }

        public string Value
        {
            get => valueE.ToString();
            set
            {
                if (int.TryParse(value, out int res))
                {
                    this.valueE = res;
                }
            }
        }

        private MyList<int> l;
        private MyList<int> standartL;
        private int valueE = 0;
        private SixthAlgorithmPage6 sixthAlgorithmPage;
        public SixthAlgorithmPageViewModel(SixthAlgorithmPage6 page)
        {
            sixthAlgorithmPage = page;


            GenerateListCommand = new RelayCommand(GenerateMyList);
            SolveProblemCommand = new RelayCommand(SolveProblem);
        }
        private void GenerateMyList()
        {
            sixthAlgorithmPage.UniformGridForArrayBeforeAdding.Children.Clear();
            sixthAlgorithmPage.UniformGridForArrayAfterAdding.Children.Clear();

            string[] randomArray = TestSetGenerator.GenerateNumsWithDublicats(16, 10, 2).ToArray();

            int[] intRandomArray = randomArray.Select(x => int.Parse(x)).ToArray();
            Array.Sort(intRandomArray);

            l = new MyList<int>(intRandomArray);
            standartL = new MyList<int>(intRandomArray);

            sixthAlgorithmPage.SolveButton.IsEnabled = true;
            FillUniformGrid(sixthAlgorithmPage.UniformGridForArrayBeforeAdding);
        }
        private void SolveProblem()
        {   
            sixthAlgorithmPage.UniformGridForArrayAfterAdding.Children.Clear();

            l.AddElementIfNotDesc(new Node<int>(valueE, null));
            
            FillUniformGrid(sixthAlgorithmPage.UniformGridForArrayAfterAdding);

            l = new MyList<int>(standartL.ToArrayList().ToArray());
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

                element.Text = l[i].Value.ToString();
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
