using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogEngine.Domain.Services.Services;
using BlogEngine.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogEngine.Controllers
{
    [Route("api/posts")]
    [ApiController]
    public class PostsApiController : ControllerBase
    {
        IPostRepository _postRepository;
        IUserRepository _userRepository;

        public PostsApiController(IPostRepository postRepository, IUserRepository userRepository)
        {
            _postRepository = postRepository;
            _userRepository = userRepository;
        }

        [HttpGet("pending")]
        public List<PostModel> GetPending()
        {
            var posts = _postRepository.GetPendingPosts();
            var response = new List<PostModel>();
            foreach (var post in posts)
            {
                response.Add(new PostModel
                {
                    Id = post.Id,
                    Content = post.Content,
                    Date = post.Date,
                    UserId = post.UserId,
                    UserName = _userRepository.GetById(post.UserId).Name
                });
            }

            return response;
        }

        [HttpGet("approve/{id}")]
        public object Approve(int id)
        {
            try
            {
                var post = _postRepository.GetById(id);

                if (post == null)
                {
                    throw new Exception("Post no found.");
                }

                _postRepository.ApprovePost(id);
                return new { message = "Post Approved." };
            }            
            catch(Exception ex)
            {
                return BadRequest(new { message = "Error approving post.", error = ex.Message });
            }            
        }

        [HttpGet("reject/{id}")]
        public object Reject(int id)
        {
            try
            {
                var post = _postRepository.GetById(id);

                if (post == null)
                {
                    throw new Exception("Post not found.");
                }

                _postRepository.RejectPost(id);
                return new { message = "Post Rejected." };
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Error rejecting post.", error = ex.Message });
            }                       
        }
    }
}