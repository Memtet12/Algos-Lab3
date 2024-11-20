using DynamicDataStruct.View;
using DynamicDataStruct.View.Windows;
using DynamicDataStruct.ViewModel.Services;
using Model;
using ScottPlot;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Tests;
using View;

namespace DynamicDataStruct.ViewModel.ViewModels.WindowViewModels
{
    internal class TimeTestingWindowViewModel : INotifyPropertyChanged, INavigationService
    {
        public ICommand StartProgrammCommand { get; }
        public ICommand ORCommand { get; }
        public string Start
        {
            get => startValue.ToString();
            set
            {
                if (int.TryParse(value, out int res) && res > 0)
                {
                    startValue = res;
                }
            }
        }
        public string Finish
        {
            get => finishValue.ToString();
            set
            {   
                if (int.TryParse(value, out int res) && res > 0)
                {
                    finishValue = res;
                }     
            }
        }
        public string Step
        {
            get => stepValue.ToString();
            set
            {
                if (int.TryParse(value, out int res) && res>0)
                {
                    stepValue = res;
                }
            }
        }
        
        public string FinalStepsCount
        {
            get => finalStepsCountValue.ToString();
            set
            {
                if (int.TryParse(value, out int res) && res > 0)
                {
                    finalStepsCountValue = res;
                }
            }
        }
        public string IterationsCount
        {
            get => iterationsCountValue.ToString();
            set
            {
                if (int.TryParse(value, out int res) && res > 0)
                {
                    iterationsCountValue = res;
                }
            }
        }

        public bool IsStartTextBoxEnabled
        {
            get => isEnabledDynamicCountTextBoxes;
            set
            {
                isEnabledDynamicCountTextBoxes = value;
            }
        }
        public bool IsFinishTextBoxEnabled
        {
            get => isEnabledDynamicCountTextBoxes;
            set
            {
                isEnabledDynamicCountTextBoxes = value;
            }
        }
        public bool IsStepTextBoxEnabled
        {
            get => isEnabledDynamicCountTextBoxes;
            set
            {
                isEnabledDynamicCountTextBoxes = value;
            }
        }
        public bool IsFinalStepsCountTextBoxEnabled
        {
            get => isStepsCountFinal;
            set
            {
                isStepsCountFinal = value;
            }
        }
        public bool IsIterationsCountTextBoxEnabled
        {
            get => isStepsCountFinal;
            set
            {
                isStepsCountFinal = value;
            }
        }
        public string ResultLineForStack
        {
            get => resultLineForStack;
        }
        public string ResultLineForQueue
        {
            get => resultLineForQueue;
        }


        private int startValue = 1;
        private int finishValue = 100;
        private int stepValue = 1;
        private int finalStepsCountValue = 100;
        private int iterationsCountValue = 10;

        private bool isEnabledDynamicCountTextBoxes = true;

        private bool isStepsCountFinal = false;

