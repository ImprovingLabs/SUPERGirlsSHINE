using System;
using System.Collections.Generic;

namespace Improving.Blogs.Domain
{
    public class Post : Entry
    {
        private Post() : base(string.Empty)
        {
            Title = string.Empty;
            Comments = new List<Comment>();
        }

        public Post(string title, string body, params string[] categories)
            : base(body, categories)
        {
            if (title == null) throw new ArgumentNullException("title");

            Title = title;
            Comments = new List<Comment>();
        }

        public string Title { get; set; }
        public List<Comment> Comments { get; private set; }

        public Comment Comment(string body, params string[] categories)
        {
            var comment = new Comment(body, categories);
            Comments.Add(comment);
            return comment;
        }

        public override IEnumerable<Entry> GetEntries()
        {
            foreach (var comment in Comments)
            {
                yield return comment;

                foreach (var entry in comment.GetEntries())
                    yield return entry;
            }
        }
    }
}