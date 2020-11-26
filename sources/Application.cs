using System;
using System.Threading;
using System.Windows.Input;

namespace Chatting_App
{
    class Application
    {
        public static Semaphore conn;

        /* HEURES PASSES :
         * 
         *  DATES           TEMPS
         *  
         *  26/11/2020      14h - 18h30     23h - 23h30
         * 
         * 
         */
        static void Main(string[] args)
        {
            conn = new Semaphore(1, 1);
            Server serv = new Server(new Database());

            Thread servAwake = new Thread(new ThreadStart(serv.welcomeOnTheSite));
            servAwake.Start();

            Chatter lilian = new Chatter("lilian");
            lilian.pingServ();
        }
    }
}
