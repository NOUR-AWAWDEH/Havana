using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Havana;
using Havana.HavanaDataSetTableAdapters;
using Library.Models.Classes;




namespace Havana.Snacks
{
    
    /// <summary>
    /// Interaction logic for Hot.xaml
    /// </summary>
    public partial class SnacksItems : Window
    {
        int currentPage = 0;
        int sizePage = 4;
       

        public SnacksItems()
        {
            InitializeComponent();
            ShowInfo(currentPage);
        }

        private void ShowInfo(int page)
        {
            DataAccess dataAccess = new DataAccess();
            List<SnackPhoto> photos = dataAccess.GetSnacksPhotos();   

            //Image[] images = new Image[4];


            int startPos = sizePage * page;

            int endPos = startPos + sizePage;

            if (endPos > photos.Count)
            {
                endPos = photos.Count;
            }

            //Delete Data
            for (int i = 0; i < 4; i++)
            {
                string ImageName = $"Image{i + 1}";
                Image image = (Image)FindName(ImageName);
                TextBlock textBlock = (TextBlock)FindName($"TextLable{i + 1}");
                image.Source = null;
                textBlock.Text = "";
                string NameButton = $"AddButt{i + 1}";
                Button button = (Button)FindResource(NameButton);

            }

            //Fill Datat
            for (int i = startPos; i < endPos; i++)
            {
                string ImageName = $"Image{i - startPos + 1}";

                Image image = (Image)FindName(ImageName);
                if (image != null)
                {
                    image.Source = photos[i].Image;
                }

                string TexLableName = $"TextLable{i - startPos + 1}";
                TextBlock textBlock = (TextBlock)FindName(TexLableName);

                if (image != null)
                {
                    textBlock.Text = photos[i].Snack.Name;
                }

            }

        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            NewOrderWindow newOrderWindow = Application.Current.Windows.OfType<NewOrderWindow>().FirstOrDefault();
            newOrderWindow.BackToOrderWindow(newOrderWindow);
            this.Close();
        }

        private void AddButt(object sender, RoutedEventArgs e)
        {
           
        }

        private void NextPage(object sender, RoutedEventArgs e)
        {
            DataAccess dataAccess = new DataAccess();
            List<SnackPhoto> photos = dataAccess.GetSnacksPhotos();
            currentPage++;
            if (photos.Count < (currentPage * sizePage))
            {
                currentPage--;
            }
           ShowInfo(currentPage);
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            currentPage--;
            if (currentPage < 0)
            {
                currentPage = 0;
            }
            ShowInfo(currentPage);
        }
    }
}