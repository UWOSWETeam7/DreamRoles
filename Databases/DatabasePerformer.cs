using Prototypes.Model;
using Prototypes.Databases.Interface;
using System.Collections.ObjectModel;
using Npgsql;
using Prototypes.Model.Interfaces;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;
using System.Security.Policy;


namespace Prototypes.Databases
{
    public partial class Database : IDatabase
    {
        /// <summary>
        /// Gets the setlist of a performer from the database
        /// </summary>
        /// <param name="performerId">the id of the performer we want the setlist of</param>
        /// <returns>A ObservableCollection of songs. Will be empty if it failed or no data is found</returns>
        public ObservableCollection<Song> GetPerformerSetList(int performerId)
        {
            //Creates a new ObservableCollection of songs
            ObservableCollection<Song> performerSetlist = new ObservableCollection<Song>();
            try
            {
                //Opens a connection to the database
                using var conn = new NpgsqlConnection(_connString);
                conn.Open();

                // Commands to get all songs that are linked to a specific user id
                using var cmd = new NpgsqlCommand("SELECT song_title, notes " +
                                                  "FROM setlists " +
                                                  "WHERE user_id = @userId;", conn);
                cmd.Parameters.AddWithValue("userId", performerId);
                using var reader = cmd.ExecuteReader();

                //While there are more songs to read in
                while (reader.Read())
                {
                    //Getting the information from the table
                    String title = reader.GetString(0);
                    String note = reader.GetString(1);
    
                //Creating a new song and adding it the ObservableCollection
                Song song = new Song(title, note);
                    performerSetlist.Add(song);
                }
            }
            catch (Npgsql.PostgresException e)
            {
                //Would be empty
                return performerSetlist;
            }
            return performerSetlist;
        }

        /**public (bool success, String message) DeleteSongFromSetlist(int performerId, string songName)
        {
            //Opens a connection to the database
            using var conn = new NpgsqlConnection(_connString);
            conn.Open();
            using var cmd = new NpgsqlCommand();
            cmd.Connection = conn;

            cmd.CommandText = "DELETE FROM setlists\r\n" +
              "WHERE user_id = @performerId AND song_title = @songName;";
            cmd.Parameters.AddWithValue("perfromerId", performerId);
            cmd.Parameters.AddWithValue("songName", songName);
          
            var result = cmd.ExecuteNonQuery();

            if (result < 0)
            {
                return (false, "Failed to remove song from performer's setlist");
            }
            else
            {
                return (true, "Successfully removed song from performer's setlist");
            }



        }*/
        
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
            try
            {
                using var conn = new NpgsqlConnection(_connString);
                conn.Open();

                // Commands to get all the performers in the database
                using var cmd = new NpgsqlCommand($"SELECT *\r\n" +
                                                  $"FROM performer\r\n" +
                                                  $"INNER JOIN dreamrolesuser\r\n" +
                                                  $"USING (user_id) " +
                                                  $"WHERE dreamrolesuser.production_year = {year};", conn);
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

                //Get all the songs for each performer
                foreach (var performer in _performers)
                {
                    performer.Songs = SelectPerformerSongs(performer.Id);
                }
            }
            catch(Npgsql.PostgresException e)
            {
                //Would be empty
                return _performers;
            }

            return _performers;
        }

