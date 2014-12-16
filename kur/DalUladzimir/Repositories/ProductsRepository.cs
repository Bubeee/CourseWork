using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using Interfaces.Entities;
using Interfaces.Interfaces;

namespace DalUladzimir.Repositories
{
  public class ProductsRepository : IProductRepository
  {
    public Product GetById(int id)
    {
      throw new NotImplementedException();
    }

    public IEnumerable<Product> GetAll()
    {
      throw new NotImplementedException();
    }

    public IEnumerable<Product> GetProductsByType(int typeId)
    {
      var products = new List<Product>();

      var conString = ConfigurationManager.ConnectionStrings["kurVova"].ConnectionString;
      var query = "SELECT [P].[Id] AS [ProductId] " +
                  ",[P].[Name] AS [ProductName]" +
                  ",[Price]" +
                  ",[Warranty]" +
                  ",[Picture]" +
                  ",[Count]" +
                  ",[T].[Id] AS [TypeId]" +
                  ",[T].[Name] AS [TypeName]" +
                  ",[M].[Name] AS [ManufName]" +
                  ",[M].[Info] AS [ManufInfo]" +
                  ",[D].[Name] AS [DeliveryName]" +
                  ",[S].[Serial] AS [StorageSerial]" +
                  "FROM [kur_Vova].[dbo].[Products] AS [P]" +
                  "LEFT JOIN [kur_Vova].[dbo].[Manufacturer] AS [M]" +
                  "ON [P].[ManufacturerId] = [M].[Id]" +
                  "LEFT JOIN [kur_Vova].[dbo].[Delivery] AS [D]" +
                  "ON [P].[DeliveryId] = [D].[Id]" +
                  "LEFT JOIN [kur_Vova].[dbo].[Storage] AS [S]" +
                  "ON [P].[StorageId] = [S].[Id]" +
                  "INNER JOIN [kur_Vova].[dbo].[Types] AS [T]" +
                  "ON [P].[TypeId] = [T].[Id]" +
                  "WHERE [TypeId] = @typeId";

      using (var connection = new SqlConnection(conString))
      {
        var command = new SqlCommand(query, connection);
        command.Parameters.Add(new SqlParameter("typeId", typeId));
        connection.Open();
        using (var reader = command.ExecuteReader())
        {
          while (reader.Read())
          {
            var product = new Product
            {
              Id = (int)reader[0],
              Name = (string)reader[1],
              Price = (int)reader[2],
              Warranty = (string)reader[3],
              Picture = (string)reader[4],
              Count = (int?)reader[5],
              ProductType = new ProductType
              {
                Id = (int)reader[6],
                Name = (string)reader[7]
              },
              Manufacturer = (string)reader[8],
              ManufacturerInfo = (string)reader[9],
              Delivery = (string)reader[10],
              StorageSerial = (string)reader[11]
            };

            products.Add(product);
          }
        }

        var attriQuery = "SELECT [AD].[Name], [A].[Value] " +
                         "FROM [kur_Vova].[dbo].[Attribute] AS [A] " +
                         "INNER JOIN [kur_Vova].[dbo].[AttributeDescription] AS [AD] " +
                         "ON [A].[AttributeDescriptionId] = [AD].[Id] " +
                         "WHERE [A].[ProductId] = @productId";

        foreach (var item in products)
        {
          command = new SqlCommand(attriQuery, connection);
          command.Parameters.Add(new SqlParameter("@productId", item.Id));
          using (var reader = command.ExecuteReader())
          {
            while (reader.Read())
            {
              var attribute = String.Format("{0} : {1}", reader[0], reader[1]);
              item.Attributes.Add(attribute);
            }
          }
        }
      }

      return products;
    }
  }
}
