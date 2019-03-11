using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary.Models
{
    public class PersonModel
    {
        /// <summary>
        /// The unique Identifier for the Person.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Represents the first name of a person
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Represents the last name of a person
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Represents the email address of a person
        /// </summary>
        public string EmailAddress { get; set; }

        /// <summary>
        /// Represents the cellphone number of a person
        /// </summary>
        public string CellPhoneNumber { get; set; }

        /// <summary>
        /// Represents the full name of a person
        /// </summary>
        public string FullName => $"{FirstName} {LastName}";

    }
}
