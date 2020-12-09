using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    [Serializable]
    public class Profile
    {
        public string login;
        private string password;

        private List<Profile> friends;
        private Dictionary<string, Conversation> conversations; // the name of the conversation with the pointer to it

        public Profile(string name, string pswd)
        {
            login = name;
            password = pswd;
            friends = new List<Profile>();
            conversations = new Dictionary<string, Conversation>();
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

        public void addConversation(string name, Conversation c)
        {
            conversations.Add(name, c);
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
                return null;
            }
        }

        public Dictionary<string, Conversation> getConversations()
        {
            return conversations;
        }

        public Dictionary<string, Conversation> getConversationsN(int i)
        {
            return conversations;
        }
    }
}
