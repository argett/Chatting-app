using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Threading;
using Network;

namespace Client
{
    class Chatter
    {
        private string name;

        private TcpClient comm;
        private string hostname;
        private int port;
        private Thread client;
        private Answer msg;

        public Chatter(string x, string h, int p)
        {
            name = x;
            hostname = h;
            port = p;
            comm = null;
            msg = null;
        }


        // ---------------------------------     CONNECTION PART     ---------------------------------


        /****************
         * 
         * the human start pinging the server to say "i want to connect"
         * serve connecting to an account and/or creating a new one
         * 
         ****************/
        public void pingServ()
        {
            string id ="", psw, res;
            comm = new TcpClient(hostname, port);

            waitMessage(true);
            string choice = Console.ReadLine(); 
            Network.Net.sendMsg(comm.GetStream(), new Network.Request("connection", "choice", choice));

            while (msg.getTitle() != "end connection") // steps before to validate the connection
            {
                waitMessage(true);
                if (msg.getTitle() != "checkpoint message" && msg.getTitle() != "end connection") // when new user is created
                {
                    // no matter if we create a new profile or connect, we have to enter the same data
                    id = Console.ReadLine();
                    psw = Console.ReadLine();
                    res = id + " " + psw;
                    Network.Net.sendMsg(comm.GetStream(), new Network.Request("connection", res));
                    this.name = id;
                }
            }
            client = new Thread(new ThreadStart(resConnection));
            client.Start();
        }

        /****************
         * 
         * call others function depending on the result of the connection
         * 
         ****************/
        private void resConnection()
        {
            waitMessage(false);
            if (msg.getTitle() == "connection allowed")
                homeServer();
            else if (msg.getTitle() == "connection denied")
                Console.WriteLine("Exiting the website...");
            else
                Console.WriteLine("ERROR 404 : failure in result of connection");

            exit();
        }


        // ---------------------------------     HOME WEBSITE PART     --------------------------------- 


        private void homeServer()
        {
            int n;
            bool _continue = true;
            while (_continue)
            {
                waitMessage(true); // print the homepage choices
                do{
                    n = readNumber();
                } while (n > 5 || n < 1);

                Network.Net.sendMsg(comm.GetStream(), new Network.Request("home page redirection", n));
                waitMessage(false);  // get the redirection
                switch (msg.getTitle())
                {
                    case "private message":
                        privateMessageHome();
                        break;
                    case "goto topics":
                        topics();
                        break;
                    case "create topic":
                        createTopic();
                        break;
                    case "add friend":
                        addFriend();
                        break;
                    case "end connection":
                        _continue = false;
                        break;
                }
            }
        }


        // ---------------------------------     PRIVATE MESSAGE PART     --------------------------------- 

        public void privateMessageHome()
        {
            Network.Net.sendMsg(comm.GetStream(), new Network.Request("private message", this.name)); //we give the name of the profile in order the databse knows which conversation to give
            waitMessage(true); // print all the disponible conversation
            if(msg.getNumber() != -1)
            {
                int convNB;
                do
                {
                    convNB = readNumber();
                } while (convNB > msg.getNumber() - 1 || convNB < 1);
                Network.Net.sendMsg(comm.GetStream(), new Network.Request("private message", this.name, convNB)); //we give the name of the profile in order the databse knows which conversation to give

                waitMessage(false); // wait the decision of the server (create new or connect)
                if (msg.getMessage() == "create new")
                    createPrivateMessage();
                else
                    discussionPage();
            }
        }

        private void createPrivateMessage()
        {
            waitMessage(true); // show instructions

            if (!msg.getError())
            {
                string name = Console.ReadLine();
                int friendNB;
                do
                {
                    friendNB = readNumber();
                } while (friendNB > msg.getNumber() - 1 || friendNB < 1);

                Network.Net.sendMsg(comm.GetStream(), new Network.Request("private message", name, friendNB));
                discussionPage();
            }
            // no else print error, it will be writen in the homeServer() function
        }


        // ---------------------------------     CONSULT TOPICS PART     --------------------------------- 


        private void topics()
        {
            waitMessage(true); // get the list of topics
            int topic;
            do
            {
                topic = readNumber();
            } while (topic > msg.getNumber()-1 || topic < 1);

            Network.Net.sendMsg(comm.GetStream(), new Network.Request("topics", topic));
            discussionPage();
        }

        private void discussionPage()
        {
            waitMessage(true); // print the comments on the topics
            do
            {
                string s = Console.ReadLine();
                Network.Net.sendMsg(comm.GetStream(), new Network.Request("topic",this.name, s));
                waitMessage(true);
            } while (msg.getTitle() != "home page redirection");
        }


        // ---------------------------------     CREATE TOPICS PART     --------------------------------- 

        
        private void createTopic()
        {
            waitMessage(true); // instructions creating a topic
            string title = Console.ReadLine();
            Network.Net.sendMsg(comm.GetStream(), new Network.Request(name + " create topic", title));
            waitMessage(false);
            if (!msg.getError())
                discussionPage(); // after creating the topic, we go to its page
            else
                Console.WriteLine(msg.getMessage());
        }


        // ---------------------------------     ADD FRIEND PART     --------------------------------- 

        
        private void addFriend()
        {
            Network.Net.sendMsg(comm.GetStream(), new Network.Request("add friend", this.name));
            waitMessage(true); // print the instructions + list of users

            int friendID;
            do
            {
                friendID = readNumber();
            } while (friendID > msg.getNumber() - 1 || friendID < 1);
            Network.Net.sendMsg(comm.GetStream(), new Network.Request("add friend", friendID)); 
        }


        // ---------------------------------     USEFULL FUNCTION     ---------------------------------

        private int readNumber()
        {
            int n;
            try
            {
                n = Int32.Parse(Console.ReadLine());
            }
            catch (Exception e)
            {
                Console.WriteLine("This is not a number");
                n = -1;
            }
            return n;
        }
        private void exit()
        {
            Console.WriteLine("\nPress any key to quit...");
            Console.ReadLine();
            // catch the thread of the client
            client.Abort();
        }

        private void waitMessage(bool print)
        {
            msg = (Answer)Network.Net.rcvMsg(comm.GetStream());
            if (print)
            {
                Console.Clear();
                Console.WriteLine(msg);
            }
        }
    }
}
