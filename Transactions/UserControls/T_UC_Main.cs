﻿using PharmacyManagementSystem._Common;
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

namespace hemasHospitalDrugInventory.Transactions.UserControls
{
    public partial class T_UC_Main : UserControl
    {
        public T_UC_Main()
        {
            InitializeComponent();
            LoadOrderTableData();
            LoadOrderedList();
        }

        void LoadOrderTableData()
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(CommonConnecString.ConnectionString))
                {
                    connect.Open();
                    string query = @"
                                SELECT 
                                    oe.OrderEventID,
                                    s.Name AS SupplierName,
                                    oe.OrderedDate,
                                    oe.OrderStatus
                                FROM OrderEvent oe
                                INNER JOIN Supplier s ON oe.SupplierID = s.SupplierID;";

                    using (SqlCommand cmd = new SqlCommand(query, connect))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            DataTable table = new DataTable();
                            adapter.Fill(table);
                            dataGridView1.DataSource = table;
                            Console.WriteLine("Data Loaded Successfully");

                            // Rename columns after setting the DataSource
                            #region Rename Column Names
                            foreach (DataGridViewColumn column in dataGridView1.Columns)
                            {
                                switch (column.Name)
                                {
                                    case "OrderEventID":
                                        column.HeaderText = "Order Event ID";
                                        break;
                                    case "SupplierName":
                                        column.HeaderText = "Supplier Name";
                                        break;
                                    case "OrderedDate":
                                        column.HeaderText = "Ordered Date";
                                        break;
                                    case "OrderStatus":
                                        column.HeaderText = "Order Status";
                                        break;
                                    default:
                                        break;
                                }
                            }
                            #endregion
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

        }

        void LoadOrderedList()
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(CommonConnecString.ConnectionString))
                {
                    connect.Open();
                    string query = @"
                                SELECT 
                                    o.OrderID,
                                    d.DrugName,
                                    d.Dosage,
                                    d.CategoryID,
                                    oe.OrderedDate,
                                    o.Quantity
                                FROM [Order] o
                                INNER JOIN Drug d ON o.Drug_ID = d.Drug_ID
                                INNER JOIN OrderEvent oe ON o.OrderEventID = oe.OrderEventID
                                WHERE oe.IsDelivered = 1;";

                    using (SqlCommand cmd = new SqlCommand(query, connect))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            DataTable table = new DataTable();
                            adapter.Fill(table);
                            dataGridView2.DataSource = table;
                            Console.WriteLine("Data Loaded Successfully into dataGridView2");

                            // Rename columns after setting the DataSource
                            #region Rename Column Names
                            foreach (DataGridViewColumn column in dataGridView2.Columns)
                            {
                                switch (column.Name)
                                {
                                    case "OrderID":
                                        column.HeaderText = "Order ID";
                                        break;
                                    case "DrugName":
                                        column.HeaderText = "Drug Name";
                                        break;
                                    case "Dosage":
                                        column.HeaderText = "Dosage";
                                        break;
                                    case "CategoryID":
                                        column.HeaderText = "Category ID";
                                        break;
                                    case "OrderedDate":
                                        column.HeaderText = "Ordered Date";
                                        break;
                                    case "Quantity":
                                        column.HeaderText = "Quantity";
                                        break;
                                    default:
                                        break;
                                }
                            }
                            #endregion
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error:" + ex, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }



    }

}
