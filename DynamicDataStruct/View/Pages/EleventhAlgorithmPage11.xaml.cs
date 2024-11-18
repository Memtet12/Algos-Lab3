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
    public partial class EleventhAlgorithmPage11 : Page, INavigatable
    {
        private EleventhAlgorithmPageViewModel windowViewModel { get; }
        public EleventhAlgorithmPage11()
        {
            InitializeComponent();
            windowViewModel = new EleventhAlgorithmPageViewModel(this);
            DataContext = windowViewModel;
        }
    }
}
