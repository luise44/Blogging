using BlogEngine.Data;
using BlogEngine.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using BlogEngine.Domain.Services.Services;

namespace BlogEngine.Domain.Services.Implementations
{
    public class CommentRepository : ICommentRepository
    {
        private readonly BlogEngineContext _context;

        public CommentRepository(BlogEngineContext context)
        {
            _context = context;
        }

        public IList<Comment> GetAll()
        {
            return _context.Comments.ToArray();
        }

        public Comment GetById(int id)
        {
            return _context.Comments.Where(x=>x.Id == id).FirstOrDefault();
        }

        public IList<Comment> GetByPostId(int postId)
        {
            return _context.Comments.Where(x=>x.PostId == postId).ToArray();
        }

        public void AddComment(int postId, int userId, string comment)
        {
            _context.Add(new Comment
            {
                Content = comment,
                Date = DateTime.Now,
                PostId = postId,
                UserId = userId
            });
            _context.SaveChanges();
        }
    }
}
