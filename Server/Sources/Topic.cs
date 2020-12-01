using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Topic
    {
        private string title;

        public Topic(string t)
        {
            title = t;
        }

        public string getTitle()
        {
            return title;
        }

    }
}
