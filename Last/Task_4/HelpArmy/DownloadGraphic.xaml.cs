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
    /// Interaction logic for DownloadGraphic.xaml
    /// </summary>
    public partial class DownloadGraphic : Window
    {
        Organization organization = new Organization();
        Volunteer volunteer = new Volunteer();
        Report report = new Report();
        public DownloadGraphic()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }
        public DownloadGraphic(Organization o, Volunteer v, Report r)
        {
            organization = o;
            volunteer = v;
            report = r;
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
            var graphic = new Graphic(organization, volunteer);
            this.Close();
            graphic.Show();
        }

        private void Grid_Initialized(object sender, EventArgs e)
        {
            DonateRepository donateRepository = new DonateRepository();
            List<Donate> donates = donateRepository.GetAll();
            List<Donate> donates_for_report = new List<Donate>();

            foreach(Donate donate in donates)
            {
                if(report.date_start_report <= donate.date_donation && report.date_end_report >= donate.date_donation && report.type_donation_report == donate.type_donation)
                {
                    donates_for_report.Add(donate);

                }
            }
            double sum = 0;
            foreach (Donate donate in donates_for_report)
            {
                sum += donate.sum_donation;
            }
            SumTextBox.Text = Convert.ToString(sum);


            
        }

    }
}
