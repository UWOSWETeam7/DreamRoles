using Prototypes.Model;
using Prototypes.Databases.Interface;
using System.Collections.ObjectModel;
using Npgsql;

namespace Prototypes.Databases
{
    public partial class Database : IDatabase
    {
        //Contains all the checked in performers
        private ObservableCollection<Performer> _checkedInPerformers;
        //Contains all the not checked in performers
        private ObservableCollection<Performer> _notCheckedInPerformers;\

        /// <summary>
        /// This will get all the checked in performers from the database
        /// </summary>
        /// <returns>a ObservableCollection with all the checked in performers. Will return a empty ObservableCollection if it can't get the checked in performers.</returns>
        public ObservableCollection<Performer> GetCheckedInPerformers()
        {
            //Prevents duplicates
            _checkedInPerformers.Clear();
           
            try
            {
                // Connects and opens a connection to the database
                using var conn = new NpgsqlConnection(_connString);
                conn.Open();

                // Commands to get all the checked in performers in the database
                using var cmd = new NpgsqlCommand(
                     "SELECT * FROM performer \r\nWHERE checked_in_status = 'checked in' OR checked_in_status = 'excused';", conn);
                using var reader = cmd.ExecuteReader();

                //While there is still a performer in the reader
                while (reader.Read())
                {
                    //Get the user id
                    int userId = reader.GetInt32(0);

                    //Find the performer from the ObservableCollection _performers
                    Performer performerToCheckIn = _performers.First(performer => performer.Id == userId);

                    if (performerToCheckIn != null)
                    {
                        //Add that performer to the ObservableCollection
                        _checkedInPerformers.Add(performerToCheckIn);
                    }
                   
                }
            }
            catch(Npgsql.PostgresException e)
            {
                //Should be empty
                return _checkedInPerformers
            }

            return _checkedInPerformers;
        }

        /// <summary>
        /// This will get all the not checked in performers from the database
        /// </summary>
        /// <returns>a ObservableCollection with all the not checked in performers. Will return a empty ObservableCollection if it can't get the not checked in performers.</returns>
        public ObservableCollection<Performer> GetNotCheckedInPerformers()
        {
            //Makes sure that there will be no duplicates
            _notCheckedInPerformers.Clear();
            try
            {
                // Connects and opens a connection to the database
                using var conn = new NpgsqlConnection(_connString);
                conn.Open();

                // Commands to get all the checked in performers in the database
                using var cmd = new NpgsqlCommand(
                     "SELECT * FROM performer \r\nWHERE checked_in_status = 'not checked in';", conn);
                using var reader = cmd.ExecuteReader();

                //While there are still performers to read in
                while (reader.Read())
                {
                    //Get the user id from that performer
                    int userId = reader.GetInt32(0);

                    //Get the performer from the ObservableCollection _performers
                    Performer? performerNotChecedIn = _performers.FirstOrDefault(performer => performer.Id == userId);

                    if (performerToCheckIn != null)
                    {
                        //Adds it to the ObservableCollection
                        _notCheckedInPerformers.Add(performerNotChecedIn);
                    }
                }
            }
            catch (Npgsql.PostgresException e)
            {
                //Should be empty
                reutrn _notCheckedInPerformers;
            }

            return _notCheckedInPerformers;
        }

        /// <summary>
        /// Changes a performers status in the database
        /// </summary>
        /// <param name="performer"> the Performer that is getting checked in</param>
        /// <param name="status">the Performer's status</param>
        /// <returns>A true and a string of "success" if it worked. False and a string of "Now rows were affected" or a error message if failed</returns>
        public (bool success, string message) CheckInPerformer(Performer performer, String status)
        {
            try
            {
                //Changing the performer's status in the program
                performer.CheckedInStatus = status;

                // Adds performer to local checked in collection
                _checkedInPerformers.Add(performer);


                // Connects and opens a connection to the database
                using var conn = new NpgsqlConnection(_connString);
                conn.Open();

                DateTime checkedInTime = DateTime.Now;
                // Command to insert a performer into the 'checked_in_performers' table
                using var cmd = new NpgsqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "INSERT INTO checked_in_performers (user_id, time_checked_in, status) " +
                                  "VALUES (@user_id, @time, @status);";
                cmd.Parameters.AddWithValue("user_id", performer.Id);
                cmd.Parameters.AddWithValue("time", checkedInTime);
                cmd.Parameters.AddWithValue("status", status);
                var success = cmd.ExecuteNonQuery();

                // Update performer table 
                UpdatePerformerStatus(performer, status);
                if (success > -1)
                {
                    return (true, "success");
                }

                return (false, "No rows were affected");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }

        }

        /// <summary>
        /// This updates the performer's status in the rehearsal_members table
        /// </summary>
        /// <param name="performer">The performer who's status is getting update</param>
        /// <param name="rehearsal">The rehearsl that the performer is checking into</param>
        /// <param name="isCheckedIn">Boolean if the performer is checking in or not</param>
        /// <returns>A true and a string of "success" if succeful. False and a string of "No rows were affected" or a error message if failed</returns>
        public (bool success, string message) UpdatePerformerRehearsalStatus(Performer performer, Rehearsal rehearsal, bool isCheckedIn)
        {
            try
            {
                //Opens a connection to the database
                using var conn = new NpgsqlConnection(_connString);
                conn.Open();

                //Command to update the rehearsal_members table by seeting the checked_in to a specific user id, time, and song title
                using var cmd = new NpgsqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "UPDATE rehearsal_members\r\n" +
                    "SET checked_in = @isCheckedIn\r\n" +
                    "WHERE user_id = @userId AND rehearsal_time = @time AND song_title = @title;";

                cmd.Parameters.AddWithValue("isCheckedIn", isCheckedIn);
                cmd.Parameters.AddWithValue("userId", performer.Id);
                cmd.Parameters.AddWithValue("time", rehearsal.Time);
                cmd.Parameters.AddWithValue("title", rehearsal.Song.Title);

                var success = cmd.ExecuteNonQuery();

                if (success > -1)
                { 
                    if (isCheckedIn)
                    {
                        //Were checked in
                        performer.CheckedInStatus = "checked in";
                    }
                    else
                    {
                        //Were not checked in
                        performer.CheckedInStatus = "not checked in";
                    }

                    return (true, "success");
                }

                return (false, "No rows were affected");

            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }


        /// <summary>
        /// Updates the checked in status of a performer
        /// </summary>
        /// <param name="performer">The name of the performer to update</param>
        /// <param name="status">The new check in status of the performer</param>
        /// <returns>a tuple with a bool value for if the update succeeded and a success/failure message</returns>
        public (bool success, string message) UpdatePerformerStatus(Performer performer, String status)
        {
            try
            {
                using var conn = new NpgsqlConnection(_connString);
                conn.Open();

                using var cmd = new NpgsqlCommand();
                cmd.Connection = conn;

                //Command to update the rehearsal_members table by setting the status for a specific user id.
                cmd.CommandText = "UPDATE rehearsal_members\r\n" +
                    "SET status = @status\r\n" +
                    "WHERE user_id = @user_id;";
                cmd.Parameters.AddWithValue("user_id", performer.Id);
                cmd.Parameters.AddWithValue("status", status);
                var success = cmd.ExecuteNonQuery();

                // if there were rows affected from the query
                if (success > -1)
                {
                    performer.CheckedInStatus = status;
                    return (true, "success");
                }

                return (false, "No rows were affected");
            }
            catch (Exception ex)
            {
                // return false and send the exception message back if exception caught
                return (false, ex.Message);
            }
        }
    }
}
