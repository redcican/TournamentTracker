using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary
{
    public static class GlobalConfig
    {
        public static List<IDataConnection> Connections { get; private set; }

        public static void InitializeConnections(bool database, bool textfiles)
        {
            if (database)
            {
                // TODO - create the SQL connections
            }
            if (textfiles)
            {
                // TODO - create the Text connections
            }
        }
    }
}
