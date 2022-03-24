using System;
using System.Collections.Generic;

namespace Improving.Blogs.Domain
{
    public abstract class EntryList
    {
        public List<Entry> FindEntriesByCategory(string category)
        {
            var found = new List<Entry>();

            Predicate<Entry> hasCategory = entry => entry.HasCategory(category);

            FindEntries(found, hasCategory);
            return found;
        }

        public void FindEntries(List<Entry> found, Predicate<Entry> isMatch)
        {
            foreach (Entry entry in GetEntries())
                if (isMatch(entry)) 
                    found.Add(entry);
        }

        public abstract IEnumerable<Entry> GetEntries();
    }
}