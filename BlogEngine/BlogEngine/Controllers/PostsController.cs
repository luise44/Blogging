using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogEngine.Domain.Services.Services;
using BlogEngine.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogEngine.Controllers
{
    public class PostsController : Controller
    {
        IPostRepository _postRepository;
        IUserRepository _userRepository;
        ICommentRepository _commentRepository;

        public PostsController(IPostRepository postRepository, IUserRepository userRepository, ICommentRepository commentRepository)
        {
            _postRepository = postRepository;
            _userRepository = userRepository;
            _commentRepository = commentRepository;
        }

        [Authorize(Roles = "Editor")]
        public IActionResult AdminView()
        {            
            List<PostModel> model = new List<PostModel>();

            var posts = _postRepository.GetPendingPosts();

            foreach (var p in posts)
            {
                PostModel postModel = new PostModel
                {
                    Content = p.Content,
                    Date = p.Date,
                    Id = p.Id,
                    Status = p.Status,
                    UserId = p.UserId,
                    UserName = _userRepository.GetById(p.UserId).Name
                };
                model.Add(postModel);
            }

            return View(model);
        }

        public IActionResult ViewPost(int id)
        {
            var post = _postRepository.GetById(id);

            var model = new PostModel
            {
                Content = post.Content,
                Date = post.Date,
                Id = post.Id,
                Status = post.Status,
                UserId = post.UserId,
                UserName = _userRepository.GetById(post.UserId).Name,
                Comments = _commentRepository.GetByPostId(post.Id).Select(x=> new CommentModel
                {
                    Content = x.Content,
                    Date = x.Date,
                    UserId = x.UserId,
                    UserName = x.UserId == 0 ? "Anonymous" : _userRepository.GetById(x.UserId).Name
                }).ToList()
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult AddComment(int id, string newComment)
        {
            var userIdClaim = HttpContext.User.FindFirst("Id")?.Value;
            var userId = userIdClaim == null ? 0 : int.Parse(userIdClaim.ToString());
            _commentRepository.AddComment(id, userId, newComment);

            return RedirectToAction("ViewPost", "Posts", new { id = id });
        }

        [HttpPost]
        public IActionResult ApprovePost(int id)
        {
            _postRepository.ApprovePost(id);

            return RedirectToAction("AdminView", "Posts");
        }

        [HttpPost]
        public IActionResult RejectPost(int id)
        {
            _postRepository.RejectPost(id);

            return RedirectToAction("AdminView", "Posts");
        }

        [Authorize(Roles = "Writer,Editor")]
        public IActionResult CreatePost()
        {
            return View(new PostModel());
        }

        [Authorize(Roles = "Writer,Editor")]
        public IActionResult SavePost(PostModel model)
        {
            var userIdClaim = HttpContext.User.FindFirst("Id")?.Value;
            var userId = int.Parse(userIdClaim.ToString());

            _postRepository.CreatePost(model.Content, userId);

            return RedirectToAction("Index", "Home");
        }
    }
}