using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T12
{
    internal class ScreenService
    {
        private User userLogin = new();
        private UserServices userServices = new ();
        private DeviceServices deviceServices = new ();

        public static void Header()
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
                Console.Write("USERNAME:"); userLogin.UserName = Console.ReadLine() + "";
                Console.Write("PASSWORD:"); userLogin.Password = Console.ReadLine() + "";                
                userLogin = userServices.CheckUser(userLogin.UserName, userLogin.Password);
                if (userLogin.Role != Role.Unavailable) 
                {                     
                    MainScreen(); 
                }
            } while (userLogin.Role != Role.Unavailable);
            
        }

        public void MainScreen()
        {
            string select;
            do
            {
                Header();
                Console.WriteLine("0.LOG OUT");
                Console.WriteLine("1.CHANGE PASSWORD");
                Console.WriteLine("2.DEVICE MANAGER");
                if (userLogin.Role == Role.Manager) Console.WriteLine("3.USER MANAGER");
                Console.Write("Select 0-");
                if (userLogin.Role == Role.Manager) Console.WriteLine("3:");
                else Console.WriteLine("2:");
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
            Console.WriteLine("ENTER NEW PASSWORD:");
            string newpassword  = Console.ReadLine() + "";
            userServices.ChangeUserPassword(userLogin.Id, newpassword);
        }

        public void UserManagerScreen()
        {
            userServices.GetData();
            string select;
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
                Console.WriteLine("SELECT 0-5:");
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
            } while (select != "0");
            
        }

        private void DeleteUserScreen()
        {
            Header();
            User user = new ();
            Console.WriteLine("DELETE USER:");
            Console.Write("ENTER ID:"); user.Id = Convert.ToInt16(Console.ReadLine());            
            userServices.Delete(user);
        }

        private void UpdateUserScreen()
        {
            Header();
            User user = new ();
            Console.WriteLine("UPDATE INFO FOR USER:");
            Console.Write("ENTER ID:"); user.Id = Convert.ToInt16(Console.ReadLine());
            Console.WriteLine("SKIP FIELD BY JUST ENTER");
            Console.Write("ENTER USER NAME:"); user.UserName = Console.ReadLine() + "";
            Console.Write("ENTER FULL NAME:"); user.FullName = Console.ReadLine() + "";
            Console.Write("ENTER PASSWORD:"); user.Password = Console.ReadLine() + "";
            Console.Write("ENTER ROLE:"); user.Role = (Role)Convert.ToInt16(Console.ReadLine() + "");
            userServices.Update(user);
        }

        private void AddNewUserScreen()
        {
            Header();
            User user = new ();
            Console.WriteLine("ADD NEW USER: ");
            Console.Write("ENTER USER NAME:"); user.UserName = Console.ReadLine() + "";
            Console.Write("ENTER FULL NAME:"); user.FullName = Console.ReadLine() + "";
            Console.Write("ENTER PASSWORD:"); user.Password = Console.ReadLine() + "";
            Console.Write("ENTER ROLE:"); user.Role = (Role) Convert.ToInt16(Console.ReadLine()+"");
            userServices.Add(user);
        }

        private void SearchUserScreen()
        {
            Header();
            Console.WriteLine("SEARCH ALL USER BY NAME: ");
            Console.Write("ENTER NAME:");string key = Console.ReadLine() + "";

            foreach(User user in userServices.SearchListByName(key)) 
                {
                Console.WriteLine(user.ToString());
                }

        }

        private void ListUserScreen()
        {
            Header();
            Console.WriteLine("LIST OF USER SORT BY NAME:");
            foreach (User user in userServices.SortedUserList()) 
            {
                Console.WriteLine(user.ToString());
            }
            Console.ReadLine();
        }

        public void DeviceManagerScreen()
        {
            deviceServices.GetData();
            string select ;
            do
            {
                Header();
                Console.WriteLine("0, BACK TO PREVIOUS");
                Console.WriteLine("1, LIST DEVICES");
                Console.WriteLine("2, ADD NEW A DEVICE");
                Console.WriteLine("3, UPDATE A DEVICE");
                if (userLogin.Role == Role.Manager) Console.WriteLine("4, DELETE A USER");
                Console.WriteLine("------------------------------------------------");
                Console.Write("SELECT 0-");
                if (userLogin.Role == Role.Manager) Console.WriteLine("4:");
                else Console.WriteLine("3:");
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
            } while (select != "0");

        }

        private void DeleteDeviceScreen()
        {
            Header();
            User item = new ();
            Console.WriteLine("DELETE DEVICE:");
            Console.Write("ENTER ID:"); item.Id = Convert.ToInt16(Console.ReadLine());
            userServices.Delete(item);
        }

        private void UpdateDeviceScreen()
        {
            Header();
            Device item = new ();
            Console.WriteLine("UPDATE INFO FOR USER:");
            Console.Write("ENTER ID:"); item.Id = Convert.ToInt16(Console.ReadLine());
            Console.WriteLine("SKIP FIELD BY JUST ENTER");
            Console.Write("ENTER USER NAME:"); item.Name = Console.ReadLine() + "";
            Console.Write("ENTER QUANTITY:"); item.Quantity = Convert.ToInt16(Console.ReadLine() + "");
            deviceServices.Update(item);
        }
        private void AddNewDeviceScreen()
        {
            Header();
            Device item = new ();
            Console.WriteLine("ADD DEVICE USER: ");
            Console.Write("ENTER DEVICE NAME:"); item.Name = Console.ReadLine() + "";
            Console.Write("ENTER QUANTITY:"); item.Quantity = Convert.ToInt16(Console.ReadLine() + "");
            deviceServices.AddNew(item);
        }

        private void ListDeviceScreen()
        {
            Header();
            Console.WriteLine("LIST OF DEVICE SORT BY NAME:");
            foreach (Device user in deviceServices.SortedDeviceList())
            {
                Console.WriteLine(user.ToString());
            }
            Console.ReadLine();
        }
    }
}
