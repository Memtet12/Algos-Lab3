using DynamicDataStruct.View;
using System;
using System.Collections;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows;
using Microsoft.Win32;
using View;
using DynamicDataStruct.ViewModel.Services;
using System.Net.NetworkInformation;
using System.Windows.Controls;
using System.Windows.Media;
using System.Collections.Generic;
using DynamicDataStruct.Model.DataStruct;
using Model;
using System.Threading;
using Tests;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System.Text;
using System.Linq;
using WorkWithFiles;

namespace DynamicDataStruct.ViewModel.ViewModels.WindowViewModels
{
    public class StackAndQueueProgramWindowViewModel : INotifyPropertyChanged, INavigationService
    {

        public ICommand SetStackModeCommand { get; }
        public ICommand SetQueueModeCommand { get; }
        public ICommand StepForwardCommand { get; }
        public ICommand EndProgrammCommand { get; }
        public ICommand ReadFromFileSystemCommand { get; }
        public ICommand ReadFromBdCommand { get; }
        public ICommand GenerateAlgorithmCommand { get; }
        public ICommand SaveToBdCommand { get; }
        public ICommand SaveToFileSystemCommand { get; }

        private int stepIndex;
        private List<Tuple<int, string>> steps;
        private MyICollection<string> collection;
        private bool collectionTypeIsStack = true;
        private StackAndQueueProgramWindow stackAndQueueProgramWindow;
        public StackAndQueueProgramWindowViewModel(StackAndQueueProgramWindow window)
        {
            stackAndQueueProgramWindow = window;
            stepIndex = 0;
            steps = new List<Tuple<int, string>>();
            

            ReadFromFileSystemCommand = new RelayCommand(ReadFromFileSystem);
            ReadFromBdCommand = new RelayCommand(ReadFromBd);
            GenerateAlgorithmCommand = new RelayCommand(GenerateAlgorithm);
            SaveToBdCommand = new RelayCommand(SaveToDB);
            SaveToFileSystemCommand = new RelayCommand(SaveToFile);
 
            SetStackModeCommand = new RelayCommand(SetStackMode);
            SetQueueModeCommand = new RelayCommand(SetQueueMode);
            StepForwardCommand = new RelayCommand(StepForward);
            EndProgrammCommand = new RelayCommand(EndProgram);
        }
        private void SetStackMode()
        {
            stackAndQueueProgramWindow.UniformGridForCollectionElements.Children.Clear();
            stackAndQueueProgramWindow.UniformGridForSteps.Children.Clear();

            stackAndQueueProgramWindow.ResultTextBlock.Text = "";
            collection = new MyStack<string>();
            steps = new List<Tuple<int, string>>();
            collectionTypeIsStack = true;

            stackAndQueueProgramWindow.ButtonStepForward.IsEnabled = false;
            stackAndQueueProgramWindow.ButtonStopProgramm.IsEnabled = false;
           

            stackAndQueueProgramWindow.ButtonSetStackMode.Background = new SolidColorBrush(Color.FromRgb(255, 199, 199));
            stackAndQueueProgramWindow.ButtonSetQueueMode.Background = new SolidColorBrush(Color.FromRgb(221, 221, 221));
        }

        private void SetQueueMode()
        {
            stackAndQueueProgramWindow.UniformGridForCollectionElements.Children.Clear();
            stackAndQueueProgramWindow.UniformGridForSteps.Children.Clear();

            stackAndQueueProgramWindow.ResultTextBlock.Text = "";

            collection = new MyQueue<string>();
            steps = new List<Tuple<int, string>>();
            collectionTypeIsStack = false;

            stackAndQueueProgramWindow.ButtonStepForward.IsEnabled = false;
            stackAndQueueProgramWindow.ButtonStopProgramm.IsEnabled = false;
            

            stackAndQueueProgramWindow.ButtonSetQueueMode.Background = new SolidColorBrush(Color.FromRgb(255, 199, 199));
            stackAndQueueProgramWindow.ButtonSetStackMode.Background = new SolidColorBrush(Color.FromRgb(221, 221, 221));
        }
        private void ReadFromFileSystem()
        {
            var fileDialog = new OpenFileDialog()
            {
                Filter = "Text files(*.txt)| *.txt"
            };
            fileDialog.ShowDialog();

            if (!(fileDialog.FileName == ""))
            {   
                try
                {
                    steps = MyStack<Tuple<int, string>>.ShowStackChanges(
                                        FileWorker.GetAllDatasetLines(fileDialog.FileName)[0].Split(" ").ToList());
                    StartProgram();
               
                }catch (Exception e)
                {
                    MessageBox.Show("К сожалению, файл который вы пытаетесть загрузить, либо не содержит \nпрограмму, либо программа, записанная в него, содержит ошибки.");
                }
                
            }
        }
        
