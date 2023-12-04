using Prototypes.Model;
using Prototypes.Databases.Interface;
using System.Collections.ObjectModel;
using Npgsql;
using Prototypes.Model.Interfaces;

namespace Prototypes.Databases
{
    public partial class Database : IDatabase
    {
        private ObservableCollection<Performer> _performers;
        
        /// <summary>
        /// Gets all the performers from the database
        /// </summary>
        /// <returns>ObservableCollection of performer objects</returns>
        public ObservableCollection<Performer> SelectAllPerformers(int year)
        {
            // Create a new ObservableCollection to store performers

            _performers.Clear();
            // Connects and opens a connection to the database
            using var conn = new NpgsqlConnection(_connString);
            conn.Open();

            // Commands to get all the performers in the database
            using var cmd = new NpgsqlCommand(
                 $"SELECT *\r\nFROM performer\r\nINNER JOIN dreamrolesuser\r\nUSING (user_id) WHERE dreamrolesuser.production_year = {year};", conn);
            using var reader = cmd.ExecuteReader();

            // Create a Performer object for each row returned from query
            while (reader.Read())
            {
                int userId = reader.GetInt16(0);
                String email = reader.IsDBNull(1) ? "" : reader.GetString(1);
                int absences = reader.IsDBNull(2) ? 0 : reader.GetInt32(2);
                String checkedInStatus = reader.GetString(3);
                String phoneNumber = reader.IsDBNull(4) ? "" : reader.GetString(4) + "";
                String firstName = reader.GetString(5);
                String lastName = reader.GetString(6);
                ObservableCollection<ISongDB> setList = new();

                // Create the Performer object and add it to the ObservableCollection
                Performer performerToAdd = new Performer(userId, firstName, lastName, setList, email, phoneNumber, absences, checkedInStatus);
                _performers.Add(performerToAdd);
            }

            foreach (var performer in _performers)
            {
                performer.Songs = SelectPerformerSongs(performer.Id);
            }

            GetCheckedInPerformers();

            return _performers;
        }

        public ObservableCollection<Performer> SelectAllPerfomersFromRehearsal(DateTime rehearsalTime, String songTitle)
        {
            // Connects and opens a connection to the database
            using var conn = new NpgsqlConnection(_connString);
            conn.Open();

            // Commands to get all the performers in the database
            using var cmd = new NpgsqlCommand("SELECT * FROM rehearsal_members\r\n" +
                "WHERE rehearsal_time= @rehearsalTime AND song_title = @songTitle;", conn);
            cmd.Parameters.AddWithValue("rehearsalTime", rehearsalTime);
            cmd.Parameters.AddWithValue("songTitle", songTitle);

            using var reader = cmd.ExecuteReader();

            ObservableCollection<Performer> rehearsalPerformers = new ObservableCollection<Performer>();
            while (reader.Read())
            {
                int userId = reader.GetInt32(0);

                rehearsalPerformers.Add(_performers.First(performer => performer.Id == userId));
            }

            return rehearsalPerformers;
 
        }

        /// <summary>
        /// Uses the given id to find a performer object with that id in a ObservableCollection
        /// </summary>
        /// <param name="id">the id of the performer that the user is trying to find</param>
        /// <returns>the performer if it is in the ObservableCollection. Returns null if the performer is not found</returns>
        
        public ObservableCollection<Performer> GetPerformersOfASong(Song song)
        {
            ObservableCollection<Performer> performersOfASong = new ObservableCollection<Performer>();
            using var conn = new NpgsqlConnection(_connString);
            conn.Open();

            using var cmd = new NpgsqlCommand("SELECT (dreamrolesuser.user_id, first_name, last_name)\r\n" +
                "FROM dreamrolesuser\r\n" +
                "INNER JOIN setlists ON dreamrolesuser.user_id = setlists.user_id\r\n" +
                "WHERE dreamrolesuser.production_year = @year AND song_title = @songTitle;", conn);
            cmd.Parameters.AddWithValue("songTitle", song.Title);
            cmd.Parameters.AddWithValue("year", 2023);

            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                string firstName = reader.GetString(1);
                string lastName = reader.GetString(2);
                string title = reader.GetString(3);

                Performer performer = _performers.FirstOrDefault(performer => performer.Id == id);
                // Create the Performer object and add it to the ObservableCollection
                if (performer != null)
                {
                    performersOfASong.Add(performer);
                }

            }

            return performersOfASong;
        }

