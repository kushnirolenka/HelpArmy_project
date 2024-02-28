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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net.Mail;
using HelpArmyDB;

namespace HelpArmy
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void Sign_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bool isReal = false;
                List<Organization> organizations = new List<Organization>();
                OrganizationRepository organizationRepository = new OrganizationRepository();
                organizations = organizationRepository.GetAll();
                Organization thisOrganization = new Organization();
                foreach (Organization organization in organizations)
                {
                    if (organization.email_organization.ToString() == EmailtextBox.Text)
                    {
                        isReal = true;
                        thisOrganization = organization;
                    }
                }

                List<Volunteer> volunteers = new List<Volunteer>();
                VolunteerRepository volunteerRepository = new VolunteerRepository();
                volunteers = volunteerRepository.GetAll();
                Volunteer thisVolunteer = new Volunteer();
                foreach (Volunteer volunteer in volunteers)
                {
                    if (volunteer.email_volunteer.ToString() == EmailtextBox.Text)
                    {
                        isReal = true;
                        thisVolunteer = volunteer;
                    }
                }
                if (!isReal)
                {
                    EmailErrorText.Text = "Аккаунту з цим e-mail не існує!";
                    MessageBox.Show("Аккаунту з цим e-mail не існує!");
                    return;
                }
                EmailErrorText.Text = "";
                if (thisVolunteer.email_volunteer == "")
                {
                    if (thisOrganization.password_organization.ToString() == PasswordtextBox.Text)
                    {
                        var helpType = new HelpType(thisOrganization, thisVolunteer);
                        this.Close();
                        helpType.Show();
                    }
                    else
                    {
                        PasswordErrorText.Text = "Неправильнй пароль!";
                        return;
                    }
                }
                else
                {
                    if (thisVolunteer.password_volunteer.ToString() == PasswordtextBox.Text)
                    {
                        var helpType = new HelpType(thisOrganization, thisVolunteer);
                        this.Close();
                        helpType.Show();
                    }
                    else
                    {
                        PasswordErrorText.Text = "Неправильнй пароль!";
                        return;
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
            
        }

        private void Volonter_Click(object sender, RoutedEventArgs e)
        {

            var registrationVol = new RegistrationVol();
            this.Close();
            registrationVol.Show();
        }

        private void Organisation_Click(object sender, RoutedEventArgs e)
        {
            var registrationOrg = new RegistrationOrg();
            this.Close();
            registrationOrg.Show();
        }

        private void Grid_Initialized(object sender, EventArgs e)
        {
            EmailtextBox.Text = "";
            PasswordtextBox.Text = "";
        }
    }
}
