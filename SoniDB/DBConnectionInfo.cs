using SharpHelpers.ExtensionMethods;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SoniDB
{
    public class DBConnectionInfo
    {
        public string DataSource { get; set; }
        //public string UserName { get; set; }
        //public string Password { get; set; }

        //public bool IsAuthRequired
        //{
        //    get
        //    {
        //        return UserName.IsNotNullOrWhiteSpace();
        //    }
        //}

        public DBConnectionInfo(string connectionString = null)
        {
            //TODO: Change logic with Regex?

            if (connectionString.IsNotNullOrWhiteSpace())
            {
                var parts = connectionString.Split(';');
                var dataSourcePart = parts.FirstOrDefault(p => p.StartsWith("datasource=", StringComparison.OrdinalIgnoreCase));
                if (dataSourcePart.IsNotNull())
                {
                    DataSource = dataSourcePart.Split('=')[1];
                }
            }
            else
            {
                DataSource = Assembly.GetExecutingAssembly().GetName().Name;
            }

            if (!Directory.Exists(DataSource))
                Directory.CreateDirectory(DataSource);
        }
    }
}
