using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T12.DAL;
using T12.DTO;
using T12.Ult_Ext;

namespace T12
{
    internal class UserServices
    {
        private List<User> users = new ();
        ///////////////////////////////////////////////////////////////////////////////////////////////////
        // Get data form database
        public void GetData()
        {
            DBService dla = new ();
            string str = "Select,USERS";
            List<string> rows = dla.SQLQuery(str);

            foreach (string row in rows)
            {
                User item = new()
                {
                    Id = Convert.ToInt32(row.Split(",")[0]),
                    UserName = (string)row.Split(",")[1],
                    FullName = (string)row.Split(",")[2],
                    Password = (string)row.Split(",")[3],
                    Role = (Role)Convert.ToInt32(row.Split(",")[4])
                };
                users.Add(item);
            }
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////
        // Check user login
        public User CheckUser(string username, string password)
        {
            password = Utils.Hash(password);            
            User item = new()            
            {
                UserName = username,
                Password = password
            };
            string str = "Select,USERS,username," + username + ",password," + password;
            //Console.WriteLine(str);
            DBService dla = new();
            List<string> rows = dla.SQLQuery(str);
            foreach (string row in rows) 
            {
                //Console.WriteLine(row);Console.ReadKey();                
                item.Id = Convert.ToInt32(row.Split(",")[0]);                
                item.FullName = (string)row.Split(",")[2];
                item.Role = (Role)Convert.ToInt32(row.Split(",")[4]);
            }
            return item;
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////
        // Add new user
        public int AddNew(User user)
        {
            user.Password = Utils.Hash(user.Password);
            string str = "Insert,USERS,";
            str += user.UserName + ',';
            str += user.FullName + ',';
            str += user.Password + ',';
            str += Convert.ToInt32(user.Role);
            DBService dla = new();
            return dla.SQLExecute(str);
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////
        // Update user
        public int Update(User user)
        {
            user.Password = Utils.Hash(user.Password);
            string str = "Update,USERS,";
            str += user.Id + ","; 
            if (user.UserName != "") { str += "username," + user.UserName + ','; }
            if (user.FullName != "") { str += "fullname," + user.FullName + ','; }
            if (user.Password != "") { str += "password," + user.Password + ','; }
            if (user.Role != Role.Unavailable) { str += "Role," + (Convert.ToInt32(user.Role)); }
            DBService dla = new();
            return dla.SQLExecute(str);
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////
        // Delete user
        public int Delete(User user)
        {
            string str = "Delete,USERS,Id,";
            str += user.Id;                        
            DBService dla = new();
            return dla.SQLExecute(str);
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////
        // List a sort list of user
        public List<User> SortedUserList() 
        {            
            return users.OrderByDescending(x => x.FullName).ToList();
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////
        // Search a list of user
        public List<User> SearchListByName(string name) 
        {
            string str = "Select,USERS,Username," + name;
            DBService dla = new();
            List<User> result = new();
            List<String> rows = dla.SQLQuery(str);
            foreach (String row in rows)
            {
                User item = new()
                {
                    Id = Convert.ToInt32(row.Split(",")[0]),
                    UserName = row.Split(",")[1],
                    FullName = row.Split(",")[2],
                    Password = row.Split(",")[3],
                    Role = (Role)Convert.ToInt32(row.Split(",")[4])
                };                
                result.Add(item);
            }
            return result;
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////
        // Change password for user
        public void ChangeUserPassword(int id,string newpwd)
        {
            newpwd = Utils.Hash(newpwd);
            string str = "Update,USERS," + id + ",password," + newpwd;
            DBService dla = new();
            dla.SQLExecute(str);
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////
    }
}
