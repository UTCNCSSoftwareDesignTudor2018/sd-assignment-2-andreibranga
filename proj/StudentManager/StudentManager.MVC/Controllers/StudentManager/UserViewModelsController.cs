using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Restaurant.Business;
using Restaurant.Business.Repos.Repositories;
using Restaurant.Data.Entities;
using Restaurant.MVC.Helpers;
using Restaurant.MVC.Models;
using Restaurant.MVC.Models.Account;

namespace Restaurant.MVC.Controllers
{
    public class UserViewModelsController : Controller
    {
        private StudentDbEntities db;
        private IUserRepository userRepository;
        private ISubjectRepository subjectRepo;
        private RepositoryFactory repositoryFactory;

        public UserViewModelsController()
        {
            this.db = new StudentDbEntities();
            repositoryFactory=new RepositoryFactory(db);
            this.userRepository = repositoryFactory.GetUserRepository();
            this.subjectRepo = repositoryFactory.GetSubjectRepository();
        }

        public ActionResult Index()
        {
            string id = UserHelpers.GetCurUserID(User);

            ViewBag.HasProfile = userRepository.GetUserIdentityHasProfile(id);
            return PartialView();
        }

        public ActionResult AllStudents()
        {
            return View();
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

        [ValidateInput(false)]
        public ActionResult StudentsGridViewPartial()
        {
            var model = userRepository.GetAllStudents().Select(z => new UserViewModel()
            {
                UserId = z.UserId,
                Id = z.Id,
                UserName = z.UserName,
                StudentCurrentYear = z.StudentCurrentYear,
                StudentIdNumber = z.StudentIdNumber,
                LastName = z.LastName,
                FirstName = z.FirstName
            }).ToList();
            return PartialView("_StudentsGridViewPartial", model);
        }

        public ActionResult StudentDetails(int id)
        {
            ViewBag.StudentId = id;
            return View();
        }

        [ValidateInput(false)]
        public ActionResult EnrollmentsGridViewPartial(int studentId)
        {
            ViewData["StudentId"] = studentId;
            var model = userRepository.GetAllStudentEnrollments(studentId).Select(z => new EnrollmentViewModel()
            {
                UserId = z.UserId,
                SubjectId = z.SubjectId,
                Grade = z.Grade,
                Subject = z.Subject
            }).ToList();
            ViewBag.Subjects = subjectRepo.GetAllSubjects().Select(z => new SubjectViewModel()
            {
                SubjectYear = z.SubjectYear,
                SubjectName = z.SubjectName,
                SubjectId = z.SubjectId,
                Observations = z.Observations
            }).ToList();
            return PartialView("_EnrollmentsGridViewPartial", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult EnrollmentsGridViewPartialAddNew(Restaurant.MVC.Models.EnrollmentViewModel item,int studentId)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    userRepository.AddEnrollment(studentId,item.SubjectId,item.Grade.Value);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            ViewData["StudentId"] = studentId;
            var model = userRepository.GetAllStudentEnrollments(studentId).Select(z => new EnrollmentViewModel()
            {
                UserId = z.UserId,
                SubjectId = z.SubjectId,
                Grade = z.Grade,
                Subject = z.Subject
            }).ToList();
            ViewBag.Subjects = subjectRepo.GetAllSubjects().Select(z => new SubjectViewModel()
            {
                SubjectYear = z.SubjectYear,
                SubjectName = z.SubjectName,
                SubjectId = z.SubjectId,
                Observations = z.Observations
            }).ToList();
            return PartialView("_EnrollmentsGridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult EnrollmentsGridViewPartialUpdate(Restaurant.MVC.Models.EnrollmentViewModel item,int studentId)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    userRepository.EditEnrollment(studentId,item.SubjectId,item.Grade.Value);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            ViewData["StudentId"] = studentId;
            var model = userRepository.GetAllStudentEnrollments(studentId).Select(z => new EnrollmentViewModel()
            {
                UserId = z.UserId,
                SubjectId = z.SubjectId,
                Grade = z.Grade,
                Subject = z.Subject
            }).ToList();
            ViewBag.Subjects = subjectRepo.GetAllSubjects().Select(z => new SubjectViewModel()
            {
                SubjectYear = z.SubjectYear,
                SubjectName = z.SubjectName,
                SubjectId = z.SubjectId,
                Observations = z.Observations
            }).ToList();
            return PartialView("_EnrollmentsGridViewPartial", model);
        }
       
    }
}
