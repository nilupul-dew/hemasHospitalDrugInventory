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
            using (SqlConnection connect = new SqlConnection(CommonConnecString.ConnectionString))
            {
                connect.Open();

                string query = @"
            SELECT 
                w.Name, 
                COUNT(wm.WardManagerID) AS WardManagerCount
            FROM 
                Ward w
            LEFT JOIN 
                Ward_Manager wm ON w.WardID = wm.WardID
            GROUP BY 
                w.Name
            ORDER BY 
                w.Name";

                SqlCommand cmd = new SqlCommand(query, connect);
                SqlDataReader reader = cmd.ExecuteReader();

                // Assuming you have a chart control named 'chartWardManagers'
                chart1.Series.Clear(); // Clear any existing series
                Series series = new Series
                {
                    Name = "Ward Managers",
                    IsValueShownAsLabel = true, // Show the count as labels on the chart
                    ChartType = SeriesChartType.Column // Column chart for X, Y axis display
                };

                chart1.Series.Add(series);

                while (reader.Read())
                {
                    string wardName = reader["Name"].ToString();
                    int managerCount = Convert.ToInt32(reader["WardManagerCount"]);

                    series.Points.AddXY(wardName, managerCount); // X-axis = WardName, Y-axis = Count
                }

                reader.Close();
            }
        }
    }
}
