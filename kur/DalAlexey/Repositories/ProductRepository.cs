using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Interfaces.Entities;
using Interfaces.Interfaces;
using System;
using System.Text;

namespace DalAlexey.Repositories
{
    public class ProductRepository : IProductRepository
    {
        public static string connectionString = @"Data Source=BUMBLEBEE\SQLEXPRESS;Integrated Security=true";
        public static string workDatabaseName = "kur";

        public Product GetById(int productId)
        {
            Product product = new Product();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                connection.ChangeDatabase(workDatabaseName);

                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "SELECT [p].[id],[p].[type_id],[p].model,[p].[price],[p].[warranty],[p].[picture]," +
                        "[p].[count],[p].[delivery_id],[d].name AS [delivery],[p].[manufacturer_id],[m].name AS [manufacturer] FROM [product] AS [p] JOIN [manufacturer] AS [m] ON [manufacturer_id]=[m].[id] JOIN [delivery]AS [d] ON [delivery_id]=[d].[id] WHERE [p].[id]=(@productid)";
                    command.Parameters.Add(new SqlParameter("@productid", productId));//Здесь это id товара
                    int productTypeId;
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        reader.Read();
                        product.Id = reader.GetInt32(1);
                        product.Name = reader.GetString(3);
                        product.Manufacturer = reader.GetString(11);
                        product.Price = reader.GetInt32(4);
                        product.Warranty = reader.GetString(5);
                        product.Delivery = reader.GetString(9);
                        product.Picture = reader.GetString(6);
                        product.Count = reader.GetInt32(7);


                        productTypeId = reader.GetInt32(2);
                    }
                    //Название таблицы с этим типом товара
                    command.CommandText = "SELECT [table_name] FROM [type_product] WHERE id = (@producttypeid);";
                    command.Parameters[0].ParameterName = "@producttypeid";
                    command.Parameters[0].Value = productTypeId;
                    string productTableName = (string)command.ExecuteScalar();

