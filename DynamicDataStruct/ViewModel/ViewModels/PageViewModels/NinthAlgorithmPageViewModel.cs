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
using System.Windows.Documents;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Tests;

namespace DynamicDataStruct.ViewModel.ViewModels.PageViewModels
{
    internal class NinthAlgorithmPageViewModel : INotifyPropertyChanged
    {
        public ICommand GenerateList1Command { get; }
        public ICommand GenerateList2Command { get; }
        public ICommand SolveCommand { get; }

        private NinthAlgorithmPage9 ninthAlgorithmPage;
        private MyList<string> list1;
        private MyList<string> list2;


        public NinthAlgorithmPageViewModel(NinthAlgorithmPage9 page)
        {
            ninthAlgorithmPage = page;
            ninthAlgorithmPage.TextBlockWithTask.Text = "9. Функция дописывает к списку L список E. Оба списка содержат \nцелые числа. В основной программе считать их из файла. ";
            GenerateList1Command = new RelayCommand(GenerateMyList1);
            GenerateList2Command = new RelayCommand(GenerateMyList2);
            SolveCommand = new RelayCommand(Solve);
        }

        private void GenerateMyList1()
        {
            ninthAlgorithmPage.UniformGridForInitialList1.Children.Clear();
            ninthAlgorithmPage.UniformGridForInitialList2.Children.Clear();
            ninthAlgorithmPage.UniformGridForFinalList.Children.Clear();

            list1 = new MyList<string>(TestSetGenerator.GenerateNumsWithDublicats(10, 5, 1).ToArray());

            ninthAlgorithmPage.ButtonSolve.IsEnabled = true;
            FillUniformGrid(ninthAlgorithmPage.UniformGridForInitialList1, list1);

        }

        private void GenerateMyList2()
        {
            ninthAlgorithmPage.UniformGridForInitialList2.Children.Clear();
            ninthAlgorithmPage.UniformGridForFinalList.Children.Clear();

            list2 = new MyList<string>(TestSetGenerator.GenerateNumsWithDublicats(10, 5, 1).ToArray());

            ninthAlgorithmPage.ButtonSolve.IsEnabled = true;
            FillUniformGrid(ninthAlgorithmPage.UniformGridForInitialList2, list2);

        }

        private void Solve()
        {
            ninthAlgorithmPage.UniformGridForFinalList.Children.Clear();

            list1.AddList(new MyList<string>(list2.ToArrayList().ToArray())); 
            FillUniformGrid(ninthAlgorithmPage.UniformGridForFinalList, list1);
            ninthAlgorithmPage.ButtonSolve.IsEnabled = false;
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
