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
         *  26/11/2020      14h - 18h30     22h - 23h
         *  27/11/2020      11h - 13h
         *  30/11/2020      15h - 16h30     18h - 21h      22h - 00h
         *  01/12/2020      20h - 20h30     23h - 00h  
         *  02/12/2020      15h - 19h  
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
