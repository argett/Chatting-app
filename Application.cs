using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatting_App
{
    class Application
    {
        /* HEURES PASSES :
         * 
         *  DATES           TEMPS
         *  
         *  26/11/2020      14h - 
         * 
         * 
         */
        static void Main(string[] args)
        {
            Server serv = new Server(new Database());
            serv.welcomeOnTheSite();

            Console.ReadKey();
        }
    }
}
