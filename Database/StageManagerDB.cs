using Microsoft.Extensions.Configuration;
using Prototype.Model;
using System.Collections.ObjectModel;
using Npgsql;

namespace Prototypes.Database
{
    class StageManagerDB : IStageManagerDB
    {
        //A ObservableCollection of Aiport objects
        private ObservableCollection<Performer> performers;
        //The string to connect to the database
        private String connString;


        /// <summary>
        /// The string needed to connect to the database
        /// </summary>
        /// <returns>A String that has the infomration to connect to the database</returns>
        static String GetConnectionString()
        {
            var connStringBuilder = new NpgsqlConnectionStringBuilder();
            connStringBuilder.Host = "airport-table-13041.5xj.cockroachlabs.cloud";
            connStringBuilder.Port = 26257;
            connStringBuilder.SslMode = SslMode.VerifyFull;
            connStringBuilder.Username = "keenan_marco";
            connStringBuilder.Password = "6tRK2gvZOx62cwwPBe8znA";
            connStringBuilder.Database = "dreamroles";
            connStringBuilder.ApplicationName = "whatever";
            connStringBuilder.IncludeErrorDetail = true;


            return connStringBuilder.ConnectionString;
        }

        /*
        /// <summary>
        /// Will get a password from the secret.json file
        /// </summary>
        /// <returns>a password</returns>
        static String FetchPassword()
        {
            IConfiguration config = new ConfigurationBuilder().AddUserSecrets<StageManagerDB>().Build();
            return config["CockroachDBPassword"] ?? "6tRK2gvZOx62cwwPBe8znA"; // this works in VS, not VSC
        }
        */

        //FIX ME YOU NEED TO GRAP FROM THE USERS AND PERFORMERS
        /// <summary>
        /// Gets all the performers from the database
        /// </summary>
        /// <returns>ObservableCollection of performer objects</returns>
        public ObservableCollection<Performer> SelectAllPerformers()
        {
            //Makes sure the performer has something in it
            if (performers != null)
            {
                performers.Clear();
            }
            //Connects and opens a connection to the database
            var conn = new NpgsqlConnection(connString);
            conn.Open();

            //Commands to get all the performers in the database
            using var cmd = new NpgsqlCommand("SELECT id, city, date, rating FROM performers;", conn);
            using var reader = cmd.ExecuteReader();

            //loops through all the information from the database
            while (reader.Read())
            {
                //Creates a new performer object and adds it to the ObservableCollection
                String id = reader.GetString(0);
                String firstName = reader.GetString(1);
                String lastName = reader.GetString(2);
                ObservableCollection<String> songs = new ObservableCollection<String>();
                songs.Add(reader.GetString(3));
                String email = reader.GetString(3);
                String phoneNumber = reader.GetString(4);
                Performer performerToAdd = new(id, firstName, lastName, songs, email, phoneNumber );
                performers.Add(performerToAdd);
                Console.WriteLine(performerToAdd);
            }
            return performers;
        }

        /// <summary>
        /// A Constructor and initializes performers and connString and populates performers
        /// </summary>
        public StageManagerDB()
        {
            performers = new ObservableCollection<Performer>();
            connString = GetConnectionString();
            SelectAllPerformers();
        }

        /// <summary>
        /// Uses the given id to find a performer object with that id in a ObservableCollection
        /// </summary>
        /// <param name="id">the id of the performer that the user is trying to find</param>
        /// <returns>the performer if it is in the ObservableCollection. Returns null if the performer is not found</returns>
        public Performer SelectPerformer(String id)
        {
            //Loop through all the aiports in the ObservableCollection
            foreach (Performer performer in performers)
            {
                //If it is found return it
                if (performer.Id == id)
                {
                    return performer;
                }
            }
            //Not found so return null
            return null;
        }

        /// <summary>
        /// Adds a new performer object into the database
        /// </summary>
        /// <param name="performer">the performer that the user wants to put into the file</param>
        /// <returns>True if it was inserted into the database, false otherwise</returns>
        public Boolean InsertPerformer(Performer performer)
        {
            try
            {
                //Connects and opens a connection to the database
                using var conn = new NpgsqlConnection(connString);
                conn.Open();

                //Commands to insert a new performer into the database
                var cmd = new NpgsqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "INSERT INTO dreamroleuser Values (@id, @first_name @last_name @title);";
                cmd.Parameters.AddWithValue("id", performer.Id);
                cmd.Parameters.AddWithValue("first_name", performer.FirstName);
                cmd.Parameters.AddWithValue("last_name", performer.LastName);
                cmd.Parameters.AddWithValue("title", "Performer");
                cmd.CommandText = "INSERT INTO performer VALUES (@id, @phone_number @email);";
                cmd.Parameters.AddWithValue("id", performer.Id);
                cmd.Parameters.AddWithValue("phone_number", performer.PhoneNumber);
                cmd.Parameters.AddWithValue("email", performer.Email);
                cmd.ExecuteNonQuery();

                //Repopulates the performers
                SelectAllPerformers();

            }
            catch (Npgsql.PostgresException e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Deletes the performer in the database with the given id
        /// </summary>
        /// <param name="id">the id of the performer that will be deleted</param>
        /// <returns>true if the performer is found in the database and deleted. False if the performer is not in the database</returns>
        public Boolean DeletePerformer(String id)
        {
            Performer performerToDel = null;

            //Looks for the performer that is to be deleted
            foreach (Performer performer in performers)
            {
                if (performer.Id == id)
                {
                    performerToDel = performer;
                    break;
                }
            }

            //Was found
            if (performerToDel != null)
            {
                //Connects and opens a connection to the database
                var conn = new NpgsqlConnection(connString);
                conn.Open();

                //Commands to delete the performer from the database
                using var cmd = new NpgsqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "DELETE FROM dreamroleuser WHERE id = @id";
                cmd.Parameters.AddWithValue("id", performerToDel.Id);
                cmd.CommandText = "DELETE FROM performers WHERE id = @id";
                cmd.Parameters.AddWithValue("id", performerToDel.Id);
                int numDeleted = cmd.ExecuteNonQuery();

                //Check that it deleted something
                if (numDeleted > 0)
                {
                    SelectAllPerformers();
                }
                return numDeleted > 0;
            }

            return false;
        }


        /// <summary>
        /// Will update a performer after checking that the performer is already in the database
        /// </summary>
        /// <param name="performer">the updated versoin of the performer</param>
        /// <returns>true if it found and updated the performer. False if it could not find the performer in the database</returns>
        public Boolean UpdatePerformer(Performer performer)
        {
            try
            {
                //Connects and opens a connection to the database
                using var conn = new NpgsqlConnection(connString);
                conn.Open();

                //Commands to grab the performer with the given id and then update them
                var cmd = new NpgsqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "UPDATE dreamroleuser SET first_name = @first_name, last_name = @last_name WHERE id = @id;";
                cmd.Parameters.AddWithValue("id", performer.Id);
                cmd.Parameters.AddWithValue("first_name", performer.FirstName);
                cmd.Parameters.AddWithValue("last_name", performer.LastName);
                cmd.CommandText = "UPDATE performers SET  phone_number = @phoneNumber, email = @email WHERE id = @id;";
                cmd.Parameters.AddWithValue("phone_number", performer.PhoneNumber);
                cmd.Parameters.AddWithValue("email", performer.Email);
                var numAffected = cmd.ExecuteNonQuery();

                //Repopulates performers so now the updated performer is in it
                SelectAllPerformers();
            }
            catch (Npgsql.PostgresException pe)
            {
                return false;
            }
            return true;
        }
    }

}
