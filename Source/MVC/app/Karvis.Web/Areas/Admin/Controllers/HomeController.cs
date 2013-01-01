using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Karvis.Domain.Tasks;
using Karvis.Domain.ViewModels;

namespace Karvis.Web.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Admin/Admin/
        private readonly IAdminTask _adminTask;

        public HomeController(IAdminTask adminTask)
        {
            _adminTask = adminTask;
        }

        public ActionResult Index()
        {
            var model = _adminTask.GetRawModel();
            return View(model);
        }
        public ActionResult ExtractJobs(ExtractJobViewModel extractJobViewModel)
        {
            throw new System.NotImplementedException();
        }

    }
}
