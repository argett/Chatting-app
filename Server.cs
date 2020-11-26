using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatting_App
{
    class Server
    {
        private Database dbs;

        public Server(Database db)
        {
            dbs = db;
        }

        // the first thing the user see on the page
        public bool welcomeOnTheSite()
        {
            Console.WriteLine("Hello, do you want to create a New account or Connect to an existing one ? N/C");
            ConsoleKeyInfo choice = Console.ReadKey();
            Console.WriteLine("");

            if (choice.Key.ToString().ToLower() == "n")  // create a new user
            {
                createUser();
                Console.WriteLine("Thank you for registering, now you can attempt to connect");
                if (!tryConnection())
                    return false;
            }
            else if (choice.Key.ToString().ToLower() == "c") // establish the connection
            {
                tryConnection();
            }
            else
            {
                return false;
            }
                
            return true;
        }

        private void createUser()
        {
            string id, psw;
            Console.WriteLine("Please choose your new ID : ");
            id = Console.ReadLine();
            Console.WriteLine("Pease choose your new password : ");
            psw = Console.ReadLine();
            dbs.addNewProfile(id, psw);
        }

        // the user enter its ID & password
        private bool tryConnection()
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
                Console.WriteLine("The connection has been established, " + id);
                return true;
            }
            else
                Console.WriteLine("The ID or the password is incorrect, please retry");
                return false;
        }
    }
}
