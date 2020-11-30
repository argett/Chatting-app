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

        public Chatter(string x, string h, int p)
        {
            name = x;
            hostname = h;
            port = p;
            comm = null;
        }

        // the human start pinging the server to say "i want to connect"
        public void pingServ()
        {
            comm = new TcpClient(hostname, port);
            while (true)
            {
                Answer msg = (Answer)Network.Net.rcvMsg(comm.GetStream()); // create new user or connect
                Console.WriteLine(msg);
                string choice = Console.ReadLine(); 
                Network.Net.sendMsg(comm.GetStream(), new Network.Request("connection", "choice", choice));
                
                while(msg.getMessage() != "connected") // steps before to validate the connection
                {
                    msg = (Answer)Network.Net.rcvMsg(comm.GetStream());
                    Console.WriteLine(msg.getMessage());
                    switch (msg.getTitle())
                    {
                        case "create user":
                            string id = Console.ReadLine();
                            string psw = Console.ReadLine();
                            string res = id + " " + psw;
                            Network.Net.sendMsg(comm.GetStream(), new Network.Request("connection", "add profile - id",res));
                            break;
                        default:
                            break;
                    }
                }
            }

        }


    }
}
