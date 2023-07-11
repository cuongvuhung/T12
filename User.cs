using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T12
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
            this.Id = 0;
            this.UserName = "No username";
            this.FullName = "No name";
            this.Password = "";
            this.Role = Role.Unavailable;
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
            return Id + "," + UserName + "," + FullName + "," + Password + "," + Role;
        }
    }
}
