using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T12
{
    internal class UserServices
    {
        private List<User> users = new List<User>();
        //public User UserLogin = new User();

        public void GetData()
        {
            DLA dla = new DLA();
            string str = "Select,USERS";
            List<SqlDataReader> data = dla.SQLQuery(str);

            foreach (SqlDataReader dr in data)
            {
                User item = new();
                item.Id = (int)dr["Id"];
                item.UserName = (string)dr["username"];
                item.FullName = (string)dr["fulname"];
                item.Password = (string)dr["Password"];
                item.Role = (Role)dr["role"];
                users.Add(item);
            }
        }
        public User checkUser(string username, string password)
        {
            User item = new();
            item.UserName = username;
            item.Password = password;
            string str = "Select,username," + username + ",password," + password;
            DLA dla = new DLA();
            List<SqlDataReader> data = dla.SQLQuery(str);
            if (data.Count > 0) 
            {
                item.Id = (int)data[0]["Id"];                
                item.FullName = (string)data[0]["fulname"];                
                item.Role = (Role)data[0]["role"];                
            }
            return item;
        }
        public int Add (User user) 
        {
            string str = "Insert,USERS,";
            str += user.UserName + ',';
            str += user.FullName + ',';
            str += user.Password + ',';
            str += Convert.ToInt16(user.Role);
            DLA dla = new DLA();
            return dla.SQLExecute(str);
        }
        public int Update(User user)
        {
            string str = "Update,USERS,";
            str += user.Id + ","; 
            if (user.UserName != "") { str += "username," + user.UserName + ','; }
            if (user.FullName != "") { str += "fullname," + user.FullName + ','; }
            if (user.Password != "") { str += "password," + user.Password + ','; }
            if (user.Role != Role.Unavailable) { str += "Role," + (Convert.ToInt16(user.Role)); }
            DLA dla = new DLA();
            return dla.SQLExecute(str);
        }
        public int Delete(User user)
        {
            string str = "Delete,USERS,";
            str += user.Id + ",";
            if (user.UserName != "") { str += "username," + user.UserName + ','; }
            if (user.FullName != "") { str += "fullname," + user.FullName + ','; }
            if (user.Password != "") { str += "password," + user.Password + ','; }
            if (user.Role != Role.Unavailable) { str += "Role," + (Convert.ToInt16(user.Role)); }
            DLA dla = new DLA();
            return dla.SQLExecute(str);
        }
        public List<User> sortedUserList() 
        {
            return users.OrderByDescending(x => x.FullName).ToList();
        }
        public List<User> searchList(string name) 
        {
            string str = "Select,USERS,Username," + name;
            DLA dla = new DLA();
            List<User> result = new List<User>();
            List<SqlDataReader> data = dla.SQLQuery(str);
            foreach (SqlDataReader dr in data)
            {
                User item = new();
                item.Id = (int)dr["Id"];
                item.UserName = (string)dr["Username"];
                item.FullName = (string)dr["Username"];
                item.Password = (string)dr["Password"];
                item.Role = (Role)dr["Role"];
                result.Add(item);
            }
            return result;
        }
        public void ChangeUserPassword(int id,string newpwd)
        {            
            string str = "Update,USERS," + id + "," + newpwd;
            DLA dla = new DLA();
            dla.SQLExecute(str);
        }
    }
}
