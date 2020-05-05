using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Text;

namespace SaccoSystem.Models
{
    public static class DataConfigs
    {
        public static string MailFromAddress = ConfigurationManager.AppSettings["MailFromAddress"];
        public static string MailUserName = ConfigurationManager.AppSettings["MailUsername"];
        public static string MailDisplayName = ConfigurationManager.AppSettings["MailDisplayName"];
        public static string MailPassword = ConfigurationManager.AppSettings["MailPassword"];
        public static string MailServer = ConfigurationManager.AppSettings["MailServer"];
        public static string MailPort = ConfigurationManager.AppSettings["MailPort"];


        public static string GetRandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }

    }
}