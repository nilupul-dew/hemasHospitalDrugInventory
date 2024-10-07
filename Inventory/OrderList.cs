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

namespace hemasHospitalDrugInventory.Inventory
{
    public partial class OrderList : Form
    {
        List<int> InventoryIDs;
        public OrderList(List<int> selectedInventoryIDs)
        {
            InitializeComponent();
            InventoryIDs = selectedInventoryIDs;

            LoadTable();
        }

        void LoadTable()
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(CommonConnecString.ConnectionString))
                {
                    connect.Open();

                    // Convert List<int> to a comma-separated string
                    string idList = string.Join(",", InventoryIDs);

                    string query = $@"
                SELECT 
                    i.InventoryID,
                    d.DrugName,
                    d.Dosage
                FROM Inventory i
                INNER JOIN Drug d ON i.Drug_ID = d.Drug_ID
                WHERE i.InventoryID IN ({idList});";

                    using (SqlCommand cmd = new SqlCommand(query, connect))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            DataTable table = new DataTable();
                            adapter.Fill(table);

                            // Add a new column for Quantity
                            table.Columns.Add("Quantity", typeof(int)); 

                            foreach (DataRow row in table.Rows)
                            {
                                row["Quantity"] = -1; // Set the default value 0 or -1
                            }


                            dataGridView1.DataSource = table;

                            // Optional: Rename columns for clarity
                            dataGridView1.Columns["InventoryID"].HeaderText = "Inventory ID";
                            dataGridView1.Columns["DrugName"].HeaderText = "Drug Name";
                            dataGridView1.Columns["Dosage"].HeaderText = "Dosage";
                            dataGridView1.Columns["Quantity"].HeaderText = "Quantity";

                            
                        }
                    }

                    dataGridView1.ReadOnly = false;
                    foreach (DataGridViewColumn column in dataGridView1.Columns)
                    {
                        if (column.Name != "Quantity")
                        {
                            column.ReadOnly = true; // Make all columns except "NewOrder" read-only
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
            // Ensure the clicked cell is not a header cell and is in the Quantity column
            if (e.RowIndex >= 0 && e.ColumnIndex == dataGridView1.Columns["Quantity"].Index)
            {
                // Start editing the cell immediately
                dataGridView1.BeginEdit(true);
            }
        }
    }
}