        /// <summary>
        /// Adds a new performer object into the database
        /// </summary>
        /// <param name="performer">the performer that the user wants to put into the file</param>
        /// <returns>True if it was inserted into the database, false otherwise</returns>
        public Boolean InsertPerformer(String firstName, String lastName, ObservableCollection<ISongDB> songs, String email, String phoneNumber, int absences)
        {

            try
            {
                // Connect and open a connection to the database
                using var conn = new NpgsqlConnection(_connString);
                conn.Open();

                // Command to insert a new performer into the 'dreamrolesuser' table
                using var cmd = new NpgsqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "INSERT INTO dreamrolesuser (first_name, last_name, title, production_year) " +
                                  "VALUES (@first_name, @last_name, @title, @production_year) " +
                                  "RETURNING user_id;";
                cmd.Parameters.AddWithValue("first_name", firstName);
                cmd.Parameters.AddWithValue("last_name", lastName);
                cmd.Parameters.AddWithValue("title", "Performer");
                cmd.Parameters.AddWithValue("production_year", 2023);
                long id = (long)cmd.ExecuteScalar();

                // Command to insert performer details into the 'performer' table
                cmd.CommandText = "INSERT INTO performer (user_id, email, absences, checked_in_status, phone_number) " +
                                  "VALUES (@user_id, @email, @absences, @checked_in_status, @phone_number);";
                cmd.Parameters.Clear(); // Clear parameters from the previous command
                cmd.Parameters.AddWithValue("user_id", id);
                cmd.Parameters.AddWithValue("phone_number", phoneNumber);
                cmd.Parameters.AddWithValue("email", email);
                cmd.Parameters.AddWithValue("absences", 0);
                cmd.Parameters.AddWithValue("checked_in_status", "not checked in");
                cmd.ExecuteNonQuery();

                //Repopulates the performers
                SelectAllPerformers(2023);

            }
            catch (Npgsql.PostgresException e)
            {
                return false;
            }
            return true;
        }

        public Boolean UpdatePerformerName(int userId, String firstName, String lastName)
        {
            try
            {
                // Connect and open a connection to the database
                using var conn = new NpgsqlConnection(_connString);
                conn.Open();

                // Command to insert a new performer into the 'dreamrolesuser' table
                using var cmd = new NpgsqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "UPDATE dreamrolesuser " +
                                  "SET first_name = @first_name, last_name = @last_name " +
                                  "WHERE user_id = @user_id;";
                cmd.Parameters.AddWithValue("user_id", userId);
                cmd.Parameters.AddWithValue("first_name", firstName);
                cmd.Parameters.AddWithValue("last_name", lastName);
                cmd.ExecuteNonQuery();

                //Repopulates the performers
                SelectAllPerformers(2023);

            }
            catch (Npgsql.PostgresException e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            return true;
        }

        public Boolean UpdatePerformerContact(int userId, String phoneNumber, String email)
        {
            try
            {
                //Connects and opens a connection to the database
                using var conn = new NpgsqlConnection(_connString);
                conn.Open();

                //Commands to grab the performer with the given id and then update them
                var cmd = new NpgsqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "UPDATE performer " +
                                  "SET  phone_number = @phone_number, email = @email " +
                                  "WHERE user_id = @user_id;";
                cmd.Parameters.AddWithValue("user_id", userId);
                cmd.Parameters.AddWithValue("phone_number", phoneNumber);
                cmd.Parameters.AddWithValue("email", email);
                cmd.ExecuteNonQuery();

                //Repopulates performers so now the updated performer is in it
                SelectAllPerformers(2023);
            }
            catch (Npgsql.PostgresException pe)
            {
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
                using var conn = new NpgsqlConnection(_connString);
                conn.Open();

                using var cmd = new NpgsqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "DELETE FROM setlists " +
                                  "WHERE user_id = @user_id";
                cmd.Parameters.AddWithValue("user_id", userId);
                int numDeleted3 = cmd.ExecuteNonQuery();

                //Then, delete from the "performers" table
                cmd.CommandText = "DELETE FROM performer " +
                                  "WHERE user_id = @user_id";
                cmd.Parameters.AddWithValue("user_id", userId);
                int numDeleted2 = cmd.ExecuteNonQuery();

                //First, delete from the "dreamroleuser" table
                cmd.CommandText = "DELETE FROM dreamrolesuser " +
                                  "WHERE user_id = @user_id";
                cmd.Parameters.AddWithValue("user_id", userId);
                int numDeleted1 = cmd.ExecuteNonQuery();



                //Check if any rows were deleted from both tables
                if (numDeleted1 > 0 && numDeleted2 > 0)
                {
                    //SelectAllPerformers() retrieves the updated list of performers
                    SelectAllPerformers(2023);
                }

                return (numDeleted1 > 0 && numDeleted2 > 0);
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
                SelectAllPerformers(2023);
            }
            catch (Npgsql.PostgresException pe)
            {
                return false;
            }
            return true;
        }

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
    }
}
