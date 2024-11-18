﻿using DynamicDataStruct.ViewModel.Services;
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
    /// Логика взаимодействия для FifthAlgorithmPage.xaml
    /// </summary>
    public partial class FifthAlgorithmPage5 : Page, INavigatable
    {
        private FifthAlgorithmPageViewModel windowViewModel { get; }
        public FifthAlgorithmPage5()
        {
            InitializeComponent();
            windowViewModel = new FifthAlgorithmPageViewModel(this);
            DataContext = windowViewModel;
        }
    }
}
