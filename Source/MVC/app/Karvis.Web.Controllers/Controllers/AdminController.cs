using System;
using System.Web.Mvc;
using Karvis.Domain;
using Karvis.Domain.Tasks;
using Karvis.Domain.ViewModels;

namespace Karvis.Web.Controllers.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/Admin/
        private readonly IAdminTask _adminTask;

        public AdminController(IAdminTask adminTask)
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
            var siteSource = (AdSource)Enum.Parse(typeof(AdSource), extractJobViewModel.SelectedAdSource);

            var result = _adminTask.ExtractJobs(siteSource);
            
                    var model = _adminTask.GetRawModel();
            model.AdminExtractJobResultViewModel = result;
            var searchSource = model.ExtractJobViewModel.SearchSource;
    

            return View(@"~\Areas\Admin\Views\Home\Index.cshtml", model);

        }

    }
}
