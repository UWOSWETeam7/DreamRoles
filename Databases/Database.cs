using Prototypes.Model;
using Prototypes.Databases.Interface;
using System.Collections.ObjectModel;
using Npgsql;
using Prototypes.Model.Interfaces;



namespace Prototypes.Databases;

class Database : IDatabase
{
    //A ObservableCollections
    private ObservableCollection<Performer> _performers;
    private ObservableCollection<Performer> _checkedInPerformers;
    private ObservableCollection<Performer> _notCheckedInPerformers;
    private ObservableCollection<Song> _songs;

    //The string to connect to the database
    private String _connString;


    /// <summary>
    /// A Constructor and initializes performers and connString and populates performers
    /// </summary>
    public Database()
    {
        _performers = new ObservableCollection<Performer>();
        _checkedInPerformers = new ObservableCollection<Performer>();
        _notCheckedInPerformers = new ObservableCollection<Performer>();
        _songs = new ObservableCollection<Song>();
        _connString = GetConnectionString();
        SelectAllPerformers(2023);
        SelectAllSongs();
        GetCheckedInPerformers();
        GetNotCheckedInPerformers();
    }

    public ObservableCollection<Performer> GetPerformersOfASong(Song song)
    {
        ObservableCollection<Performer> performersOfASong = new ObservableCollection<Performer>();
        using var conn = new NpgsqlConnection(_connString);
        conn.Open();

        using var cmd = new NpgsqlCommand("SELECT dru.first_name, dru.last_name, dru.title " +
                                  "FROM dreamrolesuser dru " +
                                  "INNER JOIN setlists sl " +
                                  "ON dru.user_id = sl.user_id " +
                                  "INNER JOIN songs s " +
                                  "ON sl.setlist_id = s.setlist_id " +
                                  "WHERE s.title = @songTitle;", conn);
        cmd.Parameters.AddWithValue("songTitle", song.Title);

        using var reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            string firstName = reader["first_name"] as string;
            string lastName = reader["last_name"] as string;
            string title = reader["title"] as string;

            Performer performer = new Performer(0, firstName, lastName, null, "", "", 0);
            // Create the Performer object and add it to the ObservableCollection
            performersOfASong.Add(performer);
        }

