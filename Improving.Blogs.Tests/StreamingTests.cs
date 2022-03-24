using System.IO;
using System.Xml.Serialization;
using Improving.Blogs.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Improving.Blogs.Tests
{
    [TestClass]
    public class StreamingTests
    {
        [TestClass]
        public class BlogStoreTest
        {
            [TestMethod]
            public void ShouldSaveAndLoadBlog()
            {
                string blogTitle = "My Blog";
                string postTitle = "Title";
                string postBody = "Body";
                string category = "Category";
                string comment = "Comment";

                var blog = new Blog(blogTitle);
                Post post = blog.PostEntry(postTitle, postBody, category);
                post.Comment(comment, category);
                post.Comment(comment, category);
                post.Comment(comment, category);
                post.Comment(comment, category);

                string filename = "BlogStore.csv";

                var store = new BlogStore(filename);
                store.Save(blog);

                Assert.IsTrue(File.Exists(filename));

                store = new BlogStore(filename);
                Blog loaded = store.Load();

                Assert.IsNotNull(loaded);
                Assert.AreEqual(blogTitle, loaded.Title);
                Assert.IsNotNull(loaded.Posts);
                Assert.AreEqual(1, loaded.Posts.Count);
                Assert.AreEqual(1, loaded.Posts[0].Categories.Length);
            }
        }

        [TestClass]
        public class XmlSerializationTest
        {
            [TestMethod]
            public void ShouldSerializeBlogAsXml()
            {
                string filename = "blog.xml";
                var serializer = new XmlSerializer(typeof (Blog));

                string blogTitle = "My Blog";
                string postTitle = "Title";
                string postBody = "Body";
                string category = "Category";
                string comment = "Comment";

                var blog = new Blog(blogTitle);
                Post post = blog.PostEntry(postTitle, postBody, category);
                post.Comment(comment, category);
                post.Comment(comment, category);
                post.Comment(comment, category);
                post.Comment(comment, category);
                using (var writer = new StreamWriter(filename))
                {
                    serializer.Serialize(writer, blog);
                }

                using (var reader = new StreamReader(filename))
                {
                    var loaded = serializer.Deserialize(reader) as Blog;
                    Assert.IsNotNull(loaded);
                }
            }
        }
    }
}