using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prototypes.Database
{
    class PerformerDB : IPerformerDB
    { 
        //The string to connect to the database
        private String _connString;

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
    }
}