        return performersOfASong;
    }

    /// <summary>
    /// The string needed to connect to the database
    /// </summary>
    /// <returns>A String that has the infomration to connect to the database</returns>
    /// 
    void GetNotCheckedInPerformers(){
        _notCheckedInPerformers = new ObservableCollection<Performer>(_performers);

        foreach (Performer performer in _checkedInPerformers)
        {
            _notCheckedInPerformers.Remove(performer);
        }
    }
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
        IConfiguration config = new ConfigurationBuilder().AddUserSecrets<Database>().Build();
        return config["CockroachDBPassword"] ?? "6tRK2gvZOx62cwwPBe8znA"; // this works in VS, not VSC
    }
    */

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
                int setListId = reader.GetInt32(0);
                String songTitle = reader.GetString(1);
                String artist = reader.GetString(2);
                int duration = reader.IsDBNull(3) ? 0 : reader.GetInt16(3);

                // Create the Performer object and add it to the ObservableCollection
                Song song = new Song(setListId, songTitle, artist, duration);
                _songs.Add(song);
            }
       
        return _songs;
    }

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
                String phoneNumber = reader.IsDBNull(1) ? "" : reader.GetInt64(1) + "";
                String email = reader.IsDBNull(2) ? "" : reader.GetString(2);
                int absences = reader.IsDBNull(3) ? 0 : reader.GetInt32(3);
                String firstName = reader.GetString(4);
                String lastName = reader.GetString(5);
                ObservableCollection<ISongDB> setList = new();

                // Create the Performer object and add it to the ObservableCollection
                Performer performerToAdd = new Performer(userId, firstName, lastName, setList, email, phoneNumber, absences);
                _performers.Add(performerToAdd);
            }

            foreach (var performer in _performers)
            {
                performer.Songs = SelectPerformerSongs(performer.Id);
            }
        
        return _performers;
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

    public Boolean UpdateSong(int setlistId, String oldSongName, String oldArtist, String songName, String artist, int duration)
    {
        DeleteSong(oldSongName, oldArtist);
        return InsertSong(setlistId, songName, artist, duration);
       
    }

    public Boolean InsertSong(int setlistId, String title, String artist, int duration)
    {
        try
        {
            // Connect and open a connection to the database
            using var conn = new NpgsqlConnection(_connString);
            conn.Open();

            // Command to insert a new performer into the 'dreamrolesuser' table
            using var cmd = new NpgsqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "INSERT INTO songs(setlist_id, title, artist, duration)" +
                              "VALUES(@setlistId, @title, @artist, @duration);";
            cmd.Parameters.AddWithValue("setlistId", setlistId);
            cmd.Parameters.AddWithValue("title", title);
            cmd.Parameters.AddWithValue("artist", artist);
            cmd.Parameters.AddWithValue("duration", duration);
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
    public Boolean DeleteSong(String songTitle, String artistName)
    {
        //Connects and opens a connection to the database
        var conn = new NpgsqlConnection(_connString);
        conn.Open();

        //Commands to delete the performer from the database
        using var cmd = new NpgsqlCommand();
        cmd.Connection = conn;
        cmd.CommandText = "DELETE FROM songs " +
                          "WHERE title = @title AND artist = @artist;";
        cmd.Parameters.AddWithValue("title", songTitle);
        cmd.Parameters.AddWithValue("artist", artistName);
        int numDeleted = cmd.ExecuteNonQuery();
        //Check that it deleted something
        if (numDeleted > 0)
        {
            SelectAllSongs();
        }
        return numDeleted > 0;
    }

    public Boolean InsertSongForPerformer(int userId, String songName, String artistName, int duration)
    {
        try
        {
            InsertSongForSetlist(userId);
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
            //SelectAllPerformers();

        }
        catch (Npgsql.PostgresException pe)
        {
            return false;
        }
        return true;
    }

    public Boolean InsertSongForSetlist(int userId)
    {
        try
        {
            // Connect and open a connection to the database
            using var conn = new NpgsqlConnection(_connString);
            conn.Open();

            // Command to insert a song into the 'songs' table
            using var cmd = new NpgsqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "INSERT INTO setlists (setlist_id, user_id) " +
                              "VALUES (@setlist_id, @user_id);";
            cmd.Parameters.AddWithValue("setlist_id", userId);
            cmd.Parameters.AddWithValue("user_id", userId);
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
    /// Gets all songs for a specific performer
    /// </summary>
    /// <param name="user_id"> the user's Id</param>
    /// <returns>a list of the user's songs</returns>
    public ObservableCollection<ISongDB> SelectPerformerSongs(int user_id)
    {
        // Create a new ObservableCollection to store songs
        ObservableCollection<ISongDB> songs = new();

        // Connects and opens a connection to the database
        using var conn = new NpgsqlConnection(_connString);
        conn.Open();

        // Commands to get all the songs from a performer in the database
        using var cmd = new NpgsqlCommand(
             "SELECT setlists.setlist_id, title, artist, duration\r\n" +
             "FROM setlists\r\nINNER JOIN songs\r\n" +
             "ON setlists.setlist_id = songs.setlist_id\r\n" +
             "WHERE user_id = @user_id;", conn);
        cmd.Parameters.AddWithValue("user_id", user_id);
        using var reader = cmd.ExecuteReader();

        // Make a new song object for every song belonging to the performer
        while (reader.Read())
        {
            int setlistId = reader.GetInt32(0);
            String title = reader.GetString(1);
            String artist = reader.GetString(2);
            int duration = reader.IsDBNull(3) ? 0 : reader.GetInt32(3);

            ISongDB song = new Song(setlistId, title, artist, duration);

            songs.Add(song);
        }

        return songs;
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
            if (numDeleted1 > 0 && numDeleted2 > 0 && numDeleted3 > 0)
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

    public ObservableCollection<Performer> NotCheckedInPerformers()
    {

        // Create a new ObservableCollection to store not checked in performers
        ObservableCollection<Performer> performers = new ObservableCollection<Performer>();

        // Connects and opens a connection to the database
        using var conn = new NpgsqlConnection(_connString);
        conn.Open();

        // Commands to get all the not checked in performers in the database
        using var cmd = new NpgsqlCommand(
             "SELECT *\r\nFROM performer\r\nLEFT JOIN dreamrolesuser\r\nON performer.user_id = dreamrolesuser.user_id\r\nWHERE NOT EXISTS\r\n(SELECT *\r\nFROM checked_in_performers\r\nWHERE checked_in_performers.user_id = performer.user_id);", conn);
        using var reader = cmd.ExecuteReader();

        // Create a Performer object for each row returned from query
        while (reader.Read())
        {
            int userId = reader.GetInt16(0);
            String phoneNumber = reader.IsDBNull(1) ? "" : reader.GetInt64(1) + "";
            String email = reader.IsDBNull(2) ? "" : reader.GetString(2);
            int absences = reader.IsDBNull(3) ? 0 : reader.GetInt32(3);
            int userId_2 = reader.GetInt16(4);
            String firstName = reader.GetString(5);
            String lastName = reader.GetString(6);
            String title = reader.GetString(7);
            ObservableCollection<ISongDB> setList = new();

            // Create the Performer object and add it to the ObservableCollection
            Performer performerToAdd = new Performer(userId, firstName, lastName, setList, email, phoneNumber, absences);
            performers.Add(performerToAdd);
        }

        foreach (var performer in performers)
        {
            performer.Songs = SelectPerformerSongs(performer.Id);
        }

        return performers;
    }
    public ObservableCollection<Performer> GetCheckedInPerformers()
    {
        
            _checkedInPerformers.Clear();
            // Connects and opens a connection to the database
            using var conn = new NpgsqlConnection(_connString);
            conn.Open();

            // Commands to get all the checked in performers in the database
            using var cmd = new NpgsqlCommand(
                 "SELECT * FROM checked_in_performers;", conn);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                int userId = reader.GetInt32(0);

                _checkedInPerformers.Add(_performers.First(performer => performer.Id == userId));
            }


        return _checkedInPerformers;
    }

    public (bool success, string message) CheckInPerformer(Performer performer, String status)
    {
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

        // Adds performer to local chekced in collection
        _checkedInPerformers.Add((performer));

        if (success > -1)
        {
            return (true, "success");
        }

        return (false, "No rows were affected");

    }

}

