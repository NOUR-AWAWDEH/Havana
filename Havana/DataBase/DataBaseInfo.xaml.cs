﻿using Library.Models.Classes;
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
using System.Windows.Shapes;

namespace Havana.DataBase
{
    /// <summary>
    /// Interaction logic for DataBase.xaml
    /// </summary>
    public partial class DataBaseInfo : Window
    {
        public DataBaseInfo()
        {
            InitializeComponent();
        }

        private void PhotoWindow(object sender, RoutedEventArgs e)
        {
            InseartPhoto inseartPhoto = new InseartPhoto();
            this.Visibility = Visibility.Hidden;   
            inseartPhoto.ShowDialog();
        }
    }
}
