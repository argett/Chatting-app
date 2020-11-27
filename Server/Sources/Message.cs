using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Message
    {
        private string owner;
        private string message;
        private DateTime date;

        public Message(string pseudo, string txt, DateTime moment)
        {
            owner = pseudo;
            message = txt;
            date = moment;
        }

        public string Pseudo
        {
            get => owner;
            set => owner = value;
        }
        public string Content
        {
            get => message;
            set => message = value;
        }
        public DateTime Date
        {
            get => date;
            set => date = value;
        }
    }
}
