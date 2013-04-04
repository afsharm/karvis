using System.Web.Mvc;
using Karvis.Domain.Tasks;
using Karvis.Domain.ViewModels;
using Razmyar.Web.Controllers.Home;

namespace Karvis.Web.Controllers.Controllers
{
    public class HomeController : BaseHomeController
    {
        public override ActionResult Index()
        {
            return (RedirectToActionPermanent("Index", "Jobs"));
        }
    }
}