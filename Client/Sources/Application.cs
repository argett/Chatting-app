using System;
using System.Threading;
//using Server;

namespace Client
{
    class Application
    {
        public static Semaphore conn;

        /* HEURES PASSES :
         * 
         *  DATES           TEMPS
         *  
         *  26/11/2020      14h - 18h30     23h - 23h30
         *  27/11/2020      11h - 
         * 
         */
        static void Main(string[] args)
        {
            Chatter lilian = new Chatter("lilian","127.0.0.1", 8976);
            lilian.pingServ();
        }
    }
}
