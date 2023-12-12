using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using HelpArmyDB;
using MessageBox = System.Windows.Forms.MessageBox;

namespace HelpArmy
{
    /// <summary>
    /// Interaction logic for Window3.xaml
    /// </summary>
    public partial class HelpType : Window
    {
        Organization organization = new Organization();
        Volunteer volunteer = new Volunteer();
        public HelpType()
        {
            
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }
        public HelpType(Organization o, Volunteer v)
        {
            InitializeComponent();
            organization = o;
            volunteer = v;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void Rectangle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var createDonate = new CreateDonate(organization, volunteer);
            this.Close();
            createDonate.Show();
        }

        private void label_Copy_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var createDonate = new CreateDonate(organization, volunteer);
            this.Close();
            createDonate.Show();
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
            if(organization.email_organization == "")
            {
                MessageBox.Show("Звіт може створювати тільки організація!");
                return;
            }
            var graphic = new Graphic(organization, volunteer);
            this.Close();
            graphic.Show();
        }

        private void Grid_Initialized(object sender, EventArgs e)
        {
            string filePath = "C:\\Users\\Osnovnij\\Desktop\\HelpArmyProject\\HelpArmy\\HelpArmy\\гуманітарна.png";

            try
            {

                BitmapImage originalImage = new BitmapImage();
                originalImage.BeginInit();
                originalImage.UriSource = new Uri(filePath, UriKind.Absolute);
                originalImage.EndInit();

                // Визначити розмір для обрізки (в даному випадку, мінімальний розмір зображення)
                int cropSize = Math.Min(originalImage.PixelWidth, originalImage.PixelHeight);

                // Визначити прямокутник обрізки, щоб центр був центром оригінального зображення
                int offsetX = (originalImage.PixelWidth - cropSize) / 2;
                int offsetY = (originalImage.PixelHeight - cropSize) / 2;

                // Обрізати зображення
                CroppedBitmap croppedBitmap = new CroppedBitmap(originalImage, new Int32Rect(offsetX, offsetY, cropSize, cropSize));

                // Створити новий WriteableBitmap і завантажити у нього обрізане зображення
                WriteableBitmap writeableBitmap = new WriteableBitmap(croppedBitmap);

                // Встановити WriteableBitmap як джерело для ImageBrush
                imageBrush2.ImageSource = writeableBitmap;
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Помилка відображення зображення: {ex.Message}", "Помилка", (MessageBoxButton)(MessageBoxButtons)MessageBoxButton.OK, (MessageBoxImage)(MessageBoxIcon)MessageBoxImage.Error);
            }

            filePath = "C:\\Users\\Osnovnij\\Desktop\\HelpArmyProject\\HelpArmy\\HelpArmy\\військова.png";

            try
            {

                BitmapImage originalImage = new BitmapImage();
                originalImage.BeginInit();
                originalImage.UriSource = new Uri(filePath, UriKind.Absolute);
                originalImage.EndInit();

                // Визначити розмір для обрізки (в даному випадку, мінімальний розмір зображення)
                int cropSize = Math.Min(originalImage.PixelWidth, originalImage.PixelHeight);

                // Визначити прямокутник обрізки, щоб центр був центром оригінального зображення
                int offsetX = (originalImage.PixelWidth - cropSize) / 2;
                int offsetY = (originalImage.PixelHeight - cropSize) / 2;

                // Обрізати зображення
                CroppedBitmap croppedBitmap = new CroppedBitmap(originalImage, new Int32Rect(offsetX, offsetY, cropSize, cropSize));

                // Створити новий WriteableBitmap і завантажити у нього обрізане зображення
                WriteableBitmap writeableBitmap = new WriteableBitmap(croppedBitmap);

                // Встановити WriteableBitmap як джерело для ImageBrush
                imageBrush1.ImageSource = writeableBitmap;
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Помилка відображення зображення: {ex.Message}", "Помилка", (MessageBoxButton)(MessageBoxButtons)MessageBoxButton.OK, (MessageBoxImage)(MessageBoxIcon)MessageBoxImage.Error);
            }
        }

        private void imageRectangleHumanitary_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var createDonate = new CreateDonate(organization, volunteer);
            createDonate.TypesComboBox.Items.Add("Харчі");
            createDonate.TypesComboBox.Items.Add("Засоби гігієни");
            createDonate.TypesComboBox.Items.Add("Медикаменти");
            createDonate.TypesComboBox.Items.Add("Фінанси(гуманітарні)");
            createDonate.TypesComboBox.Items.Add("Одяг");
            createDonate.TypesComboBox.Items.Add("Освіта");
            createDonate.TypesComboBox.Items.Add("Інше");
            createDonate.CategoryLabel.Content += "Гуманітарна";
            this.Close();
            createDonate.Show();
        }

        private void imageRectangleMilitary_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var createDonate = new CreateDonate(organization, volunteer);
            createDonate.TypesComboBox.Items.Add("Зброя");
            createDonate.TypesComboBox.Items.Add("Фінанси(військові)");
            createDonate.TypesComboBox.Items.Add("Засоби пересування");
            createDonate.TypesComboBox.Items.Add("Літальні апарати");
            createDonate.TypesComboBox.Items.Add("Інше");
            createDonate.CategoryLabel.Content += "Військова";
            this.Close();
            createDonate.Show();
        }
    }

}
