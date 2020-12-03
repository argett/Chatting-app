using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class Comment
    {
        private string[] comment;

        public Comment(string user, string message)
        {
            comment = new string[2];
            comment[0] = user;
            comment[1] = message;
        }

        public string getUser()
        {
            return comment[0];
        }
        public string getMessage()
        {
            return comment[1];
        }
    }
}
