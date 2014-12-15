using Autofac;    
using DalAlexey.Repositories;
using DalUladzimir.Repositories;
using Interfaces.Entities;
using Interfaces.Interfaces;

namespace WebUI.AutoFacModules
{
  public class DbResolutionModule : Module
  {
    public bool LoadAlexey { get; set; }
    protected override void Load(ContainerBuilder builder)
    {
      if (LoadAlexey)
      {
        builder.RegisterType<ProductRepository>().As<IRepository<Product>>();
        builder.RegisterType<ProductTypeRepository>().As<IRepository<ProductType>>();
      }
      else
      {
        builder.RegisterType<ProductRepository>().As<IRepository<Product>>();
        builder.RegisterType<TypesRepository>().As<IRepository<ProductType>>();
        builder.RegisterType<CategoriesRepository>().As<IRepository<Category>>();
      }
      base.Load(builder);
    }
  }
}