        private void ReadFromBd()
        {
            StackProgramSelectionDialogWindow stackProgramSelectionDialogWindow = new StackProgramSelectionDialogWindow();
            stackProgramSelectionDialogWindow.ShowDialog();
            string stackProgram = stackProgramSelectionDialogWindow.Result;
            if (!(stackProgram is null))
            {
                steps = MyStack<Tuple<int, string>>.ShowStackChanges(stackProgram.Split(" ").ToList());
               
                StartProgram();
            }
        }
        private void GenerateAlgorithm()
        {
            //Заполнение Шагов
            steps = MyStack<Tuple<int, string>>.ShowStackChanges(TestSetGenerator.GenerateForStackAndQueue(500));

            StartProgram();

        }
        private void SaveToDB()
        {   
            
            StringBuilder programStringBuilder = new StringBuilder();
            foreach (Tuple<int, string> tuple in steps)
            {
                programStringBuilder.Append(tuple.Item1);
                if (tuple.Item2 != null)
                {
                    programStringBuilder.Append(tuple.Item2);
                }
                programStringBuilder.Append(" ");
            }
            programStringBuilder.Remove(programStringBuilder.Length-1,1);

           

        }

        private void SaveToFile()
        {
            var fileDialog = new SaveFileDialog
            {
                Filter = "Text files(*.txt)| *.txt"
            };

            fileDialog.ShowDialog();

            if (!(fileDialog.FileName == ""))
            {   

                StringBuilder programStringBuilder = new StringBuilder();
                foreach (Tuple<int, string> tuple in steps)
                {
                    programStringBuilder.Append(tuple.Item1);
                    if (tuple.Item2 != null)
                    {
                        programStringBuilder.Append(tuple.Item2);
                    }
                    programStringBuilder.Append(" ");
                }

                programStringBuilder.Remove(programStringBuilder.Length - 1, 1);

                FileWorker.WriteAllDatasetLines(new List<string>(){ programStringBuilder.ToString()}, fileDialog.FileName);
            }
            
            
        }
        private void OutputStepsToUniformGrid(List<Tuple<int, string>> steps)
        {
            stackAndQueueProgramWindow.UniformGridForSteps.Children.Clear();
            string line;
            for (int i = 0; i < steps.Count; i++)
            {
                line = "";
                if (steps[i].Item1 == 1)
                {
                    line = $"1{steps[i].Item2}";
                }
                else
                {
                    line= steps[i].Item1.ToString();
                }

                Button step = new Button();
                step.Content = line;
                step.Height = 20;
                step.Width = stackAndQueueProgramWindow.Width / 206 * 49 - 21;
                step.Background = new SolidColorBrush(Color.FromRgb(211, 211, 211));
                step.Click += StepClick;
                stackAndQueueProgramWindow.UniformGridForSteps.Children.Add(step);
            }
        }

        private void StepClick(object sender, RoutedEventArgs e)
        {
            Button b = (Button)e.Source;
            ChooseStep(b);
        }

        public void ChooseStep(Button step)
        {
            Button lastStep = (Button)stackAndQueueProgramWindow.UniformGridForSteps.Children[stepIndex];

            int newStepIndex = stackAndQueueProgramWindow.UniformGridForSteps.Children.IndexOf(step);

            if (stepIndex < newStepIndex)
            {
                lastStep.Background = new SolidColorBrush(Color.FromRgb(211, 211, 211));
                for (int i = stepIndex; i < newStepIndex-1; i++)
                {
                    HiddenStepForward();
                }
                StepForward();
                step.Background = new SolidColorBrush(Color.FromRgb(140, 200, 255));
            }
        }
        private void HiddenStepForward()
        {
            if (stepIndex == steps.Count - 1)
            {
                EndProgram();
            }
            else
            {
                stackAndQueueProgramWindow.ResultTextBlock.Text = "";
                stepIndex++;

                //1 - Push(elem), 2 - Pop()
                switch (steps[stepIndex].Item1)
                {
                    case 1:
                        Push(steps[stepIndex].Item2);
                        break;
                    case 2:
                        Pop();
                        break;
                }
            }
        }

