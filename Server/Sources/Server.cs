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
            dbs.addTopic("bug");
            dbs.addTopic("problemes");
            dbs.addTopic("liste1");
            dbs.addTopic("/r");
            dbs.addNewProfile("li", "fg");
            serv.start();

        }

        // ---------------------------------     CONNECTION PART     ---------------------------------     

        /****************
         * 
         * creation of an instance of a user in a thread
         * 
         ****************/
        public void start()
        {
            TcpListener input = new TcpListener(new IPAddress(new byte[] { 127, 0, 0, 1 }), port);
            input.Start();
            while (true)
            {
                comm = input.AcceptTcpClient();
                Console.Write("\nUser on the home page");

                Thread login = new Thread(new ThreadStart(welcomeOnTheSite));
                login.Start();
            }
        }


        /****************
         * 
         * the welcome page, the user choose to create a new account or use an existing one
         * send the result at the client at the end
         * "connection denied" occure when the user has chosen to exit the program
         * 
         ****************/
        public static void welcomeOnTheSite()
        {
            Console.WriteLine(", the user is choosing between create or use an existing account");
            Network.Net.sendMsg(comm.GetStream(), new Network.Answer("checkpoint message", "Hello, do you want to create a New account or connect to an Existing one ? N/E : ", false));
            Request r = (Request)Network.Net.rcvMsg(comm.GetStream());
            if (checkConnChoice(r.getMessage())) // connect the user at the database
            {
                Network.Net.sendMsg(comm.GetStream(), new Network.Answer("connection allowed", false));
                homePage();
            }
            else
            {
                Network.Net.sendMsg(comm.GetStream(), new Network.Answer("connection denied", false));
                Console.WriteLine("A user is leaving the website");
            }
        }

        /****************
         * 
         * Check the choice made in the previous function
         * 
         ****************/
        private static bool checkConnChoice(string choice)
        {
            if (choice.ToLower() == "n") 
            {
                Console.WriteLine("the user is creating a new user");
                createUser();
                Console.WriteLine("the user is connecting");
                if (connecting())
                    return true;
            }
            else if (choice.ToLower() == "e")
            {
                Console.WriteLine("the user is connecting");
                if (connecting())
                    return true;
            }
            
            return false;
        }

        /****************
         * 
         * initialize a new user
         * 
         ****************/
        private static void createUser()
        {
            Network.Net.sendMsg(comm.GetStream(), new Network.Answer("create user", "Please choose your new ID and your PASSWORD (no space): ", false));
            Request r = (Request)Network.Net.rcvMsg(comm.GetStream());

            string id = "", psw = "", turn = "id";
            foreach (char s in r.getMessage())
            {
                if(s != ' ') //  separate the psw & id from the string
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
            dbs.addNewProfile(id, psw); // insert the new user

            Network.Net.sendMsg(comm.GetStream(), new Network.Answer("checkpoint message", "Your account has been correctly created. please log in now", false));
        }

        /****************
         * 
         * the user enter its ID & password, return true when connetced, return false if the user wants to quit
         * 
         ****************/
        private static bool connecting()
        {
            Network.Net.sendMsg(comm.GetStream(), new Network.Answer("connect user", "Please enter your ID and your PASSWORD (no space) (exit as id to quit): ", false));

            while (true) // continue until the user is conncted or wants to leave
            {
                Request r = (Request)Network.Net.rcvMsg(comm.GetStream());

                string id = "", psw = "", turn = "id";
                foreach (char s in r.getMessage())//  separate the psw & id from the string
                {
                    if (s != ' ') 
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

                if (id == "exit")
                {
                    endProgram();
                    return false;
                }

                if (connection(id, psw))
                    return true; 
            }
        }

        /****************
         * 
         * check if the password and the ID correspond to an existing one
         * 
         ****************/
        private static bool connection(string id, string pswrd)
        {
            if (dbs.connectProfile(id, pswrd))
            {
                Network.Net.sendMsg(comm.GetStream(), new Network.Answer("end connection", "You are now connected to the website", false));
                Console.WriteLine(id + " is now connected");
                return true;
            }
            else
            {
                Network.Net.sendMsg(comm.GetStream(), new Network.Answer("connect user", "Bad ID and or PASSWORD, please retry (no space): ", false));
                Console.WriteLine("identification failed");
                return false;
            }
        }


        // ---------------------------------     HOME WEBSITE PART     --------------------------------- 
    
        private static void homePage()
        {
            bool _continue = true;
            while (_continue)
            {
                Console.WriteLine("The user is in the homepage");
                Network.Net.sendMsg(comm.GetStream(), new Network.Answer("home page choice", "\nWhat do you want to do ? \n1 : Send a private message \n2 : Connect to a topic \n3 : Create a topic \n4 : exit \nEnter 1/2/3/4 :", false));
                Request choice = (Request)Network.Net.rcvMsg(comm.GetStream());
                switch (choice.getMessage())
                {
                    case "1":
                        Network.Net.sendMsg(comm.GetStream(), new Network.Answer("private message", false));
                        privateMsg();
                        break;
                    case "2":
                        Network.Net.sendMsg(comm.GetStream(), new Network.Answer("goto topics", false));
                        connTopic();
                        break;
                    case "3":
                        Network.Net.sendMsg(comm.GetStream(), new Network.Answer("create topic", false));
                        createTopic();
                        break;
                    case "4":
                        Network.Net.sendMsg(comm.GetStream(), new Network.Answer("end connection", false));
                        endProgram();
                        _continue = false;
                        break;
                }
            }
        }

        private static void createTopic()
        {
            ;
        }

        private static void endProgram()
        {
            Network.Net.sendMsg(comm.GetStream(), new Network.Answer("end connection", "You have chosen to exit the webpage", false));
        }

        // ---------------------------------     PRIVATE MESSAGE PART     --------------------------------- 


        private static void privateMsg()
        {
            ;
        }

        // ---------------------------------     CONSULT TOPICS PART     --------------------------------- 


        private static void connTopic()
        {
            listTopics();
            // recevoir le choix du topics
        }

        private static void listTopics()
        {
            string topics = "Choose a topic among these topics (enter the number):\n";
            int i = 0;
            List<Topic> list = dbs.getTopics();
            foreach (Topic t in list)
            {
                topics += i.ToString();
                topics += " : ";
                topics += t.getTitle();
                topics += "\n";
                i++;
            }
            Network.Net.sendMsg(comm.GetStream(), new Network.Answer("list of topics", topics, false));
        }
    }
}
