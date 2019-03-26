using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerLibrary.Models;

namespace TrackerUI
{
    public static class TournamentLogic
    {
        // 1. order our list randomly of teams
        // 2. Check if it is big enough, - if not, add in byes(轮空) - 2*2*2*2
        // 3. Create our first round of matchups
        // 4. Create every round after that - e.g. - 8 matchups - 4 matchups - 2 matchups - 1 matchup
        #region Method to create rounds from tournament
        public static void CreateRounds(TournamentModel model)
        {
            List<TeamModel> randomizedTeams = RandomizeTeamModel(model.EnteredTeams);
            int rounds = FindNumberOfRounds(randomizedTeams.Count);
            int byes = NumberOfByes(rounds, randomizedTeams.Count);

            // create the first round of matchup
            model.Rounds.Add(CreateFirstRound(byes, randomizedTeams));
            // create the rest of rounds
            CreateOtherRounds(model, rounds);
        }
        #endregion

        #region create every round after the first round
        private static void CreateOtherRounds(TournamentModel model, int rounds)
        {
            int round = 2;
            List<MatchupModel> previousRound = model.Rounds[0];
            List<MatchupModel> currRound = new List<MatchupModel>();
            MatchupModel currMatchup = new MatchupModel();

            while (round < rounds)
            {
                foreach (MatchupModel match in previousRound)
                {
                    currMatchup.Entries.Add(new MatchupEntryModel { ParentMatchup = match });

                    if (currMatchup.Entries.Count > 1)
                    {
                        currMatchup.MatchupRound = round;
                        currRound.Add(currMatchup);
                        currMatchup = new MatchupModel();
                    }
                }

                model.Rounds.Add(currRound);
                previousRound = currRound;
                currRound = new List<MatchupModel>();
                round += 1;

            }
        }

        #endregion

        #region create the first round of matchups
        private static List<MatchupModel> CreateFirstRound(int byes, List<TeamModel> teams)
        {
            List<MatchupModel> output = new List<MatchupModel>();
            MatchupModel curr = new MatchupModel();

            foreach (TeamModel team in teams)
            {
                curr.Entries.Add(new MatchupEntryModel { TeamCompeting = team });

                if (byes > 0 || curr.Entries.Count > 0)
                {
                    curr.MatchupRound = 1;
                    output.Add(curr);
                    curr = new MatchupModel();

                    if (byes > 0)
                    {
                        byes -= 1;
                    }
                }

            }
            return output;
        }

        #endregion

        #region find the number of byes every round
        private static int NumberOfByes(int rounds, int numberOfTeams)
        {
            int output = 0;
            int totalTeams = 1;
            for (int i = 1; i <= rounds; i++)
            {
                totalTeams *= 2;
            }

            output = totalTeams - numberOfTeams;
            return output;
        }

        #endregion

        #region randomize the team order
        /// <summary>
        /// order our list randomly of teams
        /// </summary>
        /// <returns></returns>
        private static List<TeamModel> RandomizeTeamModel(List<TeamModel> teams)
        {
            // var shuffledcards = cards.OrderBy(a => Guid.NewGuid()).ToList();
            return teams.OrderBy(x => Guid.NewGuid()).ToList();
        }
        #endregion

        #region find the number of rounds
        /// <summary>
        /// Check if it is big enough, - if not, add in byes - 2*2*2*2
        /// </summary>
        /// <param name="teamCount">the number of teams</param>
        /// <returns></returns>
        private static int FindNumberOfRounds(int teamCount)
        {
            int output = 1;
            int val = 2;

            while(val < teamCount)
            {
                output += 1;
                val *= 2;
            }

            return output;
        }
        #endregion
    }
}
