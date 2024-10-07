using hemasHospitalDrugInventory.Employees;
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

namespace hemasHospitalDrugInventory.Suppliers.S_Usercontrols
{
    public partial class S_UC_Index : UserControl
    {
        public S_UC_Index()
        {
            InitializeComponent();
            LoadSupplierTableData();
            LoadWardManagerTableData();
        }

        // Supplier Tab -------------------------------------------------
        void LoadSupplierTableData()
        {

            try
            {
                using (SqlConnection connect = new SqlConnection(CommonConnecString.ConnectionString))
                {
                    connect.Open();
                    string query = @"
            SELECT 
                s.SupplierID, 
                s.Name AS SupplierName, 
                s.PhoneNumber, 
                s.Email, 
                s.Address, 
                s.Bank_ACC_Number, 
                s.Bank_Name, 
                s.Bank_Branch, 
                c.CategoryName
            FROM Supplier s
            INNER JOIN Category c ON s.CategoryID = c.CategoryID;";


                    using (SqlCommand cmd = new SqlCommand(query, connect))
                    {
                        //cmd.Parameters.AddWithValue("@todayDate", DateTime.Today);

                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            DataTable table = new DataTable();
                            adapter.Fill(table);
                            dataGridView1.DataSource = table;
                            Console.WriteLine("Data Loaded Successfully");

                            // Rename columns after setting the DataSource
                            #region Rename Column Names
                            foreach (DataGridViewColumn column in dataGridView1.Columns)
                            {
                                switch (column.Name)
                                {
                                    case "SupplierID":
                                        column.HeaderText = "ID";
                                        break;
                                    case "SupplierName":
                                        column.HeaderText = "Name";
                                        break;
                                    case "PhoneNumber":
                                        column.HeaderText = "Phone Number";
                                        break;
                                    case "Email":
                                        column.HeaderText = "Email";
                                        break;
                                    case "Address":
                                        column.HeaderText = "Address";
                                        break;
                                    case "Bank_ACC_Number":
                                        column.HeaderText = "Account Number";
                                        break;
                                    case "Bank_Name":
                                        column.HeaderText = "Bank Name";
                                        break;
                                    case "Bank_Branch":
                                        column.HeaderText = "Bank Branch";
                                        break;
                                    case "CategoryName":
                                        column.HeaderText = "Category";
                                        break;

                                    default:
                                        break;
                                }
                            }
                            #endregion
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine(ex);
            }


        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Ensure the clicked cell is not a header cell
            if (e.RowIndex >= 0)
            {
       
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];

                int supplierID = Convert.ToInt32(selectedRow.Cells["SupplierID"].Value);
                string name = selectedRow.Cells["SupplierName"].Value.ToString();
                string phoneNumber = selectedRow.Cells["PhoneNumber"].Value.ToString();
                string email = selectedRow.Cells["Email"].Value.ToString();
                string address = selectedRow.Cells["Address"].Value.ToString();
                string acc = selectedRow.Cells["Bank_ACC_Number"].Value.ToString();
                string bankName = selectedRow.Cells["Bank_Name"].Value.ToString();
                string branch = selectedRow.Cells["Bank_Branch"].Value.ToString();

                SupplierManage supplierMange = new SupplierManage(supplierID, name, phoneNumber, email, address, acc, bankName, branch);
                supplierMange.FormClosed += (s, args) =>
                {
                    // Refresh the data after the previous form is closed
                    LoadSupplierTableData();
                };
                supplierMange.ShowDialog();  
            }
        }

        // Ward Manager Tab -------------------------------------------------

        void LoadWardManagerTableData()
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(CommonConnecString.ConnectionString))
                {
                    connect.Open();
                    string query = @"
                SELECT 
                    wm.WardManagerID, 
                    wm.Name AS ManagerName, 
                    wm.Email, 
                    wm.PhoneNumber, 
                    w.Name AS WardName
                FROM Ward_Manager wm
                INNER JOIN Ward w ON wm.WardID = w.WardID;";  // Joining Ward table to get the ward name

                    using (SqlCommand cmd = new SqlCommand(query, connect))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            DataTable table = new DataTable();
                            adapter.Fill(table);
                            dataGridView2.DataSource = table;
                            Console.WriteLine("Data Loaded Successfully LoadWardManagerTableData");

                            // Rename columns after setting the DataSource
                            #region Rename Column Names
                            foreach (DataGridViewColumn column in dataGridView2.Columns)
                            {
                                Console.WriteLine("Data is available LoadWardManagerTableData");
                                switch (column.Name)
                                {
                                    case "WardManagerID":
                                        column.HeaderText = "ID";
                                        break;
                                    case "ManagerName":
                                        column.HeaderText = "Name";
                                        break;
                                    case "Email":
                                        column.HeaderText = "Email";
                                        break;
                                    case "PhoneNumber":
                                        column.HeaderText = "Phone Number";
                                        break;
                                    case "WardName":
                                        column.HeaderText = "Ward Name";
                                        break;
                                    default:
                                        break;
                                }
                            }
                            #endregion
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine(ex);
            }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Ensure the clicked cell is not a header cell
            if (e.RowIndex >= 0)
            {

                DataGridViewRow selectedRow = dataGridView2.Rows[e.RowIndex];

                // Retrieve data from the selected row based on Ward Manager details
                int wardManagerID = Convert.ToInt32(selectedRow.Cells["WardManagerID"].Value);
                string managerName = selectedRow.Cells["ManagerName"].Value.ToString();
                string phoneNumber = selectedRow.Cells["PhoneNumber"].Value.ToString();
                string email = selectedRow.Cells["Email"].Value.ToString();
                string wardName = selectedRow.Cells["WardName"].Value.ToString();  // Ward Name from the joined Ward table

                // Pass the retrieved data to another form
                WardManage wardManager = new WardManage(wardManagerID, managerName, phoneNumber, email, wardName);
                wardManager.FormClosed += (s, args) =>
                {
                    // Refresh the data after the form is closed
                    LoadWardManagerTableData();
                };
                wardManager.ShowDialog();
            }

        }
    }
}
