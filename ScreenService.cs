using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T12
{
    internal class ScreenService
    {
        private User userLogin;
        private UserServices userServices = new UserServices();
        private DeviceServices deviceServices = new DeviceServices();

        public void Header()
        {
            Console.Clear();
            Console.WriteLine("---------------- DEVICE MANAGER ----------------");
            Console.WriteLine("------------------------------------------------");
        }
        
        public void LoginScreen()
        {
            do 
            {
                Header();
                Console.Write("Username"); userLogin.UserName = Console.ReadLine() + "";
                Console.Write("Password"); userLogin.Password = Console.ReadLine() + "";                
                userLogin = userServices.checkUser(userLogin.UserName, userLogin.Password);
                if (userLogin.Role != Role.Unavailable) 
                {                     
                    MainScreen(); 
                }
            } while (userLogin.Role != Role.Unavailable);
            
        }

        public void MainScreen()
        {
            string select = "";
            do
            {
                Header();
                Console.WriteLine("0.LOG OUT");
                Console.WriteLine("1.CHANGE PASSWORD");
                Console.WriteLine("2.DEVICE MANAGER");
                if (userLogin.Role == Role.Manager) Console.WriteLine("3.USER MANAGER");
                Console.WriteLine("Select 0-3:");
                select = Console.ReadLine() + "";
                switch (select)
                {
                    case "0":                    
                        break;
                    case "1":
                        ChangePasswordScreen();
                        break;
                    case "2":
                        DeviceManagerScreen();
                        break;
                    case "3":
                        UserManagerScreen();
                        break;                    
                    default:
                        break;
                }
            } while (select != "0");
            userLogin = new User();
        }

        private void ChangePasswordScreen()
        {
            Header();
            Console.WriteLine("Enter new password");
            string newpassword  = Console.ReadLine() + "";
            userServices.ChangeUserPassword(userLogin.Id, newpassword);
        }

        public void UserManagerScreen()
        {
            userServices.GetData();
            string select = "";
            do
            {
                Header();
                Console.WriteLine("0, BACK TO PREVIOUS");
                Console.WriteLine("1, LIST USERS");
                Console.WriteLine("2, SEARCH USER BY NAME");
                Console.WriteLine("3, ADD NEW A USER");
                Console.WriteLine("4, UPDATE A USER");
                Console.WriteLine("5, DELETE A USER");
                Console.WriteLine("------------------------------------------------");
                Console.WriteLine("Select 0-5:");
                select = Console.ReadLine() +"";
                switch (select)
                {
                    case "1":
                        ListUserScreen();
                        break;
                    case "2":
                        SearchUserScreen();
                        break;
                    case "3":
                        AddNewUserScreen();
                        break;
                    case "4":
                        UpdateUserScreen();
                        break;
                    case "5":
                        DeleteUserScreen();
                        break;
                    case "0":
                        break;
                    default:
                        break;
                }
            } while (select == "0");
            
        }

        private void ListUserScreen()
        {
            Header();
            Console.WriteLine("LIST OF USER SORT BY NAME:");
            foreach (User item in userServices.sortedUserList()) 
            {
                Console.WriteLine(item.ToString());
            }
            Console.ReadLine();
        }

        public void DeviceManagerScreen()
        {
            deviceServices.GetData();
            string select = "";
            do
            {
                Header();
                Console.WriteLine("0, BACK TO PREVIOUS");
                Console.WriteLine("1, LIST DEVICES");
                Console.WriteLine("2, ADD NEW A DEVICE");
                Console.WriteLine("3, UPDATE A DEVICE");
                if (userLogin.Role == Role.Manager) Console.WriteLine("4, DELETE A USER");
                Console.WriteLine("------------------------------------------------");
                Console.WriteLine("Select 0-4:");
                select = Console.ReadLine() + "";
                switch (select)
                {
                    case "1":
                        ListDeviceScreen();
                        break;
                    case "2":
                        AddNewDeviceScreen();
                        break;
                    case "3":
                        UpdateDeviceScreen();
                        break;
                    case "4":
                        if (userLogin.Role == Role.Manager) DeleteDeviceScreen();
                        break;
                    case "0":
                        break;
                    default:
                        break;
                }
            } while (select == "0");

        }
    }
}
