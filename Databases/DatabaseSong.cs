using Prototypes.Model;
using Prototypes.Databases.Interface;
using System.Collections.ObjectModel;
using Npgsql;
using Prototypes.Model.Interfaces;

namespace Prototypes.Databases
{
    public partial class Database : IDatabase
    {
        private ObservableCollection<Song> _songs;
        /// <summary>
        /// Gets all songs from the database
        /// </summary>
        /// <returns>a collection of all the songs</returns>
        public ObservableCollection<Song> SelectAllSongs()
        {
            // Create a new ObservableCollection to store songs
            _songs.Clear();
            // Connects and opens a connection to the database
            using var conn = new NpgsqlConnection(_connString);
            conn.Open();

            // Commands to get all the songs in the database
            using var cmd = new NpgsqlCommand("SELECT *" +
                                              "FROM songs;", conn);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                String songTitle = reader.GetString(0);

                // Create the Performer object and add it to the ObservableCollection
                Song song = new Song(songTitle, null);
                _songs.Add(song);
            }

            return _songs;
        }

        /// <summary>
        /// Gets all songs for a specific performer
        /// </summary>
        /// <param name="user_id"> the user's Id</param>
        /// <returns>a list of the user's songs</returns>
        public ObservableCollection<ISongDB> SelectPerformerSongs(int user_id)
        {
            ObservableCollection<ISongDB> songs = new ObservableCollection<ISongDB>();

            try
            {
                // Using statement for connection to ensure proper resource disposal
                using var conn = new NpgsqlConnection(_connString);
                conn.Open();

                // Command to insert a new performer into the 'dreamrolesuser' table
                using var cmd = new NpgsqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "SELECT song_title, notes " +
                                   "FROM setlists " +
                                   "JOIN dreamrolesuser ON setlists.user_id = dreamrolesuser.user_id " +
                                   "WHERE dreamrolesuser.user_id = @user_id AND dreamrolesuser.production_year = @year;";
                // Add parameters to the query to avoid SQL injection
                cmd.Parameters.AddWithValue("user_id", user_id);
                cmd.Parameters.AddWithValue("year", 2023);

                // Execute the query and retrieve the results
                using var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    // Retrieve the song title from the result set
                    string title = reader.GetString(0);
                    string notes = reader.GetString(0);
                    // Create a new Song object and add it to the collection
                    ISongDB song = new Song(title, notes);
                    songs.Add(song);
                }
            }
            catch (Exception ex)
            {
                // Return null to indicate an error
                return null;
            }
            // Return the collection of songs
            return songs;
        }
        /// <summary>
        /// Adds a song to the database
        /// </summary>
        /// <param name="title">The title of the song</param>
        /// <returns>a Boolean of success or failure</returns>
        public Boolean InsertSong(String title)
        {
            try
            {
                // Connect and open a connection to the database
                using var conn = new NpgsqlConnection(_connString);
                conn.Open();

                // Command to insert a new performer into the 'dreamrolesuser' table
                using var cmd = new NpgsqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "INSERT INTO songs(title)\r\n" +
                    "VALUES (@title);";
                cmd.Parameters.AddWithValue("title", title);
                cmd.ExecuteNonQuery();

                //Repopulates the performers
                SelectAllSongs();

            }
            catch (Npgsql.PostgresException e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            return true;
        }
        /// <summary>
        /// Updates a song
        /// </summary>
        /// <param name="oldSongName">The current song name</param>
        /// <param name="songName">The new song name</param>
        /// <returns></returns>
        public Boolean UpdateSong(String oldSongName, String songName)
        {
            try
            {
                InsertSong(songName);

                using var conn = new NpgsqlConnection(_connString);
                conn.Open();

                using var cmd = new NpgsqlCommand();
                cmd.Connection = conn;

                // Fetch user_id for the old song
                cmd.CommandText = "SELECT user_id " +
                                  "FROM setlists " +
                                  "WHERE song_title = @oldSongTitle;";
                cmd.Parameters.AddWithValue("oldSongTitle", oldSongName);

                using var reader1 = cmd.ExecuteReader();
                while (reader1.Read())
                {
                    int userId = reader1.GetInt32(0);

                    // Insert the new song with the same user_id
                    InsertIntoSetlist(userId, songName);
                }
                reader1.Close();
                cmd.CommandText = "SELECT rehearsal_time " +
                                  "FROM  rehearsals " +
                                  "WHERE song_title = @oldSongTitle;";
                cmd.Parameters.AddWithValue("oldSongTitle", oldSongName);

                using var reader2 = cmd.ExecuteReader();
                while (reader2.Read())
                {
                    DateTime rehearsalTime = reader2.GetDateTime(0);
                    // Insert the new song with the same user_id
                    InsertIntoRehersalPri(rehearsalTime, songName);
                }
                reader2.Close();
                cmd.CommandText = "SELECT * " +
                                 "FROM  rehearsal_members " +
                                 "WHERE song_title = @oldSongTitle;";
                cmd.Parameters.AddWithValue("oldSongTitle", oldSongName);

                using var reader3 = cmd.ExecuteReader();
                while (reader3.Read())
                {
                    int userId = reader3.GetInt32(0);
                    DateTime rehearsalTime = reader3.GetDateTime(1);
                    String checkedIn = reader3.GetString(4);

                    // Insert the new song with the same user_id
                    InsertIntoRehersalMembers(userId, rehearsalTime, checkedIn, songName);
                }
                reader3.Close();
                // Finally, delete the old song from the main song table
                DeleteSong(oldSongName);
            }
            catch (Npgsql.PostgresException e)
            {
                Console.WriteLine(e.Message);
                return false;
            }

            return true;
        }
        /// <summary>
        /// Adds a song that a specified performer will be performing
        /// </summary>
        /// <param name="userId">The Id of the user</param>
        /// <param name="songTitle">The title of the song</param>
        private void InsertIntoSetlist(int userId, String songTitle)
        {
            using var conn = new NpgsqlConnection(_connString);
            conn.Open();
            using var cmd = new NpgsqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "INSERT INTO setlists(user_id, song_title) " +
                              "VALUES(@user_id, @song_title);";
            cmd.Parameters.AddWithValue("song_title", songTitle);
            cmd.Parameters.AddWithValue("user_id", userId);
            cmd.ExecuteNonQuery();
        }
        private void InsertIntoRehersalPri(DateTime rehearsalTime, String songTitle)
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
        }
        /// <summary>
        /// Removes a song from the database
        /// </summary>
        /// <param name="songTitle">The title of the song to remove</param>
        /// <returns></returns>
        public Boolean DeleteSong(String songTitle)
        {
            //Connects and opens a connection to the database
            var conn = new NpgsqlConnection(_connString);
            conn.Open();

            //Commands to delete the performer from the database
            using var cmd = new NpgsqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "DELETE FROM songs " +
                              "WHERE title = @title";
            cmd.Parameters.AddWithValue("title", songTitle);
            int numDeleted = cmd.ExecuteNonQuery();
            //Check that it deleted something
            if (numDeleted > 0)
            {
                SelectAllSongs();
            }
            return numDeleted > 0;
        }
        /// <summary>
        /// Adds a song for a performer with notes
        /// </summary>
        /// <param name="userId">the id of the user</param>
        /// <param name="songName">the name of the song</param>
        /// <param name="notes">stage manager notes for the performer</param>
        /// <returns>a Boolean of success or failure</returns>
        public Boolean InsertSongForPerformer(int userId, String songName, String notes, bool allPerformersAdded = true)
        {
            try
            {
                // Connect and open a connection to the database
                using var conn = new NpgsqlConnection(_connString);
                conn.Open();

                // Command to insert a song into the 'songs' table
                using var cmd = new NpgsqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "INSERT INTO setlists(user_id, song_title, notes)\r\n" +
                    "VALUES(@userId, @songName, @notes);";
                cmd.Parameters.AddWithValue("userId", userId);
                cmd.Parameters.AddWithValue("songName", songName);
                cmd.Parameters.AddWithValue("notes", notes);
                cmd.Prepare(); // prepares the on the server side, making multiple executions faster
                cmd.ExecuteNonQuery();

                if(allPerformersAdded)
                {
                    //Repopulates performers so now the updated performer is in it
                    SelectAllPerformers(2023);
                }
            }
            catch (Npgsql.PostgresException pe)
            {
                return false;
            }
            return true;
        }

       
    }
}
