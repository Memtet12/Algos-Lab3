using Database.Access;
using Database;
using DynamicDataStruct.View;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using static Database.DatabaseCore;

namespace DynamicDataStruct.ViewModel.ViewModels.DialogWindowViewModels
{
    public class StackProgramSelectionDialogWindowViewModel : INotifyPropertyChanged, INotifyDataErrorInfo
    {
        //public ICommand Datavisualization { get; }
        private int id;
        private List<OperationForDataStruct> programsForStack;
        private StackProgramSelectionDialogWindow stackProgramSelectionDialogWindow;
        public StackProgramSelectionDialogWindowViewModel(StackProgramSelectionDialogWindow window)
        {
            stackProgramSelectionDialogWindow = window;
            id = 0;

            Repository<OperationForDataStruct> repository = new Repository<OperationForDataStruct>(new ApplicationContext());
            programsForStack = (List<OperationForDataStruct>)repository.Get();

            ReadDataFromDB();
        }
        private void ReadDataFromDB()
        {
            foreach (OperationForDataStruct program in programsForStack)
            {
                DataVisualization(program.Data);
            }
        }
        private void DataVisualization(string stackProgram)
        {
            Border firstColumnElement = GetBorder();
            TextBlock idTextBlock = GetTextBlock((++id).ToString());
            firstColumnElement.Child = idTextBlock;

            stackProgramSelectionDialogWindow.IdColumn.Children.Add(firstColumnElement);


            Border secondColumnElement = GetBorder();
            secondColumnElement.Padding = new Thickness(0, 0, 0.8, 0);
            TextBlock stackProgramTextBlock = GetTextBlock(stackProgram);

            ScrollViewer scrollViewerForStackProgram = GetScrollViewerWithTextBlock(stackProgramTextBlock);
            secondColumnElement.Child = scrollViewerForStackProgram;

            stackProgramSelectionDialogWindow.StackProgramColumn.Children.Add(secondColumnElement);


            Button selectionButton = GetButton();
            selectionButton.Click += SelectionButtonClick;
            stackProgramSelectionDialogWindow.SelectionColumn.Children.Add(selectionButton);
        }
        private void SelectionButtonClick(object sender, RoutedEventArgs e)
        {
            Button senderButton = (Button)sender;
            int stackProgramIndex = stackProgramSelectionDialogWindow.SelectionColumn.Children.IndexOf(senderButton);
            stackProgramSelectionDialogWindow.Result = programsForStack[stackProgramIndex].Data;
            stackProgramSelectionDialogWindow.Close();
        }
        private static ScrollViewer GetScrollViewerWithTextBlock(TextBlock stackProgram)
        {
            ScrollViewer scrollViewer = new ScrollViewer();
            scrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Disabled;
            scrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;

            scrollViewer.Content = stackProgram;
            return scrollViewer;
        }

        private static Button GetButton()
        {
            Button button = new Button();
            button.Height = 45;
            button.VerticalAlignment = VerticalAlignment.Top;
            button.Content = "Выбрать";
            button.FontSize = 18;
            button.BorderBrush = new SolidColorBrush(Colors.Black);
            return button;
        }

        private static TextBlock GetTextBlock(string text)
        {
            TextBlock textBlock = new TextBlock();

            textBlock.Text = text;
            textBlock.FontSize = 18;
            textBlock.MinWidth = 20;
            textBlock.TextAlignment = TextAlignment.Left;
            textBlock.Padding = new Thickness(3, 0, 5, 0);
            return textBlock;
        }

        private static Border GetBorder()
        {
            Border border = new Border();
            border.VerticalAlignment = VerticalAlignment.Top;
            border.BorderBrush = new SolidColorBrush(Colors.Black);
            border.BorderThickness = new Thickness(1);
            border.Height = 45;
            return border;
        }

        public bool HasErrors => throw new NotImplementedException();

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
