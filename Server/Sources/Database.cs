using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Server
{
    public class Database
    {
        private static List<Profile> allProfiles = new List<Profile>();
        private static List<Topic> allTopics = new List<Topic>();

        public static bool connectProfile(string id, string psw)
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

            return false;
        }

        public static void addNewProfile(string id, string psw)
        {
            allProfiles.Add(new Profile(id, psw));
            Console.WriteLine("The profile '" + id + "' has been created correctly. Password = '" + psw + "'");
        }

        public static List<Profile> getProfiles()
        {
            return allProfiles;
        }

        public static Profile getProfileN(int n)
        {
            return allProfiles[n];
        }

        public static int addNewTopic(string title) // return the place of the new topic
        {
            allTopics.Add(new Topic(title));
            return allTopics.Count()-1;
        }

        public static List<Topic> getTopics()
        {
            return allTopics;
        }

        public static Topic getTopic(int i)
        {
            return allTopics[i];
        }

        public static void save()
        {
            string filenamePro = new DirectoryInfo(Environment.CurrentDirectory).Parent.Parent.Parent.FullName + "\\dbs-profiles.out";
            FileStream fs1 = new FileStream(filenamePro, FileMode.Create);
            IFormatter formatter1 = new BinaryFormatter();
            formatter1.Serialize(fs1, allProfiles);
            fs1.Close();

            string filenameTop = new DirectoryInfo(Environment.CurrentDirectory).Parent.Parent.Parent.FullName + "\\dbs-topics.out";
            FileStream fs2 = new FileStream(filenameTop, FileMode.Create);
            IFormatter formatter2 = new BinaryFormatter();
            formatter2.Serialize(fs2, allTopics);
            fs2.Close();
        }

        public static void load()
        {
            try
            {
                string filenameTop = new DirectoryInfo(Environment.CurrentDirectory).Parent.Parent.Parent.FullName + "\\dbs-profiles.out";
                FileStream fs1 = new FileStream(filenameTop, FileMode.Open);
                IFormatter formatter1 = new BinaryFormatter();
                allProfiles = (List<Profile>)formatter1.Deserialize(fs1);
                fs1.Close();

                string filenameTop1 = new DirectoryInfo(Environment.CurrentDirectory).Parent.Parent.Parent.FullName + "\\dbs-topics.out";
                FileStream fs2 = new FileStream(filenameTop1, FileMode.Open);
                IFormatter formatter2 = new BinaryFormatter();
                allTopics = (List<Topic>)formatter2.Deserialize(fs2);
                fs2.Close();

                Console.WriteLine("Database loaded");
            }
            catch(Exception e)
            {
                Console.WriteLine("Can't load the database, no save file exists");
            }
        }
    }
}
