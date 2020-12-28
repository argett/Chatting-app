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
            Title = t;
            this.Discussion = new List<Comment>();
            members = new List<Profile>();
        }

        public bool addMember(Profile p)
        {
            bool find = false;
            if(p != null)
            {
                //check if the profile is already in the topic
                foreach(Profile temp in members)
                {
                    if (temp == p)
                        find = true;
                }
                if(!find)
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
