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
            LoadTableData();
        }

        private void LoadTableData()
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
                    LoadTableData();
                };
                supplierMange.ShowDialog();  
            }
        }
    }
}
