using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Improving.Blogs.Domain
{
    public class BlogStore
    {
        private const string BlogTag = "Blog";
        private const string PostTag = "Post";
        private const string CommentTag = "Comment";
        private const char DelimiterTag = ',';

        private string filename;

        public BlogStore(string filename)
        {
            this.filename = filename;
        }

        public void Save(Blog blog)
        {
            using (StreamWriter writer = new StreamWriter(filename))
            {
                writer.WriteLine(BlogTag + DelimiterTag + blog.Title);
                foreach (var post in blog.Posts)
                {
                    writer.Write(PostTag + DelimiterTag + post.Title);
                    writer.Write(DelimiterTag + post.Body);
                    foreach (var category in post.Categories)
                        writer.Write(DelimiterTag + category);
                    writer.WriteLine();

                    foreach (var comment in post.Comments)
                    {
                        writer.Write(CommentTag + DelimiterTag + comment.Body);
                        foreach (var category in comment.Categories)
                            writer.Write(DelimiterTag + category);
                        writer.WriteLine();
                    }
                }
            }
        }

        public Blog Load()
        {
            using (StreamReader reader = new StreamReader(filename))
            {
                string line = null;
                Blog blog = null;
                Post post = null;
                Comment comment = null;

                while ((line = reader.ReadLine()) != null)
                {
                    string[] tokens = line.Split(DelimiterTag);

                    var token = tokens[0];
                    switch (token)
                    {
                        case BlogTag:
                            var btitle = tokens[1];
                            blog = new Blog(btitle);
                            break;
                        case PostTag:
                            var ptitle = tokens[1];
                            var pbody = tokens[2];
                            var pcategories = GetCategories(tokens, 3);
                            post = blog.PostEntry(ptitle, pbody, pcategories);
                            break;
                        case CommentTag:
                            var cbody = tokens[1];
                            var ccategories = GetCategories(tokens, 2);
                            comment = post.Comment(cbody, ccategories);
                            break;
                        default:
                            throw new ArgumentException(token);
                    }
                }
                return blog;
            }
        }

        private string[] GetCategories(string[] tokens, int index)
        {
            var cats = new string[tokens.Length - index];
            for (int i = index; i < tokens.Length; i++)
                cats[i - index] = tokens[i];
            return cats;
        }
    }
}
