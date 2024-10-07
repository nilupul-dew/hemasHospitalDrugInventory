using PharmacyManagementSystem._Common;
using System.IO;
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
using static hemasHospitalDrugInventory.Inventory.Update_Inventory;

namespace hemasHospitalDrugInventory.Inventory
{
    public partial class Add_Inventory : Form
    {
        public Add_Inventory()
        {
            InitializeComponent();
            LoadCategories(); // Load categories on form initialization
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

        private string selectedImagePath; // Store the selected image path
        private void Add_btn_Click(object sender, EventArgs e)
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

                // Proceed with insert, no need for Drug_ID in the insert statement
                string insertQuery = @"
            INSERT INTO Drug (DrugName, Dosage, CategoryID, Manufacturer, StorageLocation, PricePerUnit, DrugImage)
            VALUES (@DrugName, @Dosage, @CategoryID, @Manufacturer, @Storage, @PricePerUnit, @DrugImage)";

                using (SqlCommand cmd = new SqlCommand(insertQuery, connect))
                {
                    // No Drug_ID parameter needed since it's an identity column
                    cmd.Parameters.AddWithValue("@DrugName", txt_name.Text);
                    cmd.Parameters.AddWithValue("@Dosage", txt_dosage.Text);
                    cmd.Parameters.AddWithValue("@CategoryID", categoryId); // Use the selected CategoryID
                    cmd.Parameters.AddWithValue("@Manufacturer", txt_manufacturer.Text);
                    cmd.Parameters.AddWithValue("@Storage", txt_storage.Text);
                    cmd.Parameters.AddWithValue("@PricePerUnit", txt_price_per_unit.Text);

                    // Add the image to the parameters
                    if (!string.IsNullOrEmpty(selectedImagePath))
                    {
                        byte[] imageData = File.ReadAllBytes(selectedImagePath);
                        cmd.Parameters.AddWithValue("@DrugImage", imageData);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@DrugImage", DBNull.Value); // If no image is selected
                    }

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Drug inserted successfully!");
                        ClearFormFields(); // Clear fields after successful insertion
                    }
                    else
                    {
                        MessageBox.Show("Failed to insert the drug.");
                    }
                }
            }
        }

        private void ClearFormFields()
        {
            txt_name.Clear();
            txt_dosage.Clear();
            txt_manufacturer.Clear();
            txt_storage.Clear();
            txt_price_per_unit.Clear();
            comboBox_category.SelectedIndex = -1; // Clear the ComboBox selection
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

        private void Img_select_btn_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Display the selected image in the PictureBox
                    pictureBox2.Image = new Bitmap(openFileDialog.FileName);
                    // Store the selected image file path
                    selectedImagePath = openFileDialog.FileName;
                }
            }
        }
    }
}
