using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hemasHospitalDrugInventory
{
    public class CommonConnecString
    {
        public static string ConnectionString
        {
            get
            {
                return @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\FLiX\source\repos\hemasHospitalDrugInventory\Database_SIM.mdf;Integrated Security=True";
            }
        }
    }
}
