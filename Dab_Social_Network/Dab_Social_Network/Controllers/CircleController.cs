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
        private readonly CircleService circleService;
        private readonly UserService userService;

        public CircleController(CircleService circleService, UserService userService)
        {
            circleService = circleService;
            userService = userService;
        }
        //get all circles
        public ActionResult Index()
        {
            return View(circleService.GetAllCircles());
        }
        //get single instance of circle
        public ActionResult Details(string id)
        {
            if (id != null)
            {
                var circle = circleService.GetSingleCircle(id);
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
                _circleService.Create(circle);
                return RedirectToAction(nameof(Index));

            }

            return View();
        }

        public ActionResult AddUser(string userId, string circleName)
        {
            var user = _userService.Get(userId);

            var circles = _circleService.Get();
            var circle = new Circle();
            foreach (var c in circles)
            {
                if (c.Name == circleName)
                    circle = c;
            }

            if (user.CircleIds == null)
            {
                user.CircleIds = new List<string>();
            }

            if (circle.UserIds == null)
            {
                circle.UserIds = new List<string>();
            }

            user.CircleIds.Add(circle.CircleId);
            _userService.Update(user.UserId, user);

            circle.UserIds.Add(user.UserId);
            _circleService.Update(circle.CircleId, circle);

            return RedirectToAction("profile", "Session", new { id = user.UserId });
        }






        // GET: Circle/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var circle = _circleService.Get(id);
            if (circle == null)
            {
                return NotFound();
            }



            return View(circle);
        }


        // POST: Circle/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, Circle circle)
        {
            if (id != circle.CircleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _circleService.Update(id, circle);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                //return View(circleService.Get());
                return View(circle);


            }
        }





        // GET: Circle/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var circle = _circleService.Get(id);
            if (circle == null)
            {
                return NotFound();
            }

            return View(circle);
        }

        // POST: Circle/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {

            try
            {
                var circle = _circleService.Get(id);

                if (circle == null)
                {
                    return NotFound();
                }

                _circleService.Remove(circle.CircleId);

                return RedirectToAction(nameof(Index));

            }
            catch
            {
                return View();

            }
        }
    }
}
