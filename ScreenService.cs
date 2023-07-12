using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
////////////////////////////////////////////////////////////////////////////////////////////////////////
// LOGIN SCREEN
// MAIN SCREEN
// -- 0.QUIT
// -- 1.LOGOUT
// -- 2.CHANGE PASSWORD
// -- 3.DEVICE MANAGER SCREEN
// ------ 3.0.TO PREVIOUS
// ------ 3.1.LIST DEVICES
// ------ 3.2.ADD DEVICE
// ------ 3.3.UPDATE DEVICE
// ------ 3.4.DELETE DEVICE (MANAGER ONLY)
// -- 4.USER MANAGER SCREEN (MANAGER ONLY)
// ------ 4.0.TO PREVIOUS (MANAGER ONLY)
// ------ 4.1.LIST USERS (MANAGER ONLY)
// ------ 4.2.SEARCH USERS (MANAGER ONLY)
// ------ 4.3.ADD USER (MANAGER ONLY)
// ------ 4.4.UPDATE USER (MANAGER ONLY)
// ------ 4.5.DELETE USER (MANAGER ONLY)
////////////////////////////////////////////////////////////////////////////////////////////////////////

namespace T12
{
    internal class ScreenService
    {
        private User userLogin = new();
        private UserServices userServices = new(); 
        private DeviceServices deviceServices = new(); 

