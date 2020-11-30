using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using Network;

namespace Server
{
    public class Server
    {
        private int port;
        private static Database dbs;
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
                Console.WriteLine("User on the home page");

                Thread login = new Thread(new ThreadStart(welcomeOnTheSite));
                login.Start();
            }
        }

        // the first thing the user see on the page
        public static void welcomeOnTheSite()
        {
            Console.WriteLine("the user is choosing between create or use an existing account");
            Network.Net.sendMsg(comm.GetStream(), new Network.Answer("Hello, do you want to create a New account or Connect to an existing one ? N/C : ", false));
            Request r = (Request)Network.Net.rcvMsg(comm.GetStream());
            checkConnChoice(r.getMessage());
        }

        private static bool checkConnChoice(string choice)
        {
            if (choice.ToLower() == "n")
            {
                Console.WriteLine("the user is creating a new user");
                createUser();
                connecting();
                return true;
            }
            else if (choice.ToLower() == "c")
            {
                Console.WriteLine("the user is connecting");
                connecting();
                return true;
            }
            else return false;
        }

        private static void createUser()
        {
            Network.Net.sendMsg(comm.GetStream(), new Network.Answer("create user", "Please choose your new ID and your PASSWORD (no space): ", false));
            Request r = (Request)Network.Net.rcvMsg(comm.GetStream());

            string id = "", psw = "", turn = "id";
            foreach (char s in r.getMessage())
            {
                if(s != ' ')
                {
                    if (turn == "id")
                        id += s;
                    else if (turn == "psw")
                        psw += s;
                    else
                        Console.WriteLine("ERROR 404 : failure parsing id/psw");
                }
                else
                {
                    turn = "psw";
                }
            }
            dbs.addNewProfile(id, psw);

            Network.Net.sendMsg(comm.GetStream(), new Network.Answer("message of validation", "Your account has been correctly created. please log in now", false));
        }

        // the user enter its ID & password, return false if the user wants to quit
        private static void connecting()
        {
            Console.WriteLine("createUser");
            /*
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
            */
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

        private static void analyseRequest(Request r)
        {
            switch(r.getPurpose())
            {
                case "connection":
                    
                    break;
                default:
                    Console.WriteLine("error of request purpose");
                    break;
            }
        }
    }
}
