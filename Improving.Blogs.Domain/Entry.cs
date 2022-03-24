using System;
using System.Collections.Generic;
using System.Linq;

namespace Improving.Blogs.Domain
{
    public abstract class Entry : EntryList
    {
        public Entry(string body, params string[] categories)
        {
            if (body == null) throw new ArgumentNullException("body");

            Body = body;
            Categories = categories;
        }

        public string Body { get; set; }
        public string[] Categories { get; set; }

        public bool HasCategory(string category)
        {
            return Categories.Contains(category);
        }
    }
}