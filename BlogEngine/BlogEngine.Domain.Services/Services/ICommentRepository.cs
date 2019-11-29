using BlogEngine.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogEngine.Domain.Services.Services
{
    public interface ICommentRepository
    {
        IList<Comment> GetAll();
        Comment GetById(int id);
        IList<Comment> GetByPostId(int postId);
        void AddComment(int postId, int userId, string comment);
    }
}
