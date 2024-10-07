using hemasHospitalDrugInventory.Employees.E_Usercontrols;
using hemasHospitalDrugInventory.Home;
using hemasHospitalDrugInventory.Inventory.I_UserControls;
using hemasHospitalDrugInventory.Suppliers.S_Usercontrols;
using hemasHospitalDrugInventory.Transactions.UserControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hemasHospitalDrugInventory.Suppliers
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            InitializeUserControls();
        }

        void InitializeUserControls()
        {
            // Initialize user controls
            T_UC_Main t_UC_Main = new T_UC_Main();
            S_UC_Index s_UC_Index = new S_UC_Index();
            I_UC_Main i_UC_Main = new I_UC_Main();
            UC_Home uc_Home = new UC_Home();

            // Set the DockStyle to fill so it occupies the entire panel
            t_UC_Main.Dock = DockStyle.Fill;
            s_UC_Index.Dock = DockStyle.Fill;
            i_UC_Main.Dock = DockStyle.Fill;
            uc_Home.Dock = DockStyle.Fill;

            // Add user controls to the main panel (panelMain)
            panel2.Controls.Add(t_UC_Main);
            panel2.Controls.Add(s_UC_Index);
            panel2.Controls.Add(i_UC_Main);
            panel2.Controls.Add(uc_Home);

            // Show the first user control initially
            i_UC_Main.BringToFront();
        }

        private void employee_btn_Click(object sender, EventArgs e)
        {
           

        }

        private void transaction_btn_Click(object sender, EventArgs e)
        {

        }
    }
}
