using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerLibrary.Models;

//@PlaceNumber int,
//@PlaceName nvarchar(50),
//@PrizeAmount money,
//@PrizePercentage float,
//@id int = 0 output
    
namespace TrackerLibrary.DataAccess
{
    public class SqlConnector : IDataConnection
    {
        private const string db = "Tournaments";

        #region SQL CreatePrize model
        /// <summary>
        /// Saves a new prize to the database       
        /// </summary>
        /// <param name="model">The prize information</param>
        /// <returns>The prize information, including the unique identifier.</returns>
        public PrizeModel CreatePrize(PrizeModel model)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                var p = new DynamicParameters();
                p.Add("@PlaceNumber", model.PlaceNumber);
                p.Add("@PlaceName", model.PlaceName);
                p.Add("@PrizeAmount", model.PrizeAmount);
                p.Add("@PrizePercentage", model.PrizePercentage);
                p.Add("@id", dbType: DbType.Int32, direction: ParameterDirection.Output);

                connection.Execute("dbo.spPrizes_Insert", param: p, commandType: CommandType.StoredProcedure);

                model.Id = p.Get<int>("@id");

                return model;
            }
        }

        #endregion

        #region SQL CreatePerson model
        /// <summary>
        /// Saves a new person member to the database
        /// </summary>
        /// <param name="model">The person information</param>
        /// <returns>The person information, including the unique identifier</returns>
        public PersonModel CreatPerson(PersonModel model)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                var p = new DynamicParameters();
                p.Add("@FirstName", model.FirstName);
                p.Add("@LastName", model.LastName);
                p.Add("@EmailAddress", model.EmailAddress);
                p.Add("@CellPhoneNumber", model.CellPhoneNumber);
                p.Add("@id", dbType: DbType.Int32, direction: ParameterDirection.Output);

                connection.Execute("dbo.spPeople_Insert", param: p, commandType: CommandType.StoredProcedure);

                model.Id = p.Get<int>("@id");

                return model;
            }
        }
        #endregion

        #region SQL Get all Person
        public List<PersonModel> GetPerson_All()
        {
            List<PersonModel> output;
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db))) 
            {
                output = connection.Query<PersonModel>("dbo.spPeople_GetAll").ToList();
            }
            return output;
        }
        #endregion

        #region SQL to Create  Teams
        public TeamModel CreateTeam(TeamModel model)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                var t = new DynamicParameters();
                t.Add("@TeamName", model.TeamName);
                t.Add("@id", dbType: DbType.Int32, direction: ParameterDirection.Output);

                connection.Execute("dbo.spTeams_Insert", param: t, commandType: CommandType.StoredProcedure);

                model.Id = t.Get<int>("@id");

                foreach (PersonModel tm in model.TeamMembers)
                {
                    var p = new DynamicParameters();
                    p.Add(@"TeamId", model.Id);
                    p.Add(@"PersonId", tm.Id);

                    connection.Execute("dbo.spTeamMembers_Insert", param: p, commandType: CommandType.StoredProcedure);
                }

                return model;
            }
        }

        #endregion

        #region SQL Get all teams
        public List<TeamModel> GetTeam_All()
        {
            List<TeamModel> output;
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                output = connection.Query<TeamModel>("dbo.spTeam_GetAll").ToList();

                foreach (TeamModel team in output)
                {
                    var p = new DynamicParameters();
                    p.Add("@TeamId", team.Id);

                    team.TeamMembers = connection.Query<PersonModel>("dbo.spTeamMembers_GetByTeam", param: p, 
                        commandType: CommandType.StoredProcedure).ToList();
                }
            }
            return output;

        }
        #endregion

        #region SQL to Create tournaments.
        /// <summary>
        /// Method to create a tournament
        /// </summary>
        /// <param name="model">Tournament model</param>
        /// <returns></returns>
        public void CreateTournament(TournamentModel model)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                SaveTournament(connection, model);

                SaveTournamentPrize(connection, model);

                SaveTournamentEntries(connection, model);

            }
        }
        /// <summary>
        /// Seperate method to save a tournament model 
        /// </summary>
        /// <param name="connection">IDB SQL connectin instance</param>
        /// <param name="model">Tournament model</param>
        private void SaveTournament(IDbConnection connection, TournamentModel model)
        {
            var t = new DynamicParameters();
            t.Add("@TournamentName", model.TournamentName);
            t.Add("@EntryFee", model.EntryFee);
            t.Add("@id", dbType: DbType.Int32, direction: ParameterDirection.Output);

            connection.Execute("dbo.spTournaments_Insert", param: t, commandType: CommandType.StoredProcedure);

            model.Id = t.Get<int>("@id");
        }
        /// <summary>
        /// Seperate method to save a prize model
        /// </summary>
        /// <param name="connection">IDB SQL connection instance</param>
        /// <param name="model">Tournament model</param>
        private void SaveTournamentPrize(IDbConnection connection, TournamentModel model)
        {
            foreach (PrizeModel pz in model.Prizes)
            {
                var p = new DynamicParameters();
                p.Add(@"TournamentId", model.Id);
                p.Add(@"PrizedId", pz.Id);
                p.Add("@id", dbType: DbType.Int32, direction: ParameterDirection.Output);

                connection.Execute("dbo.spTeamMembers_Insert", param: p, commandType: CommandType.StoredProcedure);
            }
        }
        /// <summary>
        /// Seperate method to save a team model
        /// </summary>
        /// <param name="connection">IDB SQL connection instance</param>
        /// <param name="model">Tournament model</param>
        private void SaveTournamentEntries(IDbConnection connection, TournamentModel model)
        {

            foreach (TeamModel tm in model.EnteredTeams)
            {
                var p = new DynamicParameters();
                p.Add(@"TournamentId", model.Id);
                p.Add(@"TeamId", tm.Id);
                p.Add("@id", dbType: DbType.Int32, direction: ParameterDirection.Output);

                connection.Execute("dbo.spTournamentEntries_Insert", param: p, commandType: CommandType.StoredProcedure);
            }

        }
        #endregion
    }

}
