using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
        }

        void LoadGraph()
        {
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
    }
}
