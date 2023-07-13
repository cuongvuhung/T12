using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T12.Ult_Ext;

namespace T12.DTO
{
    internal class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }
        public User()
        {
            Id = 0;
            UserName = "No username";
            FullName = "No name";
            Password = "";
            Role = Role.Unavailable;
        }
        public User(int Id, string UserName, string FullName, string Password, Role Role)
        {
            this.Id = Id;
            this.UserName = UserName;
            this.FullName = FullName;
            this.Password = Password;
            this.Role = Role;
        }
        public override string ToString()
        {
            return Id + "," + UserName + "," + FullName + "," + Role;
        }
    }
}
