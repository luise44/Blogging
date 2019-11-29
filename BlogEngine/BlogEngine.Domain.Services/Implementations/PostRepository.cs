using BlogEngine.Data;
using BlogEngine.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using BlogEngine.Domain.Services.Services;

namespace BlogEngine.Domain.Services.Implementations
{
    public class PostRepository: IPostRepository
    {
        private readonly BlogEngineContext _context;

        public PostRepository(BlogEngineContext context)
        {
            _context = context;
        }

        public IList<Post> GetAll()
        {
            return _context.Posts.ToArray();
        }

        public Post GetById(int id)
        {
            return _context.Posts.Where(x=> x.Id == id).FirstOrDefault();
        }

        public IList<Post> GetPendingPosts()
        {
            return _context.Posts.Where(x=>x.Status == 1).ToArray();
        }

        public IList<Post> GetApprovedPosts()
        {
            return _context.Posts.Where(x => x.Status == 2).ToArray();
        }

        public void ApprovePost(int id)
        {
            var post = _context.Posts.Where(x=> x.Id == id).First();
            post.Status = 2;
            _context.SaveChanges();
        }

        public void RejectPost(int id)
        {
            var post = _context.Posts.Where(x => x.Id == id).First();
            post.Status = 3;
            _context.SaveChanges();
        }

        public void CreatePost(string content, int userId)
        {
            var newPost = new Post
            {
                Content = content,
                Date = DateTime.Now,
                Status = 1,
                UserId = userId
            };
            _context.Add(newPost);
            _context.SaveChanges();
        }
    }
}
