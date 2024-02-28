using System;
using System.Collections.Generic;
using System.Text;
using Npgsql;
using Bogus;

namespace HelpArmyDB
{
    class TablesDB
    {

        //fills all tables with random data
        public void FillTables()
        {
            List<string> categoryNames = new List<string> { "Humanitarian", "Military" };
            List<string> statusNames = new List<string> { "Pending", "Received", "Processing" };
            string connectionString = "Host=localhost;Port=5432;Database=helparmy;Username=postgres;Password=001001001b;";
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    FillOrganization(connection, 50);
                    FillVolunteers(connection, 50);
                    FillDonation(connection, 50, categoryNames, statusNames);
                    FillReport(connection, 50);


                    Console.WriteLine("Data successfully added to database.");
                    Console.WriteLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
        //outputs table data to the console
        public void PrintTable(NpgsqlConnection connection, string talbe)
        {
            using (var command = new NpgsqlCommand($"SELECT * FROM {talbe}", connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    Console.WriteLine($"{talbe}:");
                    while (reader.Read())
                    {
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            Console.WriteLine($"{reader.GetName(i)}: {reader[i]}");
                        }
                        Console.WriteLine();
                    }
                }
            }
        }
        //outputs all tables data to the console
        public void PrintAllTables()
        {
            List<string> tableNames = new List<string> { "volunteer", "organization", "donation", "report" };
            string connectionString = "Host=localhost;Port=5432;Database=helparmy;Username=postgres;Password=001001001b;";
            try
            {
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    foreach (string tableName in tableNames)
                    {
                        Console.WriteLine($"Data from the {tableName} table:");;
                        PrintTable(connection,tableName);
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
        //fills Organization table with random data
        static void FillOrganization(NpgsqlConnection connection, int numberOfRecords)
        {
            var fakeData = new Bogus.Faker();
            using (var command = new NpgsqlCommand("INSERT INTO organization (name_organization, city_organization, email_organization, password_organization) " +
                                                        "VALUES (@name, @city, @email, @password)", connection))
            {
                for (int i = 0; i < numberOfRecords; i++)
                {
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@Name", fakeData.Company.CompanyName());
                    command.Parameters.AddWithValue("@city", fakeData.Address.City());
                    command.Parameters.AddWithValue("@email", fakeData.Internet.Email());
                    command.Parameters.AddWithValue("@password", fakeData.Internet.Password());

                    command.ExecuteNonQuery();
                }
            }
        }
        //fills Volunteers table with random data
        static void FillVolunteers(NpgsqlConnection connection, int numberOfRecords)
        {
            var fakeData = new Bogus.Faker();
            using (var command = new NpgsqlCommand("INSERT INTO volunteer (name_volunteer, surname_volunteer, " +
                    "city_volunteer, email_volunteer, password_volunteer) " +
                    "VALUES (@name, @surname, @city, @email, @password)", connection))
            {
                for (int i = 0; i < numberOfRecords; i++)
                {
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@name", fakeData.Name.FirstName());
                    command.Parameters.AddWithValue("@surname", fakeData.Name.LastName());
                    command.Parameters.AddWithValue("@city", fakeData.Address.City());
                    command.Parameters.AddWithValue("@email", fakeData.Internet.Email());
                    command.Parameters.AddWithValue("@password", fakeData.Internet.Password());

                    command.ExecuteNonQuery();
                }
            }
        }
        //fills Donation table with random data
        static void FillDonation(NpgsqlConnection connection, int numberOfRecords, List<string> categoryNames, List<string> statusNames)
        {
            var fakeData = new Bogus.Faker();
            using (var command = new NpgsqlCommand("INSERT INTO donation (category_donation, name_donation, " +
                                                        "type_donation, sum_donation, object_donation, " +
                                                        "number_donation, date_donation, status_donation) " +
                                                        "VALUES (@category, @name, @type, @sum, @object, " +
                                                        "@number, @date, @status)", connection))
            {
                for (int i = 0; i < numberOfRecords; i++)
                {
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@category", categoryNames[fakeData.Random.Int(0, 1)]);
                    command.Parameters.AddWithValue("@name", fakeData.Name.FirstName());
                    command.Parameters.AddWithValue("@type", fakeData.Lorem.Word());
                    command.Parameters.AddWithValue("@sum", fakeData.Random.Int(0, 1000000));
                    command.Parameters.AddWithValue("@object", fakeData.Lorem.Word());
                    command.Parameters.AddWithValue("@number", fakeData.Random.Int(0, 10000));
                    command.Parameters.AddWithValue("@date", fakeData.Date.Past());
                    command.Parameters.AddWithValue("@status", statusNames[fakeData.Random.Int(0, 2)]);

                    command.ExecuteNonQuery();
                }
            }
        }
        //fills Report table with random data
        static void FillReport(NpgsqlConnection connection, int numberOfRecords)
        {
            var fakeData = new Bogus.Faker();
            using (var command = new NpgsqlCommand("INSERT INTO report (type_donation_report, date_start_report, " +
                   "date_end_report, id_donation, id_organization) " +
                   "VALUES (@typeDonationReport, @dateStartReport, @dateEndReport, @idDonation, @idOrganization)", connection))
            {
                for (int i = 0; i < numberOfRecords; i++)
                {
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@typeDonationReport", fakeData.Random.Word());
                    command.Parameters.AddWithValue("@dateStartReport", fakeData.Date.Past());
                    command.Parameters.AddWithValue("@dateEndReport", fakeData.Date.Future());
                    command.Parameters.AddWithValue("@idDonation", fakeData.Random.Int(1, numberOfRecords));
                    command.Parameters.AddWithValue("@idOrganization", fakeData.Random.Int(1, numberOfRecords));

                    command.ExecuteNonQuery();
                }
            }
        }
    }

}
