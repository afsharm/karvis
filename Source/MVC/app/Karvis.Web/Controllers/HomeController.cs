using System.Collections.Generic;
using System.Web.Mvc;
using Karvis.Domain.Tasks;
using Karvis.Domain.ViewModels;




namespace Karvis.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IJobTask _jobTask;
        private readonly ISearchTask _searchTask;
        public HomeController(IJobTask jobTask, ISearchTask searchTask)
        {
            _jobTask = jobTask;
            _searchTask = searchTask;
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
        [HttpPost]
        public ActionResult Search(SearchViewModel searchViewModel, string sort, string sortdir, int? page = 1)
        {
            var model = _searchTask.Search(searchViewModel, sort, sortdir, (int)page);
            return View("SearchResult",model);
        }
        [HttpGet]
        public ActionResult Search()
        {
            var model = _searchTask.GetRawModel();
            return View(model);
        }
    }
}