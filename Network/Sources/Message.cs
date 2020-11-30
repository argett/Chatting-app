using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Network
{
    public interface Message
    {
        string ToString();
    }

    [Serializable]
    public class Requests : Message
    {
        private string purpose; // connection, topics, public message on topic, private message, conversation
        private string target;  // user or topic name
        private string message; // if there is a message

        public Requests(string p, string t, string m)
        {
            purpose = p;
            target = t;
            message = m;
        }


        public string getPurpose()
        {
            return purpose;
        }

        public string getTarget()
        {
            return target;
        }

        public string getMessage()
        {
            return message;
        }

        public override string ToString()
        {
            return purpose + " " + target + " " + message;
        }

    }

    [Serializable]
    public class Answer : Message
    {
        private string title; // name of the conversation, topic
        private List<string> content; // the liste of message, topics
        private bool error;

        public Answer(string tit, bool err)
        {
            title = tit;
            error = err;
            content = null;
        }

        public Answer(string tit, List<string> cont, bool err)
        {
            title = tit;
            content = cont;
            error = err;
        }

        public string getTitle()
        {
            return title;
        }

        public List<string> getContent()
        {
            return content;
        }

        public bool getError()
        {
            return error;
        }

        public override string ToString()
        {
            string s = title + " ";

            if(content != null)
            {
                foreach (string t in content)
                {
                    s.Insert(s.Length - 1, t + " ");
                }
            }

            if(error)
                s.Insert(s.Length - 1, "problem");
            else
                s.Insert(s.Length - 1, "nominal");

            return s;
        }

    }
}
