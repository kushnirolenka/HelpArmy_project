using System;
using System.Collections.Generic;
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
    /// Interaction logic for Graphic.xaml
    /// </summary>
    public partial class Graphic : Window
    {
        Organization organization = new Organization();
        Volunteer volunteer = new Volunteer();
        string category = "";
        public Graphic()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }
        public Graphic(Organization o, Volunteer v)
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
            var graphic = new Graphic(organization, volunteer);
            this.Close();
            graphic.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(category == "")
            {
                CategotyErrorText.Text = "Веберіть категорію!";
                return;
            }
            else
            {
                CategotyErrorText.Text = "";
            }
            if(TypesComboBox.SelectedItem == null)
            {
                TypeErrorText.Text = "Веберіть тип!";
                return;
            }
            else
            {
                TypeErrorText.Text = "";
            }
            if(StartDatePicker.SelectedDate == null)
            {
                StartErrorText.Text = "Виберіть дату початку!";
            }
            else
            {
                StartErrorText.Text = "";
            }
            if (EndDatePicker.SelectedDate == null)
            {
                EndDatePicker.Text = "Виберіть дату початку!";
            }
            else
            {
                StartErrorText.Text = "";
            }
            Report report = new Report();
            report.type_donation_report = TypesComboBox.Text;
            report.date_start_report = StartDatePicker.SelectedDate.Value;
            report.date_end_report = EndDatePicker.SelectedDate.Value;
            report.id_organization = organization.id_organization;

            var downloadGraphic = new DownloadGraphic(organization, volunteer, report);
            this.Close();
            downloadGraphic.Show();
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            category = "military";
            TypesComboBox.Items.Clear();
            TypesComboBox.Items.Add("Харчі");
            TypesComboBox.Items.Add("Засоби гігієни");
            TypesComboBox.Items.Add("Медикаменти");
            TypesComboBox.Items.Add("Фінанси(військові)");
            TypesComboBox.Items.Add("Одяг");
            TypesComboBox.Items.Add("Освіта");
            TypesComboBox.Items.Add("Інше");

        }

        private void Image_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            category = "humanitary";
            TypesComboBox.Items.Clear();
            TypesComboBox.Items.Add("Зброя");
            TypesComboBox.Items.Add("Фінанси(гуманітарні)");
            TypesComboBox.Items.Add("Засоби пересування");
            TypesComboBox.Items.Add("Літальні апарати");
            TypesComboBox.Items.Add("Інше");
        }

        private void SelectDates_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
