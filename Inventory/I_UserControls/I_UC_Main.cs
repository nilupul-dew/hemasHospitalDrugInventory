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

namespace hemasHospitalDrugInventory.Inventory.I_UserControls
{
    public partial class I_UC_Main : UserControl
    {
        public I_UC_Main()
        {
            InitializeComponent();
            Load_All_InventoryTableData();
            Load_nearExpiry_InventoryTableData();
            Load_LowStock_InventoryTableData();
        }

        void Load_All_InventoryTableData()
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(CommonConnecString.ConnectionString))
                {
                    connect.Open();
                    string query = @"
            SELECT 
                i.InventoryID,
                d.DrugName, 
                d.StorageLocation, 
                d.CategoryID, 
                s.Name AS SupplierName, 
                s.PhoneNumber,
                i.ExpireDate, 
                i.Quantity
            FROM Inventory i
            INNER JOIN Supplier s ON i.SupplierID = s.SupplierID
            INNER JOIN Drug d ON i.Drug_ID = d.Drug_ID;";

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
                                    case "InventoryID":
                                        column.HeaderText = "Inventory ID";
                                        break;
                                    case "DrugName":
                                        column.HeaderText = "Drug Name";
                                        break;
                                    case "StorageLocation":
                                        column.HeaderText = "Storage Location";
                                        break;
                                    case "CategoryID":
                                        column.HeaderText = "Category ID";
                                        break;
                                    case "SupplierName":
                                        column.HeaderText = "Supplier Name";
                                        break;
                                    case "PhoneNumber":
                                        column.HeaderText = "Phone Number";
                                        break;
                                    case "ExpireDate":
                                        column.HeaderText = "Expiration Date";
                                        break;
                                    case "Quantity":
                                        column.HeaderText = "Quantity";
                                        break;

                                    default:
                                        break;
                                }
                            }
                            #endregion

                            // Add a Checkbox Column for "New Order"
                            DataGridViewCheckBoxColumn newOrderColumn = new DataGridViewCheckBoxColumn();
                            newOrderColumn.Name = "NewOrder";
                            newOrderColumn.HeaderText = "New Order";
                            newOrderColumn.FalseValue = false;
                            newOrderColumn.TrueValue = true;

                            // Add the checkbox column to the DataGridView as the first column
                            if (!dataGridView1.Columns.Contains("NewOrder"))
                            {
                                dataGridView1.Columns.Insert(0, newOrderColumn); // Insert at index 0 for the first column
                            }

                            // Ensure the entire DataGridView is ReadOnly, except for the checkbox column
                            dataGridView1.ReadOnly = false;  // Allow DataGridView to be editable
                            foreach (DataGridViewColumn column in dataGridView1.Columns)
                            {
                                if (column.Name != "NewOrder")
                                {
                                    column.ReadOnly = true; // Make all columns except "NewOrder" read-only
                                }
                            }
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
                // Check if the clicked column is the New Order column
                if (e.ColumnIndex == dataGridView1.Columns["NewOrder"].Index)
                {

                    return;
                }
                else
                {
                    DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];

                    // Retrieve data from the selected row based on Ward Manager details
                    int inventoryID = Convert.ToInt32(selectedRow.Cells["InventoryID"].Value);

                    Console.WriteLine("Selected Inventory ID: " + inventoryID);

                    Update_Inventory updateInventory = new Update_Inventory(inventoryID);
                    updateInventory.FormClosed += (s, args) =>
                    {
                        Load_All_InventoryTableData();
                        Load_nearExpiry_InventoryTableData();
                        Load_LowStock_InventoryTableData();
                    };
                    updateInventory.ShowDialog();

                }
            }




            //// Ensure the click is not on the header row or outside the grid bounds
            //if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            //{
            //    if (e.RowIndex >= 0 && e.RowIndex < dataGridView1.Rows.Count) // Check for valid index
            //    {
            //        DataGridViewRow clickedRow = dataGridView1.Rows[e.RowIndex];

            //        // Check if the checkbox column "NewOrder" exists and toggle the checkbox
            //        if (dataGridView1.Columns.Contains("NewOrder"))
            //        {
            //            DataGridViewCheckBoxCell checkboxCell = (DataGridViewCheckBoxCell)clickedRow.Cells["NewOrder"];
            //            bool isChecked = Convert.ToBoolean(checkboxCell.Value);

            //            // Toggle the checkbox state
            //            checkboxCell.Value = !isChecked;

            //            dataGridView1.RefreshEdit(); // Optionally refresh the cell to reflect the change
            //        }
            //    }
            //}
        }

        void Load_nearExpiry_InventoryTableData()
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(CommonConnecString.ConnectionString))
                {
                    connect.Open();
                    string query = @"
            SELECT 
                i.InventoryID,
                d.DrugName, 
                d.Dosage, 
                d.CategoryID, 
                s.Name AS SupplierName, 
                s.PhoneNumber,
                i.ExpireDate, 
                i.Quantity,
                DATEDIFF(DAY, GETDATE(), i.ExpireDate) AS DaysUntilExpiry
            FROM Inventory i
            INNER JOIN Supplier s ON i.SupplierID = s.SupplierID
            INNER JOIN Drug d ON i.Drug_ID = d.Drug_ID
            WHERE DATEDIFF(DAY, GETDATE(), i.ExpireDate) <= 30;";

                    using (SqlCommand cmd = new SqlCommand(query, connect))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            DataTable table = new DataTable();
                            adapter.Fill(table);
                            dataGridView2.DataSource = table;
                            Console.WriteLine("Data Loaded Successfully");

                            // Rename columns after setting the DataSource
                            #region Rename Column Names
                            foreach (DataGridViewColumn column in dataGridView2.Columns)
                            {
                                switch (column.Name)
                                {
                                    case "InventoryID":
                                        column.HeaderText = "Inventory ID";
                                        break;
                                    case "DrugName":
                                        column.HeaderText = "Drug Name";
                                        break;
                                    case "Dosage":
                                        column.HeaderText = "Dosage";
                                        break;
                                    case "CategoryID":
                                        column.HeaderText = "Category ID";
                                        break;
                                    case "SupplierName":
                                        column.HeaderText = "Supplier Name";
                                        break;
                                    case "PhoneNumber":
                                        column.HeaderText = "Phone Number";
                                        break;
                                    case "ExpireDate":
                                        column.HeaderText = "Expiration Date";
                                        break;
                                    case "Quantity":
                                        column.HeaderText = "Quantity";
                                        break;
                                    case "DaysUntilExpiry":
                                        column.HeaderText = "Days Until Expiry";
                                        break;

                                    default:
                                        break;
                                }
                            }
                            #endregion

                            // Add a Checkbox Column for "New Order"
                            DataGridViewCheckBoxColumn newOrderColumn = new DataGridViewCheckBoxColumn();
                            newOrderColumn.Name = "NewOrder";
                            newOrderColumn.HeaderText = "New Order";
                            newOrderColumn.FalseValue = false;
                            newOrderColumn.TrueValue = true;

                            // Add the checkbox column to the DataGridView as the first column
                            if (!dataGridView2.Columns.Contains("NewOrder"))
                            {
                                dataGridView2.Columns.Insert(0, newOrderColumn); // Insert at index 0 for the first column
                            }

                            // Ensure the entire DataGridView is ReadOnly, except for the checkbox column
                            dataGridView2.ReadOnly = false;  // Allow DataGridView to be editable
                            foreach (DataGridViewColumn column in dataGridView2.Columns)
                            {
                                if (column.Name != "NewOrder")
                                {
                                    column.ReadOnly = true; // Make all columns except "NewOrder" read-only
                                }
                            }


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

        void Load_LowStock_InventoryTableData()
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(CommonConnecString.ConnectionString))
                {
                    connect.Open();
                    string query = @"
                                    SELECT 
                                        i.InventoryID,
                                        d.DrugName, 
                                        d.Dosage, 
                                        d.CategoryID, 
                                        i.ExpireDate, 
                                        i.Quantity
                                    FROM Inventory i
                                    INNER JOIN Drug d ON i.Drug_ID = d.Drug_ID
                                    WHERE i.Quantity < 200;";  // Only get records where Quantity is less than 100

                    using (SqlCommand cmd = new SqlCommand(query, connect))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            DataTable table = new DataTable();
                            adapter.Fill(table);
                            dataGridView3.DataSource = table;
                            Console.WriteLine("Data Loaded Successfully3");

                            // Rename columns after setting the DataSource
                            #region Rename Column Names
                            foreach (DataGridViewColumn column in dataGridView3.Columns)
                            {
                                switch (column.Name)
                                {
                                    case "InventoryID":
                                        column.HeaderText = "Inventory ID";
                                        break;
                                    case "DrugName":
                                        column.HeaderText = "Drug Name";
                                        break;
                                    case "Dosage":
                                        column.HeaderText = "Dosage";
                                        break;
                                    case "CategoryID":
                                        column.HeaderText = "Category ID";
                                        break;
                                    case "ExpireDate":
                                        column.HeaderText = "Expiration Date";
                                        break;
                                    case "Quantity":
                                        column.HeaderText = "Quantity";
                                        break;

                                    default:
                                        break;
                                }
                            }
                            #endregion

                            // Add a Checkbox Column for "New Order"
                            DataGridViewCheckBoxColumn newOrderColumn = new DataGridViewCheckBoxColumn();
                            newOrderColumn.Name = "NewOrder";
                            newOrderColumn.HeaderText = "New Order";
                            newOrderColumn.FalseValue = false;
                            newOrderColumn.TrueValue = true;

                            // Add the checkbox column to the DataGridView as the first column
                            if (!dataGridView3.Columns.Contains("NewOrder"))
                            {
                                dataGridView3.Columns.Insert(0, newOrderColumn); // Insert at index 0 for the first column
                            }

                            // Ensure the entire DataGridView is ReadOnly, except for the checkbox column
                            dataGridView3.ReadOnly = false;  // Allow DataGridView to be editable
                            foreach (DataGridViewColumn column in dataGridView3.Columns)
                            {
                                if (column.Name != "NewOrder")
                                {
                                    column.ReadOnly = true; // Make all columns except "NewOrder" read-only
                                }
                            }
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

        private void NewOreder_btn_Click(object sender, EventArgs e)
        {
            TabPage selectedTab = tabControl1.SelectedTab;

            switch (selectedTab.Name)
            {
                case "tabPage_1":
                    // All
                    AddAllTab();
                    Console.WriteLine("All Tab Selected");
                    break;

                case "tabPage_2":
                    // Near Expiry
                    NearExpiryTab();
                    Console.WriteLine("Near Expiry Tab Selected");
                    break;
                case "tabPage_3":
                    // Low Stock
                    LowStockTab();
                    break;

                default:

                    MessageBox.Show("Invalide Tab.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }

        }
        private void Add_btn_Click(object sender, EventArgs e)
        {
            Add_Inventory addInventory = new Add_Inventory();
            addInventory.FormClosed += (s, args) =>
            {
                // Refresh the data after the form is closed
                Load_All_InventoryTableData();
                Load_nearExpiry_InventoryTableData();
                Load_LowStock_InventoryTableData();

            };
            addInventory.ShowDialog();
        }

        void AddAllTab()
        {
            List<int> selectedInventoryIDs = new List<int>(); // List to hold InventoryIDs of checked rows

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                // Check if the New Order checkbox is checked
                DataGridViewCheckBoxCell checkBoxCell = (DataGridViewCheckBoxCell)row.Cells["NewOrder"];
                if (checkBoxCell != null && Convert.ToBoolean(checkBoxCell.Value) == true)
                {
                    int inventoryID = Convert.ToInt32(row.Cells["InventoryID"].Value);
                    selectedInventoryIDs.Add(inventoryID);
                }
            }

            // Check if the list is not empty
            if (selectedInventoryIDs.Count > 0)
            {
                

                OrderList orderList = new OrderList(selectedInventoryIDs);
                orderList.FormClosed += (s, args) =>
                {
                    // Refresh the data after the form is closed
                    Load_All_InventoryTableData();
                };
                orderList.ShowDialog();
            }
            else
            {
                // Show an error message if the list is empty
                MessageBox.Show("Please select at least one item to proceed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
        void NearExpiryTab()
        {
            List<int> selectedInventoryIDs = new List<int>(); // List to hold InventoryIDs of checked rows

            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                // Check if the New Order checkbox is checked
                DataGridViewCheckBoxCell checkBoxCell = (DataGridViewCheckBoxCell)row.Cells["NewOrder"];
                if (checkBoxCell != null && Convert.ToBoolean(checkBoxCell.Value) == true)
                {
                    int inventoryID = Convert.ToInt32(row.Cells["InventoryID"].Value);
                    selectedInventoryIDs.Add(inventoryID);
                }
            }

            // Check if the list is not empty
            if (selectedInventoryIDs.Count > 0)
            {

                OrderList orderList = new OrderList(selectedInventoryIDs);
                orderList.FormClosed += (s, args) =>
                {
                    // Refresh the data after the form is closed
                    Load_nearExpiry_InventoryTableData();
                };
                orderList.ShowDialog();
            }
            else
            {
                // Show an error message if the list is empty
                MessageBox.Show("Please select at least one item to proceed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
        void LowStockTab()
        {
            List<int> selectedInventoryIDs = new List<int>(); // List to hold InventoryIDs of checked rows

            foreach (DataGridViewRow row in dataGridView3.Rows)
            {
                // Check if the New Order checkbox is checked
                DataGridViewCheckBoxCell checkBoxCell = (DataGridViewCheckBoxCell)row.Cells["NewOrder"];
                if (checkBoxCell != null && Convert.ToBoolean(checkBoxCell.Value) == true)
                {
                    int inventoryID = Convert.ToInt32(row.Cells["InventoryID"].Value);
                    selectedInventoryIDs.Add(inventoryID);
                }
            }

            // Check if the list is not empty
            if (selectedInventoryIDs.Count > 0)
            {

                OrderList orderList = new OrderList(selectedInventoryIDs);
                orderList.FormClosed += (s, args) =>
                {
                    // Refresh the data after the form is closed
                    Load_LowStock_InventoryTableData();
                };
                orderList.ShowDialog();
            }
            else
            {
                // Show an error message if the list is empty
                MessageBox.Show("Please select at least one item to proceed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        
    }
}
