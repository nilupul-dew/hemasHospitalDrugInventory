﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagementSystem._Common
{
    public static class CommonConnecString //Mithila
    {
        public static string ConnectionString
        {
            get
            {
                return @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\FLiX\Source\Repos\hemasHospitalDrugInventory\Database_SIM.mdf;Integrated Security=True";
            }
        }
    }

    public static class CommonConnecString_D
    {
        public static string ConnectionString
        {
            get
            {
                return @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\FLiX\Source\Repos\hemasHospitalDrugInventory\Database_SIM.mdf;Integrated Security=True";
            }
        }
    }


}