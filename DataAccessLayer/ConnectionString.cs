using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace DataAccessLayer
{
    public class ConnectionString
    {
        public static NpgsqlConnection Connection()
        {
            string cstring = "Write Your Connection String Here";//for example "Server=;Port=;Database=;User Id=;Password="
            NpgsqlConnection con = new NpgsqlConnection(cstring);
            return con;
        }
    }
}
