using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Restaurant.Data.Entities;
using Restaurant.MVC.Models.Account;

namespace Restaurant.MVC.Controllers.StudentManager
{
    public class UserViewModelsController : Controller
    {
        private StudentDbEntities db = new StudentDbEntities();

        // GET: UserViewModels
        public ActionResult Index()
        {
            return View(db.UserViewModels.ToList());
        }

        // GET: UserViewModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserViewModel userViewModel = db.UserViewModels.Find(id);
            if (userViewModel == null)
            {
                return HttpNotFound();
            }
            return View(userViewModel);
        }

        // GET: UserViewModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserViewModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserId,Id,UserName,StudentCurrentYear,StudentIdNumber,LastName,FirstName")] UserViewModel userViewModel)
        {
            if (ModelState.IsValid)
            {
                db.UserViewModels.Add(userViewModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(userViewModel);
        }

        // GET: UserViewModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserViewModel userViewModel = db.UserViewModels.Find(id);
            if (userViewModel == null)
            {
                return HttpNotFound();
            }
            return View(userViewModel);
        }

        // POST: UserViewModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserId,Id,UserName,StudentCurrentYear,StudentIdNumber,LastName,FirstName")] UserViewModel userViewModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userViewModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userViewModel);
        }

        // GET: UserViewModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserViewModel userViewModel = db.UserViewModels.Find(id);
            if (userViewModel == null)
            {
                return HttpNotFound();
            }
            return View(userViewModel);
        }

        // POST: UserViewModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserViewModel userViewModel = db.UserViewModels.Find(id);
            db.UserViewModels.Remove(userViewModel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
