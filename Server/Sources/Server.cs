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

        private Request req;

        Server(Database db, int n)
        {
            dbs = db;
            port = n;
            comm = null;
        }

        static void Main(string[] args)
        {
            Server serv = new Server(new Database(),8976);
            dbs.addNewTopic("latin");
            dbs.getTopic(0).addComment("xXx_D4rk_Sasuk3_xXx", "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum");
            dbs.getTopic(0).addComment("le_genie du MAL", "Ut enim ad minima veniam, quis nostrum exercitationem ullam corporis suscipit laboriosam, nisi ut aliquid ex ea commodi consequatur?");
            dbs.getTopic(0).addComment("xXx_D4rk_Sasuk3_xXx", "Neque porro quisquam est, qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit, sed quia non numquam eius modi tempora incidunt ut labore et dolore magnam aliquam quaerat voluptatem");
            dbs.getTopic(0).addComment("le_genie du MAL", "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum");
            dbs.getTopic(0).addComment("Charles xavier", "Quis autem vel eum iure reprehenderit qui in ea voluptate velit esse quam nihil molestiae consequatur, vel illum qui dolorem eum fugiat quo voluptas nulla pariatur");
            dbs.getTopic(0).addComment("xXx_D4rk_Sasuk3_xXx", "Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam, eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo. Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit aut fugit, sed quia consequuntur magni dolores eos qui ratione voluptatem sequi nesciunt");
            dbs.addNewTopic("prenoms droles");
            dbs.getTopic(1).addComment("Jean Bonneau", "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum");
            dbs.getTopic(1).addComment("Kelly Diote", "Ut enim ad minima veniam, quis nostrum exercitationem ullam corporis suscipit laboriosam, nisi ut aliquid ex ea commodi consequatur?");
            dbs.getTopic(1).addComment("Alain Posteur", "Neque porro quisquam est, qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit, sed quia non numquam eius modi tempora incidunt ut labore et dolore magnam aliquam quaerat voluptatem");
            dbs.getTopic(1).addComment("Nain posteur", "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. ");
            dbs.getTopic(1).addComment("Cathy Mini", "At vero eos et accusamus et iusto odio dignissimos ducimus qui blanditiis praesentium voluptatum deleniti atque corrupti quos dolores et quas molestias excepturi sint occaecati cupiditate non provident, similique sunt in culpa qui officia deserunt mollitia animi, id est laborum et dolorum fuga. Et harum quidem rerum facilis est et expedita distinctio");
            dbs.getTopic(1).addComment("Oussama Lairbizar", "Sed totam rem aperiam, eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo. Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit aut fugit, sed quia consequuntur magni dolores eos qui ratione voluptatem sequi nesciunt");
            dbs.addNewTopic("espace"); 
            dbs.getTopic(2).addComment("Trou", "Noir");
            dbs.getTopic(2).addComment("Etoile", "Soleil");
            dbs.addNewTopic("/r");
            dbs.getTopic(3).addComment("oui", "AmlTheAche013");
            dbs.getTopic(3).addComment("non", "blog");
            dbs.getTopic(3).addComment("hey", "announcement");
            dbs.getTopic(3).addComment("yo", "18-25");
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
        public void welcomeOnTheSite()
        {
            Console.WriteLine(", the user is choosing between create or use an existing account");
            Network.Net.sendMsg(comm.GetStream(), new Network.Answer("checkpoint message", "Hello, do you want to create a New account or connect to an Existing one ? N/E : ", false));
            waitMessage();
            if (checkConnChoice(req.getMessage())) // connect the user at the database
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
        private bool checkConnChoice(string choice)
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
        private void createUser()
        {
            Network.Net.sendMsg(comm.GetStream(), new Network.Answer("create user", "Please choose your new ID and your PASSWORD (no space): ", false));
            waitMessage();

            string id = "", psw = "", turn = "id";
            foreach (char s in req.getMessage())
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
        private bool connecting()
        {
            Network.Net.sendMsg(comm.GetStream(), new Network.Answer("connect user", "Please enter your ID and your PASSWORD (no space) (exit as id to quit): ", false));

            while (true) // continue until the user is conncted or wants to leave
            {
                waitMessage();
                string[] data = separateData();

                if (data[0] == "exit")
                {
                    endProgram();
                    return false;
                }

                if (connection(data[0], data[1]))
                    return true; 
            }
        }

        /****************
         * 
         * separate the complete string into and ID and a PASSWORD
         * 
         ****************/
        private string[] separateData()
        {
            string[] data = new string[2];
            string id = "", psw = "", turn = "id";
            foreach (char s in req.getMessage())
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
            data[0] = id;
            data[1] = psw;
            return data;
        }

        /****************
         * 
         * check if the password and the ID correspond to an existing one
         * 
         ****************/
        private bool connection(string id, string pswrd)
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
    
        private void homePage()
        {
            bool _continue = true;
            while (_continue)
            {
                Console.WriteLine("The user is in the homepage");
                Network.Net.sendMsg(comm.GetStream(), new Network.Answer("home page choice", "What do you want to do ? \n1 : Send a private message \n2 : Connect to a topic \n3 : Create a topic \n4 : exit \nEnter 1/2/3/4 :", false));
                waitMessage();
                switch (req.getMessage())
                {
                    case "1":
                        Console.WriteLine("The user goes to the private message webpage");
                        Network.Net.sendMsg(comm.GetStream(), new Network.Answer("private message", false));
                        privateMsg();
                        break;
                    case "2":
                        Console.WriteLine("The user goes to the topics webpage");
                        Network.Net.sendMsg(comm.GetStream(), new Network.Answer("goto topics", false));
                        connTopic();
                        break;
                    case "3":
                        Console.WriteLine("The user goes to the create topic webpage");
                        Network.Net.sendMsg(comm.GetStream(), new Network.Answer("create topic", false));
                        createTopic();
                        break;
                    case "4":
                        Console.WriteLine("The user quit the programm");
                        Network.Net.sendMsg(comm.GetStream(), new Network.Answer("end connection", false));
                        endProgram();
                        _continue = false;
                        break;
                }
            }
        }

        // ---------------------------------     PRIVATE MESSAGE PART     --------------------------------- 


        private static void privateMsg()
        {
            ;
        }

        // ---------------------------------     CONSULT TOPICS PART     --------------------------------- 


        private void connTopic()
        {
            listTopics(); 
            waitMessage();
            int topicN = Int32.Parse(req.getMessage()) -1;
            Console.WriteLine("The user has chosen the topic " + dbs.getTopic(topicN).getTitle());
            topicPage(topicN);
        }

        private void listTopics()
        {
            string topics = "Choose a topic among these topics (enter the number):\n";
            int i = 1;
            List<Topic> list = dbs.getTopics();
            foreach (Topic t in list)
            {
                topics += i.ToString();
                topics += " : ";
                topics += t.getTitle();
                topics += "\n";
                i++;
            }
            Network.Net.sendMsg(comm.GetStream(), new Network.Answer(i + " topics", topics, false));

        }

        private void topicPage(int i)
        {
            bool _continue = true;
            while (_continue)
            {
                string printTopic = "\t/////////////////////////////////////////////////\n";
                printTopic += "\t\t\t  " + dbs.getTopic(i).getTitle().ToUpper() + "\n";
                printTopic += "\t/////////////////////////////////////////////////\n\n\n";
                foreach (Topic.Comment c in dbs.getTopic(i).getComments())
                {
                    printTopic += "     ";
                    printTopic += c.getUser();
                    printTopic += " :\n\n";
                    printTopic += c.getMessage();
                    printTopic += "\n------------------------------------\n";
                }

                printTopic += "\n\n\t***** ADD A COMMENT OR ENTER 'EXIT' TO QUIT *****\n";

                Network.Net.sendMsg(comm.GetStream(), new Network.Answer("topic", printTopic, false));
                waitMessage();
                if (req.getMessage().ToUpper() == "EXIT")
                {
                    Network.Net.sendMsg(comm.GetStream(), new Network.Answer("home page redirection", false));
                    _continue = false;
                }
                else
                    dbs.getTopic(i).addComment(req.getPurpose(), req.getMessage());
            }
        }

        // ---------------------------------     CREATE TOPICS PART     --------------------------------- 

        private void createTopic()
        {
            Network.Net.sendMsg(comm.GetStream(), new Network.Answer("create topic", "To create a topic, please enter its name : \n", false));
            waitMessage();
            int topicN = dbs.addNewTopic(req.getMessage());
            Console.WriteLine("New topic +'" + req.getMessage() + "' created");
            topicPage(topicN);
        }



        private void waitMessage()
        {
            req = (Request)Network.Net.rcvMsg(comm.GetStream());
        }

        private static void endProgram()
        {
            Network.Net.sendMsg(comm.GetStream(), new Network.Answer("end connection", "You have chosen to exit the webpage", false));
        }
    }
}
