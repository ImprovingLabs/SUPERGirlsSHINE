using System;
using System.Linq;
using Improving.Blogs.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Improving.Blogs.Tests
{
    [TestClass]
    public class CommentTests
    {
        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void ShouldRequireABody()
        {
            new Comment(null);
        }

        [TestMethod]
        public void ShouldSupportCategories()
        {
            string category = "category";
            string body = "body";
            Comment comment = new Comment(body, category);
            Assert.IsNotNull(comment.Categories);
            Assert.AreEqual(1, comment.Categories.Length);
            Assert.IsTrue(comment.Categories.Contains(category));
        }

    }
}
