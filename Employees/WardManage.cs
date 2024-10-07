using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TreeView;
using System.Xml.Linq;
using PharmacyManagementSystem._Common;
using System.Data.SqlClient;

namespace hemasHospitalDrugInventory.Employees
{
    public partial class WardManage : Form
    {
        int ManagerID;
        string ManagerName;
        string PhoneNumber;
        string Email;
        string WardName;
        public WardManage(int wardManagerID, string managerName, string phoneNumber, string email, string wardName)
        {
            InitializeComponent();

            ManagerID = wardManagerID;
            ManagerName = managerName;
            PhoneNumber = phoneNumber;
            Email = email;
            WardName = wardName;

            LoadWardNames();
            DisplayData();


        }

        void DisplayData()
        {
            name_tbx.Text = ManagerName;
            email_tbx.Text = Email;
            phone_tbx.Text = PhoneNumber;
        }

        void LoadWardNames() 
        {
            // Load the ward names to the ComboBox
            try
            {
                using (SqlConnection connect = new SqlConnection(CommonConnecString.ConnectionString))
                {
                    connect.Open();

                    string query = "SELECT WardID, Name FROM Ward";

                    using (SqlCommand cmd = new SqlCommand(query, connect))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            List<KeyValuePair<int, string>> wardList = new List<KeyValuePair<int, string>>(); // Create a list to hold the wards

                            while (reader.Read())
                            {
                                int wardID = reader.GetInt32(0); 
                                string wardName = reader.GetString(1); 

                                // Add the ward to the list
                                wardList.Add(new KeyValuePair<int, string>(wardID, wardName));
                            }

                            // Bind the ComboBox to the list
                            ward_cmbx.DataSource = new BindingSource(wardList, null);
                            ward_cmbx.DisplayMember = "Value";  // What to show in the ComboBox (ward name)
                            ward_cmbx.ValueMember = "Key";  // The actual WardID value
                        }
                    }
                }

                // Loop through the combo box items to find a matching ward name
                for (int i = 0; i < ward_cmbx.Items.Count; i++)
                {
                    // Cast each item back to KeyValuePair<int, string>
                    var item = (KeyValuePair<int, string>)ward_cmbx.Items[i];

                    // Compare the ward name (Value part of the KeyValuePair) with the string WardName
                    if (item.Value == WardName)
                    {
                        // If a match is found, set the selected index of the combo box
                        ward_cmbx.SelectedIndex = i;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while loading wards to combo box: " + ex.Message);
            }

            //int selectedWardID = (int)ward_cmbx.SelectedValue;

        }

        bool ValidateInputs()
        {
            // Name validation (letters and spaces only)
            if (!System.Text.RegularExpressions.Regex.IsMatch(name_tbx.Text, @"^[a-zA-Z\s]+$"))
            {
                MessageBox.Show("Please enter a valid name (letters and spaces only).", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Phone number validation (numbers only)
            if (!System.Text.RegularExpressions.Regex.IsMatch(phone_tbx.Text, @"^[0-9]+$"))
            {
                MessageBox.Show("Please enter a valid phone number (numbers only).", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Email validation (simple check for an email pattern)
            if (!System.Text.RegularExpressions.Regex.IsMatch(email_tbx.Text, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                MessageBox.Show("Please enter a valid email address.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;

           
        }

        private void Update_btn_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs())
            {
               return;
            };

            try
            {
                using (SqlConnection connect = new SqlConnection(CommonConnecString.ConnectionString))
                {
                    connect.Open();

                    string query = @"UPDATE Ward_Manager 
                         SET Name = @Name, 
                             Email = @Email, 
                             PhoneNumber = @PhoneNumber,
                             WardID = @WardID
                         WHERE WardManagerID = @WardManagerID";

                    using (SqlCommand cmd = new SqlCommand(query, connect))
                    {                       
                        cmd.Parameters.AddWithValue("@WardManagerID", ManagerID);  
                        cmd.Parameters.AddWithValue("@Name", name_tbx.Text);
                        cmd.Parameters.AddWithValue("@Email", email_tbx.Text);
                        cmd.Parameters.AddWithValue("@PhoneNumber", phone_tbx.Text);
                        cmd.Parameters.AddWithValue("@WardID", (int)ward_cmbx.SelectedValue); 

                        int rowsAffected = cmd.ExecuteNonQuery();

                        // Check if the update was successful
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Record updated successfully.", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Close(); // Close the form after update
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
                MessageBox.Show("Error WardManager/Update_btn_Click: " + ex.Message);
            }


        }

        private void Delete_btn_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete this record?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection connect = new SqlConnection(CommonConnecString.ConnectionString))
                    {
                        connect.Open();

                        string query = @"DELETE FROM Ward_Manager WHERE WardManagerID = @WardManagerID";

                        using (SqlCommand cmd = new SqlCommand(query, connect))
                        {
                            cmd.Parameters.AddWithValue("@WardManagerID", ManagerID);

                            // Execute the delete command
                            int rowsAffected = cmd.ExecuteNonQuery();

                            // Check if the delete was successful
                            if (rowsAffected > 0)
                            {
                                //MessageBox.Show("Record deleted successfully.", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show("No record found with the specified WardManagerID.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error WardManagerID/Delete_btn_Click: " + ex.Message);
                }

            }
        }
    }
    
}
