using DynamicDataStruct.ViewModel.Services;
using DynamicDataStruct.ViewModel.ViewModels.PageViewModels;
using DynamicDataStruct.ViewModel.ViewModels.WindowViewModels;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DynamicDataStruct.View
{
    /// <summary>
    /// Логика взаимодействия для FirstAlgorithmPage.xaml
    /// </summary>
    public partial class FirstAlgorithmPage1 : Page, INavigatable
    {
        private FirstAlgorithmPageViewModel windowViewModel { get; }
        public FirstAlgorithmPage1()
        {
            InitializeComponent();
            windowViewModel = new FirstAlgorithmPageViewModel(this);
            DataContext = windowViewModel;
        }
    }
}
