using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Npgsql;

namespace HelpArmy
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class RegistrationVol : Window
    {
        public RegistrationVol()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void textBox_Copy1_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            bool HaveError = false;
            string password = PasswordText.Password;

            bool isValidPassword = !string.IsNullOrEmpty(password) &&
                                   new[] { char.IsDigit, char.IsUpper, char.IsLower, (Func<char, bool>)(c => !char.IsWhiteSpace(c)) }
                                       .All(test => password.Any(test));
            if (NametextBox.Text == "")
            {
                nameErrorText.Text = "Введіть ім'я!";
                HaveError = true;
            }
            else
            {
                nameErrorText.Text = "";
            }
            if (SurnametextBox.Text == "")
            {
                surnameErrorText.Text = "Введіть прізвище!!";
                HaveError = true;
            }
            else
            {
                surnameErrorText.Text = "";
            }
            if (CitycomboBox.SelectedItem == null)
            {
                cityErrorText.Text = "Виберіть тип донату!";
                HaveError = true;
            }
            else
            {
                cityErrorText.Text = "";
            }
            if (string.IsNullOrWhiteSpace(EmailtextBox.Text) || !EmailtextBox.Text.Contains("@") || EmailtextBox.Text != EmailtextBox.Text.ToLower())
            {
                emailErrorText.Text = "Пошта введена некоректно!";
                HaveError = true;
            }
            else
            {
                emailErrorText.Text = "";
            }
            if (!isValidPassword)
            {
                passwordErrorText.Text = "Пароль повинен містити хоча б одну цифру, велику та малу літеру, і не має містити пробілів.";
                HaveError = true;
            }
            else
            {
                passwordErrorText.Text = "";
            }
            if (HaveError)
                return;

            try
            {
                string connectionString = "Host=localhost;Port=5432;Database=helparmy;Username=postgres;Password=001001001b;";
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    using (var command = new NpgsqlCommand("INSERT INTO volunteer (name_volunteer, surname_volunteer, " +
                    "city_volunteer, email_volunteer, password_volunteer) " +
                    "VALUES (@name, @surname, @city, @email, @password)", connection))
                    {
                        command.Parameters.Clear();
                        command.Parameters.AddWithValue("@name", NametextBox.Text);
                        command.Parameters.AddWithValue("@surname", SurnametextBox.Text);
                        command.Parameters.AddWithValue("@city", CitycomboBox.Text);
                        command.Parameters.AddWithValue("@email", EmailtextBox.Text);
                        command.Parameters.AddWithValue("@password", PasswordText.Password.ToString());

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }



            var donations = new Donations();
            this.Close();
            donations.Show();
        }

        private void PasswordText_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        private void textBox_Copy1_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (System.Text.RegularExpressions.Regex.IsMatch(textBox.Text, @"\s"))
            {
                textBox.Text = textBox.Text.Replace(" ", "");
                textBox.CaretIndex = textBox.Text.Length;
            }
        }

        private void Grid_Initialized(object sender, EventArgs e)
        {
            NametextBox.Text = "";
            SurnametextBox.Text = "";
        }
    }
}
