using Prototypes.Model;
using Prototypes.Databases.Interface;
using System.Collections.ObjectModel;
using Npgsql;
using Prototypes.Model.Interfaces;
using System.Data;
namespace Prototypes.Databases;

public partial class Database : IDatabase
{

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
        _rehearsals = new ObservableCollection<Rehearsal>();
        _connString = GetConnectionString();
        SelectAllPerformers(2023);
        SelectAllSongs();
        GetCheckedInPerformers();
        GetNotCheckedInPerformers();
        GetAllRehearsals();
    }


    /// <summary>
    /// This will create a connection string to connect to the database
    /// </summary>
    /// <returns>A  String that will connect to the database</returns>
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
    
    /// <summary>
    /// This will change the Performer's access code in the database to a random 7 digit code.
    /// </summary>
    /// <param name="newAccessCode">It is the newly randomized 7 digit code</param>
    /// <returns>A Boolean true if it could be added to the database and false it could not be.</returns>
    public Boolean UpdatePerformerAccessCode(int newAccessCode)
    {
        try
        {
            //Connects and opens a connection to the database
            using var conn = new NpgsqlConnection(_connString);
            conn.Open();

            //Command to update the production table with the new access code
            var cmd = new NpgsqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "UPDATE production " +
                              "SET performer_access_code = @performer_access_code " +
                              "WHERE production_year = 2023;";
            cmd.Parameters.AddWithValue("performer_access_code", newAccessCode);
            cmd.ExecuteNonQuery();

        }
        catch (Npgsql.PostgresException pe)
        {
            return false;
        }
        return true;

    }

    /// <summary>
    /// Get the Performer's access code from the database
    /// </summary>
    /// <returns>A String that is 7 characters long that contains the access code. Will return null if it could not get the Performer's access code</returns>
    public String GetPerformerAccessCode()
    {
        String performerAccessCode = null;
        try
        {
            //Connect and open a connection to the database
            using var conn = new NpgsqlConnection(_connString);
            conn.Open();
    
            //Command to grab the performer_access_code from the production table
            var cmd = new NpgsqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "SELECT performer_access_code " +
                              "FROM public.production " +
                              "WHERE production_year = 2023;";

            // Execute the query and read the result
            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                //Getting the access code from the reader.
                 performerAccessCode = reader["performer_access_code"].ToString();
            }
        }
        catch (Npgsql.PostgresException pe)
        {  
            return null;
        }

        return performerAccessCode;
    }

    /// <summary>
    /// Gets the Stage Manger access code from the database
    /// </summary>
    /// <returns>A String that is 7 characters long that contains the access code. Will return null if it could not get the Stage Manager's access code</returns>
    public String GetManagerAccessCode()
    {
        String managerAccessCode = null;
        try
        {
            //Connect and open a connection to the database
            using var conn = new NpgsqlConnection(_connString);
            conn.Open();

            //Command to grab the stagemanager_accesss_code from the production table
            var cmd = new NpgsqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "SELECT stagemanager_access_code " +
                              "FROM public.production " +
                              "WHERE production_year=2023;";

            //Execute the query and read the result
            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                //Get the Stage Manger's access code from the reader
                managerAccessCode = reader["stagemanager_access_code"].ToString();
            }
        }
        catch (Npgsql.PostgresException pe)
        {
            return null;
        }

        return managerAccessCode;
    }

    /// <summary>
    /// Gets the Choreographer's access code from the database
    /// </summary>
    /// <returns>A String that is 7 character's long with the access code. Will return null if it can't get the Choreographer's access code</returns>
    public String GetChoreoAccessCode()
    {
        String choreoAccessCode = null;
        try
        {
            //Connects and opens a connection to the database
            using var conn = new NpgsqlConnection(_connString);
            conn.Open();

            //Command to grab the choreographer_access_code from the production table 
            var cmd = new NpgsqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "SELECT choreographer_access_code " +
                              "FROM public.production " +
                              "WHERE production_year=2023;";

            // Execute the query and read the result
            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                //Get the access code from the reader
                choreoAccessCode = reader["choreographer_access_code"].ToString();
            }
        }
        catch (Npgsql.PostgresException pe)
        {
            return choreoAccessCode;
        }

        return choreoAccessCode;
    }

    /// <summary>
    /// This will delete all the information in the database except the information in the production table.
    /// </summary>
    /// <returns>True if it deleted all the information except in the production table. False if it failed</returns>
    public Boolean DeleteAllTables()
    {
        try
        {
            //Open a connection to the database
            using var conn = new NpgsqlConnection(_connString);
            conn.Open();

            //Create a command variable
            var cmd = new NpgsqlCommand();

            //Command to delete everything from the dreamroleusers table, this will also delete everything from the performer table and the rehearsal_memebers
            cmd.Connection = conn;
            cmd.CommandText = "DELETE FROM dreamrolesuser;";
            cmd.ExecuteNonQuery();

            //Command to delete everything from the songs table , this will also delete everything from the setlists table and the rehearsals table
            cmd.CommandText = "DELETE FROM songs;";
            cmd.ExecuteNonQuery();
        }
        catch (Npgsql.PostgresException e) 
        {
            return false;
        }
        return true;
    }
}

