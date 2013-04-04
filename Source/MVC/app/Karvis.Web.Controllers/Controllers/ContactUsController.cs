using System.Web.Mvc;
using Karvis.Domain.Tasks;
using Karvis.Domain.ViewModels;
using Razmyar.Web.Controllers.Home;

namespace Karvis.Web.Controllers.Controllers
{
    public class ContactUsController : Controller
    {
        public ActionResult Index()
        {
            return View(new ContactUsViewModel());
        }
    }
}