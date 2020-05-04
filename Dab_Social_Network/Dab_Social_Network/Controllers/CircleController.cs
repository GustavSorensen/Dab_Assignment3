using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Dab_Social_Network.Models;
using Dab_Social_Network.Services;


namespace DAB3_SocialNetwork.Controllers
{
    public class CircleController : Controller
    {
        private readonly Service<Circle> circleService;
        private readonly Service<User> userService;

        public CircleController(Service<Circle> circleService, Service<User> userService)
        {
            this.circleService = circleService;
            this.userService = userService;
        }
        //get all circles
        public ActionResult Index()
        {
            return View(circleService.GetAll());
        }
        //get single instance of circle
        public ActionResult Details(string id)
        {
            if (id != null)
            {
                var circle = circleService.GetSingle(id);
                if (circle != null)
                {
                    return View(circle);
                }
            }
            return NotFound();
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
            var user = userService.GetSingle(userId);

            var circles = circleService.GetAll();
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

            return RedirectToAction("profile", "Session", new { id = user.Id });
        }

        // GET: Circle/Edit/5
        public ActionResult Edit(string id)
        {
            if (id != null)
            {
                var circle = circleService.GetSingle(id);
                if(circle != null)
                {
                    return View(circle);
                }
            }
            return NotFound();
        }


        // POST: Circle/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, Circle circle)
        {
            if (id != circle.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                circleService.Update(circle, id);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(circle);
            }
        }
        // GET: Circle/Delete/5
        public ActionResult Delete(string id)
        {
            if (id != null)
            {
                var circle = circleService.GetSingle(id);
                if (circle != null)
                {
                    return View(circle);
                }
            }
            return NotFound();

        }

        // POST: Circle/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            try
            {
                var circle = circleService.GetSingle(id);

                if (circle == null)
                {
                    return NotFound();
                }

                circleService.Delete(circle.Id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();

            }
        }
    }
}
