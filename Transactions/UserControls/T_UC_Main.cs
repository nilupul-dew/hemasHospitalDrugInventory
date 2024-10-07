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

namespace hemasHospitalDrugInventory.Transactions.UserControls
{
    public partial class T_UC_Main : UserControl
    {
        public T_UC_Main()
        {
            InitializeComponent();
            LoadOrderTableData();
        }

        void LoadOrderTableData()
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(CommonConnecString.ConnectionString))
                {
                    connect.Open();

                    // SQL query to select data from Order table and join with Inventory and Supplier
                    string query = @"
                SELECT 
                    o.OrderID,
                    i.ProductName,
                    o.Quantity,
                    s.Name AS SupplierName,
                    o.IsDelivered
                FROM [Order] o
                INNER JOIN Inventory i ON o.InventoryID = i.InventoryID
                INNER JOIN Supplier s ON o.SupplierID = s.SupplierID;";

                    using (SqlCommand cmd = new SqlCommand(query, connect))
                    {
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
                                    case "OrderID":
                                        column.HeaderText = "Order ID";
                                        break;
                                    case "ProductName":
                                        column.HeaderText = "Product Name";
                                        break;
                                    case "Quantity":
                                        column.HeaderText = "Quantity";
                                        break;
                                    case "SupplierName":
                                        column.HeaderText = "Supplier Name";
                                        break;
                                    case "IsDelivered":
                                        column.HeaderText = "Delivered";
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
                MessageBox.Show("T_UC_Main, Error: " + ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine(ex);
            }
        }

    }

}
