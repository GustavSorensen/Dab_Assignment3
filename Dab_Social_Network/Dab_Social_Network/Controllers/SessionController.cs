using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Dab_Social_Network.Models;
using Dab_Social_Network.Services;
using Dab_Social_Network.Models.ViewModels;


namespace Dab_Social_Network.Controllers
{
    public class SessionController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly Service<User> userService;
        private readonly PostService postService;
        private readonly Service<Circle> circleService;

        static volatile User user;
        static volatile ProfileViewModel UserList = new ProfileViewModel();


        public SessionController(ILogger<HomeController> logger, Service<User> userService, PostService postService, Service<Circle> circleService)
        {
            this.logger = logger;
            this.userService = userService;
            this.postService = postService;
            this.circleService = circleService;
        }
        public IActionResult Feed(string id)
        {
            List<Post> followerposts = new List<Post>();
            List<User> followers = new List<User>();
            List<User> blockedlist = new List<User>();
            if (user.FollowerIds != null)
            {
                // Get users we follow and their posts
                foreach (var follower in user.FollowerIds)
                {
                    if (user.BlockedUserIds.Contains(follower))
                    {
                        continue;
                    }
                    followers.Add(userService.Get(follower));
                    //if there are no posts just continue
                    if (followers.Last().PostIds == null)
                    {
                        continue;
                    }
                    foreach (var post in followers.Last().PostIds)
                    {
                        followerposts.Add(postService.Get(post));
                    }
                }
            }
            if (user.BlockedUserIds != null)
            {
                foreach (var bUser in user.BlockedUserIds)
                {
                    blockedlist.Add(userService.Get(bUser));
                }
            }

            ViewData["BlockedUsers"] = blockedlist;
            ViewData["FollowerPosts"] = followerposts;
            ViewData["Followers"] = followers;

            return View(UserList);
        }

        public IActionResult profile(string id)
        {
            if (_user == null)
            {
                _user = _userService.Get(id);
            }

            // Get all posts
            UserList.user = _user;
            if (_user.PostIds != null)
            {
                foreach (var PostId in _user.PostIds)
                {
                    UserList.posts.Add(_postService.Get(PostId));
                }
            }

            //Get all circles and comments
            if (_user.CircleIds != null)
            {
                foreach (var userCircleId in _user.CircleIds)
                {
                    UserList.circles.Add(_circleService.Get(userCircleId));
                    if (UserList.circles.Last().PostIds == null) // --- bedre metode?
                        continue;
                    foreach (var circlepost in UserList.circles.Last().PostIds)
                    {
                        UserList.circleposts.Add(_postService.Get(circlepost));
                    }
                }
            }

            //get all comments in circles

            return View(UserList);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public ActionResult GoToFeed()
        {
            if (_user != null)
            {
                return RedirectToAction("Feed", "Session", new { id = _user.UserId });
            }
            return NotFound();
        }

        public ActionResult GoToProfile()
        {
            if (_user != null)
            {
                return RedirectToAction("profile", "Session", new { id = _user.UserId });
            }
            return NotFound();
        }
    }
}
