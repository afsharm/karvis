using System.Collections.Generic;
using System.Web.Mvc;
using Karvis.Domain.Tasks;
using Karvis.Domain.ViewModels;

namespace Karvis.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IJobTask _jobTask;

        public HomeController(IJobTask jobTask)
        {
            _jobTask = jobTask;
        }

        [HttpGet]
        public ActionResult Index(string sort, string sortdir, int? page = 1)
        {
           var list = _jobTask.GetSummeryPaged(sort, sortdir, (int) page);


            return View(list);
        }

        public ActionResult Description(int id)
        {
            return View(_jobTask.GetJobDescription(id));
        }
    }
}