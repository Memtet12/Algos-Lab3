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
    internal class ThirdAlgorithmPageViewModel : INotifyPropertyChanged
    {
        public ICommand GenerateListCommand { get; }
        public ICommand SolveProblemCommand { get; }

        private MyList<string> l;
        private ThirdAlgorithmPage3 thirdAlgorithmPage;
        public ThirdAlgorithmPageViewModel(ThirdAlgorithmPage3 page)
        {
            thirdAlgorithmPage = page;

            GenerateListCommand = new RelayCommand(GenerateMyList);
            SolveProblemCommand = new RelayCommand(SolveProblem);
        }
        private void GenerateMyList()
        {
            thirdAlgorithmPage.AnswerTextBlock.Text = "Количество различных элементов списка равно: ";
            thirdAlgorithmPage.UniformGridForArray.Children.Clear();

            l = new MyList<string>(TestSetGenerator.GenerateNumsWithDublicats(16, 10, 2).ToArray());
            

            thirdAlgorithmPage.SolveButton.IsEnabled = true;
            FillUniformGrid(thirdAlgorithmPage.UniformGridForArray);
        }
        private void SolveProblem()
        {
            thirdAlgorithmPage.AnswerTextBlock.Text= "Количество различных элементов списка равно: " + l.CountUniqueElements();
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
