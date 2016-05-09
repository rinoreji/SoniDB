using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpHelpers.ExtensionMethods;
using Newtonsoft.Json;
using System.IO;

namespace SoniDB
{
    public class Database : IDisposable
    {
        private string _connectionName;
        public DBConnectionInfo ConnectionInfo { get; set; }

        public Database(string connectionName = null)
        {
            _connectionName = connectionName;

            InitDB();
        }

        private void InitDB()
        {
            ConnectionInfo = CreateDBConnectionInfo();
        }

        private DBConnectionInfo CreateDBConnectionInfo()
        {
            if (_connectionName.IsNullOrWhiteSpace())
                return new DBConnectionInfo();

            var connectionSettings = ConfigurationManager.ConnectionStrings[_connectionName];
            if (connectionSettings.IsNull())
                return new DBConnectionInfo();
            else
            {
                return new DBConnectionInfo(ConfigurationManager.ConnectionStrings[_connectionName].ConnectionString);
            }
        }

        public void Dispose()
        {
            //TODO: implement logic to clean up
        }

        //TODO: Keep only interface here, move implementation to seperate class Think OOP
        public Collection<T1> GetCollection<T1>()
        {
            var collectionName = "{0}_Collection".UseFormat(typeof(T1).Name);
            var fileFullname = Path.Combine(ConnectionInfo.DataSource, collectionName);
            if (File.Exists(fileFullname))
            {
                return new Serializer().Deserialize<Collection<T1>>(fileFullname);
            }
            else
            {
                var newCollection = new Collection<T1>();
                newCollection.Path = fileFullname;
                new Serializer().Serialize(fileFullname, newCollection);
                return newCollection;
            }
        }
    }
}