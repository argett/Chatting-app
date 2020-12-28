using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    [Serializable]
    public class Conversation : Agora
    {
        private Profile user1;
        private string surnameUser1;

        private Profile user2;
        private string surnameUser2;

        private List<Comment> discussion;
        private List<DateTime> time; // time of the message when it was sent

        public Conversation(Profile p1, Profile p2)
        {
            user1 = p1;
            user2 = p2;
            surnameUser1 = p1.login;
            surnameUser2 = p2.login;

            time = new List<DateTime>();
            discussion = new List<Comment>();
        }

        public void addMessage(Profile sender, string msg)
        {
            if(sender == user1)
            {
                discussion.Add(new Comment(surnameUser1, msg));
            }
            else
            {
                discussion.Add(new Comment(surnameUser2, msg));
            }
            time.Add(DateTime.Now);
        }

        public Comment getCommentN(int i)
        {
            return discussion[i];
        }

        public List<Comment> getDiscussion()
        {
            return discussion;
        }

        public string getSurname1()
        {
            return surnameUser1;
        }

        public string getSurname2()
        {
            return surnameUser2;
        }

        public void setSurname(Profile user, string surnameFriend)
        {
            if (user == user1)
                surnameUser2 = surnameFriend;
            else
                surnameUser1 = surnameFriend;
        }

        public DateTime getTimingN(int i)
        {
            return time[i];
        }
    }
}
