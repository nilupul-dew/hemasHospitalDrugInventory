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
            LoadSuppleirNames();
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

        void LoadSuppleirNames()
        {
            // Load the ward names to the ComboBox
            try
            {
                using (SqlConnection connect = new SqlConnection(CommonConnecString.ConnectionString))
                {
                    connect.Open();

                    string query = "SELECT SupplierID, Name FROM Supplier";

                    using (SqlCommand cmd = new SqlCommand(query, connect))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            List<KeyValuePair<int, string>> supplierList = new List<KeyValuePair<int, string>>(); // Create a list to hold the wards

                            while (reader.Read())
                            {
                                int supplierID = reader.GetInt32(0);
                                string supplierName = reader.GetString(1);

                                // Add the ward to the list
                                supplierList.Add(new KeyValuePair<int, string>(supplierID, supplierName));
                            }

                            // Bind the ComboBox to the list
                            suppliers_cmbx.DataSource = new BindingSource(supplierList, null);
                            suppliers_cmbx.DisplayMember = "Value";  // What to show in the ComboBox (ward name)
                            suppliers_cmbx.ValueMember = "Key";  // The actual WardID value

                            suppliers_cmbx.SelectedIndex = -1; // Clear the selected item
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while loading wards to combo box: " + ex.Message);
            }

            //int selectedWardID = (int)ward_cmbx.SelectedValue;

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

        private void Order_btn_Click(object sender, EventArgs e)
        {
            // Check if no item is selected in the ComboBox
            if (suppliers_cmbx.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a supplier.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            bool hasNegativeQuantity = false; // Flag to track if any quantity is less than 0

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells["Quantity"].Value != null)
                {
                    int quantity = Convert.ToInt32(row.Cells["Quantity"].Value);

                    if (quantity < 0)
                    {
                        hasNegativeQuantity = true;
                        break; 
                    }
                }
            }

            if (hasNegativeQuantity)
            {
                MessageBox.Show("One or more rows have a Quantity less than 0.","Invalid Quantity",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            else
            {
                CreateOrderEvent();


            }

        }

        int OrderEventID = -1;
        void CreateOrderEvent()
        {
            Console.WriteLine("Creating Order Event...");
            try
            {
                using (SqlConnection connect = new SqlConnection(CommonConnecString.ConnectionString))
                {
                    connect.Open();

                    string query = @"
                        INSERT INTO [OrderEvent] (SupplierID, IsDelivered, OrderedDate, OrderStatus)
                        VALUES (@SupplierID, @IsDelivered, @OrderedDate, @OrderStatus);
                        SELECT SCOPE_IDENTITY();";  // This will return the last inserted identity value

                    using (SqlCommand cmd = new SqlCommand(query, connect))
                    {
                        // Add parameters to the SQL query to prevent SQL injection
                        cmd.Parameters.AddWithValue("@SupplierID", (int)suppliers_cmbx.SelectedValue);
                        cmd.Parameters.AddWithValue("@IsDelivered", 0);
                        cmd.Parameters.AddWithValue("@OrderedDate", DateTime.Now); // Automatically set the current date
                        cmd.Parameters.AddWithValue("@OrderStatus", "ordered");

                        object result = cmd.ExecuteScalar();  // ExecuteScalar returns the first column of the first row in the result set
                        if (result != null)
                        {
                            OrderEventID = Convert.ToInt32(result);
                            CreateOrders();


                        }
                        else
                        {
                            MessageBox.Show("Failed to create order event.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
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

        void CreateOrders()
        {
            Console.WriteLine("CreateOrders...");
            using (SqlConnection connect = new SqlConnection(CommonConnecString.ConnectionString))
            {
                connect.Open();

                var retrievedInventoryData = new List<(int DrugID, int Quantity)>();

                foreach (int inventoryID in InventoryIDs)
                {
                    string query = @"
                                SELECT Drug_ID, Quantity 
                                FROM Inventory 
                                WHERE InventoryID = @InventoryID;";
                    using (SqlCommand cmd = new SqlCommand(query, connect))
                    {
                        cmd.Parameters.AddWithValue("@InventoryID", inventoryID);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int drugID = reader.GetInt32(reader.GetOrdinal("Drug_ID")); // Get Drug_ID
                                int quantity = reader.GetInt32(reader.GetOrdinal("Quantity")); // Get Quantity

                                retrievedInventoryData.Add((drugID, quantity));
                            }
                        }
                    }
                }

                foreach (var (DrugID, Quantity) in retrievedInventoryData)
                {
                    string insertQuery = @"
                                        INSERT INTO [Order] (OrderEventID, Drug_ID, Quantity)
                                        VALUES (@OrderEventID, @DrugID, @Quantity);";

                    using (SqlCommand insertCmd = new SqlCommand(insertQuery, connect))
                    {
                        // Add parameters to the insert query
                        insertCmd.Parameters.AddWithValue("@OrderEventID", OrderEventID);
                        insertCmd.Parameters.AddWithValue("@DrugID", DrugID);
                        insertCmd.Parameters.AddWithValue("@Quantity", Quantity);

                        // Execute the insert query
                        int rowsAffected = insertCmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            // MessageBox.Show($"Order created for InventoryID: {inventoryID}, Drug ID: {drugID}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Console.WriteLine($"Order created for Quantity: {Quantity}, Drug ID: {DrugID}");
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show($"Failed to create order for Drug: {DrugID}.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }

                }


            }
        }
    }
}
