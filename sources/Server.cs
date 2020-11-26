using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatting_App
{
    class Server
    {
        public static Chatter userTryingAccess;
        public static Thread conn;

        private Database dbs;

        public Server(Database db)
        {
            dbs = db;
            conn = null;
            userTryingAccess = null;
        }

        // the first thing the user see on the page
        public void welcomeOnTheSite()
        {
            while (true)
            {
                if (userTryingAccess != null)
                {
                    Console.Write("Hello, do you want to create a New account or Connect to an existing one ? N/C : ");
                    string choice = Console.ReadLine();
                    if (choice.ToLower() == "n")  // create a new user
                    {
                        createUser();
                        tryConnection();

                    }
                    else if (choice.ToLower() == "c") // establish the connection
                    {
                        tryConnection();
                    }
                    else
                    {
                        // forfate the connection
                        userTryingAccess.connected = false;
                        userTryingAccess.accessingServ = false;
                        userTryingAccess = null;
                    }
                }
            }
        }

        private void createUser()
        {
            string id, psw;
            Console.WriteLine("Please choose your new ID : ");
            id = Console.ReadLine();
            Console.WriteLine("Pease choose your new password : ");
            psw = Console.ReadLine();
            dbs.addNewProfile(id, psw);
            Console.WriteLine("Thank you for registering, now you can attempt to connect");
        }


        private void tryConnection()
        {
            if (connecting())
            {
                userTryingAccess.connected = true;
                userTryingAccess.accessingServ = false;
                userTryingAccess = null;
            }
            else
            {
                userTryingAccess.connected = false;
                userTryingAccess.accessingServ = false;
                userTryingAccess = null;
            }
        }

        // the user enter its ID & password, return false if the user wants to quit
        private bool connecting()
        {
            ConsoleKeyInfo choice;
            string id, psw;
            bool _continue = false;
            do
            {
                Console.WriteLine("Please enter your ID : ");
                id = Console.ReadLine();
                Console.WriteLine("Pease enter your password : ");
                psw = Console.ReadLine();

                if (connection(id, psw))
                {
                    return true; // we have the right id + pws
                }
                else
                {
                    // try again or quit
                    Console.WriteLine("Do you want to try again ? Y/N ");
                    choice = Console.ReadKey();
                    Console.WriteLine("");
                    if (choice.Key.ToString().ToLower() == "y")
                        _continue = true;
                    else
                        _continue = false;
                }
            } while (_continue);
            return false;
        }

        // if the user wants to sign in
        private bool connection(string id, string pswrd)
        {
            if (dbs.connectProfile(id, pswrd))
            {
                userTryingAccess.account = new Profile(id, pswrd);
                Console.WriteLine("The connection has been established, " + id);
                return true;
            }
            else
                Console.WriteLine("The ID or the password is incorrect, please retry");
                return false;
        }
    }
}
