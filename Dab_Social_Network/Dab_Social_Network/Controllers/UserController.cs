using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dab_Social_Network.Services;
using Dab_Social_Network.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace Dab_Social_Network.Controllers
{
    public class UserController : Controller
    {
        private readonly UserService userService;

        public UserController(UserService userService)
        {
            this.userService = userService;
        }
        // GET: User
        public ActionResult Index()
        {
            return View(userService.Get());
        }
        // GET: User/Details/5
        public ActionResult Details(string id)
        {
            return View(userService.Get(id));
        }
        public ActionResult Create()
        {
            return View();
        }
        // POST: User/Create
        [HttpPost]
        public ActionResult Create(User user)
        {
            try
            {
                userService.Add(user);
                return NotFound();
                //return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult AddFollower(string userId, string userToFollow)
        {
            var user = userService.Get(userToFollow);

            user.FollowerIds.ToList().Add(userId);
            
            return RedirectToAction("profile", "Session", new { id = user.Id });
        }

        public ActionResult BlockUser(string userId, string userToBlock)
        {
            var user = userService.Get(userToBlock);

            user.BlockedUserIds.ToList().Add(userId);

            return RedirectToAction("profile", "Session", new { id = user.Id });
        }

        // GET: User/Delete/5
        public ActionResult Delete(string id)
        {
            try
            {
                userService.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}