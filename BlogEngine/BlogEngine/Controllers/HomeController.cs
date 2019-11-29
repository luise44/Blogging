using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BlogEngine.Models;
using BlogEngine.Domain.Services.Services;

namespace BlogEngine.Controllers
{
    public class HomeController : Controller
    {
        IPostRepository _postRepository;
        IUserRepository _userRepository;

        public HomeController(IPostRepository postRepository, IUserRepository userRepository)
        {
            _postRepository = postRepository;
            _userRepository = userRepository;
        }

        public IActionResult Index()
        {
            List<PostModel> model = new List<PostModel>();

            var posts = _postRepository.GetApprovedPosts();

            foreach(var p in posts)
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

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
