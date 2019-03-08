using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerLibrary.DataAccess;

namespace TrackerLibrary
{
    public static class GlobalConfig
    {
        public static IDataConnection Connection { get; private set; }

        public static void InitializeConnections(DatabaseTypes db)
        {
            if (db == DatabaseTypes.Sql)
            {
                // TODO - Set up the SQL Connector properly.
                SqlConnector sql = new SqlConnector();
                Connection = sql;
            }
            else if (db == DatabaseTypes.TextFile)
            {
                // TODO - Set up the Text connector properly.
                TextConnector text = new TextConnector();
                Connection = text;
            }
        }

        // from App.config get the SQL connection string
        public static string CnnString (string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }
    }
}
