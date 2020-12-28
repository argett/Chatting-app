using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    [Serializable]
    public abstract class Agora
    {
        private List<Comment> discussion;


        public List<Comment> Discussion
        {
            get => discussion;
            set => discussion = value;
        }

        public void addComment(string u, string m)
        {
            Comment c = new Comment(u, m);
            discussion.Add(c);
        }
    }
}
