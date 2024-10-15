using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Video.DirectShow;
using AForge.Video;
using ZXing;

namespace hemasHospitalDrugInventory
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        FilterInfoCollection filter;
        VideoCaptureDevice captureDevice;

        private void Form1_Load(object sender, EventArgs e)
        {
            // Get available camera devices and add them to the combobox
            filter = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo filterInfo in filter)
            {
                comboBox_camera.Items.Add(filterInfo.Name);
            }
            // Set default camera to the first in the list
            comboBox_camera.SelectedIndex = 0;
        }

        private void button_scan_Click(object sender, EventArgs e)
        {
            // Initialize the selected capture device
            captureDevice = new VideoCaptureDevice(filter[comboBox_camera.SelectedIndex].MonikerString);
            captureDevice.NewFrame += CaptureDevice_NewFrame;
            captureDevice.Start();  // Start the camera
            timer1.Start();  // Start the timer for scanning
        }

        private void CaptureDevice_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            // Display the video feed in pictureBox
            pictureBox.Image = (Bitmap)eventArgs.Frame.Clone();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Stop the capture device when the form closes to avoid locking the camera
            if (captureDevice != null && captureDevice.IsRunning)
            {
                captureDevice.Stop();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (pictureBox.Image != null)
            {
                try
                {
                    // Try to decode the image
                    BarcodeReader barcode = new BarcodeReader();
                    Result result = barcode.Decode((Bitmap)pictureBox.Image);

                    // If a result is found, display it in the textBox
                    if (result != null)
                    {
                        // Use Invoke to update UI safely from another thread
                        this.Invoke(new MethodInvoker(delegate ()
                        {
                            textBox.Text = result.Text;  // Display result in textBox
                        }));

                        // Optionally stop the scan if one result is enough
                        timer1.Stop();
                        if (captureDevice.IsRunning)
                        {
                            captureDevice.Stop();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
        }
    }
}
