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
using System.Windows.Forms.DataVisualization.Charting;

namespace hemasHospitalDrugInventory.Home
{
    public partial class UC_Home : UserControl
    {
        public UC_Home()
        {
            InitializeComponent();
            LoadWardManagerChart();
        }

        public void LoadWardManagerChart()
        {
            List<string> wardNamesList = new List<string>();
            List<int> managerCountList = new List<int>();

            try
            {
                using (SqlConnection connect = new SqlConnection(CommonConnecString.ConnectionString))
                {
                    connect.Open();

                    string query = @"
                             SELECT 
                             w.Name AS WardName,
                            COUNT(wm.WardManagerID) AS WardManagerCount
                            FROM Ward w
                            LEFT JOIN Ward_Manager wm ON w.WardID = wm.WardID
                            GROUP BY w.Name;";

                    using (SqlCommand cmd = new SqlCommand(query, connect))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();

                        // Reading data from the database
                        while (reader.Read())
                        {
                            string wardName = reader["WardName"].ToString();
                            int managerCount = Convert.ToInt32(reader["WardManagerCount"]);

                            // Add to lists
                            wardNamesList.Add(wardName);
                            managerCountList.Add(managerCount);
                        }
                    }
                }

                // Convert lists to arrays
                string[] wardNamesArray = wardNamesList.ToArray();
                int[] managerCountArray = managerCountList.ToArray();

                // Now you can use the arrays like:
                // string[] categories = { "Category 1", "Category 2", "Category 3" };
                // int[] values = { 10, 20, 15 };

                // Clear any existing series and chart areas
                chart1.Series.Clear();
                chart1.ChartAreas.Clear();

                // Add a new chart area and series
                chart1.ChartAreas.Add("chartArea1");
                chart1.Series.Add("Series1");

                // Bind data to the series
                chart1.Series["Series1"].Points.DataBindXY(wardNamesArray, managerCountArray);

                // Customize chart appearance
                chart1.ChartAreas[0].AxisX.Title = "Ward Name";
                chart1.ChartAreas[0].AxisY.Title = "Ward Manager Count";
                chart1.Series["Series1"].ChartType = SeriesChartType.Bar;

                chart1.ChartAreas[0].AxisX.Interval = 1;  // Ensure all labels are shown
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