        private void StepForward()
        {
            if (stepIndex == steps.Count - 1)
            {
                EndProgram();
            }
            else
            {
                stackAndQueueProgramWindow.ResultTextBlock.Text = "";
                Button lastStep = (Button)stackAndQueueProgramWindow.UniformGridForSteps.Children[stepIndex];
                lastStep.Background = new SolidColorBrush(Color.FromRgb(211, 211, 211));
                stepIndex++;
                Button newStep = (Button)stackAndQueueProgramWindow.UniformGridForSteps.Children[stepIndex];
                newStep.Background = new SolidColorBrush(Color.FromRgb(140, 200, 255));

                //1 - Push(elem), 2 - Pop(), 3 - Top(), 4 - isEmpty(), 5 - Print()
                try
                {
                    switch (steps[stepIndex].Item1)
                    {
                        case 1:
                            Push(steps[stepIndex].Item2);
                            break;
                        case 2:
                            Pop();
                            break;
                        case 3:
                            Top();
                            break;
                        case 4:
                            IsEmpty();
                            break;
                        case 5:
                            Print();
                            break;
                    }
                }
                catch(Exception e)
                {
                    MessageBox.Show("К сожалению, что-то пошло не так");
                }
            }
        }
        private void Push(string valueForPush)
        {
            valueForPush = valueForPush.Substring(1);
            Border border = new Border();
            border.HorizontalAlignment = HorizontalAlignment.Center;
            border.VerticalAlignment = VerticalAlignment.Bottom;
            border.BorderBrush = new SolidColorBrush(Colors.Black);
            border.BorderThickness = new Thickness(1);
            border.Margin = new Thickness(0, 1, 0, 1);

            TextBlock element = new TextBlock();

            element.Text = valueForPush;
            element.FontSize = 18;
            element.Background = new SolidColorBrush(Colors.LightBlue);
            element.MinWidth = 40;
            element.Padding = new Thickness(2, 1, 2, 1);
            element.TextAlignment = TextAlignment.Center;

            border.Child = element;

            stackAndQueueProgramWindow.UniformGridForCollectionElements.Children.Insert(0, border);

            collection.Push(valueForPush);
        }
        private void Pop()
        {
            if (collectionTypeIsStack)
            {
                stackAndQueueProgramWindow.UniformGridForCollectionElements.Children.RemoveAt(0);
            }
            else
            {
                stackAndQueueProgramWindow.UniformGridForCollectionElements.Children.RemoveAt(stackAndQueueProgramWindow.UniformGridForCollectionElements.Children.Count - 1);
            }

            collection.Pop();
        }
        private void Top()
        {
            stackAndQueueProgramWindow.ResultTextBlock.Text = collection.Top();
        }
        private void IsEmpty()
        {
            stackAndQueueProgramWindow.ResultTextBlock.Text = collection.IsEmpty().ToString();
        }
        private void Print()
        {   
            StringBuilder result = new StringBuilder("( ");

            foreach (string el in collection.Print())
            {
                result.AppendFormat(el + ", ");
            }

            result.Remove(result.Length - 2, 2);

            result.Append(" )");
            
            stackAndQueueProgramWindow.ResultTextBlock.Text = result.ToString();
        }
        private void StartProgram()
        {
            if (collectionTypeIsStack)
            {
                collection = new MyStack<string>();
            }
            else
            {
                collection = new MyQueue<string>();
            }
            stepIndex = 0;
            OutputStepsToUniformGrid(steps);
            FirstStep();


            stackAndQueueProgramWindow.ButtonStepForward.IsEnabled = true;
            stackAndQueueProgramWindow.ButtonStopProgramm.IsEnabled = true;
        }

        private void FirstStep()
        {
            Button firstStep = (Button)stackAndQueueProgramWindow.UniformGridForSteps.Children[0];
            firstStep.Background = new SolidColorBrush(Color.FromRgb(140, 200, 255));

            //1 - Push(elem), 2 - Pop(), 3 - Top(), 4 - isEmpty(), 5 - Print()
            switch (steps[stepIndex].Item1)
            {
                case 1:
                    Push(steps[stepIndex].Item2);
                    break;
                case 2:
                    Pop();
                    break;
                case 3:
                    Top();
                    break;
                case 4:
                    IsEmpty();
                    break;
                case 5:
                    Print();
                    break;
            }
        }

        private void EndProgram()
        {
            stackAndQueueProgramWindow.UniformGridForCollectionElements.Children.Clear();
            stackAndQueueProgramWindow.UniformGridForSteps.Children.Clear();

            stackAndQueueProgramWindow.ResultTextBlock.Text = "";
            if (collectionTypeIsStack)
            {
                collection = new MyStack<string>();
            }
            else
            {
                collection = new MyQueue<string>();
            }
            stepIndex = 0;
            steps = new List<Tuple<int, string>>();

            stackAndQueueProgramWindow.ButtonStepForward.IsEnabled = false;
            stackAndQueueProgramWindow.ButtonStopProgramm.IsEnabled = false;

        }

        

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
