using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatting_App
{
    class Profile
    {
        public string login;
        private string password;

        private List<Profile> friends;
        private Dictionary<string, Conversation> conversations;

        public Profile(string name, string pswd)
        {
            login = name;
            password = pswd;
            friends = null;
            conversations = null;
        }

        public string getPassword()
        {
            return password;
        }

        public void addFriend(Profile person)
        {
            friends.Add(person);
        }

        public List<Profile> getFriends()
        {
            return friends;
        }

        public void addFriend(string name, Conversation conv)
        {
            conversations.Add(name, conv);
        }

        public Conversation getConversationWithName(string title)
        {
            Conversation value;
            if (conversations.TryGetValue(title, out value))
            {
                return value;
            }
            else
            {
                return null; ;
            }
        }
    }
}
