using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Restaurant.Business.Repos.Repositories;
using Restaurant.Data.Entities;
using Restaurant.MVC.Models;

namespace Restaurant.MVC.Controllers.StudentManager
{
    public class SubjectController : Controller
    {
        private StudentDbEntities ctx;
        private ISubjectRepository subjectRepository;

        public SubjectController()
        {
            this.ctx=new StudentDbEntities();
            this.subjectRepository=new SubjectRepository(ctx);
        }


        // GET: Subject
        public ActionResult Index()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult SubjectsGridViewPartial()
        {
            var model = subjectRepository.GetAllSubjects().Select(z=> new SubjectViewModel()
            {
                SubjectYear = z.SubjectYear,
                SubjectName = z.SubjectName,
                SubjectId = z.SubjectId,
                Observations = z.Observations
            }).ToList();
            return PartialView("~/Views/Subject/_SubjectsGridViewPartial.cshtml", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult SubjectsGridViewPartialAddNew(SubjectViewModel item)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    subjectRepository.AddSubject(item.SubjectName,item.SubjectYear,item.Observations);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            var model = subjectRepository.GetAllSubjects().Select(z => new SubjectViewModel()
            {
                SubjectYear = z.SubjectYear,
                SubjectName = z.SubjectName,
                SubjectId = z.SubjectId,
                Observations = z.Observations
            }).ToList();
            return PartialView("~/Views/Subject/_SubjectsGridViewPartial.cshtml", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult SubjectsGridViewPartialUpdate(SubjectViewModel item)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    subjectRepository.UpdateSubject(item.SubjectId,item.SubjectName,item.SubjectYear,item.Observations);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            var model = subjectRepository.GetAllSubjects().Select(z => new SubjectViewModel()
            {
                SubjectYear = z.SubjectYear,
                SubjectName = z.SubjectName,
                SubjectId = z.SubjectId,
                Observations = z.Observations
            }).ToList();
            return PartialView("~/Views/Subject/_SubjectsGridViewPartial.cshtml", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult SubjectsGridViewPartialDelete(SubjectViewModel item)
        {
            if (item.SubjectId >= 0)
            {
                try
                {
                    subjectRepository.DeleteSubject(item.SubjectId);
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            var model = subjectRepository.GetAllSubjects().Select(z => new SubjectViewModel()
            {
                SubjectYear = z.SubjectYear,
                SubjectName = z.SubjectName,
                SubjectId = z.SubjectId,
                Observations = z.Observations
            }).ToList();
            return PartialView("~/Views/Subject/_SubjectsGridViewPartial.cshtml", model);
        }
    }
}