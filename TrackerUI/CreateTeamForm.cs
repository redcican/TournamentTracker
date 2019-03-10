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
        public CreateTeamForm()
        {
            InitializeComponent();
        }

        private void createMemberbutton_Click(object sender, EventArgs e)
        {
            if (ValidateForm())
            {
                PersonModel p = new PersonModel();
                p.FirstName = firstNameValue.Text;
                p.LastName = lastNameValue.Text;
                p.CellPhoneNumber = cellPhoneValue.Text;
                p.EmailAddress = emailValue.Text;

                GlobalConfig.Connection.CreatPerson(p);
            }
            else
            {
                MessageBox.Show("You need to fill in all of the fields!");
            }
        }


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
    }
}
