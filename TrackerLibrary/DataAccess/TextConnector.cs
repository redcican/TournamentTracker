using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerLibrary.Models;

namespace TrackerLibrary.DataAccess
{
    public class TextConnector : IDataConnection
    {
        // TODO - Wire up the CreatePrize for text files.
        public PrizeModel CreatePrize(PrizeModel model)
        {
            // 1. Load the text file
            // 2. Convert the text to List<Prizemodel>
            // 3. Find the max ID
            // 4. Add the new record to the new ID (max+1)
            // 5. Convert the prizes to List<string>
            // 6. Save the List<string> to the text file
        }
    }
}
