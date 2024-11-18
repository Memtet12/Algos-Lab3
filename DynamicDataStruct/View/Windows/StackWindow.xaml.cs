using DynamicDataStruct.ViewModel.Services;
using DynamicDataStruct.ViewModel.ViewModels.WindowViewModels;
using System.Windows;


namespace View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class StackWindow : Window, INavigationService
    {
        private StackWindowViewModel windowViewModel { get; }
        public StackWindow()
        {
            InitializeComponent();
            windowViewModel = new StackWindowViewModel(this);
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
