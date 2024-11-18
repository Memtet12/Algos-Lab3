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
    /// Логика взаимодействия для Page1.xaml
    /// </summary>
    public partial class EighthAlgorithmPage8 : Page, INavigatable
    {
        private EighthAlgorithmPageViewModel windowViewModel { get; }
        public EighthAlgorithmPage8()
        {
            InitializeComponent();
            windowViewModel = new EighthAlgorithmPageViewModel(this);
            DataContext = windowViewModel;
        }
    }
}
