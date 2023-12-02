using Prototypes.Model;
using Prototypes.Databases.Interface;
using System.Collections.ObjectModel;
using Npgsql;


namespace Prototypes.Databases
{
    public partial class Database : IDatabase
    {
        private ObservableCollection<Rehearsal> _rehearsals;

        public ObservableCollection<Rehearsal> GetAllRehearsals()
      {
            _rehearsals.Clear();
            using var conn = new NpgsqlConnection(_connString);
            conn.Open();
            using var cmd = new NpgsqlCommand(
                    "SELECT * FROM rehearsals;", conn);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                DateTime time = reader.GetDateTime(0);
                Song song = _songs.FirstOrDefault(song => song.Title.Equals(reader.GetString(1)));
                _rehearsals.Add(new Rehearsal(time, song));
            }

            conn.Close();
            return _rehearsals;
        }
        public ObservableCollection<Rehearsal> GetPerformerRehearsals(Performer performer)
        {
            using var conn = new NpgsqlConnection(_connString);
            conn.Open();
            using var cmd = new NpgsqlCommand(
                    "SELECT *\r\nFROM rehearsal_members\r\nWHERE user_id = @userId;", conn);
            cmd.Parameters.AddWithValue("userId", performer.Id);
            using var reader = cmd.ExecuteReader();

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

                cmd.CommandText = "SELECT user_id " +
                                  "FROM setlists " +
                                  "WHERE song_title = @song_title;";
                cmd.Parameters.AddWithValue("song_title", songTitle);
                using var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    int userId = reader.GetInt32(0);
                    InsertIntoRehersalMembers(userId, rehearsalTime, false, songTitle);
                }
            }
            catch (Npgsql.PostgresException e)
            {
                Console.WriteLine(e.Message);
                return (false, e.Message);
            }

            GetAllRehearsals();
            return (true, "successfully added rehearsal");
        }

        public void InsertIntoRehersalMembers(int userId, DateTime rehearsalTime, bool checkedIn, string songName)
        {
            using var conn = new NpgsqlConnection(_connString);
            conn.Open();
            using var cmd = new NpgsqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "INSERT INTO rehearsal_members(user_id, rehearsal_time, song_title, checked_in) " +
                              "VALUES(@user_id, @rehearsal_time, @song_title, @checked_in);";
            cmd.Parameters.AddWithValue("user_id", userId);
            cmd.Parameters.AddWithValue("rehearsal_time", rehearsalTime);
            cmd.Parameters.AddWithValue("song_title", songName);
            cmd.Parameters.AddWithValue("checked_in", checkedIn);
            cmd.ExecuteNonQuery();
        }

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
