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

    public Boolean UpdatePerformerAccessCode(int newAccessCode)
    {
        try
        {
            //Connects and opens a connection to the database
            using var conn = new NpgsqlConnection(_connString);
            conn.Open();

            //command to update the production table with the new access code
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

    public String GetPerformerAccessCode()
    {
        String performerAccessCode = null;
        try
        {
            using var conn = new NpgsqlConnection(_connString);
            conn.Open();

            var cmd = new NpgsqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "SELECT performer_access_code " +
                              "FROM public.production " +
                              "WHERE production_year = 2023;";

            // Execute the query and read the result
            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                // Assuming performer_access_code is a string; adjust if it's a different data type
                 performerAccessCode = reader["performer_access_code"].ToString();
            }
        }
        catch (Npgsql.PostgresException pe)
        {
            // Handle the exception, log it, or perform other error handling as needed
            return performerAccessCode;
        }

        return performerAccessCode;
    }

    public String GetManagerAccessCode()
    {
        String managerAccessCode = null;
        try
        {
            using var conn = new NpgsqlConnection(_connString);
            conn.Open();

            var cmd = new NpgsqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "SELECT stagemanager_access_code " +
                              "FROM public.production " +
                              "WHERE production_year=2023;";

            // Execute the query and read the result
            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                // Assuming performer_access_code is a string; adjust if it's a different data type
                managerAccessCode = reader["stagemanager_access_code"].ToString();
            }
        }
        catch (Npgsql.PostgresException pe)
        {
            // Handle the exception, log it, or perform other error handling as needed
            return managerAccessCode;
        }

        return managerAccessCode;
    }
    public String GetChoreoAccessCode()
    {
        String choreoAccessCode = null;
        try
        {
            using var conn = new NpgsqlConnection(_connString);
            conn.Open();

            var cmd = new NpgsqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "SELECT choreographer_access_code " +
                              "FROM public.production " +
                              "WHERE production_year=2023;";

            // Execute the query and read the result
            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                // Assuming performer_access_code is a string; adjust if it's a different data type
                choreoAccessCode = reader["choreographer_access_code"].ToString();
            }
        }
        catch (Npgsql.PostgresException pe)
        {
            // Handle the exception, log it, or perform other error handling as needed
            return choreoAccessCode;
        }

        return choreoAccessCode;
    }

    public Boolean DeleteAllTables()
    {
        try
        {
            using var conn = new NpgsqlConnection(_connString);
            conn.Open();

            //command to update the production table with the new access code
            var cmd = new NpgsqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "DELETE FROM dreamrolesuser;";
            cmd.ExecuteNonQuery();

            cmd.CommandText = "DELETE FROM songs;";
            cmd.ExecuteNonQuery();
        }
        catch (Exception e) 
        {
            return false;
        }
        return true;
    }
}

