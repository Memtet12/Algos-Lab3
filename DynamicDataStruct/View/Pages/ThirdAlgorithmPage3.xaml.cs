using DynamicDataStruct.ViewModel.Services;
using DynamicDataStruct.ViewModel.ViewModels.PageViewModels;
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

namespace DynamicDataStruct.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для ThirdAlgorithmPage.xaml
    /// </summary>
    public partial class ThirdAlgorithmPage3 : Page, INavigatable
    {
        private ThirdAlgorithmPageViewModel windowViewModel { get; }
        public ThirdAlgorithmPage3()
        {
            InitializeComponent();
            windowViewModel = new ThirdAlgorithmPageViewModel(this);
            DataContext = windowViewModel;
        }
    }
}
