using System;
using System.Threading;

namespace Chatting_App
{
    class Chatter
    {
        public string name;
        public bool accessingServ;
        public bool connected;
        public Profile account;

        public Chatter(string x)
        {
            name = x;
            accessingServ = false;
            connected = false;
            account = null;
        }

        // the human start pinging the server to say "i want to connect"
        public void pingServ()
        {
            // only one person at a time can trying to connect on the server
            Application.conn.WaitOne();
            accessingServ = true;
            Server.userTryingAccess = this;
            // wait to be sur to be connected before unleashing the connection
            Thread wait = new Thread(new ThreadStart(waitResponse));
            wait.Start();
        }

        public void waitResponse()
        {
            while (!connected)
            {
                Thread.Sleep(100); // no need to check every millisecond, we're not running out of time 
            }
            //if we're connected, then allow another user to connect 
            Application.conn.Release(1);
        }
    }
}
