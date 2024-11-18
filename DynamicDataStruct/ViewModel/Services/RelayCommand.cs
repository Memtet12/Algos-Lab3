﻿using System;
using System.Windows.Input;

namespace DynamicDataStruct.ViewModel.Services
{
    public class RelayCommand : ICommand
    {
        private readonly Action action;
        public RelayCommand(Action action) => this.action = action;
        public bool CanExecute(object parameter) => true;
        public event EventHandler CanExecuteChanged;
        public void Execute(object parameter) => action();
    }
}
