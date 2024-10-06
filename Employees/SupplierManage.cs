using PharmacyManagementSystem._Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TreeView;

namespace hemasHospitalDrugInventory.Employees
{
    public partial class SupplierManage : Form
    {
        public SupplierManage()
        {
            InitializeComponent();
        }

        int SupplierID;
        string S_Name;
        string S_PhoneNumber;
        string S_Email;
        string Address;
        string Acc;
        string BankName;
        string Branch;
        public SupplierManage(
            int supplierID,
            string name,
            string phoneNumber,
            string email,
            string address,
            string acc,
            string bankName,
            string branch)
        {
            InitializeComponent();

            SupplierID = supplierID;
            S_Name = name;
            S_PhoneNumber = phoneNumber;
            S_Email = email;
            Address = address;
            Acc = acc;
            BankName = bankName;
            Branch = branch;

            DisplayData();
        }


        void DisplayData()
        {
            phoneNumber_tbx.Text = S_PhoneNumber;
            email_tbx.Text = S_Email;
            address_tbx.Text = Address;
            acc_tbx.Text = Acc;
            bankName_tbx.Text = BankName;
            branch_tbx.Text = Branch;

            supplierName_lbl.Text = $"{S_Name}  (ID: {SupplierID})";
        }

        void Update_btn_Click(object sender, EventArgs e)
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

        bool UserInputValidate()
        {
            // Check if required fields are empty
           
            if (string.IsNullOrWhiteSpace(address_tbx.Text))
            {
                MessageBox.Show("Address is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                address_tbx.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(email_tbx.Text))
            {
                MessageBox.Show("Email is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                email_tbx.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(phoneNumber_tbx.Text))
            {
                MessageBox.Show("Phone number is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                phoneNumber_tbx.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(acc_tbx.Text))
            {
                MessageBox.Show("Account number is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                acc_tbx.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(bankName_tbx.Text))
            {
                MessageBox.Show("Bank Name is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                bankName_tbx.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(branch_tbx.Text))
            {
                MessageBox.Show("Branch name is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                branch_tbx.Focus();
                return false;
            }

            // Validate if Phone Number is numeric
            if (!long.TryParse(phoneNumber_tbx.Text, out _))
            {
                MessageBox.Show("Phone number must be numeric.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                phoneNumber_tbx.Focus();
                return false;
            }

            // Validate if Bank Account Number is numeric
            if (!long.TryParse(acc_tbx.Text, out _))
            {
                MessageBox.Show("Account number must be numeric.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                acc_tbx.Focus();
                return false;
            }

            // If all validations pass
            return true;
        }

        void Delete_btn_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete this record?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection connect = new SqlConnection(CommonConnecString.ConnectionString))
                    {
                        connect.Open();

                        string query = @"DELETE FROM Supplier WHERE SupplierID = @SupplierID";

                        using (SqlCommand cmd = new SqlCommand(query, connect))
                        {
                            cmd.Parameters.AddWithValue("@SupplierID", SupplierID);

                            // Execute the delete command
                            int rowsAffected = cmd.ExecuteNonQuery();

                            // Check if the delete was successful
                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Record deleted successfully.", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show("No record found with the specified SupplierID.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error SupplierManage/Delete_btn_Click: " + ex.Message);
                }

            }
        }
    }
}
