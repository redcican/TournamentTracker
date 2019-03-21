using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerLibrary.Models;


// 1. Load the text file
// 2. Convert the text to List<Prizemodel>
// 3. Find the max ID
// 4. Add the new record to the new ID (max+1)
// 5. Convert the prizes to List<string>
// 6. Save the List<string> to the text file

namespace TrackerLibrary.DataAccess.TextHelpers
{
    public static class TextConnectorProcessor
    {
        #region FullFilePath
        // fileName: e.g PrizeModels.csv
        public static string FullFilePath(this string fileName)
        {
            return $"{ConfigurationManager.AppSettings["filePath"]}\\{fileName}";
        }

        #endregion

        #region LoadFile
        // 1. Load the text file
        public static List<string> LoadFile(this string file)
        {
            if (!File.Exists(file))
            {
                return new List<string>();
            }
            return File.ReadAllLines(file).ToList();
        }
        #endregion

        #region ConverToPrizeModels
        // 2. Convert the text file to List<PrizeModel>
        public static List<PrizeModel> ConvertToPrizeModels (this List<string> lines)
        {
            List<PrizeModel> output = new List<PrizeModel>();

            foreach (string line in lines)
            {
                string[] cols = line.Split(',');
                PrizeModel p = new PrizeModel
                {
                    Id = int.Parse(cols[0]),
                    PlaceNumber = int.Parse(cols[1]),
                    PlaceName = cols[2],
                    PrizeAmount = decimal.Parse(cols[3]),
                    PrizePercentage = double.Parse(cols[4])
                };
                output.Add(p);
            }

            return output;
        }
        #endregion

        #region ConverToPersonModels
        // Convert the text file to List<PersonModel>
        public static List<PersonModel> ConvertToPersonModels(this List<string> lines)
        {
            List<PersonModel> output = new List<PersonModel>();

            foreach (string line in lines)
            {
                string[] cols = line.Split(',');
                PersonModel p = new PersonModel
                {
                    Id = int.Parse(cols[0]),
                    FirstName = cols[1],
                    LastName = cols[2],
                    EmailAddress = cols[3],
                    CellPhoneNumber = cols[4]
                };
                output.Add(p);
            }

            return output;
        }

        #endregion

        #region ConvertToTeamModel
        // Convert the text file to the team model
        public static List<TeamModel> ConvertToTeamModels(this List<string> lines, string PeopleFileName)
        {
            // id, team name, list of ids separated by the pipe
            // e.g. 3, Tim's team, 1|2|3|4
            List<TeamModel> output = new List<TeamModel>();
            List<PersonModel> people = PeopleFileName.FullFilePath().LoadFile().ConvertToPersonModels();

            foreach (string line in lines)
            {
                string[] cols = line.Split(',');

                TeamModel t = new TeamModel
                {
                    Id = int.Parse(cols[0]),
                    TeamName = cols[1]
                };

                string[] personId = cols[2].Split('|');

                foreach (string id in personId)
                {
                    t.TeamMembers.Add(people.Where(x=>x.Id == int.Parse(id)).First());
                }
                output.Add(t);
            }
            return output;
        }

        #endregion

        #region SaveToPrizeFile
        // 5. Convert the prizes to List<string>
        // 6. Save the List<PrizeModel> to the text file
        public static void SaveToPrizeFile(this List<PrizeModel>models, string filename)

        {
            List<string> lines = new List<string>();
            foreach (PrizeModel p in models)
            {
                lines.Add($"{p.Id},{p.PlaceNumber},{p.PlaceName},{p.PrizeAmount},{p.PrizePercentage}");
            }
            File.WriteAllLines(filename.FullFilePath(), lines);
        }
        #endregion

        #region SaveToPeopleFile
        // Save the List<PersonModel> to the text file
        public static void SaveToPeopleFile(this List<PersonModel> models, string filename)
        {
            List<string> lines = new List<string>();
            foreach (PersonModel p in models)
            {
                lines.Add($"{p.Id},{p.FirstName},{p.LastName},{p.EmailAddress},{p.CellPhoneNumber}");
            }
            File.WriteAllLines(filename.FullFilePath(), lines);
        }
        #endregion

        #region Save to team File
        // Save the List<TeamModel> to the text file
        public static void SaveToTeamFile(this List<TeamModel> models, string filename)
        {
            List<string> lines = new List<string>();
            foreach (TeamModel t in models)
            {
                lines.Add($"{t.Id},{t.TeamName},{ConvertPeopleListToString(t.TeamMembers)}");
            }
            File.WriteAllLines(filename.FullFilePath(), lines);
        }

        // Convert a People List to the string
        private static string ConvertPeopleListToString(List<PersonModel> people)
        {
            string output = "";
            if (people.Count == 0)
            {
                return "";
            }

            foreach (PersonModel p in people)
            {
                output += $"{p.Id}|";
            }
            output = output.Substring(0, output.Length - 1);
            return output;
        }

        #endregion
    }
}
