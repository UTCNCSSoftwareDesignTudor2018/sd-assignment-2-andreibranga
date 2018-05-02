using DevExpress.Web.Mvc;
using System;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Objects;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Threading.Tasks;
using System.Diagnostics;
using Restaurant.Business;
using Restaurant.Business.Repos.Repositories;
using Restaurant.MVC.Helpers;
using Restaurant.Data;
using Restaurant.Data.Entities;

namespace Restaurant.MVC.Controllers
{
  [Authorize]
  public class HomeController : Controller
  {
      private StudentDbEntities ctx;
      private RepositoryFactory repositoryFactory;
      private IUserRepository userRepository;
      private IReportsRepository reportsRepository;

      public HomeController()
      {
          this.ctx=new StudentDbEntities();
          this.repositoryFactory=new RepositoryFactory(ctx);
          this.userRepository = repositoryFactory.GetUserRepository();
          this.reportsRepository = repositoryFactory.GetReportsRepository();
      }
    public ActionResult Index()
    {
        
        ViewBag.Name = User.Identity.Name;
        if (User.IsInRole("STUDENT"))
        {
            ViewBag.StudentId = userRepository.GetUserIdForIdentity(UserHelpers.GetCurUserID(User));
            ViewBag.Average = reportsRepository.GetUserLastReport(userRepository.GetUserIdForIdentity(UserHelpers.GetCurUserID(User))).averageMark;
        }
      return View();
    }

  }
}
