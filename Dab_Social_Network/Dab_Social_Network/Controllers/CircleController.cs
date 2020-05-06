using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Dab_Social_Network.Models;
using Dab_Social_Network.Services;


namespace Dab_Social_Network.Controllers
{
    public class CircleController : Controller
    {
        private readonly CircleService circleService;
        private readonly UserService userService;

        public CircleController(CircleService circleService, UserService userService)
        {
            this.circleService = circleService;
            this.userService = userService;
        }
        //get all circles
        public ActionResult Index()
        {
            return View(circleService.Get());
        }
        //get single instance of circle
        public ActionResult Details(string id)
        {
            if (id != null)
            {
                var circle = circleService.Get(id);
                if (circle != null)
                {
                    return View(circle);
                }
            }
            return NotFound();
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        // POST: Circle/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Circle circle)
        {
            if (ModelState.IsValid)
            {
                circleService.Add(circle);

                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        public ActionResult AddUser(string userId, string circleName)
        {
            var user = userService.Get(userId);

            var circles = circleService.Get();
            var circle = new Circle();
            foreach (var c in circles)
            {
                if (c.Name == circleName)
                    circle = c;
            }
            user.CircleIds.ToList().Add(circle.Id);
            userService.Update(user, user.Id);

            circle.UserIds.ToList().Add(user.Id);
            circleService.Update(circle, circle.Id);

            return RedirectToAction("Profile", "Session", new { id = user.Id });
        }

        // GET: Circle/Edit/5
        public ActionResult Edit(string id)
        {
            if (id != null)
            {
                var circle = circleService.Get(id);
                if(circle != null)
                {
                    return View(circle);
                }
            }
            return NotFound();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        // GET: Circle/Delete/5
        public ActionResult Delete(string id)
        {
            if (id != null)
            {
                var circle = circleService.Get(id);
                if (circle != null)
                {
                    return View(circle);
                }
            }
            return NotFound();

        }
    }
}
