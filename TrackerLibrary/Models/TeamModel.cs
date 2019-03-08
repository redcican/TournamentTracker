using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary.Models
{
    public class TeamModel
    {
        /// <summary>
        /// Represents a Teammbers class
        /// </summary>
        public List<PersonModel> TeamMembers { get; set; } = new List<PersonModel>();

        /// <summary>
        /// Represent the Name of a Team
        /// </summary>
        public string TeamName { get; set; }
    }
}