        /// <summary>
        /// Will get all the performers for a specific rehearsal which is defined by a time and song
        /// </summary>
        /// <param name="rehearsalTime">The data and time of the rehearsal</param>
        /// <param name="songTitle">What song is being rehearsed</param>
        /// <returns>A ObservableCollection of perforers</returns>
        public ObservableCollection<Performer> SelectAllPerfomersFromRehearsal(DateTime rehearsalTime, String songTitle)
        {
            //Creates a new ObservableCollection of Performer object
            ObservableCollection<Performer> rehearsalPerformers = new ObservableCollection<Performer>();

            try
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

                //While there are still more performers to read in
                while (reader.Read())
                {
                    int userId = reader.GetInt32(0);
                    String status = reader.GetString(3);

                    //Find the performer
                    Performer performer = _performers.First(performer => performer.Id == userId);
                    //Change the status of the checked in status
                    performer.CheckedInStatus = status;
                    //Add Performer to ObservableCollection
                    rehearsalPerformers.Add(performer);
                }
            }
            catch (Npgsql.PostgresException e)
            {
                //Should be empty
                return rehearsalPerformers;
            }
            return rehearsalPerformers;
        }

        /// <summary>
        /// Get all the performers that have this specific song in their setlist
        /// </summary>
        /// <param name="song">The song whe want all the performers for</param>
        /// <returns>A ObservableCollection of performer objects</returns>
        public ObservableCollection<Performer> GetPerformersOfASong(Song song)
        {
            //A new ObservableCollection of performers
            ObservableCollection<Performer> performersOfASong = new ObservableCollection<Performer>();

            try
            {
                //Opens a connection to the database
                using var conn = new NpgsqlConnection(_connString);
                conn.Open();

                //Command to get everything from the dreamroleuser table finds all users where their id is also in the setlist with the specified song
                using var cmd = new NpgsqlCommand("SELECT *\r\n" +
                    "FROM dreamrolesuser\r\n" +
                    "INNER JOIN setlists ON dreamrolesuser.user_id = setlists.user_id\r\n" +
                    "WHERE dreamrolesuser.production_year = @year AND song_title = @songTitle;", conn);
                cmd.Parameters.AddWithValue("songTitle", song.Title);
                cmd.Parameters.AddWithValue("year", 2023);

                using var reader = cmd.ExecuteReader();

                //While there is still more users to read in
                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string firstName = reader.GetString(1);
                    string lastName = reader.GetString(2);
                    string title = reader.GetString(3);
                    
                    //find the performer based on thier id
                    Performer performer = _performers.FirstOrDefault(performer => performer.Id == id);
                    
                    if (performer != null)
                    {
                        // Create the Performer object and add it to the ObservableCollection
                        performersOfASong.Add(performer);
                    }
                }
            }
            catch (Npgsql.PostgresException e)
            {
                return performersOfASong;
            }

            return performersOfASong;
        }

        /// <summary>
        /// Will insert a new performer into the database
        /// </summary>
        /// <param name="firstName">The first name of the performer</param>
        /// <param name="lastName">The last name of the performer</param>
        /// <param name="songs">The songs the performer is in</param>
        /// <param name="email">The email of the performer</param>
        /// <param name="phoneNumber">The phone number of the performer</param>
        /// <param name="absences">The number of rehearsal the performer has missed</param>
        /// <returns>True and the user id if a performer was added. False and a 0 if failed</returns>
        public (bool Success, int UserId) InsertPerformer(string firstName, string lastName, ObservableCollection<ISongDB> songs, string email, string phoneNumber, int absences, bool allPerformersAdded = true)
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

                cmd.Prepare(); // prepares the on the server side, making multiple executions faster
                cmd.ExecuteNonQuery();

                if(allPerformersAdded)
                {
                    // Repopulates the performers
                    SelectAllPerformers(2023);

                }
                // Return a tuple with success status and user ID
                return (true, (int)id);
            }
            catch (Npgsql.PostgresException e)
            {
                // Return a tuple indicating failure with a default user ID of 0
                return (false, 0);
            }
        }

        /// <summary>
        /// Updates the performer's first and last name in the database
        /// </summary>
        /// <param name="userId">The performer's id</param>
        /// <param name="firstName"> the first name of the performer</param>
        /// <param name="lastName">The last name of the performer</param>
        /// <returns>True if it updated. False if it failed to update</returns>
        public Boolean UpdatePerformerName(int userId, String firstName, String lastName)
        {
            try
            {
                // Connect and open a connection to the database
                using var conn = new NpgsqlConnection(_connString);
                conn.Open();

                // Command to update the first and last name of the performer in the 'dreamrolesuser' table
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

        /// <summary>
        /// Update the performer's contact information in the databade
        /// </summary>
        /// <param name="userId">The performer's id</param>
        /// <param name="phoneNumber">The phonenumber of the performer</param>
        /// <param name="email">The email of the performer</param>
        /// <returns>Returns true if it could be updated. False if it failed to update</returns>
        public Boolean UpdatePerformerContact(int userId, String phoneNumber, String email)
        {
            try
            {
                //Connects and opens a connection to the database
                using var conn = new NpgsqlConnection(_connString);
                conn.Open();

                //Commands to grab the performer with the given id and then update their contact information
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
                try
                {
                    using var conn = new NpgsqlConnection(_connString);
                    conn.Open();

                    //Command to delete performer with this id from the dreamrolesuser which will remove it from all the other tables where this performer is mentioned
                    using var cmd = new NpgsqlCommand();
                    cmd.CommandText = "DELETE FROM dreamrolesuser " +
                                      "WHERE user_id = @user_id";
                    cmd.Parameters.AddWithValue("user_id", userId);
                    int numDeleted = cmd.ExecuteNonQuery();



                    //Check if any rows were deleted from both tables
                    if (numDeleted > 0)
                    {
                        //SelectAllPerformers() retrieves the updated list of performers
                        SelectAllPerformers(2023);
                    }

                    return (numDeleted > 0);
                }
                catch (Npgsql.PostgresException e)
                {
                    return false;
                }
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
        /// <summary>
        /// Gets a performer by userId
        /// </summary>
        /// <param name="userId">The user Id to search by</param>
        /// <returns>A Performer object with matching Id or null if not found</returns>
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
        /// Gets all rehearsals that do not have a checked in status for a performer
        /// </summary>
        /// <param name="userId">The Id of the performer to search for</param>
        /// <returns>a collection of rehearsals that do not have a checked in status for a specified performer</returns>
        public ObservableCollection<Rehearsal> SelectPerformerAbsences(int userId)
        {
            ObservableCollection<Rehearsal> missedRehearsals = new ObservableCollection<Rehearsal>();
            using var conn = new NpgsqlConnection(_connString);
            conn.Open();
            // Get all rehearsals prior to the current time that do not have a status of checked in for a performer
            using var cmd = new NpgsqlCommand("SELECT * FROM rehearsal_members\r\n" +
                "WHERE user_id = @userId AND status != 'checked in' AND rehearsal_time < CURRENT_TIMESTAMP;", conn);

            cmd.Parameters.AddWithValue("userId", userId);
            try
            {
                using var reader = cmd.ExecuteReader();
                // for all query results, searches the collection of all rehearsals that have a matching song title and time
                while (reader.Read())
                {
                    DateTime time = reader.GetDateTime(1);
                    String songTitle = reader.GetString(2);

                    Rehearsal missedRehearsal = _rehearsals.First(rehearsal => rehearsal.Song.Title == songTitle && rehearsal.Time == time);
                    missedRehearsals.Add(missedRehearsal);
                }

                return missedRehearsals;
            } catch (Exception ex)
            {
                throw ex; 
            }
        }
    }
}
