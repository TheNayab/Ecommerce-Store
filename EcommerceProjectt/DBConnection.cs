using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EcommerceProjectt
{
    public class DBConnection
    {
         public static string server = "localhost";
         public static string database = "ecommercewebproject";
         public static string username = "root";
         public static string password = "Nayab10033";
                       
         public static string conString = "server=" + server + ";" + "database=" + database + ";" + "uid=" + username + ";" + "password=" + password + ";";
    }                  
}