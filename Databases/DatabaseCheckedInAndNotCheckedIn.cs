using Prototypes.Model;
using Prototypes.Databases.Interface;
using System.Collections.ObjectModel;
using Npgsql;

namespace Prototypes.Databases
{
    public partial class Database : IDatabase
    {
        private ObservableCollection<Performer> _checkedInPerformers;
        private ObservableCollection<Performer> _notCheckedInPerformers;
        public ObservableCollection<Performer> GetCheckedInPerformers()
        {

            _checkedInPerformers.Clear();
            // Connects and opens a connection to the database
            using var conn = new NpgsqlConnection(_connString);
            conn.Open();

            // Commands to get all the checked in performers in the database
            using var cmd = new NpgsqlCommand(
                 "SELECT * FROM performer \r\nWHERE checked_in_status = 'checked in' OR checked_in_status = 'excused';", conn);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                int userId = reader.GetInt32(0);

                Performer performerToCheckIn = _performers.First(performer => performer.Id == userId);
                _checkedInPerformers.Add(performerToCheckIn);
            }


            return _checkedInPerformers;
        }

        /// <summary>
        /// The string needed to connect to the database
        /// </summary>
        /// <returns>A String that has the infomration to connect to the database</returns>
        /// 
        public ObservableCollection<Performer> GetNotCheckedInPerformers()
        {
            _notCheckedInPerformers.Clear();
            // Connects and opens a connection to the database
            using var conn = new NpgsqlConnection(_connString);
            conn.Open();

            // Commands to get all the checked in performers in the database
            using var cmd = new NpgsqlCommand(
                 "SELECT * FROM performer \r\nWHERE checked_in_status = 'not checked in';", conn);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                int userId = reader.GetInt32(0);

                Performer? performerToCheckIn = _performers.FirstOrDefault(performer => performer.Id == userId);
                if (performerToCheckIn != null)
                {
                    _notCheckedInPerformers.Add(performerToCheckIn);
                }
            }


            return _notCheckedInPerformers;
        }

        public (bool success, string message) CheckInPerformer(Performer performer, String status)
        {
            try
            {

                performer.CheckedInStatus = status;

                // Adds performer to local chekced in collection
                _checkedInPerformers.Add(performer);


                // Connects and opens a connection to the database
                using var conn = new NpgsqlConnection(_connString);
                conn.Open();

                DateTime checkedInTime = DateTime.Now;
                // Command to insert a performer into the 'ched_in_performers' table
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

        public (bool success, string message) UpdatePerformerRehearsalStatus(Performer performer, Rehearsal rehearsal, bool isCheckedIn)
        {
            try
            {
                using var conn = new NpgsqlConnection(_connString);
                conn.Open();

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
                        performer.CheckedInStatus = "checked in";
                    }
                    else
                    {
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
        public (bool success, string message) UpdatePerformerStatus(Performer performer, String status)
        {
            try
            {
                using var conn = new NpgsqlConnection(_connString);
                conn.Open();

                using var cmd = new NpgsqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "UPDATE performer\r\n" +
                    "SET checked_in_status = @status\r\n" +
                    "WHERE user_id = @user_id;";
                cmd.Parameters.AddWithValue("user_id", performer.Id);
                cmd.Parameters.AddWithValue("status", status);
                var success = cmd.ExecuteNonQuery();

                if (success > -1)
                {
                    performer.CheckedInStatus = status;
                    return (true, "success");
                }

                return (false, "No rows were affected");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }
    }
}
