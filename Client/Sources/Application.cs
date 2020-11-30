using System;
using System.Threading;
using Network;

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
         *  27/11/2020      11h - 13h
         *  30/11/2020      13h30 - 14h     15h - 16h30     18h - 21h   -15min
         * 
         */
        static void Main(string[] args)
        {
            Chatter lilian = new Chatter("lilian", "127.0.0.1", 8976);
            
            try
            {
                lilian.pingServ();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
