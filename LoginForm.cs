﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hemasHospitalDrugInventory
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void username_Enter(object sender, EventArgs e)
        {
            if(usernameText.Text == "Username")
            {
                usernameText.Text = "";
                usernameText.ForeColor= Color.Black;
            }
        }

        private void username_Leave(object sender, EventArgs e)
        {
            if (usernameText.Text == "")
            {
                usernameText.Text = "Username";
                usernameText.ForeColor = Color.Silver;
            }
        }

        private void password_Enter(object sender, EventArgs e)
        {
            if(passwordText.Text == "Password")
            {
                passwordText.Text = "";
                passwordText.ForeColor = Color.Black;
            }
        }

        private void password_Leave(object sender, EventArgs e)
        {
            if (passwordText.Text == "")
            {
                passwordText.Text = "Password";
                passwordText.ForeColor = Color.Silver;
            }
        }

    }
}
