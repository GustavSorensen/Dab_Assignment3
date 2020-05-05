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
        private readonly UserService userService;
        private readonly PostService postService;
        private readonly CircleService circleService;

        static volatile User user;
        static volatile ProfileViewModel profileViewModel = new ProfileViewModel();


        public SessionController(ILogger<HomeController> logger, UserService userService, PostService postService, CircleService circleService)
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

            return View(profileViewModel);
        }

        public IActionResult profile(string id)
        {
            if (user == null)
            {
                user = userService.Get(id);
            }

            // Get all posts
            profileViewModel.User = user;
            if (user.PostIds != null)
            {
                foreach (var PostId in user.PostIds)
                {
                    profileViewModel.UserPosts.ToList().Add(postService.Get(PostId));
                }
            }

            //Get all circles and posts
            if (user.CircleIds != null)
            {
                foreach (var userCircleId in user.CircleIds)
                {
                    profileViewModel.Circles.ToList().Add(circleService.Get(userCircleId));
                    if (profileViewModel.Circles.ToList().Last().PostIds == null)
                    {
                        continue;
                    }
                    foreach (var post in profileViewModel.Circles.Last().PostIds)
                    {
                        profileViewModel.CirclePosts.ToList().Add(postService.Get(post));
                    }
                }
            }
            return View(profileViewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public ActionResult GoToFeed()
        {
            if (user != null)
            {
                return RedirectToAction("Feed", "Session", new { id = user.Id });
            }
            return NotFound();
        }

        public ActionResult GoToProfile()
        {
            if (user != null)
            {
                return RedirectToAction("profile", "Session", new { id = user.Id });
            }
            return NotFound();
        }
    }
}
