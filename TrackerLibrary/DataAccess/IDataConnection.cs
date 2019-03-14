using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerLibrary.Models;

namespace TrackerLibrary.DataAccess
{
    public interface IDataConnection
    {
        // Initialize the PrizeModel for the DataConnectin
        PrizeModel CreatePrize(PrizeModel model);

        // Initialize the PersonModel for the DataConnection
        PersonModel CreatPerson(PersonModel model);

        // Get all peopel
        List<PersonModel> GetPerson_All();

        // Get all teams
        TeamModel CreateTeam(TeamModel model);
    }
}
