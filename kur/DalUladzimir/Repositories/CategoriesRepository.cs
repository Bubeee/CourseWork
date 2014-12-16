using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using Interfaces.Entities;
using Interfaces.Interfaces;

namespace DalUladzimir.Repositories
{
  public class CategoriesRepository : ICategoriesRepository
  {
    public Category GetById(int id)
    {
      var category = new Category();

      var conString = ConfigurationManager.ConnectionStrings["kurVova"].ConnectionString;
      var query = "SELECT [Id], [Name], [Picture] " +
                  "FROM [dbo].[Categories]" +
                  "WHERE [Id] = @id";

      using (var connection = new SqlConnection(conString))
      {
        var command = new SqlCommand(query, connection);
        command.Parameters.Add(new SqlParameter("@id", id));
        connection.Open();
        using (var reader = command.ExecuteReader())
        {
          while (reader.Read())
          {
            category.Id = (int) reader[0];
            category.Name = (string) reader[1];
            category.Image = (string) reader[2];
          }
        }
      }

      return category;
    }

    public IEnumerable<Category> GetAllCategories()
    {
      var categories = new List<Category>();

      var conString = ConfigurationManager.ConnectionStrings["kurVova"].ConnectionString;
      var query = "SELECT [Id], [Name], [Picture] " +
                  "FROM [dbo].[Categories]";

      using (var connection = new SqlConnection(conString))
      {
        var command = new SqlCommand(query, connection);
        connection.Open();
        using (var reader = command.ExecuteReader())
        {
          while (reader.Read())
          {
            var category = new Category
            {
              Id = (int) reader[0], 
              Name = (string) reader[1], 
              Image = (string) reader[2]
            };

            categories.Add(category);
          }
        }
      }

      return categories;
    }

    public int Create(Category model)
    {
      throw new System.NotImplementedException();
    }
  }
}