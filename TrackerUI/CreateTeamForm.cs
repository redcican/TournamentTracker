﻿using System;
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
        private List<PersonModel> availableTeamMembers = new List<PersonModel>();
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
            selectTeamMemberDropDown.DataSource = availableTeamMembers;
            selectTeamMemberDropDown.DisplayMember = "FullName";

            teamMembersListBox.DataSource = selectedTeamMembers;
            teamMembersListBox.DisplayMember = "FullName";
        }
        #endregion

        #region create Team Member Button
        private void createMemberbutton_Click(object sender, EventArgs e)
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

                GlobalConfig.Connection.CreatPerson(p);

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
    }
}
