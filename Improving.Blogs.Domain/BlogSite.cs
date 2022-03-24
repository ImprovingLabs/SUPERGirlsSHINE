using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Improving.Blogs.Domain
{
    public class BlogSite : EntryList
    {
        public List<Blog> Blogs { get; private set; }

        public BlogSite()
        {
            Blogs = new List<Blog>();
        }

        public Blog CreateBlog(string name)
        {
            var blog = new Blog(name);
            Blogs.Add(blog);
            return blog;
        }

        public override IEnumerable<Entry> GetEntries()
        {
            foreach (var blog in Blogs)
                foreach (var entry in blog.GetEntries())
                    yield return entry;
        }
    }
}
