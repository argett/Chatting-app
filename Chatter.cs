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
            Thread.Sleep(500); // we give time to the server to prepare the connection and cassign the account
            Application.conn.Release(1);
        }


    }
}
