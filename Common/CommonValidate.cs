using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Common
{
    public class CommonValidate
    {
        public static bool IsTelephone(string str_telephone)
        {

            Regex regex = new Regex("^1\\d{10}$");
            return regex.IsMatch(str_telephone);

        }

        public static bool IsEmail(string str_Email)
        {
            
           // Regex regex = new Regex("^([a-zA-Z0-9_-])+@([a-zA-Z0-9_-])+.([a-zA-Z0-9_-])+$");

            Regex regex = new Regex("^([A-Za-z0-9]{1}[A-Za-z0-9_]*)@([A-Za-z0-9_]+)[.]([A-Za-z0-9_]*)$");

            
            return regex.IsMatch(str_Email);
        }
    }
}
