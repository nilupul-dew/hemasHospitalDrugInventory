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
    }
}