                    //значения дополнительных полей товара
                    command.CommandText = "SELECT * FROM " + productTableName + " AS [t] WHERE [t].[product_id]= @productid";
                    command.Parameters[0].ParameterName = "@productid";
                    command.Parameters[0].Value = productId;
                    //MessageBox.Show(command.Parameters.Count.ToString());

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        reader.Read();
                        for (int i = 0; i < reader.FieldCount - 1; i++)//-1 потому что первый столбец это id товара
                        {
                            product.Attributes.Add(reader.GetValue(i + 1).ToString());
                        }
                    }
                }
            }
            return product;
        }

        public int Create(ProductCreate model)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlTransaction trans = connection.BeginTransaction();

                try
                {
                    connection.Open();
                    connection.ChangeDatabase(workDatabaseName);

                    DataTable dataTable = new DataTable();
                    using (SqlCommand command = new SqlCommand("", connection, trans))
                    {
                        trans = connection.BeginTransaction();
                        {
                            command.Transaction = trans;
                            command.CommandText = "INSERT INTO [product]" +
                                                        "([type_id],[model],[manufacturer_id],[price],[warranty]," +
                                                            "[delivery_id],[picture],[count])" +
                                                        "VALUES" +
                                                        "(@typeId,@model,@manufacturerId,@price,@warranty,@deliveryId,@picture,@count)";
                            command.Parameters.Add(new SqlParameter("@typeId", model.TypeId));
                            command.Parameters.Add(new SqlParameter("@model", model.Name));
                            command.Parameters.Add(new SqlParameter("@manufacturerId", model.ManufacturerId));
                            command.Parameters.Add(new SqlParameter("@price", model.Price));
                            command.Parameters.Add(new SqlParameter("@warranty", model.Warranty));
                            command.Parameters.Add(new SqlParameter("@deliveryId", model.DeliveryId));
                            command.Parameters.Add(new SqlParameter("@picture", model.Picture));
                            //TODO Make count other
                            command.Parameters.Add(new SqlParameter("@count", model.Count));

                            int productId = (int)command.ExecuteNonQuery();

                            model.Id = productId;

                            //вставка в таблицу типа товара
                            command.CommandText = "INSERT INTO [type_product" + model.TypeId + "]" +
                                                    "(" +
                                                        "product_id," +
                                                        CreateProductColumnsQuery(model.Attributes.Count) +
                                                    ")" +
                                                    "VALUES" +
                                                    "(" +
                                                    "@productId" +
                                                    "," +
                                                    CreateProductAttributesQuery(model.Attributes) +
                                                    ")";
                            command.Parameters.Clear();
                            command.Parameters.Add(new SqlParameter("@productId", model.TypeId));
                            command.ExecuteNonQuery();
                        }
                    }
                }
                catch (Exception ex)
                {
                    if (trans != null) trans.Rollback();
                    //throw;
                    return -1;
                }
            }
            return 1;
        }

        private string CreateProductColumnsQuery(int countColumns)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < countColumns; i++)
            {
                sb.Append("col");
                sb.Append(i.ToString());
                if (i < (countColumns - 1))
                {
                    sb.Append(",");
                }
            }
            return sb.ToString();
        }
        private string CreateProductAttributesQuery(List<string> attributes)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < attributes.Count; i++)
            {
                sb.Append("col");
                sb.Append(i.ToString());
                if (i < (attributes.Count - 1))
                {
                    sb.Append(",");
                }
            }
            return sb.ToString();
        }

        public IEnumerable<Product> GetProductsByType(int typeId)
        {
            var products = new List<Product>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                connection.ChangeDatabase(workDatabaseName);

                DataTable dataTable = new DataTable();

                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "SELECT [p].[id],[p].[type_id],[p].model,[p].[price],[p].[warranty],[p].[picture]," +
                        "[p].[count],[p].[delivery_id],[d].name AS [delivery],[p].[manufacturer_id],[m].name AS [manufacturer] FROM [product] AS [p] JOIN [manufacturer] AS [m] ON [manufacturer_id]=[m].[id] JOIN [delivery]AS [d] ON [delivery_id]=[d].[id] " +
                        "WHERE [p].[type_id]=(@typeId)";
                    command.Parameters.Add(new SqlParameter("@typeId", typeId));//Здесь это id типа

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Product product = new Product();
                            product.Id = dataTable.Rows[0].Field<int>("id");
                            product.Name = dataTable.Rows[0].Field<string>("model");
                            product.Manufacturer = dataTable.Rows[0].Field<string>("manufacturer");
                            product.Price = dataTable.Rows[0].Field<int>("price");
                            product.Warranty = dataTable.Rows[0].Field<string>("warranty");
                            product.Delivery = dataTable.Rows[0].Field<string>("delivery");
                            product.Picture = dataTable.Rows[0].Field<string>("picture");
                            product.Count = dataTable.Rows[0].Field<int>("count");
                            products.Add(product);
                        }
                    }

                    //Название таблицы с этим типом товара
                    //command.CommandText = "SELECT [table_name] FROM [type_product] WHERE id = (@producttypeid);";
                    //command.Parameters[0].ParameterName = "@producttypeid";
                    //command.Parameters[0].Value = typeId;
                    //string productTableName = (string)command.ExecuteScalar();
                    string productTableName = "type_product" + typeId;

                    //значения дополнительных полей товара
                    command.CommandText = "SELECT * FROM " + productTableName;
                    command.Parameters.Clear();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        int index = 0;
                        while (reader.Read())
                        {
                            for (int i = 1; i < reader.FieldCount; i++)//первый столбец это id товара
                            {
                                products[index].Attributes.Add(reader.GetValue(i).ToString());
                            }
                            index++;
                        }
                    }
                }
            }
            return products;
        }
        public void AddManufacturer(string manufacturerName, string manufacturerInfo)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                connection.ChangeDatabase(workDatabaseName);

                using (SqlCommand command = new SqlCommand("", connection))
                {
                    //создание производителя
                    command.CommandText = "INSERT INTO [manufacturer] ([name],[info]) VALUES (@manufacturerName,@manufacturerInfo)";
                    command.Parameters.Add(new SqlParameter("@manufacturerName", manufacturerName));
                    command.Parameters.Add(new SqlParameter("@manufacturerInfo", manufacturerInfo));

                    command.ExecuteNonQuery();
                }
            }
        }

        public void AddDelivery(string delivery)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                connection.ChangeDatabase(workDatabaseName);

                using (SqlCommand command = new SqlCommand("", connection))
                {
                    //создание производителя
                    command.CommandText = "INSERT INTO [delivery] ([name]) VALUES (@delivery)";
                    command.Parameters.Add(new SqlParameter("@delivery", delivery));

                    command.ExecuteNonQuery();
                }
            }
        }

        public Dictionary<int, string> GetManuf()
        {
            throw new System.NotImplementedException();
        }

        public Dictionary<int, string> GetDeliveries()
        {
            throw new NotImplementedException();
        }

        public Dictionary<int, string> GetStorages()
        {
            throw new NotImplementedException();
        }
    }
}