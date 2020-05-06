using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dab_Social_Network.Models;
using Dab_Social_Network.Services;
using Dab_Social_Network.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Dab_Social_Network.Controllers
{
    public class PostController : Controller
    {
        private readonly PostService postService;
        private readonly UserService userService;
        private readonly CircleService circleService;
        private readonly CommentService commentService;

        public PostController(PostService postService, UserService userService, CircleService circleService, CommentService commentService)
        {
            this.commentService = commentService;
            this.postService = postService;
            this.userService = userService;
            this.circleService = circleService;
        }
        // GET: Post
        public ActionResult Index()
        {
            return View(postService.Get());
        }
        [HttpGet]
        [AutoValidateAntiforgeryToken]
        public ActionResult Create(string userId)
        {
            ViewData["UserId"] = userId;
            ViewData["TimeCreated"] = DateTime.Now.ToString();
            return View(); //gets the create view
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public ActionResult Create(Post post)
        {
            try 
            {
                post.TimeCreated = DateTime.Now;
                postService.Update(post, post.Id);
                postService.Add(post);
                var user = userService.Get(post.UserId);
                user.PostIds.Add(post.Id);
                userService.Update(user, user.Id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return NotFound();
            }
        }
        // GET: Post/Create
        [HttpGet]
        [AutoValidateAntiforgeryToken]
        public ActionResult CreateToCircle(string userId, string circleId)
        {
            ViewData["id"] = userId;
            var circle = circleService.Get(circleId);
            var model = new PostViewModel()
            {
                Post = new Post(),
                Circle = circle
            };
            return View();
        }

        // POST: Post/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateToCirlce(PostViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.Post.TimeCreated= DateTime.Now;
                var createdPost = postService.Add(model.Post);

                var user = userService.Get(model.Post.Id);

                if (user != null)
                {
                    var circles = circleService.Get();
                    Circle circle = new Circle();

                    foreach (var c in circles)
                    {
                        if (c.Name == model.Circle.Name)
                        {
                            circle = c;
                        }
                    }
                    circle.PostIds.ToList().Add(createdPost.Id);
                    circleService.Update(circle, circle.Id);

                    if (string.IsNullOrEmpty(model.Circle.Name))
                    {
                        user.PostIds.ToList().Add(createdPost.Id);
                        userService.Update(user, user.Id);
                    }
                }
                else
                {
                    return NotFound();
                }
                return RedirectToAction("profile", "Session", new 
                {
                    id = userService.Get(model.Post.Id).Id 
                });
            }
            return View();
        }

        [HttpGet]
        [AutoValidateAntiforgeryToken]
        public ActionResult CreateComment(string userId, string postId)
        {
            ViewData["UserId"] = userId;
            ViewData["PostId"] = postId;

            var post = postService.Get(postId);

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateComment(Comment comment)
        {
            comment.TimeCreated = DateTime.Now;

            var post = postService.Get(comment.PostId);

            post.Comments.Add(comment);

            postService.Update(post, post.Id);
            commentService.Update(comment, comment.Id);
            return RedirectToAction("Profile", "Session", new
            {
                id = comment.UserId
            });
        }
         [HttpGet]
        // GET: Post/Delete/5
        public ActionResult Delete(string id)
        {
            try
            {
                var post = postService.Get(id);

                if (post == null)
                {
                    return NotFound();
                }

                postService.Delete(post.Id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return NotFound();
            }
        }
    }
}