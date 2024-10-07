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
    public partial class Update_Inventory : Form
    {
        private int invent_Id = 1;
        public Update_Inventory()
        {
            InitializeComponent();
            LoadCategories(); // Load categories on form initialization
            LoadTable(); // Load the inventory table data


        }

        void LoadTable()
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(CommonConnecString.ConnectionString))
                {
                    connect.Open();

                    string query = @"
                        SELECT                    
                        d.Drug_ID,
                        d.DrugName,
                        d.Dosage,
                        d.CategoryID,  -- Fetch CategoryID to select the right item in ComboBox
                        c.CategoryName,
                        d.Manufacturer,
                        d.StorageLocation,
                        d.PricePerUnit
                        FROM Inventory i
                        INNER JOIN Drug d ON i.Drug_ID = d.Drug_ID 
                        INNER JOIN Category c ON d.CategoryID = c.CategoryID  
                        WHERE i.InventoryID = @InventoryID";

                    using (SqlCommand cmd = new SqlCommand(query, connect))
                    {
                        cmd.Parameters.AddWithValue("@InventoryID", invent_Id);

                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            txt_id.Text = reader["Drug_ID"].ToString();
                            txt_name.Text = reader["DrugName"].ToString();
                            txt_dosage.Text = reader["Dosage"].ToString();
                            txt_manufacturer.Text = reader["Manufacturer"].ToString();
                            txt_storage.Text = reader["StorageLocation"].ToString();
                            txt_price_per_unit.Text = reader["PricePerUnit"].ToString();

                            // Retrieve CategoryID from the reader to select the right ComboBox item
                            int existingCategoryId = reader.GetInt32(reader.GetOrdinal("CategoryID"));

                            // Find the existing category in the ComboBox and select it
                            foreach (ComboBoxItem item in comboBox_category.Items)
                            {
                                if (item.Id == existingCategoryId)
                                {
                                    comboBox_category.SelectedItem = item; // Set the existing category as selected
                                    break; // Exit the loop once the item is found
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ClearFormFields()
        {
            txt_id.Clear();
            txt_name.Clear();
            txt_dosage.Clear();
            txt_manufacturer.Clear();
            txt_storage.Clear();
            txt_price_per_unit.Clear();
            comboBox_category.SelectedIndex = -1; // Clear the ComboBox selection
        }

        private void Delete_btn_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete this record?", "Confirmation", MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.Yes)
            {
                using (SqlConnection connect = new SqlConnection(CommonConnecString.ConnectionString))
                {
                    connect.Open();

                    // Query to delete the record from the Drug table
                    string deleteQuery = "DELETE FROM Drug WHERE Drug_ID = @DrugID";

                    using (SqlCommand cmd = new SqlCommand(deleteQuery, connect))
                    {
                        // Bind the DrugID parameter
                        cmd.Parameters.AddWithValue("@DrugID", txt_id.Text);

                        // Execute the delete query
                        int rowsAffected = cmd.ExecuteNonQuery();

                        // Show success or error message
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Data deleted successfully!");

                            // Optionally, clear the form fields after deletion
                            ClearFormFields();
                        }
                        else
                        {
                            MessageBox.Show("Failed to delete the data.");
                        }
                    }
                }
            }
        }
        private void LoadCategories()
        {
            using (SqlConnection connect = new SqlConnection(CommonConnecString.ConnectionString))
            {
                connect.Open();
                string query = "SELECT CategoryID, CategoryName FROM Category";

                using (SqlCommand cmd = new SqlCommand(query, connect))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        // Add each category to the ComboBox
                        comboBox_category.Items.Add(new ComboBoxItem
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1)
                        });
                    }
                }
            }
        }

        // Helper class to store category ID and Name in the ComboBox
        public class ComboBoxItem
        {
            public int Id { get; set; }
            public string Name { get; set; }

            public override string ToString()
            {
                return Name; // This is what will be displayed in the ComboBox
            }
        }

        private void Update_btn_Click(object sender, EventArgs e)
        {
            using (SqlConnection connect = new SqlConnection(CommonConnecString.ConnectionString))
            {
                connect.Open();

                // Get the selected category ID
                if (comboBox_category.SelectedItem == null)
                {
                    MessageBox.Show("Please select a category.");
                    return;
                }

                ComboBoxItem selectedCategory = (ComboBoxItem)comboBox_category.SelectedItem;
                int categoryId = selectedCategory.Id; // Get the selected CategoryID

                // Proceed with update
                string updateQuery = @"
        UPDATE Drug
        SET 
            DrugName = @DrugName,
            Dosage = @Dosage,
            CategoryID = @CategoryID,
            Manufacturer = @Manufacturer,
            StorageLocation = @Storage,
            PricePerUnit = @PricePerUnit
        WHERE Drug_ID = @DrugID";

                using (SqlCommand cmd = new SqlCommand(updateQuery, connect))
                {
                    cmd.Parameters.AddWithValue("@DrugID", txt_id.Text);
                    cmd.Parameters.AddWithValue("@DrugName", txt_name.Text);
                    cmd.Parameters.AddWithValue("@Dosage", txt_dosage.Text);
                    cmd.Parameters.AddWithValue("@CategoryID", categoryId); // Use the retrieved CategoryID
                    cmd.Parameters.AddWithValue("@Manufacturer", txt_manufacturer.Text);
                    cmd.Parameters.AddWithValue("@Storage", txt_storage.Text);
                    cmd.Parameters.AddWithValue("@PricePerUnit", txt_price_per_unit.Text);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Data updated successfully!");
                    }
                    else
                    {
                        MessageBox.Show("Failed to update the data.");
                    }
                }
            }
        }
    }
}
