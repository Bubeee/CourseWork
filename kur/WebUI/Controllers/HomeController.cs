using System.Web.Mvc;

namespace WebUI.Controllers
{
  public class HomeController : Controller
  {
    public ActionResult Index()
    {
      return View();
    }

    public ActionResult About()
    {
      return View();
    }

    public ActionResult Contact()
    {
      return View();
    }
  }
}