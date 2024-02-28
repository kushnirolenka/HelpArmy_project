using System;
using System.IO;
using Npgsql;

namespace HelpArmyDB
{
    public class DB_connect
    {
        public void SetupingScript()
        {
            string connectionString = "Host=localhost;Port=5432;Username=postgres;Password=001001001b;";
            try
            {
                // Reading an SQL script from a file
                string script = File.ReadAllText("tables_HelpArmy.sql");

                // Connecting to PostgreSQL
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    // Execute the SQL script to create the database
                    using (NpgsqlCommand createDbCommand = new NpgsqlCommand("CREATE DATABASE HelpArmy;", connection))
                    {
                        createDbCommand.ExecuteNonQuery();
                    }
                    connection.Close();
                    Console.WriteLine("Database 'HelpArmy' created successfully.");
                }
                connectionString = "Host=localhost;Port=5432;Database=helparmy;Username=postgres;Password=001001001b;";
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();
                    // Executing an SQL script to create tables in the 'HelpArmy' database
                    using (NpgsqlCommand command = new NpgsqlCommand(script, connection))
                    {
                        command.ExecuteNonQuery();
                        Console.WriteLine("The tables were created successfully.");
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}