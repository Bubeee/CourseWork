using System.Collections.Generic;
using System.Web.Mvc;
using Interfaces.Entities;
using Interfaces.Interfaces;

namespace WebUI.Controllers
{
  public class CategoriesController : Controller
  {
    private readonly IRepository<Category> _repository;

    public CategoriesController(IRepository<Category> repository)
    {
      _repository = repository;
    }

    // GET: Categories
    public ActionResult Index()
    {
      var categories = new List<Category>();
      categories.AddRange(_repository.GetAll());

      return View(categories);
    }
  }
}