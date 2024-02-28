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
    /// Interaction logic for DonateInfo.xaml
    /// </summary>
    public partial class DonateInfo : Window
    {
        public int id_donation;
        Organization organization = new Organization();
        Volunteer volunteer = new Volunteer();
        public DonateInfo(int id)
        {
            id_donation = id;
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }
        public DonateInfo(int id, Organization o, Volunteer v)
        {
            organization = o;
            volunteer = v;
            id_donation = id;
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

        private void DonationGrid_Initialized(object sender, EventArgs e)
        {
            try
            {
                DonateRepository donateRepository = new DonateRepository();
                Donate donate = donateRepository.Get(id_donation);
                textBoxName.Text = donate.name_donation;
                textBoxFinancingObject.Text = donate.object_donation;
                textBoxQuantity.Text = Convert.ToString(donate.number_donation);
                textBoxSum.Text = Convert.ToString(donate.sum_donation);

                if(donate.category_donation == "humanitary")
                {
                    TypesComboBox.Items.Add("Харчі");
                    TypesComboBox.Items.Add("Засоби гігієни");
                    TypesComboBox.Items.Add("Медикаменти");
                    TypesComboBox.Items.Add("Фінанси");
                    TypesComboBox.Items.Add("Одяг");
                    TypesComboBox.Items.Add("Освіта");
                    TypesComboBox.Items.Add("Інше");
                    CategoryLabel.Content += "Гуманітарна";
                }
                else
                {
                    TypesComboBox.Items.Add("Зброя");
                    TypesComboBox.Items.Add("Фінанси");
                    TypesComboBox.Items.Add("Засоби пересування");
                    TypesComboBox.Items.Add("Літальні апарати");
                    TypesComboBox.Items.Add("Інше");
                    CategoryLabel.Content += "Військова";
                }
                TypesComboBox.SelectedItem = donate.type_donation;

                SelectedDate.SelectedDate = donate.date_donation;

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
                imageBrush1.ImageSource = writeableBitmap;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            bool HaveError = false;
            if (textBoxName.Text == "")
            {
                NameErrorText.Text = "Назва повинна бути не пуста!";
                HaveError = true;
            }
            else
            {
                NameErrorText.Text = "";
            }
            if (TypesComboBox.SelectedItem == null)
            {
                TypeErrorText.Text = "Виберіть тип донату!";
                HaveError = true;
            }
            else
            {
                TypeErrorText.Text = "";
            }
            if (textBoxSum.Text == "")
            {
                SumErrorText.Text = "Сума повинна бути не пуста!";
                HaveError = true;
            }
            else
            {
                SumErrorText.Text = "";
            }
            if (textBoxFinancingObject.Text == "")
            {
                FinancingObjectErrorText.Text = "Об'єкт фінансування повиннен бути не пустий!";
                HaveError = true;
            }
            else
            {
                FinancingObjectErrorText.Text = "";
            }
            if (textBoxQuantity.Text == "")
            {
                QuantityErrorText.Text = "Кількість повинна бути не пуста!";
                HaveError = true;
            }
            else
            {
                QuantityErrorText.Text = "";
            }
            if (HaveError)
                return;
            try
            {
                DonateRepository donateRepository = new DonateRepository();
                Donate donate = donateRepository.Get(id_donation);

                Donate temp = new Donate();
                temp.name_donation = textBoxName.Text;
                temp.type_donation = TypesComboBox.Text;
                temp.sum_donation = Convert.ToInt32(textBoxSum.Text);
                temp.object_donation = textBoxFinancingObject.Text;
                temp.number_donation = Convert.ToInt32(textBoxQuantity.Text);
                temp.date_donation = SelectedDate.SelectedDate.Value;
                if (CategoryLabel.Content.ToString() == "Гуманітарна")
                {
                    temp.category_donation = "humanitary";
                }
                else
                {
                    temp.category_donation = "military";
                }
                temp.photo_donation = donate.photo_donation;

                donateRepository.Update(temp, id_donation);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
