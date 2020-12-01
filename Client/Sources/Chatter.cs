using System;
using System.Net.Sockets;
using System.Threading;
using Network;

namespace Client
{
    class Chatter
    {
        public string name;
        //public Profile account;

        private TcpClient comm;
        private string hostname;
        private int port;

        private Answer msg;

        public Chatter(string x, string h, int p)
        {
            name = x;
            hostname = h;
            port = p;
            comm = null;
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
            string id, psw, res;
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
                }
            }

            resConnection();
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
            {
                Console.WriteLine("Exiting the website...");
                exit();
            }
            else
            {
                Console.WriteLine("ERROR 404 : failure in result of connection");
                exit();
            }
        }

        // ---------------------------------     HOME WEBSITE PART     --------------------------------- 

        private void homeServer()
        {
            int n;
            while (true)
            {
                waitMessage(true);
                do{
                    n = Int32.Parse(Console.ReadLine());
                } while (n > 4 && n < 1);
                Network.Net.sendMsg(comm.GetStream(), new Network.Request("home page redirection", n.ToString()));
                waitMessage(false);
                switch (msg.getTitle())
                {
                    case "private message":
                        break;
                    case "goto topics":
                        topics();
                        break;
                    case "create topic":
                        break;
                    case "end connection":
                        exit();
                        break;
                }
            }
        }


        // ---------------------------------     PRIVATE MESSAGE PART     --------------------------------- 


        // ---------------------------------     CONSULT TOPICS PART     --------------------------------- 

        private void topics()
        {
            waitMessage(true);
            // envoyer le chiox du topic
        }



        private void exit()
        {
            Console.WriteLine("\n\nPress any key to quit...");
            Console.ReadLine();
            Environment.Exit(0);
        }

        private void waitMessage(bool print)
        {
            msg = (Answer)Network.Net.rcvMsg(comm.GetStream()); // create new user or connect
            if (print)
                Console.WriteLine(msg);
        }
    }
}
