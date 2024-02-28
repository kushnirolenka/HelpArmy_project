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
using HelpArmyDB;

namespace HelpArmy
{
    /// <summary>
    /// Interaction logic for Profile.xaml
    /// </summary>
    public partial class Profile : Window
    {
        Organization organization = new Organization();
        Volunteer volunteer = new Volunteer();
        
        public Profile()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }
        public Profile(Organization o, Volunteer v)
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

        private void Grid_Initialized(object sender, EventArgs e)
        {
            if (organization.email_organization == "")
            {
                SurnameLabel.Content = "Фамілія: " +  volunteer.surname_volunteer;
                NameLabel.Content = "Ім'я: " + volunteer.name_volunteer;
                CityLabel.Content = "Місто: " + volunteer.city_volunteer;
                EmailLabel.Content = "E-mail: " + volunteer.email_volunteer;
            }
            else
            {
                SurnameLabel.Content = "";
                NameLabel.Content = "Назва: " + organization.name_organization;
                CityLabel.Content = "Місто: " + organization.city_organization;
                EmailLabel.Content = "E-mail: " + organization.email_organization;
            }
        }
    }
}
