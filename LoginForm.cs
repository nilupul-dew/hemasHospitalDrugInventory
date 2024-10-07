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
    public partial class LoginForm : Form
    {
        public LoginForm()
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

        private void loginBtn_Click(object sender, EventArgs e)
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

                    string query = @"UPDATE Supplier 
                         SET PhoneNumber = @PhoneNumber, 
                             Email = @Email, 
                             Address = @Address,
                             Bank_ACC_Number = @Bank_ACC_Number,
                             Bank_Name = @Bank_Name,
                             Bank_Branch = @Bank_Branch
                         WHERE SupplierID = @SupplierID";

                    using (SqlCommand cmd = new SqlCommand(query, connect))
                    {

                        cmd.Parameters.AddWithValue("@SupplierID", SupplierID);
                        cmd.Parameters.AddWithValue("@PhoneNumber", phoneNumber_tbx.Text);
                        cmd.Parameters.AddWithValue("@Email", email_tbx.Text);
                        cmd.Parameters.AddWithValue("@Address", address_tbx.Text);
                        cmd.Parameters.AddWithValue("@Bank_ACC_Number", acc_tbx.Text);
                        cmd.Parameters.AddWithValue("@Bank_Name", bankName_tbx.Text);
                        cmd.Parameters.AddWithValue("@Bank_Branch", branch_tbx.Text);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        // Check if the update was successful
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Record updated successfully.", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("No record found with the specified index.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error SupplierManage/Update_btn_Click: " + ex.Message);
            }
        }
    }
}
