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
            List<string> DrugNamesList = new List<string>();
            List<int> quantityList = new List<int>();

            try
            {
                using (SqlConnection connect = new SqlConnection(CommonConnecString.ConnectionString))
                {
                    connect.Open();

                    string query = @"
                             SELECT 
                            d.DrugName AS DrugName,
                            SUM(ISNULL(i.Quantity, 0)) AS Quantity
                            FROM Drug d
                            LEFT JOIN Inventory i ON d.Drug_ID = i.Drug_ID
                            GROUP BY d.DrugName;";

                    using (SqlCommand cmd = new SqlCommand(query, connect))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();

                        // Reading data from the database
                        while (reader.Read())
                        {
                            string DrugName = reader["DrugName"].ToString();
                            int Quantity = Convert.ToInt32(reader["Quantity"]);

                            // Add to lists
                            DrugNamesList.Add(DrugName);
                            quantityList.Add(Quantity);
                        }
                    }
                }

                // Convert lists to arrays
                string[] DrugNamesArray = DrugNamesList.ToArray();
                int[] quantityArray = quantityList.ToArray();

                // Now you can use the arrays like:
                // string[] categories = { "Category 1", "Category 2", "Category 3" };
                // int[] values = { 10, 20, 15 };

                // Clear any existing series and chart areas
                chart1.Series.Clear();
                chart1.ChartAreas.Clear();

                // Add a new chart area and series
                chart1.ChartAreas.Add("chartArea1");
                chart1.Series.Add("Drug Count");

                // Bind data to the series
                chart1.Series["Drug Count"].Points.DataBindXY(DrugNamesArray, quantityArray);

                // Customize chart appearance
                chart1.ChartAreas[0].AxisX.Title = "Drug Name";
                chart1.ChartAreas[0].AxisY.Title = "Availble Quantity";
                chart1.Series["Drug Count"].ChartType = SeriesChartType.Bar;

                chart1.ChartAreas[0].AxisX.Interval = 1;  // Ensure all labels are shown
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