        public void Header()
        {
            Console.Clear();
            Console.WriteLine("---------------- DEVICE MANAGER ----------------");
            Console.WriteLine("------------------------------------------------");
            Console.WriteLine($"-------- USER: {userLogin.FullName}");
            Console.WriteLine($"-------- Role: {userLogin.Role}");
            Console.WriteLine("------------------------------------------------");
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////
        // LOGIN SCREEN        
        public void LoginScreen()
        {
            do 
            {
                Header();
                Console.WriteLine("-------------------- LOGIN ---------------------");
                Console.Write("USERNAME:"); userLogin.UserName = Console.ReadLine() + "";
                Console.Write("PASSWORD:"); userLogin.Password = Console.ReadLine() + "";                
                userLogin = userServices.CheckUser(userLogin.UserName, userLogin.Password);
                if (userLogin.Role != Role.Unavailable) 
                {                     
                    MainScreen(); 
                } 
                else 
                {
                    Console.Write("Login fail!");Console.ReadKey();
                }
            } while (userLogin.Role == Role.Unavailable);
            
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////
        // MAIN SCREEN
        public void MainScreen()
        {
            string select;
            do
            {
                Header();
                Console.WriteLine("--------------------- MAIN ---------------------");
                Console.WriteLine("0.QUIT");
                Console.WriteLine("1.LOG OUT");
                Console.WriteLine("2.CHANGE PASSWORD");
                Console.WriteLine("3.DEVICE MANAGER");
                if (userLogin.Role == Role.Manager) Console.WriteLine("4.USER MANAGER");
                Console.Write("Select 0-");
                if (userLogin.Role == Role.Manager) Console.WriteLine("4:");
                else Console.WriteLine("3:");
                select = Console.ReadLine() + "";
                switch (select)
                {
                    case "0": 
                        select = "1";
                        break;
                    case "1":
                        userLogin = new User();
                        break;
                    case "2":
                        ChangePasswordScreen();
                        break;
                    case "3":
                        DeviceManagerScreen();
                        break;
                    case "4":
                        if (userLogin.Role == Role.Manager) UserManagerScreen();
                        break;                    
                    default:
                        break;
                }
            } while (select != "1");
            Console.Write("-------------------- LOG OUT -------------------"); Console.ReadKey();
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////
        // -- 2.CHANGE PASSWORD
        private void ChangePasswordScreen()
        {
            Header();
            Console.WriteLine("ENTER NEW PASSWORD:");
            string newpassword  = Console.ReadLine() + "";
            userServices.ChangeUserPassword(userLogin.Id, newpassword);
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////
        // -- 3.DEVICE MANAGER SCREEN
        public void DeviceManagerScreen()
        {
            string select;
            do
            {                
                deviceServices = new DeviceServices();
                deviceServices.GetData();
                Header();
                Console.WriteLine("---------------- DEVICE MANAGER ----------------");
                Console.WriteLine("0.BACK TO PREVIOUS");
                Console.WriteLine("1.LIST DEVICES");
                Console.WriteLine("2.ADD NEW A DEVICE");
                Console.WriteLine("3.UPDATE A DEVICE");
                if (userLogin.Role == Role.Manager) Console.WriteLine("4, DELETE A DEVICE");
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
        ///////////////////////////////////////////////////////////////////////////////////////////////////
        // ------ 3.1.LIST DEVICES
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
        ///////////////////////////////////////////////////////////////////////////////////////////////////
        // ------ 3.2.ADD DEVICE
        private void AddNewDeviceScreen()
        {
            Header();
            Device item = new();
            Console.WriteLine("ADD DEVICE: ");
            Console.Write("ENTER DEVICE NAME:"); item.Name = Console.ReadLine() + "";
            Console.Write("ENTER QUANTITY:"); item.Quantity = Convert.ToInt32(Console.ReadLine());
            deviceServices.AddNew(item);
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////
        // ------ 3.3.UPDATE DEVICE
        private void UpdateDeviceScreen()
        {
            Header();
            Device item = new();
            Console.WriteLine("UPDATE INFO FOR DEVICE:");
            Console.Write("ENTER ID:"); item.Id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("SKIP FIELD BY JUST ENTER");
            Console.Write("ENTER NAME:"); item.Name = Console.ReadLine() + "";
            Console.Write("ENTER QUANTITY:"); item.Quantity = Convert.ToInt32(Console.ReadLine());
            deviceServices.Update(item);
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////
        // ------ 3.4.DELETE DEVICE (MANAGER ONLY)
        private void DeleteDeviceScreen()
        {
            Header();
            User item = new();
            Console.WriteLine("DELETE DEVICE:");
            Console.Write("ENTER ID:"); item.Id = Convert.ToInt32(Console.ReadLine());
            userServices.Delete(item);
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////
        // -- 4.USER MANAGER SCREEN (MANAGER ONLY)
        public void UserManagerScreen()
        {
            string select;
            do
            {
                userServices = new UserServices();
                userServices.GetData();
                Header();
                Console.WriteLine("----------------- USER MANAGER -----------------");
                Console.WriteLine("0.BACK TO PREVIOUS");
                Console.WriteLine("1.LIST USERS");
                Console.WriteLine("2.SEARCH USER BY NAME");
                Console.WriteLine("3.ADD NEW A USER");
                Console.WriteLine("4.UPDATE A USER");
                Console.WriteLine("5.DELETE A USER");
                Console.WriteLine("------------------------------------------------");
                Console.WriteLine("SELECT 0-5:");
                select = Console.ReadLine() + "";
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
        ///////////////////////////////////////////////////////////////////////////////////////////////////
        // ------ 4.1.LIST USERS (MANAGER ONLY)
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
        ///////////////////////////////////////////////////////////////////////////////////////////////////
        // ------ 4.2.SEARCH USERS (MANAGER ONLY)
        private void SearchUserScreen()
        {
            Header();
            Console.WriteLine("SEARCH ALL USER BY NAME: ");
            Console.Write("ENTER NAME:"); string key = Console.ReadLine() + "";

            foreach (User user in userServices.SearchListByName(key))
            {
                Console.WriteLine(user.ToString());
            }
            Console.ReadKey();
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////
        // ------ 4.3.ADD USER (MANAGER ONLY)
        private void AddNewUserScreen()
        {
            Header();
            User user = new();
            Console.WriteLine("ADD NEW USER: ");
            Console.Write("ENTER USER NAME:"); user.UserName = Console.ReadLine() + "";
            Console.Write("ENTER FULL NAME:"); user.FullName = Console.ReadLine() + "";
            Console.Write("ENTER PASSWORD:"); user.Password = Console.ReadLine() + "";
            Console.Write("ENTER ROLE:"); user.Role = (Role)Convert.ToInt32(Console.ReadLine() + "");
            userServices.AddNew(user);
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////
        // ------ 4.4.UPDATE USER (MANAGER ONLY)
        private void UpdateUserScreen()
        {
            Header();
            User user = new();
            Console.WriteLine("UPDATE INFO FOR USER:");
            Console.Write("ENTER ID:"); user.Id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("SKIP FIELD BY JUST ENTER");
            Console.Write("ENTER USER NAME:"); user.UserName = Console.ReadLine() + "";
            Console.Write("ENTER FULL NAME:"); user.FullName = Console.ReadLine() + "";
            Console.Write("ENTER PASSWORD:"); user.Password = Console.ReadLine() + "";
            Console.Write("ENTER ROLE:"); user.Role = (Role)Convert.ToInt32(Console.ReadLine() + "");
            userServices.Update(user);
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////
        // ------ 4.5.DELETE USER (MANAGER ONLY)
        private void DeleteUserScreen()
        {
            Header();
            User user = new ();
            Console.WriteLine("DELETE USER:");
            Console.Write("ENTER ID:"); user.Id = Convert.ToInt32(Console.ReadLine());            
            userServices.Delete(user);
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////
    }
}
