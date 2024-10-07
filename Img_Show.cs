using PharmacyManagementSystem._Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace hemasHospitalDrugInventory
{
    public partial class Img_Show : Form
    {
        public Img_Show()
        {
            InitializeComponent();
            LoadData();
        }

        private void Load_btn_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(CommonConnecString.ConnectionString))
            {
                int imageId = 1;
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT ImageData FROM ImagesTable WHERE ID = @id", conn);
                cmd.Parameters.AddWithValue("@id", imageId);

                // Execute the command and read the image data
                byte[] imageData = (byte[])cmd.ExecuteScalar();

                if (imageData != null)
                {
                    // Convert byte array to image and display it in PictureBox
                    using (MemoryStream ms = new MemoryStream(imageData))
                    {
                        pictureBox1.Image = System.Drawing.Image.FromStream(ms); // Use fully qualified name
                    }
                }
                else
                {
                    MessageBox.Show("No image found for the given ID.");
                }
            }
        }

        private void LoadData()
        {
            using (SqlConnection conn = new SqlConnection(CommonConnecString.ConnectionString))
            {
                try
                {
                    conn.Open();

                    // SQL query to select data from the table
                    string query = "SELECT ID, Name, Age FROM Person";

                    SqlDataAdapter dataAdapter = new SqlDataAdapter(query, conn);
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);

                    // Add a checkbox column (unbound column)
                    DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn
                    {
                        HeaderText = "Select",
                        Width = 50,
                        Name = "checkBoxColumn"
                    };

                    // Add the checkbox column to DataGridView
                    dataGridView1.Columns.Add(checkBoxColumn);

                    // Bind the database data to DataGridView
                    dataGridView1.DataSource = dataTable;

                    // Set the DataGridView properties
                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void select_btn_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear(); // Clear previous items

            List<int> selectedPrimaryKeys = new List<int>();

            // Loop through the DataGridView rows
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                // Check if the checkbox is checked
                if (Convert.ToBoolean(row.Cells["checkBoxColumn"].Value))
                {
                    // Add the primary key (ID) to the list
                    int primaryKey = Convert.ToInt32(row.Cells["ID"].Value);
                    selectedPrimaryKeys.Add(primaryKey);

                    // Add the primary key to the ListBox
                    listBox1.Items.Add(primaryKey);
                }
            }

            if (selectedPrimaryKeys.Count > 0)
            {
                MessageBox.Show("Selected Primary Keys added to ListBox.");
            }
            else
            {
                MessageBox.Show("No rows selected.");
            }
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["Age"].Index) // Check if it's the Age column
            {
                // Get the new Age value and the corresponding ID
                int id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["ID"].Value);
                int newAge = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Age"].Value);

                // Update the Age in the database
                UpdateAgeInDatabase(id, newAge);
            }
        }

        // Method to update Age in the database
        private void UpdateAgeInDatabase(int id, int newAge)
        {
            using (SqlConnection conn = new SqlConnection(CommonConnecString.ConnectionString))
            {
                conn.Open();
                string query = "UPDATE Person SET Age = @newAge WHERE ID = @id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@newAge", newAge);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
