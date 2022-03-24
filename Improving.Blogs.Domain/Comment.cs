using System.Collections.Generic;

namespace Improving.Blogs.Domain
{
    public class Comment : Entry
    {
        public Comment()
            : base(string.Empty)
        { }

        public Comment(string body, params string[] categories)
            : base(body, categories)
        { }

        public override IEnumerable<Entry> GetEntries()
        {
            yield break;
        }
    }
}