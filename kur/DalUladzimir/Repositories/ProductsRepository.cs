﻿using System;
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
      var product = new Product();

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
                  "WHERE [P].[Id] = @id";

      using (var connection = new SqlConnection(conString))
      {
        var command = new SqlCommand(query, connection);
        command.Parameters.Add(new SqlParameter("@id", id));
        connection.Open();
        using (var reader = command.ExecuteReader())
        {
          while (reader.Read())
          {
            product.Id = (int)reader[0];
            product.Name = reader[1] == DBNull.Value ? null : (string)reader[1];
            product.Price = (int)reader[2];
            product.Warranty = reader[3] == DBNull.Value ? null : (string)reader[3];
            product.Picture = reader[3] == DBNull.Value ? null : (string)reader[4];
            product.Count = (int)reader[5];
            product.ProductType = new ProductType
            {
              Id = (int)reader[6],
              Name = reader[7] == DBNull.Value ? null : (string)reader[7]
            };
            product.Manufacturer = reader[8] == DBNull.Value ? null : (string)reader[8];
            product.ManufacturerInfo = reader[9] == DBNull.Value ? null : (string)reader[9];
            product.Delivery = reader[10] == DBNull.Value ? null : (string)reader[10];
            product.StorageSerial = reader[11] == DBNull.Value ? null : (string)reader[11];
          }
        }

        var attriQuery = "SELECT [AD].[Name], [A].[Value] " +
                             "FROM [kur_Vova].[dbo].[Attribute] AS [A] " +
                             "INNER JOIN [kur_Vova].[dbo].[AttributeDescription] AS [AD] " +
                             "ON [A].[AttributeDescriptionId] = [AD].[Id] " +
                             "WHERE [A].[ProductId] = @productId";

        command = new SqlCommand(attriQuery, connection);
        command.Parameters.Add(new SqlParameter("@productId", product.Id));
        using (var reader = command.ExecuteReader())
        {
          while (reader.Read())
          {
            var attribute = String.Format("{0} : {1}", reader[0], reader[1]);
            product.Attributes.Add(attribute);
          }
        }
      }

      return product;
    }

    public int Create(ProductCreate model)
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
              Name = reader[1] == DBNull.Value ? null : (string)reader[1],
              Price = (int)reader[2],
              Warranty = reader[3] == DBNull.Value ? null : (string)reader[3],
              Picture = reader[3] == DBNull.Value ? null : (string)reader[4],
              Count = (int)reader[5],
              ProductType = new ProductType
              {
                Id = (int)reader[6],
                Name = reader[7] == DBNull.Value ? null : (string)reader[7],
              },
              Manufacturer = reader[8] == DBNull.Value ? null : (string)reader[8],
              ManufacturerInfo = reader[9] == DBNull.Value ? null : (string)reader[9],
              Delivery = reader[10] == DBNull.Value ? null : (string)reader[10],
              StorageSerial = reader[11] == DBNull.Value ? null : (string)reader[11],
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

    public void AddManufacturer(string manufacturerName, string manufacturerInfo)
    {
      throw new NotImplementedException();
    }

    public void AddDelivery(string delivery)
    {
      throw new NotImplementedException();
    }

    public Dictionary<int, string> GetManuf()
    {
      var manufacturers = new Dictionary<int, string>();

      var conString = ConfigurationManager.ConnectionStrings["kurVova"].ConnectionString;
      var query = "SELECT [Id], [Name] " +
                  "FROM [kur_Vova].[dbo].[Manufacturer]";

      using (var connection = new SqlConnection(conString))
      {
        var command = new SqlCommand(query, connection);
        connection.Open();
        using (var reader = command.ExecuteReader())
        {
          while (reader.Read())
          {
            manufacturers.Add((int)reader[0], (string)reader[1]);
          }
        }
      }

      return manufacturers;
    }
  }
}
