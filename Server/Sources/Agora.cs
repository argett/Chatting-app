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
        private List<Comment> comments;

        public List<Comment> Comments
        {
            get => comments;
            set => comments = value;
        }

        public void addComment(string u, string m)
        {
            Comment c = new Comment(u, m);
            comments.Add(c);
        }
    }
}
