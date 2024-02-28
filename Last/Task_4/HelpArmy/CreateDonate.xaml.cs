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
using MessageBox = System.Windows.Forms.MessageBox;
﻿using Npgsql;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using HelpArmyDB;

namespace HelpArmy
{
    /// <summary>
    /// Interaction logic for CreateDonate.xaml
    /// </summary>
    public partial class CreateDonate : Window
    {
        Organization organization = new Organization();
        Volunteer volunteer = new Volunteer();
        string category = "";
        string imagePath = "";
        List<byte[]> images = new List<byte[]>();
        public CreateDonate()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }
        public CreateDonate(Organization o, Volunteer v)
        {
            volunteer = v;
            organization = o;
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }
        private void Count_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Plus1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Зображення (*.png;*.jpeg;*.jpg;*.gif)|*.png;*.jpeg;*.jpg;*.gif|Всі файли (*.*)|*.*";
            openFileDialog.ShowDialog();

            string filePath = openFileDialog.FileName;
            imagePath = filePath;

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

                byte[] data;
                JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(writeableBitmap));
                using (MemoryStream ms = new MemoryStream())
                {
                    encoder.Save(ms);
                    data = ms.ToArray();
                }
                images.Add(data);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка відображення зображення: {ex.Message}", "Помилка", (MessageBoxButtons)MessageBoxButton.OK, (MessageBoxIcon)MessageBoxImage.Error);
            }
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
                string connectionString = "Host=localhost;Port=5432;Database=helparmy;Username=postgres;Password=001001001b;";
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    using (var command = new NpgsqlCommand("INSERT INTO donation (category_donation, name_donation, " +
                                                        "type_donation, sum_donation, object_donation, " +
                                                        "number_donation, date_donation, photo_donation) " +
                                                        "VALUES (@category, @name, @type, @sum, @object, " +
                                                        "@number, @date, @photo)", connection))
                    {
                        command.Parameters.Clear();
                        command.Parameters.AddWithValue("@category", category);
                        command.Parameters.AddWithValue("@name", textBoxName.Text);
                        command.Parameters.AddWithValue("@type", TypesComboBox.Text);
                        command.Parameters.AddWithValue("@sum", Convert.ToInt32(textBoxSum.Text));
                        command.Parameters.AddWithValue("@object", textBoxFinancingObject.Text);
                        command.Parameters.AddWithValue("@number", Convert.ToInt32(textBoxQuantity.Text));
                        command.Parameters.AddWithValue("@date", SelectedDate.SelectedDate);
                        command.Parameters.AddWithValue("@photo", File.ReadAllBytes(imagePath));
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            

            //Donate donate = new Donate(textBoxName.Text, TypesComboBox.Text, Convert.ToDouble(textBoxSum.Text), textBoxFinancingObject.Text, Convert.ToInt32(textBoxQuantity.Text), SelectedDate.DisplayDate, images);
            //MessageBox.Show(donate.ToString());
            //donate.WriteToFile("Donates.txt", donate);
            var donations = new Donations(organization, volunteer);
            this.Close();
            donations.Show();
        }

        private void textBoxSum_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
            {
                // Якщо символ не є цифрою, відмінити введення
                e.Handled = true;
            }
        }

        private void textBoxQuantity_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
            {
                // Якщо символ не є цифрою, відмінити введення
                e.Handled = true;
            }
        }

        private void Grid_Initialized(object sender, EventArgs e)
        {
            if(CategoryLabel.Content.ToString() == "Категорія: Військова")
            {
                category = "military";
            }
            else
            {
                category = "humanitary";
            }
            textBoxSum.Text = "";
            textBoxName.Text = "";
            textBoxFinancingObject.Text = "";
            textBoxQuantity.Text = "";
        }

        private void TypesComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}

