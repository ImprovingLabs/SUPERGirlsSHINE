using System;
using Improving.Blogs.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Improving.Blogs.Tests
{
    [TestClass]
    public class PostTests
    {
        [TestMethod]
        public void ShouldBeAbleToSubscribeToABlog()
        {
            bool alerted = false;
            Blog blog = new Blog("title");
            Action<Post> subscription = delegate(Post post) { alerted = true; };
            blog.EntryPosted += subscription;

            blog.PostEntry("title", "body");

            Assert.IsTrue(alerted);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void ShouldRequireATitle()
        {
            new Blog("Title").PostEntry(null, "Body");
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void ShouldRequireABody()
        {
            new Blog("Title").PostEntry("Title", null);
        }

        [TestMethod]
        public void ShouldSupportPostingWithCategoryArray()
        {
            string category1 = "category1";
            string category2 = "category2";

            string[] categories = { category1, category2 };

            Blog blog = new Blog("title");
            Post post = blog.PostEntry("Title", "Body", categories);

            Assert.IsNotNull(post.Categories);
            Assert.AreEqual(2, post.Categories.Length);
        }

        [TestMethod]
        public void ShouldSupportPostingWithCategories()
        {
            string category1 = "category1";
            string category2 = "category2";

            Blog blog = new Blog("title");
            Post post = blog.PostEntry("Title", "Body", category1, category2);

            Assert.IsNotNull(post.Categories);
            Assert.AreEqual(2, post.Categories.Length);
        }

        [TestMethod]
        public void ShouldSupportPostingWithNoCategories()
        {
            Blog blog = new Blog("title");
            Post post = blog.PostEntry("Title", "Body");

            Assert.IsNotNull(post.Categories);
            Assert.AreEqual(0, post.Categories.Length);
        }

        [TestMethod]
        public void ShouldAllowCommentOnPost()
        {
            Blog blog = new Blog("title");
            Post post = blog.PostEntry("title", "body");

            string body = "comment body";
            var comment = post.Comment(body);

            Assert.IsNotNull(comment);
            Assert.AreEqual(1, post.Comments.Count);
            Assert.AreEqual(body, post.Comments[0].Body);
        }
    }
}
