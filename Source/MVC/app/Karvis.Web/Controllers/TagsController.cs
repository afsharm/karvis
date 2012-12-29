using System.Web.Mvc;
using Karvis.Domain.Tasks;

namespace Karvis.Web.Controllers
{
    public class TagsController : Controller
    {

                private readonly ITagTask _tagTask;

        public TagsController(ITagTask tagTask)
        {
            _tagTask = tagTask;
        }

        public ActionResult Index()
        {
            return View(_tagTask.GetTagCloud());
        }
    }
}