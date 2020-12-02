using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class Topic
    {
        public class Comment
        {
            private string[] comment;

            public Comment(string user, string message){
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

        private string title;
        private List<Comment> comments;
        

        public Topic(string t)
        {
            title = t;
            comments = new List<Comment>();
        }

        public string getTitle()
        {
            return title;
        }

        public List<Comment> getComments()
        {
            return comments;
        }

        public void addComment(string u, string m)
        {
            Comment c = new Comment(u, m);
            comments.Add(c);
        }

    }
}
