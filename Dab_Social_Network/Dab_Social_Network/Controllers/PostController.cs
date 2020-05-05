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

        public PostController(PostService postService, UserService userService, CircleService circleService)
        {
            this.postService = postService;
            this.userService = userService;
            this.circleService = circleService;
        }
        // GET: Post
        public ActionResult Index()
        {
            return View(postService.Get());
        }

        // GET: Post/Create
        [HttpGet]
        [AutoValidateAntiforgeryToken]
        public ActionResult Create(string id)
        {
            PostViewModel viewModel = new PostViewModel();

            ViewData["Circles"] = viewModel.circles;

            Post post = new Post()
            {
                UserId = id,
            };
            return View();
        }

        // POST: Post/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PostViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.post.TimeCreated= DateTime.Now;
                var createdPost = postService.Add(model.post);

                var user = userService.Get(model.post.Id);

                if (user != null)
                {
                    var circles = circleService.Get();
                    Circle circle = new Circle();

                    foreach (var c in circles)
                    {
                        if (c.Name == model.circle.Name)
                        {
                            circle = c;
                        }
                    }
                    circle.PostIds.ToList().Add(createdPost.Id);
                    circleService.Update(circle, circle.Id);

                    if (string.IsNullOrEmpty(model.circle.Name))
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
                    id = userService.Get(model.post.Id).Id 
                });
            }
            return View();
        }


        [HttpGet]
        [AutoValidateAntiforgeryToken]
        public ActionResult CreateComment(string userId, string postId)
        {
            ViewData["Id"] = userId;

            var post = postService.Get(postId);

            var viewModel = new PostViewModel
            {
                post = post,
                comment = new Comment()
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateComment(PostViewModel viewModel)
        {
            viewModel.comment.TimeCreated = DateTime.Now;

            var post = postService.Get(viewModel.post.Id);

            post.Comments.ToList().Add(viewModel.comment);

            postService.Update(post, post.Id);

            return RedirectToAction("profile", "Session", new
            {
                id = viewModel.comment.Id
            });
        }

        // GET: Post/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = postService.Get(id);
            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }

        // POST: Post/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
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
                return View();
            }
        }
    }
}