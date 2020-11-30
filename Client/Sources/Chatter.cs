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
            while (true)
            {
                msg = (Answer)Network.Net.rcvMsg(comm.GetStream()); // create new user or connect
                Console.WriteLine(msg);
                string choice = Console.ReadLine(); 
                Network.Net.sendMsg(comm.GetStream(), new Network.Request("connection", "choice", choice));

                while (msg.getTitle() != "end connection") // steps before to validate the connection
                {
                    msg = (Answer)Network.Net.rcvMsg(comm.GetStream());
                    Console.WriteLine(msg.getMessage());

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
        }

        /****************
         * 
         * call others function depending on the result of the connection
         * 
         ****************/
        private void resConnection()
        {
            msg = (Answer)Network.Net.rcvMsg(comm.GetStream());
            if (msg.getTitle() == "connection allowed")
                connectedToServ();
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

        private void connectedToServ()
        {
            Console.WriteLine("IN CONNECTION");
        }

        private void exit()
        {
            Console.ReadLine();
            Environment.Exit(0);
        }
    }
}
