using System.Collections.Generic;
using System.Web.Mvc;
using Interfaces.Entities;
using Interfaces.Interfaces;

namespace WebUI.Controllers
{
  public class CategoriesController : Controller
  {
    private readonly ICategoriesRepository _repository;

    public CategoriesController(ICategoriesRepository repository)
    {
      _repository = repository;
    }

    // GET: Categories
    public ActionResult Index()
    {
      var categories = new List<Category>();
      categories.AddRange(_repository.GetAllCategories());

      return View(categories);
    }
  }
}