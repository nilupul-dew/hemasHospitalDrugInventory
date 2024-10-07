using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hemasHospitalDrugInventory
{
    internal class ConnectString
    {
        public static string ConnectionString
        {
            get
            {
                return @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\User\source\repos\hemasHospitalDrugInventory\Database_SIM.mdf;Integrated Security=True";
            }
        }
    }
}
