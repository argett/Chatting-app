using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatting_App
{
    class Database
    {
        private List<Profile> allProfiles;
        private List<Topic> allTopics;

        public Database()
        {
            allProfiles = new List<Profile>();
            allTopics = new List<Topic>();
        }

        public bool connectProfile(string id, string psw)
        {
            // check if the profile exists
            if(allProfiles != null)
            {
                foreach (Profile pro in allProfiles)
                {
                    if (pro.login == id && pro.getPassword() == psw)
                        return true;
                }
            }
            else
            {
                Console.WriteLine("The database is empty, there is no user");
            }
            return false;
        }

        public void addNewProfile(string id, string psw)
        {
            allProfiles.Add(new Profile(id, psw));
            Console.WriteLine("Your profile '" + id + "' has been created correctly. Password = '" + psw + "'\n");
        }
    }
}
