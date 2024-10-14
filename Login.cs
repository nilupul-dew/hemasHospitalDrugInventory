using hemasHospitalDrugInventory.Suppliers;
using PharmacyManagementSystem._Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hemasHospitalDrugInventory
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void username_Enter(object sender, EventArgs e)
        {
            if (usernameText.Text == "Username")
            {
                usernameText.Text = "";
                usernameText.ForeColor = Color.Black;
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
            if (passwordText.Text == "Password")
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
        bool UserInputValidate()
        {
            // Check if required fields are empty

            if (string.IsNullOrWhiteSpace(usernameText.Text))
            {
                MessageBox.Show("Username is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                usernameText.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(passwordText.Text))
            {
                MessageBox.Show("Password is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                passwordText.Focus();
                return false;
            }
            return true;
        }

        private void loginBtn_Click_1(object sender, EventArgs e)
        {
            if (!UserInputValidate())
            {
                return;
            }


            try
            {
                using (SqlConnection connect = new SqlConnection(CommonConnecString.ConnectionString))
                {
                    connect.Open();

                    string userName = usernameText.Text;
                    string password = passwordText.Text;
                    string query = "SELECT COUNT(*) FROM [dbo].[User] WHERE UserName = @userName AND Password = @password";

                    SqlCommand command = new SqlCommand(query, connect);
                    command.Parameters.AddWithValue("@UserName", userName);
                    command.Parameters.AddWithValue("@Password", password);

                    int count = (int)command.ExecuteScalar();

                    if (count > 0)
                    {
                        Main main = new Main();
                        main.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("User Not Found.", " Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Login/Login_btn_Click: " + ex.Message);

            }
        }
    }
    
}
