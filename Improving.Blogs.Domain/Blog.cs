using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Improving.Blogs.Domain
{
    public class Blog : EntryList
    {
        public string Title { get; set; }
        public List<Post> Posts { get; private set; }

        public event Action<Post> EntryPosted;

        private Blog() // for XML Serialization
        {
            Posts = new List<Post>();
        }

        public Blog(string title)
            : this()
        {
            if (title == null) throw new ArgumentNullException("Title");

            Title = title;
        }

        public Post PostEntry(string title, string body, params string[] categories)
        {
            var post = new Post(title, body, categories);
            Posts.Add(post);

            if (EntryPosted != null) EntryPosted(post);

            return post;
        }

        public override IEnumerable<Entry> GetEntries()
        {
            foreach (var post in Posts)
            {
                yield return post;

                foreach (var comment in post.GetEntries())
                    yield return comment;
            }
        }
    }
}
