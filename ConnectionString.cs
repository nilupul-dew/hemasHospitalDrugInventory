using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyManagementSystem._Common
{
    public static class CommonConnecStringM
    {
        public static string ConnectionString
        {
            get
            {
                return @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\Programming\PMS\PharmacyManagementSystem\_Common\Database_M.mdf;Integrated Security=True";
            }
        }
    }

    public static class CommonConnecStringD
    {
        public static string ConnectionString
        {
            get
            {
                return @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\Programming\PMS\PharmacyManagementSystem\_Common\Database_M.mdf;Integrated Security=True";
            }
        }
    }
}