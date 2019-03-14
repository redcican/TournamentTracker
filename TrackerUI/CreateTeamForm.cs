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
    public partial class CreateTeamForm : Form
    {
        #region Variable for Team members
        private List<PersonModel> availableTeamMembers = GlobalConfig.Connection.GetPerson_All();
        private List<PersonModel> selectedTeamMembers = new List<PersonModel>();
        #endregion

        #region initialize
        public CreateTeamForm()
        {
            InitializeComponent();

            //CreateSampleData();

            WireUpLists();
        }
        #endregion

        #region Testdata
        private void CreateSampleData()
        {
            availableTeamMembers.Add(new PersonModel {FirstName="Tim", LastName="Corey" });
            availableTeamMembers.Add(new PersonModel { FirstName = "Sue", LastName = "Storm" });

            selectedTeamMembers.Add(new PersonModel { FirstName = "Chen", LastName = "Shikun" });
            selectedTeamMembers.Add(new PersonModel { FirstName = "Jane", LastName = "Jones" });
        }

        #endregion

        #region Method to wire up a selected team from combobox to list
        private void WireUpLists()
        {
            // set the value to null to refresh the drop down
            selectTeamMemberDropDown.DataSource = null;
            selectTeamMemberDropDown.DataSource = availableTeamMembers;
            selectTeamMemberDropDown.DisplayMember = "FullName";

            teamMembersListBox.DataSource = null;
            teamMembersListBox.DataSource = selectedTeamMembers;
            teamMembersListBox.DisplayMember = "FullName";
        }
        #endregion

        #region create Team Member Button
        private void CreateMemberbutton_Click(object sender, EventArgs e)
        {
            if (ValidateForm())
            {
                PersonModel p = new PersonModel
                {
                    FirstName = firstNameValue.Text,
                    LastName = lastNameValue.Text,
                    CellPhoneNumber = cellPhoneValue.Text,
                    EmailAddress = emailValue.Text
                };

                p = GlobalConfig.Connection.CreatPerson(p);
                selectedTeamMembers.Add(p);
                WireUpLists();

                // clear out the input
                firstNameValue.Text = "";
                lastNameValue.Text = "";
                cellPhoneValue.Text = "";
                emailValue.Text = "";
            }
            else
            {
                MessageBox.Show("You need to fill in all of the fields!");
            }
        }

        #endregion

        #region validation form for create team form
        // TODO - Add validation to the CreateTeamForm
        private bool ValidateForm()
        {
            // check the firstName value
            if(firstNameValue.Text.Length == 0)
            {
                return false;
            }
            // check the lastName value
            if (lastNameValue.Text.Length == 0)
            {
                return false;
            }
            // check the email value
            if (emailValue.Text.Length == 0)
            {
                return false;
            }
            // check the cellphone value
            if (cellPhoneValue.Text.Length == 0)
            {
                return false;
            }

            return true;
        }
        #endregion

        #region Add team member to the list button
        private void AddMemberButton_Click(object sender, EventArgs e)
        {
            PersonModel p = (PersonModel)selectTeamMemberDropDown.SelectedItem;

            if (p!=null)
            {
                // remove the selected p from the selected drop down
                availableTeamMembers.Remove(p);
                // add the selected p to the box list
                selectedTeamMembers.Add(p);
                // refresh the lists.
                WireUpLists(); 
            }
        }
        #endregion

        #region Remove the team member from the list
        private void RemoveSelectedMemberButton_Click(object sender, EventArgs e)
        {
            PersonModel p = (PersonModel)teamMembersListBox.SelectedItem;
            if (p!=null)
            {
                // remove the selected p from the list box
                selectedTeamMembers.Remove(p);
                // readd the p to the drop down list
                availableTeamMembers.Add(p);
                // refresh the lists
                WireUpLists(); 
            }
        }

        #endregion


        #region Create a team name
        private void CreateTeamButton_Click(object sender, EventArgs e)
        {
            if (ValidateTeamName())
            {
                TeamModel t = new TeamModel
                {
                    TeamName = teamNameValue.Text,
                    TeamMembers = selectedTeamMembers
                };

                t = GlobalConfig.Connection.CreateTeam(t);

                // TODO - if we aren't closing this form after creation, reset the form 
            }
            else
            {
                MessageBox.Show("You must give the team a name!");
            }
        }

        #endregion

        #region validation for create a team name
        private bool ValidateTeamName()
        {
            if (teamNameValue.Text.Length == 0)
                return false;
            return true;
        }
        #endregion
    }
}
