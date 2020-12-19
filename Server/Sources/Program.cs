using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Database.load();
            /*
            TEST REALISES POUR LA CREATION DE LA BDD

            Database.addNewTopic("latin");
            Database.getTopic(0).addComment("xXx_D4rk_Sasuk3_xXx", "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum");
            Database.getTopic(0).addComment("le_genie du MAL", "Ut enim ad minima veniam, quis nostrum exercitationem ullam corporis suscipit laboriosam, nisi ut aliquid ex ea commodi consequatur?");
            Database.getTopic(0).addComment("xXx_D4rk_Sasuk3_xXx", "Neque porro quisquam est, qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit, sed quia non numquam eius modi tempora incidunt ut labore et dolore magnam aliquam quaerat voluptatem");
            Database.getTopic(0).addComment("le_genie du MAL", "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum");
            Database.getTopic(0).addComment("Charles xavier", "Quis autem vel eum iure reprehenderit qui in ea voluptate velit esse quam nihil molestiae consequatur, vel illum qui dolorem eum fugiat quo voluptas nulla pariatur");
            Database.getTopic(0).addComment("xXx_D4rk_Sasuk3_xXx", "Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam, eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo. Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit aut fugit, sed quia consequuntur magni dolores eos qui ratione voluptatem sequi nesciunt");
            Database.addNewTopic("prenoms droles");
            Database.getTopic(1).addComment("Jean Bonneau", "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum");
            Database.getTopic(1).addComment("Kelly Diote", "Ut enim ad minima veniam, quis nostrum exercitationem ullam corporis suscipit laboriosam, nisi ut aliquid ex ea commodi consequatur?");
            Database.getTopic(1).addComment("Alain Posteur", "Neque porro quisquam est, qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit, sed quia non numquam eius modi tempora incidunt ut labore et dolore magnam aliquam quaerat voluptatem");
            Database.getTopic(1).addComment("Nain posteur", "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. ");
            Database.getTopic(1).addComment("Cathy Mini", "At vero eos et accusamus et iusto odio dignissimos ducimus qui blanditiis praesentium voluptatum deleniti atque corrupti quos dolores et quas molestias excepturi sint occaecati cupiditate non provident, similique sunt in culpa qui officia deserunt mollitia animi, id est laborum et dolorum fuga. Et harum quidem rerum facilis est et expedita distinctio");
            Database.getTopic(1).addComment("Oussama Lairbizar", "Sed totam rem aperiam, eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo. Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit aut fugit, sed quia consequuntur magni dolores eos qui ratione voluptatem sequi nesciunt");
            Database.addNewTopic("espace");
            Database.getTopic(2).addComment("Trou", "Noir");
            Database.getTopic(2).addComment("Etoile", "Soleil");
            Database.addNewTopic("/r");
            Database.getTopic(3).addComment("oui", "AmlTheAche013");
            Database.getTopic(3).addComment("non", "blog");
            Database.getTopic(3).addComment("hey", "announcement");
            Database.getTopic(3).addComment("yo", "18-25");

            Database.addNewProfile("li", "fg");
            Database.addNewProfile("oui", "non");
            Database.addNewProfile("a", "b");
            Database.getProfileN(0).addFriend(Database.getProfileN(1));
            Database.getProfileN(1).addFriend(Database.getProfileN(0));
            Conversation conv = new Conversation(Database.getProfileN(0), Database.getProfileN(1));
            Database.getProfileN(0).addConversation("un nom super cool", conv);
            Database.getProfileN(1).addConversation("un nom super cool", conv);
            Database.getProfileN(0).addFriend(Database.getProfileN(2));
            Database.getProfileN(2).addFriend(Database.getProfileN(0));
            conv = new Conversation(Database.getProfileN(0), Database.getProfileN(2));
            Database.getProfileN(0).addConversation("un autre nom super cool", conv);
            Database.getProfileN(2).addConversation("un autre nom super cool", conv);
            */

            Server serv = new Server(8976);
            serv.start();

        }
    }
}
