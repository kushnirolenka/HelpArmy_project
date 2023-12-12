using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using HelpArmyDB;

namespace HelpArmy
{
    /// <summary>
    /// Interaction logic for Donates.xaml
    /// </summary>


    public partial class Donations : Window
    {
        DonateList donateList = new DonateList();
        Organization organization = new Organization();
        Volunteer volunteer = new Volunteer();
        public Donations()
        {

            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }
        public Donations(Organization o, Volunteer v)
        {
            organization = o;
            volunteer = v;
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }
        private void MenuItemDonates_Click(object sender, RoutedEventArgs e)
        {
            var donations = new Donations(organization, volunteer);
            this.Close();
            donations.Show();
        }

        private void MenuItemHelpType_Click(object sender, RoutedEventArgs e)
        {
            var helpType = new HelpType(organization, volunteer);
            this.Close();
            helpType.Show();
        }

        private void MenuItemProfile_Click(object sender, RoutedEventArgs e)
        {
            var profile = new Profile(organization, volunteer);
            this.Close();
            profile.Show();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (organization.email_organization == "")
            {
                MessageBox.Show("Звіт може створювати тільки організація!");
                return;
            }
            var graphic = new Graphic(organization, volunteer);
            this.Close();
            graphic.Show();
        }
        private void EditClick(object sender, RoutedEventArgs e, int id)
        {
            var donateInfo = new DonateInfo(id, organization, volunteer);
            this.Close();
            donateInfo.Show();
        }

        private void Grid_Initialized(object sender, EventArgs e)
        {
            //using (StreamReader sr = new StreamReader("Donates.txt"))
            //{
            //    try
            //    {
            //        int counter = 0;
            //        string name = "";
            //        string type = "";
            //        double sum = 0;
            //        string financingObject = "";
            //        int quantity = 0;
            //        DateTime date = DateTime.MinValue;
            //        List<string> images = new List<string>();



            //        while (!sr.EndOfStream)
            //        {

            //            string line = sr.ReadLine();
            //            if (line == "")
            //            {
            //                donateList.AddDonate(name, type, sum, financingObject, quantity, date, images);
            //                counter = -1;
            //                images = new List<string>();
            //            }
            //            if (counter == 0)
            //                name = line;
            //            if (counter == 1)
            //                type = line;
            //            if (counter == 2)
            //                sum = Convert.ToDouble(line);
            //            if (counter == 3)
            //                financingObject = line;
            //            if (counter == 4)
            //                quantity = Convert.ToInt32(line);
            //            if (counter == 5)
            //                date = Convert.ToDateTime(line);
            //            if (counter >= 6)
            //            {
            //                images.Add(line);
            //            }
            //            counter++;

            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(ex.Message);
            //    }

            //}

            try
            {
                DonateRepository donateRepository = new DonateRepository();
                donateList.SetDonates(donateRepository.GetAll());
                int j = 0;
                foreach (var donate in donateList.Donates)
                {

                    Label nameLabel = new Label();

                    nameLabel.Content = "Name: " + donate.name_donation;
                    nameLabel.FontSize = 16;
                    nameLabel.MaxWidth = 150;
                    nameLabel.Margin = new Thickness(-360, 30 + j * 100, 0, 0);
 
                    DonationsGrid.Children.Add(nameLabel);
                    Grid.SetRow(nameLabel, 0);
                    Grid.SetColumn(nameLabel, 0);

                    Label typeLabel = new Label();

                    typeLabel.Content = "Type: " + donate.type_donation;
                    typeLabel.FontSize = 16;
                    typeLabel.Margin = new Thickness(300, 30 + j * 100, 0, 0);
                    DonationsGrid.Children.Add(typeLabel);
                    Grid.SetRow(typeLabel, 1);
                    Grid.SetColumn(typeLabel, 1);

                    Label dateLabel = new Label();

                    dateLabel.Content = "Date: " + donate.date_donation.ToShortDateString();
                    dateLabel.FontSize = 16;
                    dateLabel.Margin = new Thickness(500, 30 + j * 100, 0, 0);
                    DonationsGrid.Children.Add(dateLabel);
                    Grid.SetRow(dateLabel, 2);
                    Grid.SetColumn(dateLabel, 2);

                    Button Edit = new Button();
                    Edit.FontSize = 16;
                    Edit.Width = 100;
                    Edit.Height = 30;
                    Edit.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FEC506"));
                    Edit.HorizontalAlignment = HorizontalAlignment.Left;
                    Edit.VerticalAlignment = VerticalAlignment.Top;
                    Edit.Margin = new Thickness(650, 30 + j * 100, 0, 0);
                    Edit.Content = "Детальніше";
                    Edit.Click += (sender, e) => EditClick(sender, e, donate.id_donation);
                    DonationsGrid.Children.Add(Edit);

                    Image image = new Image();
                    image.HorizontalAlignment = HorizontalAlignment.Left;
                    image.VerticalAlignment = VerticalAlignment.Top;
                    image.Margin = new Thickness(20, 12 + 100 * j, 0, 0);
                    image.Width = 80;
                    image.Height = 80;

                    // Встановлення зображення з файлу
                    if (donate.photo_donation != null)
                    {

                        BitmapImage bitmapImage = new BitmapImage();
                        using (MemoryStream memoryStream = new MemoryStream(donate.photo_donation))
                        {
                            memoryStream.Position = 0;

                            bitmapImage.BeginInit();
                            bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                            bitmapImage.StreamSource = memoryStream;
                            bitmapImage.EndInit();
                        }
                        // Визначити розмір для обрізки (в даному випадку, мінімальний розмір зображення)
                        int cropSize = Math.Min(bitmapImage.PixelWidth, bitmapImage.PixelHeight);

                        // Визначити прямокутник обрізки, щоб центр був центром оригінального зображення
                        int offsetX = (bitmapImage.PixelWidth - cropSize) / 2;
                        int offsetY = (bitmapImage.PixelHeight - cropSize) / 2;

                        // Обрізати зображення
                        CroppedBitmap croppedBitmap = new CroppedBitmap(bitmapImage, new Int32Rect(offsetX, offsetY, cropSize, cropSize));

                        // Створити новий WriteableBitmap і завантажити у нього обрізане зображення
                        WriteableBitmap writeableBitmap = new WriteableBitmap(croppedBitmap);
                        image.Source = writeableBitmap;
                    }


                    DonationsGrid.Children.Add(image);
                    Grid.SetRow(image, 0);  // Встановлюємо рядок
                    Grid.SetColumn(image, 3);  // Встановлюємо колонку
                    

                    Border border = new Border();
                    border.BorderBrush = Brushes.Black;
                    border.BorderThickness = new Thickness(1);
                    border.Margin = new Thickness(0, 100 * j, 0, 0);
                    DonationsGrid.Children.Add(border);
                    Grid.SetRow(border, 0);
                    Grid.SetColumn(border, 0);

                    //ScrollViewer scrollViewer = new ScrollViewer();
                    //DonationsGrid.Children.Add(scrollViewer);

                    j++;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

    }
}
