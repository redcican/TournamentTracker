using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary
{
    public class TournamentModel
    {
        /// <summary>
        /// Represents the name of a tournament
        /// </summary>
        public string TournamentName { get; set; }

        /// <summary>
        /// Represents the entry fee of a tournament
        /// </summary>
        public decimal EntryFee { get; set; }

        /// <summary>
        /// Represents the teams which entered
        /// </summary>
        public List<TeamModel> EnteredTeams { get; set; }

        /// <summary>
        /// Represents the prize of a tournaments
        /// </summary>
        public List<PrizeModel> Prizes { get; set; } = new List<PrizeModel>();

        /// <summary>
        /// Represents the rounds of tournament
        /// </summary>
        public List<List<MatchupModel>> Rounds { get; set; } = new List<List<MatchupModel>>();

    }
}
