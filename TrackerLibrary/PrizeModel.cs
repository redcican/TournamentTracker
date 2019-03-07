using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary
{
    public class PrizeModel
    {
        /// <summary>
        /// The unique Identifier for the prize. 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Represents the number of winning place
        /// </summary>
        public int PlaceNumber { get; set; }

        /// <summary>
        /// Represents the name of a winning place
        /// </summary>
        public string PlaceName { get; set; }

        /// <summary>
        /// Represents the amount of prize for a winner
        /// </summary>
        public decimal PrizeAmount { get; set; }

        /// <summary>
        /// Represents the percentage of a winning prize
        /// </summary>
        public double PrizePercentage { get; set; }
    }
}
