﻿using Microsoft.Extensions.Configuration;
using Prototype.Model;
using System.Collections.ObjectModel;
using Npgsql;
using Prototypes.Business_Logic;

namespace Prototypes.Database
{
    class StageManagerDB : IStageManagerDB
    {
        //A ObservableCollection of Aiport objects
        private ObservableCollection<Performer> _performers;
        //The string to connect to the database
        private String _connString;


        /// <summary>
        /// A Constructor and initializes performers and connString and populates performers
        /// </summary>
        public StageManagerDB()
        {
            _performers = new ObservableCollection<Performer>();
            _connString = GetConnectionString();
            SelectAllPerformers();
        }

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

        public Boolean InsertSongForPerformer(int userId, String songName, String artistName, String duration)
        {
            try
            {
                // Connect and open a connection to the database
                using var conn = new NpgsqlConnection(_connString);
                conn.Open();

                // Command to insert a song into the 'songs' table
                using var cmd = new NpgsqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "INSERT INTO songs (setlist_id, title, artist, duration) " +
                                  "VALUES (@setlist_id, @title, @artist, @duration);";
                cmd.Parameters.AddWithValue("setlist_id", userId);
                cmd.Parameters.AddWithValue("title", songName);
                cmd.Parameters.AddWithValue("artist", artistName);
                cmd.Parameters.AddWithValue("duration", duration);
                cmd.ExecuteNonQuery();

                //Repopulates performers so now the updated performer is in it
                SelectAllPerformers();
            }
            catch (Npgsql.PostgresException pe)
            {
                return false;
            }
            return true;
        }

        public Boolean UpdatePerformerContact(int userId, int phoneNumbner, String email)
            {
                try
                {
                    //Connects and opens a connection to the database
                    using var conn = new NpgsqlConnection(_connString);
                    conn.Open();

                    //Commands to grab the performer with the given id and then update them
                    var cmd = new NpgsqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "UPDATE performers " +
                                      "SET  phone_number = @phoneNumber, email = @email " +
                                      "WHERE user_id = @user_id;";
                    cmd.Parameters.AddWithValue("phone_number", phoneNumbner);
                    cmd.Parameters.AddWithValue("email", email);

                    //Repopulates performers so now the updated performer is in it
                    SelectAllPerformers();
                }
                catch (Npgsql.PostgresException pe)
                {
                    return false;
                }
                return true;
        }


        /// <summary>
        /// Gets all the performers from the database
        /// </summary>
        /// <returns>ObservableCollection of performer objects</returns>
        public ObservableCollection<Performer> SelectAllPerformers()
        {
            // Create a new ObservableCollection to store performers
            ObservableCollection<Performer> performers = new ObservableCollection<Performer>();

            // Connects and opens a connection to the database
            using var conn = new NpgsqlConnection(_connString);
            conn.Open();

            // Commands to get all the performers in the database
            using var cmd = new NpgsqlCommand("SELECT user_id, phone_number, email " +
                                              "FROM performer;", conn);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                int userId = reader.GetInt16(0);
                int phoneNumber = reader.GetInt16(1);
                String email = reader.GetString(2);
                

                // Fetch first_name and last_name using a separate query
                using var nameCmd = new NpgsqlCommand("SELECT first_name, last_name " +
                                                      "FROM user_info " +
                                                      "WHERE user_id = @user_id;", conn);
                nameCmd.Parameters.AddWithValue("user_id", userId);
                using var nameReader = nameCmd.ExecuteReader();

                if (nameReader.Read())
                {
                    string firstName = nameReader.GetString(0);
                    string lastName = nameReader.GetString(1);

                    // Fetch songs for the performer using a separate query
                    using var songsCmd = new NpgsqlCommand("SELECT song_name " +
                                                           "FROM songs " +
                                                           "WHERE user_id = @user_id;", conn);
                    songsCmd.Parameters.AddWithValue("user_id", userId);
                    using var songsReader = songsCmd.ExecuteReader();

                    ObservableCollection<string> songs = new ObservableCollection<string>();

                    while (songsReader.Read())
                    {
                        string songName = songsReader.GetString(0);
                        songs.Add(songName);
                    }

                    // Create the Performer object and add it to the ObservableCollection
                    Performer performerToAdd = new Performer(userId, firstName, lastName, songs, email, phoneNumber);
                    performers.Add(performerToAdd);
                }
            }

