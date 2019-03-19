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
            prizeListBox.DataSource = null;
            prizeListBox.DataSource = selectedPrizes;
            prizeListBox.DisplayMember = "PlaceName";


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
            TeamModel tm = new TeamModel()
        }
    }
}
        #endregion