using BlogEngine.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogEngine.Domain.Services.Services
{
    public interface IPostRepository
    {
        IList<Post> GetAll();
        Post GetById(int id);
        IList<Post> GetPendingPosts();
        IList<Post> GetApprovedPosts();
        void ApprovePost(int id);
        void RejectPost(int id);
        void CreatePost(string content, int userId);
    }
}
