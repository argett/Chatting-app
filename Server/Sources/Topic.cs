using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    [Serializable]
    public class Topic : Agora
    {
        private string title;
        private List<Profile> members;

        public string Title
        {
            get => title;
            set => title = value;
        }

        public Topic(string t)
        {
            this.Title = t;
            this.Comments = new List<Comment>();
            members = new List<Profile>();
        }

        public bool addMember(Profile p)
        {
            if(p != null)
            {
                members.Add(p);
                return true;
            }
            return false;
        }

        public List<Profile> getMembers()
        {
            return members;
        }

    }
}
