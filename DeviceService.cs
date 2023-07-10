using System.Data.SqlClient;

namespace T12
{
    internal class DeviceServices
    {
        private List<Device> devices = new List<Device>();        

        public void GetData()
        {
            DLA dla = new DLA();
            string str = "Select,DEVICES";
            List<SqlDataReader> data = dla.SQLQuery(str);

            foreach (SqlDataReader dr in data)
            {
                Device item = new();
                item.Id = (int)dr["Id"];
                item.Name = (string)dr["Name"];
                item.Quantity = (int)dr["Quantity"];                
                devices.Add(item);
            }
        }
        public int Add(Device device)
        {
            string str = "Insert,DEVICES,";
            str += device.Name + ',';
            str += device.Quantity + ',';
            DLA dla = new DLA();
            return dla.SQLExecute(str);
        }
        public int Update(Device device)
        {
            string str = "Update,DEVICES,";
            str += device.Id + ",";
            if (device.Name != "") { str += "name," + device.Name + ','; }
            if (device.Quantity != 0) { str += "quantity," + device.Quantity; }
            DLA dla = new DLA();
            return dla.SQLExecute(str);
        }
        public int Delete(Device device)
        {
            string str = "Delete,DEVICES,";
            str += device.Id + ",";
            if (device.Name != "") { str += "username," + device.Name + ','; }
            if (device.Quantity != 0) { str += "quantity," + device.Quantity; }
            DLA dla = new DLA();
            return dla.SQLExecute(str);
        }
        public List<Device> list()
        {
            return list().OrderByDescending(x => x.Name).ToList();
        }
        public List<Device> searchList(string name)
        {
            string str = "Select,DEVICES,Username," + name;
            DLA dla = new DLA();
            List<Device> result = new List<Device>();
            List<SqlDataReader> data = dla.SQLQuery(str);
            foreach (SqlDataReader dr in data)
            {
                Device item = new();
                item.Id = (int)dr["Id"];
                item.Name = (string)dr["Name"];
                item.Quantity = (int)dr["Quantity"];
                result.Add(item);
            }
            return result;
        }
    }
}
