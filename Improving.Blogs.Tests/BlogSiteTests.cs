using System;
using System.Collections.Generic;
using Improving.Blogs.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Improving.Blogs.Tests
{
    [TestClass]
    public class BlogSiteTests
    {
        [TestMethod]
        public void NewSiteShouldHaveNoBlogs()
        {
            BlogSite site = new BlogSite();
            Assert.AreEqual(0, site.Blogs.Count);
        }

        [TestMethod]
        public void ShouldSupportCreatingBlogs()
        {
            BlogSite site = new BlogSite();
            Blog blog = site.CreateBlog("Blog Title");
            Assert.IsNotNull(blog);
        }

        [TestMethod]
        public void ShouldMaintainAListOfCreatedBlogs()
        {
            BlogSite site = new BlogSite();
            Blog blog = site.CreateBlog("Blog Title");
            Assert.IsNotNull(site.Blogs);
            Assert.AreEqual(1, site.Blogs.Count);
            Assert.AreEqual(blog, site.Blogs[0]);
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void ShouldRequireABlogTitleToCreateABlog()
        {
            new BlogSite().CreateBlog(null);
        }

        [TestMethod]
        public void ShouldFindPostsByCategory()
        {
            string category = "category";
            Blog blog = new Blog("title");
            Post toFind = blog.PostEntry("title", "body", category);
            Post toNotFind = blog.PostEntry("title2", "body2");
            List<Entry> found = blog.FindEntriesByCategory(category);
            Assert.AreEqual(1, found.Count);
            Assert.AreEqual(toFind, found[0]);
        }

        [TestMethod]
        public void ShouldFindNoPostsByCategoryWhenBlogIsEmpty()
        {
            Blog blog = new Blog("title");
            List<Entry> posts = blog.FindEntriesByCategory("category");
            Assert.IsNotNull(posts);
            Assert.AreEqual(0, posts.Count);
        }

        [TestMethod]
        public void ShouldFindPostsByCategories()
        {
            string category = "category";

            BlogSite site = new BlogSite();
            Blog blog1 = site.CreateBlog("blog1");
            Blog blog2 = site.CreateBlog("blog2");
            Post toFind1 = blog1.PostEntry("title", "body", category, "category2");
            Post toNotFind1 = blog1.PostEntry("title", "body", "category2");
            Post toFind2 = blog2.PostEntry("title", "body", category);
            Post toNotFind2 = blog2.PostEntry("title", "body");

            List<Entry> posts = site.FindEntriesByCategory(category);
            Assert.AreEqual(2, posts.Count);
            Assert.IsTrue(posts.Contains(toFind1));
            Assert.IsTrue(posts.Contains(toFind2));
        }

    }

}
