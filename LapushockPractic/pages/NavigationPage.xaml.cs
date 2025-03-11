﻿using System;
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

namespace LapushockPractic.Pages
{
    /// <summary>
    /// Логика взаимодействия для NavigationPage.xaml
    /// </summary>
    public partial class NavigationPage : Page
    {
        public NavigationPage()
        {
            InitializeComponent();
        }

        private void Button_Click_Product(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Pages.ProductPage());
        }

        private void Button_Click_Material(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Pages.MaterialPage());
        }
    }
}
