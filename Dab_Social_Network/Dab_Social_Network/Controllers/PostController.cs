using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dab_Social_Network.Models;
using Dab_Social_Network.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DAB3_SocialNetwork.Controllers
{
    public class PostController : Controller
    {
        private readonly PostService postService;
        private readonly Service<User> userService;
        private readonly Service<Circle> circleService;

        public PostController(PostService postService, Service<User> userService, Service<Circle> circleService)
        {
            this.postService = postService;
            this.userService = userService;
            this.circleService = circleService;
        }
        // GET: Post
        public ActionResult Index()
        {
            return View(postService.GetAll());
        }

        // GET: Post/Create
        [HttpGet]
        [AutoValidateAntiforgeryToken]
        public ActionResult Create(string id)
        {
            ViewData["Id"] = id;

            var model = new PostCreateViewModel()
            {
                post = new Post(),
                Circle = ""
            };


            return View();
        }

        // POST: Post/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PostCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.post.Time = DateTime.Now;
                var createdPost = postService.Add(model.post);

                var user = userService.GetSingle(model.post.Id);

                if (user != null)
                {
                    if (user.PostIds == null)
                    {
                        user.PostIds = new List<string>();
                    }

                    var circles = circleService.GetAll();
                    Circle circle = new Circle();

                    foreach (var c in circles)
                    {
                        if (c.Name == model.Circle)
                            circle = c;
                    }

                    if (circle.PostIds == null)
                    {
                        circle.PostIds = new List<string>();
                    }

                    circle.PostIds.Add(createdPost.PostId);
                    _circleService.Update(circle.CircleId, circle);

                    if (string.IsNullOrEmpty(model.Circle))
                    {
                        user.PostIds.Add(createdPost.PostId);
                        _userService.Update(user.UserId, user);
                    }
                }
                else
                {
                    return NotFound();
                }


                return RedirectToAction("profile", "Session", new { id = _userService.Get(model.post.Author).UserId });
            }

            return View();
        }


        [HttpGet]
        [AutoValidateAntiforgeryToken]
        public ActionResult CreateComment(string userId, string postId)
        {
            ViewData["Author"] = userId;

            var post = _postService.Get(postId);

            var viewModel = new PostCommentViewModel
            {
                Post = post,
                Comment = new Comment()
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateComment(PostCommentViewModel postComment)
        {
            postComment.Comment.Time = DateTime.Now;

            var post = _postService.Get(postComment.Post.PostId);

            if (post.Comments == null)
            {
                post.Comments = new List<Comment>();
            }

            post.Comments.Add(postComment.Comment);

            _postService.Update(post.PostId, post);

            return RedirectToAction("profile", "Session", new
            {
                id = postComment.Comment.Author
            });
        }





        // GET: Post/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}


        // POST: Post/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add update logic here

        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}




        // GET: Post/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = _postService.Get(id);
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
                var post = _postService.Get(id);

                if (post == null)
                {
                    return NotFound();
                }

                _postService.Remove(post.PostId);

                return RedirectToAction(nameof(Index));

            }
            catch
            {
                return View();

            }
        }
    }
}