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
using System.Windows.Documents;

namespace DynamicDataStruct.ViewModel.ViewModels.PageViewModels
{
    internal class TenthAlgorithmPageViewModel : INotifyPropertyChanged
    {
        public ICommand GenerateListCommand { get; }
        public ICommand SolveCommand { get; }

        private TenthAlgorithmPage10 tenthAlgorithmPage;
        private MyList<string> list;

        public TenthAlgorithmPageViewModel(TenthAlgorithmPage10 page)
        {
            tenthAlgorithmPage = page;
            tenthAlgorithmPage.TextBlockWithTask.Text = "10. Функция разбивает список целых чисел на два списка по первому\nвхождению заданного числа и выводит вторую часть. Если этого числа \nв списке нет, второй список будет пустым, а первый не изменится.";
            GenerateListCommand = new RelayCommand(GenerateMyList);
            SolveCommand = new RelayCommand(Solve);
        }
        private void GenerateMyList()
        {
            tenthAlgorithmPage.UniformGridForInitialList.Children.Clear();
            tenthAlgorithmPage.UniformGridSecondPartList.Children.Clear();

            list = new MyList<string>(TestSetGenerator.GenerateNumsWithDublicats(20, 7, 1).ToArray());

            tenthAlgorithmPage.ButtonSolve.IsEnabled = true;
            FillUniformGrid(tenthAlgorithmPage.UniformGridForInitialList, list);

        }
        private void Solve()
        {
            tenthAlgorithmPage.UniformGridSecondPartList.Children.Clear();
            MyList<string> p = new MyList<string>(list.ToArrayList().ToArray());

            list = list.SplitList(new Node<string>(tenthAlgorithmPage.TextBoxSelectingElement.Text, null));
            FillUniformGrid(tenthAlgorithmPage.UniformGridSecondPartList, list);
            list = new MyList<string>(p.ToArrayList().ToArray());
        }

        private void FillUniformGrid(UniformGrid uniformGrid, MyList<string> list)
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
