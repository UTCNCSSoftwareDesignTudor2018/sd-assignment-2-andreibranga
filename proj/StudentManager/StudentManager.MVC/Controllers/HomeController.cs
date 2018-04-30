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
using Restaurant.MVC.Helpers;
using Restaurant.Data;
using Restaurant.Data.Entities;

namespace Restaurant.MVC.Controllers
{
  [Authorize]
  public class HomeController : Controller
  {
    public ActionResult Index()
    {
        
        ViewBag.Name = User.Identity.Name;
      return View();
    }

  }
}
