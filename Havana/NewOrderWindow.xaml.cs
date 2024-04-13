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
using System.Windows.Shapes;


namespace Havana
{
    /// <summary>
    /// Interaction logic for NewOrder.xaml
    /// </summary>
    public partial class NewOrderWindow : Window
    {
        public NewOrderWindow()
        {
            InitializeComponent();
        }

        private void BackToHavana(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            if (mainWindow != null)
            {
                this.Visibility = Visibility.Hidden;
                mainWindow.Visibility = Visibility.Visible;
            }
        }

        private void HotSnaksButt(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ColdSnaksButt(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void SweetsSnaksButt(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void TeaDrinksButt(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void CoffeeDrinksButt(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void JuicesDrinksButt(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void WaterDrinksButt(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void SearchButt(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}