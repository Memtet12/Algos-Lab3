using DynamicDataStruct.View;
using DynamicDataStruct.ViewModel.Services;
using Model;
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
        public TimeTestingWindowViewModel()
        {
            StartProgrammCommand = new RelayCommand(StartProgramm);
            ORCommand = new RelayCommand(OR);
        }
        private void StartProgramm()
        {
            try
            {   
                resultLineForStack = "";
                resultLineForQueue = "";

                if (!isStepsCountFinal)
                {
                    GenericTest<MyStack<string>> testForStack = new GenericTest<MyStack<string>>(stepValue, finishValue, startValue, new MyStack<string>());
                    Dictionary<int, Tuple<double, double>> resultForStack = testForStack.StartTest();
                    resultLineForStack = getResultLine(resultForStack);

                    OnPropertyChanged(nameof(ResultLineForStack));

                    GenericTest<MyQueue<string>> testForQueue = new GenericTest<MyQueue<string>>(stepValue, finishValue, startValue, new MyQueue<string>());
                    Dictionary<int, Tuple<double, double>> resultForQueue = testForQueue.StartTest();
                    resultLineForQueue = getResultLine(resultForQueue);

                    OnPropertyChanged(nameof(ResultLineForQueue));
                }
                else
                {
                    GenericTest<MyStack<string>> testForStack = new GenericTest<MyStack<string>>(1, iterationsCountValue, 1, new MyStack<string>());

                    Dictionary<int, Tuple<double, double>> resultForStack = testForStack.StartTest(finalStepsCountValue);
                    resultLineForStack = getResultLine(resultForStack);

                    OnPropertyChanged(nameof(ResultLineForStack));

                    GenericTest<MyQueue<string>> testForQueue = new GenericTest<MyQueue<string>>(1, iterationsCountValue, 1, new MyQueue<string>());
                    Dictionary<int, Tuple<double, double>> resultForQueue = testForQueue.StartTest(finalStepsCountValue);
                    resultLineForQueue = getResultLine(resultForQueue);

                    OnPropertyChanged(nameof(ResultLineForQueue));
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
                OnPropertyChanged(nameof(IsFinishTextBoxEnabled));
                OnPropertyChanged(nameof(IsStepTextBoxEnabled));
                OnPropertyChanged(nameof(IsFinalStepsCountTextBoxEnabled));
                OnPropertyChanged(nameof(IsIterationsCountTextBoxEnabled));
            }
            else
            {
                isEnabledDynamicCountTextBoxes = true;
                isStepsCountFinal = false;

                OnPropertyChanged(nameof(IsStartTextBoxEnabled));
                OnPropertyChanged(nameof(IsFinishTextBoxEnabled));
                OnPropertyChanged(nameof(IsStepTextBoxEnabled));
                OnPropertyChanged(nameof(IsFinalStepsCountTextBoxEnabled));
                OnPropertyChanged(nameof(IsIterationsCountTextBoxEnabled));
            }
        }
        private string getResultLine(Dictionary<int, Tuple<double, double>> resultDict)
        {
            
            StringBuilder result = new StringBuilder();
            if (isStepsCountFinal)
            {
                foreach (int key in resultDict.Keys)
                {

                    result.Append(iterationsCountValue + " : " + resultDict[key].Item1 + " : " + resultDict[key].Item2 + "\n");
                }
            }
            else
            {
                foreach (int key in resultDict.Keys)
                {

                    result.Append(key + " : " + resultDict[key].Item1 + " : " + resultDict[key].Item2 + "\n");
                }
            }

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
