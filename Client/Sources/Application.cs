﻿using System;
using System.Threading;
using Network;

namespace Client
{
    class Application
    {
        /* HEURES PASSES :
         * 
         *  DATES           TEMPS
         *  
         *  26/11/2020   5h30   14h - 18h30     22h - 23h
         *  27/11/2020   2h     11h - 13h
         *  30/11/2020   6h30   15h - 16h30     18h - 21h      22h - 00h
         *  01/12/2020   1h30   20h - 20h30     23h - 00h  
         *  02/12/2020   4h30   15h - 19h30  
         *  03/12/2020   5h     11h30 - 13h     14h - 15h      17h30 - 20h
         *  04/12/2020   7h30   9h - 12h30      14h - 18h
         *  09/12/2020   4h     18h - 20h       22h - 00h
         *  17/12/2020   0h30   10h-10h15       23h - 23h15
         *  18/12/2020   2h     10h50-11h20     11h30 - 13h
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
