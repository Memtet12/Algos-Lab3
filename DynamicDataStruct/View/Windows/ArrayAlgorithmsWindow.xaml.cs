using DynamicDataStruct.View.Windows;
using DynamicDataStruct.ViewModel.Services;
using DynamicDataStruct.ViewModel.ViewModels.WindowViewModels;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DynamicDataStruct.View
{
    /// <summary>
    /// Логика взаимодействия для ArrayAlgorithmsWindow.xaml
    /// </summary>
    public partial class ArrayAlgorithmsWindow : Window, INavigationService
    {
        private ArrayAlgorithmsWindowViewModel windowViewModel { get; }
        public ArrayAlgorithmsWindow()
        {
            InitializeComponent();
            windowViewModel = new ArrayAlgorithmsWindowViewModel(this);
            DataContext = windowViewModel;
        }

        private void StackAndQueueMenuItem_Click(object sender, RoutedEventArgs e)
        {
            INavigationService.SetStackAndQueueWindow(this);
        }
        private void CalculatorMenuItem_Click(object sender, RoutedEventArgs e)
        {
            INavigationService.SetCalculatorWindow(this);
        }
        private void StackAndQueueProgrammsMenuItem_Click(object sender, RoutedEventArgs e)
        {
            INavigationService.SetStackAndQueueProgramWindow(this);
        }
        private void ArrayAlgorithmsMenuItem_Click(object sender, RoutedEventArgs e)
        {
            INavigationService.SetArrayAlgorithmsWindow(this);
        }
        private void TimeTestingMenuItem_Click(object sender, RoutedEventArgs e)
        {
            INavigationService.SetTimeTestingWindow(this);
        }
    }
}
