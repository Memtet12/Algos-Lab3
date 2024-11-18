using DynamicDataStruct.ViewModel.ViewModels.DialogWindowViewModels;
using System.Windows;

namespace DynamicDataStruct.View
{
    /// <summary>
    /// Логика взаимодействия для StackProgramSelectionDialogWindow.xaml
    /// </summary>
    public partial class StackProgramSelectionDialogWindow : Window
    {
        public string Result;
        private StackProgramSelectionDialogWindowViewModel windowViewModel { get; }
        public StackProgramSelectionDialogWindow()
        {
            InitializeComponent();
            windowViewModel = new StackProgramSelectionDialogWindowViewModel(this);
            DataContext = windowViewModel;
        }
    }
}
