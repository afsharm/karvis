using System.Web.Mvc;
using Karvis.Domain.Tasks;
using Karvis.Domain.ViewModels;

namespace Karvis.Web.Controllers.Controllers
{
    public class JobController : Controller
    {
        private readonly IJobTask _jobTask;

        public JobController(IJobTask jobTask)
        {
            _jobTask = jobTask;
        }

        public ActionResult Stats()
        {
            throw new System.NotImplementedException();
        }
        [HttpGet]
        public ActionResult SubmitJob()
        {
            
            return View(new SubmitJobViewModel ());
        }
        [HttpPost]
        public ActionResult SubmitJob(SubmitJobViewModel submitJobViewModel)
        {

            _jobTask.SubmitJob(submitJobViewModel);
            return RedirectToAction("Index", "Admin");
        }
    }
}