            return performers;
        }

        /// <summary>
        /// Uses the given id to find a performer object with that id in a ObservableCollection
        /// </summary>
        /// <param name="id">the id of the performer that the user is trying to find</param>
        /// <returns>the performer if it is in the ObservableCollection. Returns null if the performer is not found</returns>
        public Performer SelectPerformer(int userId)
        {
            //Loop through all the aiports in the ObservableCollection
            foreach (Performer performer in _performers)
            {
                //If it is found return it
                if (performer.Id == userId)
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
                // Connect and open a connection to the database
                using var conn = new NpgsqlConnection(_connString);
                conn.Open();

                // Command to insert a new performer into the 'dreamrolesuser' table
                using var cmd = new NpgsqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "INSERT INTO dreamrolesuser (user_id, first_name, last_name, title) " +
                                  "VALUES (@user_id, @first_name, @last_name, @title);";
                cmd.Parameters.AddWithValue("user_id", performer.Id);
                cmd.Parameters.AddWithValue("first_name", performer.FirstName);
                cmd.Parameters.AddWithValue("last_name", performer.LastName);
                cmd.Parameters.AddWithValue("title", "Performer");
                cmd.ExecuteNonQuery();

                // Command to insert performer details into the 'performer' table
                cmd.CommandText = "INSERT INTO performer (user_id, phone_number, email) " +
                                  "VALUES (@user_id, @phone_number, @email);";
                cmd.Parameters.Clear(); // Clear parameters from the previous command
                cmd.Parameters.AddWithValue("user_id", performer.Id);
                cmd.Parameters.AddWithValue("phone_number", performer.PhoneNumber);
                cmd.Parameters.AddWithValue("email", performer.Email);
                cmd.ExecuteNonQuery();

                // Command to insert into 'setlists' table
                cmd.CommandText = "INSERT INTO setlists (user_id) " +
                                  "VALUES (@user_id);";
                cmd.Parameters.Clear(); // Clear parameters from the previous command
                cmd.Parameters.AddWithValue("user_id", performer.Id);
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
        /// Deletes the performer in the database with the given userId
        /// </summary>
        /// <param name="userId">the userId of the performer that will be deleted</param>
        /// <returns>true if the performer is found in the database and deleted. False if the performer is not in the database</returns>
        public Boolean DeletePerformer(int userId)
        {
            Performer performerToDel = null;

            //Looks for the performer that is to be deleted
            foreach (Performer performer in _performers)
            {
                if (performer.Id == userId)
                {
                    performerToDel = performer;
                    break;
                }
            }

            //Was found
            if (performerToDel != null)
            {
                //Connects and opens a connection to the database
                var conn = new NpgsqlConnection(_connString);
                conn.Open();

                //Commands to delete the performer from the database
                using var cmd = new NpgsqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "DELETE FROM dreamroleuser " +
                                  "WHERE user_id = @user_id";
                cmd.Parameters.AddWithValue("user_id", performerToDel.Id);
                cmd.CommandText = "DELETE FROM performers " +
                                  "WHERE user_id = @user_id";
                cmd.Parameters.AddWithValue("user_id", performerToDel.Id);
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
                using var conn = new NpgsqlConnection(_connString);
                conn.Open();

                //Commands to grab the performer with the given id and then update them
                var cmd = new NpgsqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "UPDATE dreamroleuser " +
                                  "SET first_name = @first_name, last_name = @last_name " +
                                  "WHERE user_id = @user_id;";
                cmd.Parameters.AddWithValue("user_id", performer.Id);
                cmd.Parameters.AddWithValue("first_name", performer.FirstName);
                cmd.Parameters.AddWithValue("last_name", performer.LastName);
                cmd.CommandText = "UPDATE performers " +
                                  "SET  phone_number = @phoneNumber, email = @email " +
                                  "WHERE user_id = @user_id;";
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
