using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T12.DAL
{
    public class Config
    {
        public string conStr;
        public Config()
        {
            FileStream fileStream = new("config.cfg", FileMode.OpenOrCreate);
            StreamReader streamReader = new(fileStream);
            string dataSource = streamReader.ReadLine() + "";
            string catalog = streamReader.ReadLine() + "";
            string user = streamReader.ReadLine() + "";
            string password = streamReader.ReadLine() + "";
            conStr = dataSource + catalog + user + password;
            streamReader.Close();
        }
    }
}
