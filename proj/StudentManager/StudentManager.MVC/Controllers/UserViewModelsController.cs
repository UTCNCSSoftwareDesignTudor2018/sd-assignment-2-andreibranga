using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Restaurant.Business.Repos.Repositories;
using Restaurant.Data.Entities;
using Restaurant.MVC.Helpers;
using Restaurant.MVC.Models.Account;

namespace Restaurant.MVC.Controllers
{
    public class UserViewModelsController : Controller
    {
        private StudentDbEntities db;
        private IUserRepository userRepository;
        public UserViewModelsController()
        {
            this.db = new StudentDbEntities();
            this.userRepository=new UserRepository(db);
        }

        public ActionResult Index()
        {
            string id = UserHelpers.GetCurUserID(User);

            ViewBag.HasProfile = userRepository.GetUserIdentityHasProfile(id);
            return PartialView();
        }

        // GET: UserViewModels/Create
        public ActionResult Create()
        {
            return PartialView();
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
                userRepository.AddUserProfile(UserHelpers.GetCurUserID(User),userViewModel.UserName,
                    userViewModel.StudentCurrentYear.Value,userViewModel.StudentIdNumber,
                    userViewModel.LastName,userViewModel.FirstName);

            }

            return PartialView(userViewModel);
        }

        // GET: UserViewModels/Edit/5
        public ActionResult Edit()
        {
            int id = userRepository.GetUserIdForIdentity(UserHelpers.GetCurUserID(User));

            var  user = userRepository.GetUserById(id);
            UserViewModel userViewModel=new UserViewModel()
            {
                UserId = user.UserId,
                Id = user.Id,
                UserName = user.UserName,
                StudentCurrentYear = user.StudentCurrentYear,
                StudentIdNumber = user.StudentIdNumber,
                LastName = user.LastName,
                FirstName = user.FirstName
            };
            if (userViewModel == null)
            {
                return HttpNotFound();
            }
            return PartialView(userViewModel);
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
                userRepository.UpdateUserProfileById(userViewModel.UserId,userViewModel.UserName,
                    userViewModel.StudentCurrentYear.Value,userViewModel.StudentIdNumber,
                    userViewModel.LastName,userViewModel.FirstName);
            }
            return PartialView(userViewModel);
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
