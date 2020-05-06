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
        private static volatile User loggedInUser;

        public UserController(UserService userService)
        {
            this.userService = userService;
        }
        // GET: User
        public ActionResult Index()
        {
            return View(userService.Get());
        }
        public ActionResult IndexToFollow()
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
        public ActionResult LogIn(string userId)
        {
            loggedInUser = userService.Get(userId);
            return RedirectToAction("Profile", "Session", new { id = userId });
        }
        // POST: User/Create
        [HttpPost]
        public ActionResult Create(User user)
        {
            try
            {
                userService.Add(user);
                loggedInUser = user;
                return RedirectToAction("Profile", "Session", new { id = user.Id });
            }
            catch
            {
                return View();
            }
        }

        public ActionResult AddFollower(string userId)
        {
            if(!loggedInUser.FollowerIds.Contains(userId))
            {
                loggedInUser.FollowerIds.Add(userId);
                userService.Update(loggedInUser, loggedInUser.Id);
            }
            return RedirectToAction("Profile", "Session", new { id = loggedInUser.Id });
        }

        public ActionResult BlockUser(string userId)
        {
            if (!loggedInUser.BlockedUserIds.Contains(userId))
            {
                loggedInUser.BlockedUserIds.ToList().Add(userId);
                userService.Update(loggedInUser, loggedInUser.Id);
            }
            return RedirectToAction("Profile", "Session", new { id = loggedInUser.Id });
        }

        // GET: User/Delete/5
        public ActionResult Delete(string userId)
        {
            try
            {
                var user = userService.Get(userId);

                if (user == null)
                {
                    return NotFound();
                }

                userService.Delete(user.Id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return NotFound();
            }
        }
    }
}