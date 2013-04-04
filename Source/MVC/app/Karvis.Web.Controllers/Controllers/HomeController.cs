using System.Web.Mvc;
using Karvis.Domain.Tasks;
using Karvis.Domain.ViewModels;
using Razmyar.Web.Controllers.Home;

namespace Karvis.Web.Controllers.Controllers
{
    public class HomeController : BaseHomeController
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

        public ActionResult ContactUs()
        {
            return View(new ContactUsViewModel());
        }

        public ActionResult AboutUs()
        {
            return View(new AboutUsViewModel());
        }

        public ActionResult Tag(string name, string sort, string sortdir, int? page = 1)
        {
             var model=_searchTask.GetJobsByTagName(name, sort,  sortdir,  (int) page );
            return View("Index", model);
        }

      
    }
}