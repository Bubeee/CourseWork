using System.Web.Mvc;

namespace WebUI.Controllers
{
  public class CategoriesController : Controller
  {
    // GET: Categories
    public ActionResult Index()
    {
      return View();
    }
  }
}