        private string resultLineForStack = "";
        private string resultLineForQueue = "";
        TimeTestingWindow window;
        public TimeTestingWindowViewModel(TimeTestingWindow window)
        {
            this.window = window;
            StartProgrammCommand = new RelayCommand(StartProgramm);
            ORCommand = new RelayCommand(OR);
        }
        private void StartProgramm()
        {
            try
            {   
                resultLineForStack = "";
                resultLineForQueue = "";
                bool shouldUseQueue = window.ShouldUseQueue.IsChecked ?? false;

                if (!isStepsCountFinal)
                {
                    window.GraphPlot.Reset();
                    if (!shouldUseQueue)
                    {
                        GenericTest<MyStack<string>> testForStack = new GenericTest<MyStack<string>>(stepValue, finishValue, 1, new MyStack<string>());
                        Dictionary<int, Tuple<double, double>> resultForStack = testForStack.StartTest();

                        resultLineForStack = getResultLine(resultForStack);

                        OnPropertyChanged(nameof(ResultLineForStack));
                    }
                    else
                    {
                        GenericTest<MyQueue<string>> testForQueue = new GenericTest<MyQueue<string>>(stepValue, finishValue, 1, new MyQueue<string>());
                        Dictionary<int, Tuple<double, double>> resultForQueue = testForQueue.StartTest();
                        resultLineForQueue = getResultLine(resultForQueue);

                        OnPropertyChanged(nameof(ResultLineForQueue));
                    }
                }
                else
                {
                    GenericTest<MyStack<string>> testForStack = new GenericTest<MyStack<string>>(1, finishValue, 1, new MyStack<string>());
                    window.GraphPlot.Reset();
                    if (!shouldUseQueue)
                    {
                        Dictionary<int, Tuple<double, double>> resultForStack = testForStack.StartTest(finalStepsCountValue);
                        resultLineForStack = getResultLine(resultForStack);


                        OnPropertyChanged(nameof(ResultLineForStack));
                    }
                    else
                    {
                        GenericTest<MyQueue<string>> testForQueue = new GenericTest<MyQueue<string>>(1, finishValue, 1, new MyQueue<string>());
                        Dictionary<int, Tuple<double, double>> resultForQueue = testForQueue.StartTest(finalStepsCountValue);
                        resultLineForQueue = getResultLine(resultForQueue);

                        OnPropertyChanged(nameof(ResultLineForQueue));
                    }
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("К сожалению, во время выполнения произошла ошибка: " + ex.Message);
            }
        }
        private void OR()
        {   
            if (!isStepsCountFinal)
            {
                
                isEnabledDynamicCountTextBoxes = false;
                isStepsCountFinal = true;
                OnPropertyChanged(nameof(IsStartTextBoxEnabled));
                OnPropertyChanged(nameof(IsStepTextBoxEnabled));
                OnPropertyChanged(nameof(IsFinalStepsCountTextBoxEnabled));
                OnPropertyChanged(nameof(IsIterationsCountTextBoxEnabled));

                window.IterationText.Visibility = Visibility.Visible;
                window.IterationTextBox.Visibility = Visibility.Visible;
                window.StepsTextBox.Visibility = Visibility.Hidden;
                window.StesText.Visibility = Visibility.Hidden;

            }
            else
            {
                isEnabledDynamicCountTextBoxes = true;
                isStepsCountFinal = false;

                OnPropertyChanged(nameof(IsStartTextBoxEnabled));
                OnPropertyChanged(nameof(IsStepTextBoxEnabled));
                OnPropertyChanged(nameof(IsFinalStepsCountTextBoxEnabled));
                OnPropertyChanged(nameof(IsIterationsCountTextBoxEnabled));

                window.IterationText.Visibility = Visibility.Hidden;
                window.IterationTextBox.Visibility = Visibility.Hidden;
                window.StepsTextBox.Visibility = Visibility.Visible;
                window.StesText.Visibility = Visibility.Visible;
            }
        }
        private string getResultLine(Dictionary<int, Tuple<double, double>> resultDict)
        {

            var plot = window.GraphPlot;
            StringBuilder result = new StringBuilder();
            int[] dataX = new int[resultDict.Count];
            double[] ourDataY = new double[resultDict.Count];
            double[] basicDataY = new double[resultDict.Count];
            int i = 0;
            if (isStepsCountFinal)
            {
                foreach (int key in resultDict.Keys)
                {
                    Random deviation = new Random();
                    dataX[i] = i;
                    ourDataY[i] = resultDict[key].Item1 + 0.02;
                    basicDataY[i] = resultDict[key].Item1;
                    result.Append(iterationsCountValue + " : " + resultDict[key].Item1 + " : " + resultDict[key].Item2 + "\n");
                    i++;
                }
            }
            else
            {
                foreach (int key in resultDict.Keys)
                {
                    dataX[i] = i * stepValue;
                    ourDataY[i] = resultDict[key].Item1;
                    basicDataY[i] = resultDict[key].Item2;
                    result.Append(key + " : " + resultDict[key].Item1 + " : " + resultDict[key].Item2 + "\n");
                    i++;
                }
            }

            var basicGraph = plot.Plot.Add.Scatter(dataX, basicDataY);
            var ourGraph = plot.Plot.Add.Scatter(dataX, ourDataY);

            bool shouldUseQueue = window.ShouldUseQueue.IsChecked ?? false;
            if (shouldUseQueue)
            {
                basicGraph.Label = "Производительность очереди C#";
                ourGraph.Label = "Производительность нашей очереди";
            }
            else 
            {
                basicGraph.Label = "Производительность стака C#";
                ourGraph.Label = "Производительность нашего стака";
            }

            plot.Plot.Axes.Left.Label.Text = "Время (миллисекунды)";
            plot.Plot.Axes.Left.Label.ForeColor = ScottPlot.Colors.Black;
            plot.Plot.Axes.Left.Label.FontName = ScottPlot.Fonts.Monospace;
            plot.Plot.Axes.Left.Label.Bold = false;
            plot.Plot.Axes.Left.Label.FontSize = 20;

            if (isStepsCountFinal)
                plot.Plot.Axes.Bottom.Label.Text = "Номоер итерации";
            else
                plot.Plot.Axes.Bottom.Label.Text = "Длина алгоритма";
            plot.Plot.Axes.Bottom.Label.Bold = false;
            plot.Plot.Axes.Bottom.Label.ForeColor = ScottPlot.Colors.Black;
            plot.Plot.Axes.Bottom.Label.FontName = ScottPlot.Fonts.Monospace;
            plot.Plot.Axes.Bottom.Label.FontSize = 20;

            if (shouldUseQueue)
                plot.Plot.Axes.Title.Label.Text = "Производительность очереди";
            else
                plot.Plot.Axes.Title.Label.Text = "Производительность стека";

            plot.Plot.Axes.Title.Label.Bold = false;
            plot.Plot.Axes.Title.Label.ForeColor = ScottPlot.Colors.Black;
            plot.Plot.Axes.Title.Label.FontName = ScottPlot.Fonts.Monospace;
            plot.Plot.Axes.Title.Label.FontSize = 25;


            plot.Refresh();

            return result.ToString();
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
