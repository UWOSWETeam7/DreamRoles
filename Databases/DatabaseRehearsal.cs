using Prototypes.Model;
using Prototypes.Databases.Interface;
using System.Collections.ObjectModel;
using Npgsql;


namespace Prototypes.Databases
{
    public partial class Database : IDatabase
    {
        private ObservableCollection<Rehearsal> _rehearsals;
        /// <summary>
        /// Gets all rehearsals from the database
        /// </summary>
        /// <returns>a collection of all rehearsals</returns>
        public ObservableCollection<Rehearsal> GetAllRehearsals()
        {
            //Make sure it doesn't have duplicates
            _rehearsals.Clear();

            //Opens a connection to the database
            using var conn = new NpgsqlConnection(_connString);
            conn.Open();

            //Select everything from the rehearsals table
            using var cmd = new NpgsqlCommand(
                    "SELECT * FROM rehearsals;", conn);
            using var reader = cmd.ExecuteReader();

            // constructs a rehearsal with a time and song for each row returned
            while (reader.Read())
            {
                DateTime time = reader.GetDateTime(0);
                Song song = _songs.FirstOrDefault(song => song.Title.Equals(reader.GetString(1))); // finds song with matching title from songs collection or null if not found
                _rehearsals.Add(new Rehearsal(time, song));
            }

            return _rehearsals; 
        }

        /// <summary>
        /// Get all rehearsals that the specified performer is a part of
        /// </summary>
        /// <param name="performer">The performer to get the rehearsals for</param>
        /// <returns>a collection of the performer's rehearsals</returns>
        public ObservableCollection<Rehearsal> GetPerformerRehearsals(Performer performer)
        {
            using var conn = new NpgsqlConnection(_connString);
            conn.Open();

            using var cmd = new NpgsqlCommand("SELECT *\r\n" +
                                              "FROM rehearsal_members\r\n" +
                                              "WHERE user_id = @userId;", conn);
            cmd.Parameters.AddWithValue("userId", performer.Id);
            using var reader = cmd.ExecuteReader();

            // make new collection of rehearsals to return
            ObservableCollection<Rehearsal> performerRehearsals = new ObservableCollection<Rehearsal>();

            while (reader.Read())
            {
                var time = reader.GetDateTime(1);
                Song song = _songs.FirstOrDefault(song => song.Title == reader.GetString(2));
                Rehearsal rehearsal = new Rehearsal(time, song);
                //Rehearsal rehearsal = _rehearsals.FirstOrDefault(rehearsal => rehearsal.Time.Equals(time) && rehearsal.Song.Equals(song));

                performerRehearsals.Add(rehearsal);
            }
            return performerRehearsals;
        }
        /// <summary>
        /// Adds a new rehearsal to the database
        /// </summary>
        /// <param name="rehearsalTime">The date and time of the rehearsal</param>
        /// <param name="songTitle">the title of the song that will be performed</param>
        /// <returns>a tuple with a bool for success or failure and a success/failure message</returns>
        public (bool success, string message) InsertIntoRehearsals(DateTime rehearsalTime, String songTitle)
        {
            try
            {
                using var conn = new NpgsqlConnection(_connString);
                conn.Open();
                using var cmd = new NpgsqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "INSERT INTO rehearsals(rehearsal_time, song_title) " +
                                  "VALUES(@rehearsal_time, @song_title);";
                cmd.Parameters.AddWithValue("song_title", songTitle);
                cmd.Parameters.AddWithValue("rehearsal_time", rehearsalTime);
                cmd.ExecuteNonQuery();

                // Gets a list of users that perform the song being rehearsed
                cmd.CommandText = "SELECT user_id " +
                                  "FROM setlists " +
                                  "WHERE song_title = @song_title;";
                cmd.Parameters.AddWithValue("song_title", songTitle);
                using var reader = cmd.ExecuteReader();

                // Adds the rehearsal into the database for each performer that performs the song
                while (reader.Read())
                {
                    int userId = reader.GetInt32(0);
                    InsertIntoRehersalMembers(userId, rehearsalTime, "not checked in", songTitle);
                }
            }
            // returns false and the exception error message if exception is thrown
            catch (Npgsql.PostgresException e)
            {
                Console.WriteLine(e.Message);
                return (false, e.Message);
            }
            // refresh the rehearsals list and return true and success message
            GetAllRehearsals();
            return (true, "successfully added rehearsal");
        }
        /// <summary>
        /// Adds a rehearsal in a performer's list of rehearsals
        /// </summary>
        /// <param name="userId">The Id of the performer to add the rehearsal for</param>
        /// <param name="rehearsalTime">The time of the rehearsal</param>
        /// <param name="checkedIn">The check in status of the performer for the rehearsal</param>
        /// <param name="songName">The name of the song being rehearsed</param>
        /// <returns>A tuple with a bool for success/failure and a success/failure message</returns>
        public (bool success, String message) InsertIntoRehersalMembers(int userId, DateTime rehearsalTime, String checkedIn, string songName)
        {
            using var conn = new NpgsqlConnection(_connString);
            conn.Open();
            using var cmd = new NpgsqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "INSERT INTO rehearsal_members(user_id, rehearsal_time, song_title, status) " +
                              "VALUES(@user_id, @rehearsal_time, @song_title, @checked_in);";
            cmd.Parameters.AddWithValue("user_id", userId);
            cmd.Parameters.AddWithValue("rehearsal_time", rehearsalTime);
            cmd.Parameters.AddWithValue("song_title", songName);
            cmd.Parameters.AddWithValue("checked_in", checkedIn);
            var result = cmd.ExecuteNonQuery();

            // if no rows were affected, return false and error message
            if(result < 0)
            {
                return (false, "Failed to insert performer into rehearsal");
            }
            // return true and success message
            return (true, "Successfully added performer into rehearsal");
        }

