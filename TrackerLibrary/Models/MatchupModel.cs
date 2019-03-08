using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary.Models
{
    public class MatchupModel
    {
        /// <summary>
        /// Represents the matchup team
        /// </summary>
        public List<MatchupEntryModel> Entries { get; set; } = new List<MatchupEntryModel>();

        /// <summary>
        /// Represents the team winner
        /// </summary>
        public TeamModel Winner { get; set; }

        /// <summary>
        /// Represents the round of every matchup
        /// </summary>
        public int MatchupRound { get; set; }
    }
}
