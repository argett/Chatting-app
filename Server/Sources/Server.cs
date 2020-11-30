using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;

namespace Server
{
    public class Server
    {
        private int port;
        private Database dbs;
        private static TcpClient comm;

        Server(Database db, int n)
        {
            dbs = db;
            port = n;
            comm = null;
        }

        static void Main(string[] args)
        {
            Server serv = new Server(new Database(),8976);
            serv.start();

        }

        public void start()
        {
            TcpListener input = new TcpListener(new IPAddress(new byte[] { 127, 0, 0, 1 }), port);
            input.Start();
            while (true)
            {
                comm = input.AcceptTcpClient();
                Console.WriteLine("USER on the home page");

                Thread login = new Thread(new ThreadStart(welcomeOnTheSite));
                login.Start();
            }
        }

        // the first thing the user see on the page
        public static void welcomeOnTheSite()
        {
            Console.WriteLine("the user is entering its ID & psw");
            //Network.Net.sendMsg(comm.GetStream(), new Network.Answer("Hello, do you want to create a New account or Connect to an existing one ? N/C : ", false));
            Network.Answer ans= new Network.Answer("Hello, do you want to create a New account or Connect to an existing one ? N/C : ", false);

            Console.WriteLine(ans.ToString());
            /*

            Console.WriteLine("");
            string choice = Console.ReadLine();
            if (choice.ToLower() == "n")  // create a new user
            {
                //createUser();
                //connecting();

            }
            else if (choice.ToLower() == "c") // establish the connection
            {
                connecting();
            }
            */
        }

        private static void createUser()
        {
            string id, psw;
            Console.WriteLine("Please choose your new ID : ");
            id = Console.ReadLine();
            Console.WriteLine("Pease choose your new password : ");
            psw = Console.ReadLine();
            //dbs.addNewProfile(id, psw);
            Console.WriteLine("Thank you for registering, now you can attempt to connect");
        }

        // the user enter its ID & password, return false if the user wants to quit
        private static bool connecting()
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
        private static bool connection(string id, string pswrd)
        {
            if (true)//dbs.connectProfile(id, pswrd))
            {
                //userTryingAccess.account = new Profile(id, pswrd);
                Console.WriteLine("The connection has been established, " + id);
                return true;
            }
            else
                Console.WriteLine("The ID or the password is incorrect, please retry");
                return false;
        }
    }
}
