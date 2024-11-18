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
    /// Логика взаимодействия для SixthAlgorithmPage.xaml
    /// </summary>
    public partial class SixthAlgorithmPage6 : Page, INavigatable
    {
        private SixthAlgorithmPageViewModel windowViewModel { get; }
        public SixthAlgorithmPage6()
        {
            InitializeComponent();
            windowViewModel = new SixthAlgorithmPageViewModel(this);
            DataContext = windowViewModel;
        }
    }
}
