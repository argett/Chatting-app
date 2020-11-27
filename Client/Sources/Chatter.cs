using System;
using System.Threading;
//using Server;
using System.Net.Sockets;

namespace Client
{
    class Chatter
    { 
        public string name;
        //public Profile account;

        private string hostname;
        private int port;

        public Chatter(string x, string h, int p)
        {
            name = x;
            hostname = h;
            port = p;
            //account = null;
        }

        // the human start pinging the server to say "i want to connect"
        public void pingServ()
        {
            TcpClient comm = new TcpClient(hostname, port);
            Console.WriteLine("i am on the home page");

            //Server.Server.welcomeOnTheSite();
        }

        
    }
}
