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
using System.Windows.Forms.DataVisualization.Charting;
using System.Xml.Linq;

namespace hemasHospitalDrugInventory
{
    public partial class BarChart : Form
    {
        public BarChart()
        {
            InitializeComponent();

            // Example data
            string[] categories = { "ICU", "Surgical Ward", "CCU", "ISaolation ward", "Burned Unit", "Neurogy Ward", "Psychiatric Ward" };
            int[] values = { 300, 250, 600, 100, 150, 200, 900 };

            // Clear any existing series and chart areas
            chart1.Series.Clear();
            chart1.ChartAreas.Clear();

            // Add a new chart area and series
            chart1.ChartAreas.Add("chartArea1");
            chart1.Series.Add("Series1");

            // Bind data to the series
            chart1.Series["Series1"].Points.DataBindXY(categories, values);

            // Customize chart appearance
            chart1.ChartAreas[0].AxisX.Title = "Categories";
            chart1.ChartAreas[0].AxisY.Title = "Values";
            chart1.Series["Series1"].ChartType = SeriesChartType.Bar;
        }

        private void Img_select_btn_Click(object sender, EventArgs e)
        {
            // Open file dialog to select an image
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Display selected image in PictureBox
                pictureBox1.Image = Image.FromFile(openFileDialog.FileName);
            }
        }

        private void Img_save_btn_Click(object sender, EventArgs e)
        {
            // Ensure an image is selected
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("Please select an image.");
                return;
            }

            // Convert image to byte array
            byte[] imageBytes;
            using (MemoryStream ms = new MemoryStream())
            {
                pictureBox1.Image.Save(ms, pictureBox1.Image.RawFormat);
                imageBytes = ms.ToArray();
            }

            // Save image to database
            using (SqlConnection conn = new SqlConnection(CommonConnecString.ConnectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO ImagesTable (ImageName, ImageData) VALUES (@name, @data)", conn);

                cmd.Parameters.AddWithValue("@name", textBox1.Text);  // Image name from TextBox
                cmd.Parameters.AddWithValue("@data", imageBytes);  // Image byte data

                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("Image saved successfully.");
        }

        private void Next_btn_Click(object sender, EventArgs e)
        {
            Img_Show img = new Img_Show();
            img.FormClosed += (s, args) =>
            {
                this.Show();
            };
            this.Hide();
            img.Show();
        }
    }
}