        /// <summary>
        /// Removes a rehearsal for a performer
        /// </summary>
        /// <param name="userId">The id of the performer to delete the rehearsal for</param>
        /// <param name="rehearsalTime">The time of the rehearsal</param>
        /// <param name="songName">The title of the song being performed</param>
        /// <returns></returns>
        public (bool success, String message) DeleteRehearsalMember(int userId, DateTime rehearsalTime, String songName)
        {
            using var conn = new NpgsqlConnection(_connString);
            conn.Open();
            using var cmd = new NpgsqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "DELETE FROM rehearsal_members\r\n" +
                "WHERE rehearsal_time = @rehearsalTime AND song_title = @songName AND user_id = @userId;";
            cmd.Parameters.AddWithValue("rehearsalTime", rehearsalTime);
            cmd.Parameters.AddWithValue("songName", songName);
            cmd.Parameters.AddWithValue("userId", userId);
            var result = cmd.ExecuteNonQuery();

            if (result < 0)
            {
                return (false, "Failed to remove performer from rehearsal");
            }
            else
            {
                return (true, "Successfully removed performer from rehearsal");
            }
        }

        /// <summary>
        /// Gets the date and time of a rehearsal
        /// </summary>
        /// <returns>the rehearsal time</returns>
        public DateTime GetRehearsalDateTime()
        {
            using var conn = new NpgsqlConnection(_connString);
            conn.Open();
            using var cmd = new NpgsqlCommand(
                    "SELECT performance_time FROM performance;", conn);
            using var reader = cmd.ExecuteReader();
            DateTime rehearsalTime = reader.GetDateTime(0);
            return rehearsalTime;
        }

        /// <summary>
        /// Removes a rehearsal
        /// </summary>
        /// <param name="time">The time of the rehearsal</param>
        /// <param name="songTitle">The song title of the rehearsal</param>
        /// <returns>a tuple with a bool for success/failure and a success/failure message</returns>
        public (bool success, string message) DeleteRehearsal(DateTime time, String songTitle)
        {
            using var conn = new NpgsqlConnection(_connString);
            conn.Open();
            using var cmd = new NpgsqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "DELETE FROM rehearsals\r\n" +
                "WHERE rehearsal_time = @time AND song_title = @songTitle;";
            cmd.Parameters.AddWithValue("time", time);
            cmd.Parameters.AddWithValue("songTitle", songTitle);
            var result = cmd.ExecuteNonQuery();

            if (result < 0)
            {
                return (false, "No rehearsals were deleted");
            }

            // refresh the current list of rehearsals
            GetAllRehearsals();
            return (true, "rehearsal deleted");
        }
    }
}
