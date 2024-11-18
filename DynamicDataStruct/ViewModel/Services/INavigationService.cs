using DynamicDataStruct.View;
using DynamicDataStruct.View.Windows;
using System.Windows;
using System.Windows.Input;
using View;

namespace DynamicDataStruct.ViewModel.Services
{
    internal interface INavigationService
    {
        static void SetStackAndQueueWindow(Window window)
        {
            window.Hide();
            StackWindow newWindow = new StackWindow();
            newWindow.Show();
            window.Close();
        }
        static void SetStackAndQueueProgramWindow(Window window)
        {
            window.Hide();
            StackAndQueueProgramWindow newWindow = new StackAndQueueProgramWindow();
            newWindow.Show();
            window.Close();
        }
        static void SetCalculatorWindow(Window window)
        {
            window.Hide();
            CalculatorWindow newWindow = new CalculatorWindow();
            newWindow.Show();
            window.Close();
        }
        static void SetArrayAlgorithmsWindow(Window window)
        {
            window.Hide();
            ArrayAlgorithmsWindow newWindow = new ArrayAlgorithmsWindow();
            newWindow.Show();
            window.Close();
        }
        static void SetTimeTestingWindow(Window window)
        {
            window.Hide();
            TimeTestingWindow newWindow = new TimeTestingWindow();
            newWindow.Show();
            window.Close();
        }
    }
}
