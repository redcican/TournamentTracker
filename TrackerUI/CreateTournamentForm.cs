using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrackerLibrary;
using TrackerLibrary.Models;

namespace TrackerUI
{
    public partial class CreateTournamentForm : Form, IPrizeRequester, ITeamRequester
    {
        #region public members
        List<TeamModel> availableTeams = GlobalConfig.Connection.GetTeam_All();
        List<TeamModel> selectedTeams = new List<TeamModel>();
        List<PrizeModel> selectedPrizes = new List<PrizeModel>();
        #endregion

        #region initilize 
        public CreateTournamentForm()
        {
            InitializeComponent();
            WireUpLists();
        }
        #endregion

        #region drop down and list box
        // show the team to the drop down menu
        private void WireUpLists()
        {
            selectTeamDropDown.DataSource = null;
            selectTeamDropDown.DataSource = availableTeams;
            selectTeamDropDown.DisplayMember = "TeamName";

            // List box for the team name
            tournamentTeamsListBox.DataSource = null;
            tournamentTeamsListBox.DataSource = selectedTeams;
            tournamentTeamsListBox.DisplayMember = "TeamName";

            // List box for the prize
            tournamentPrizeListBox.DataSource = null;
            tournamentPrizeListBox.DataSource = selectedPrizes;
            tournamentPrizeListBox.DisplayMember = "PlaceName";


        }
        #endregion

        #region add team click button
        // Add team click button
        private void AddTeamButton_Click(object sender, EventArgs e)
        {

            TeamModel t = (TeamModel)selectTeamDropDown.SelectedItem;

            if (t != null)
            {
                // remove the selected t from the selected drop down
                availableTeams.Remove(t);
                // add the selected t to the box list
                selectedTeams.Add(t);
                // refresh the lists.
                WireUpLists();
            }
        }

        #endregion

        #region create Prize form button

        private void CreatePrizeButton_Click(object sender, EventArgs e)
        {
            // 1. Call the CreatePrizeForm 
            // this: represents the instance of CreateTournamentForm which implement IPrizeRequester
            CreatePrizeForm frm = new CreatePrizeForm(this);
            frm.Show();

        }

        public void PrizeComplete(PrizeModel model)
        {
            // 2. Get back from the form a PrizeModel
            // 3. Take the PrizeModel and put it into our list of selected prizes
            selectedPrizes.Add(model);
            WireUpLists();
        }
        #endregion

        #region create new team button 
        public void TeamComplete(TeamModel model)
        {
            selectedTeams.Add(model);
            WireUpLists();
        }

        private void CreateNewTeamLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CreateTeamForm ctf = new CreateTeamForm(this);
            ctf.Show();
        }
        #endregion

        #region remove selected team button
        private void RemoveSelectedPlayerButton_Click(object sender, EventArgs e)
        {
            TeamModel t = (TeamModel)tournamentTeamsListBox.SelectedItem;
            if (t != null)
            {
                // remove the selected p from the list box
                selectedTeams.Remove(t);
                // readd the p to the drop down list
                availableTeams.Add(t);
                // refresh the lists
                WireUpLists();
            }
        }
        #endregion

        #region remove selected prize button
        private void RomoveSelectedPrizesButton_Click(object sender, EventArgs e)
        {
            PrizeModel p = (PrizeModel)tournamentPrizeListBox.SelectedItem;
            if (p != null)
            {
                // remove the selected p from the list box
                selectedPrizes.Remove(p);
                WireUpLists();
            }
        }
        #endregion

        #region create the tournament button
        private void CreateTournamentButton_Click(object sender, EventArgs e)
        {
            // Validate the entry fee data

            bool feeAcceptable = decimal.TryParse(entryFeeValue.Text, out decimal fee);

            if (!feeAcceptable)
            {
                MessageBox.Show("You need to enter a valid Entry fee!",
                    "Invalid Fee", 
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            // Create our tournament model
            TournamentModel tm = new TournamentModel
            {
                TournamentName = tournamentNameValue.Text,
                EntryFee = fee,
                Prizes = selectedPrizes,
                EnteredTeams = selectedTeams,
            };

            // Wires our Matchups
            // 1. order our list randomly of teams
            // 2. Check if it is big enough, - if not, add in byes - 2*2*2*2
            // 3. Create our first round of matchups
            // 4. Create every round after that - e.g. - 8 matchups - 4 matchups - 2 matchups - 1 matchup
            TournamentLogic.CreateRounds(tm);
            // Create Tournament entry
            // Create all of the prizes entries 
            // Create all of the team entries
            GlobalConfig.Connection.CreateTournament(tm);
            
        }
        #endregion
    }
}
        