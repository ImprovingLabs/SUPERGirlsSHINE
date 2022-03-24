using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Improving.Blogs.Domain;

namespace Improving.Blogs.Tests
{
    [TestClass]
    public class BlogTests
    {
        [TestMethod]
        public void ShouldPostAnEntry()
        {
            string title = "Title";
            string body = "Body";

            Blog blog = new Blog(title);
            Post post = blog.PostEntry(title, body);

            Assert.IsNotNull(post);
            Assert.AreEqual(title, post.Title);
            Assert.AreEqual(body, post.Body);
        }


        [TestMethod]
        public void ShouldRememberPosts()
        {
            Blog blog = new Blog("title");
            blog.PostEntry("title", "body");
            Assert.IsNotNull(blog.Posts);
            Assert.AreEqual(1, blog.Posts.Count);
        }

        [TestMethod]
        public void ShouldFindCategorizedCommentsWhenSearchingForEntriesByCategory()
        {
            string category = "category";

            Blog blog = new Blog("title");
            Post post = blog.PostEntry("title", "body", category);
            Comment comment = post.Comment("comment", category);
            List<Entry> entries = blog.FindEntriesByCategory(category);
            Assert.IsNotNull(category);
            Assert.AreEqual(2, entries.Count);
            Assert.IsTrue(entries.Contains(post));
            Assert.IsTrue(entries.Contains(comment));
        }

    }
}
