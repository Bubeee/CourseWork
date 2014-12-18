using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces.Entities;
using Interfaces.Interfaces;

namespace DalAlexey.Repositories
{
    public class ProductTypeRepository : ITypesRepository
    {
        public static string connectionString = @"Data Source=BUMBLEBEE\SQLEXPRESS;Integrated Security=true";
        public static string workDatabaseName = "kur";

        public ProductType GetById(int id)
        {
            var productType = new ProductType();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                connection.ChangeDatabase(workDatabaseName);

                DataTable dataTable = new DataTable();

                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "SELECT [view_name] FROM [field] WHERE [type_product_id] = (@producttypeid) ORDER BY [id]";
                    command.Parameters.Add(new SqlParameter("@producttypeid", id));

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            productType.AttributeNames.Add(reader.GetString(0));
                        }
                    }
                }
            }
            return productType;
        }

        public int Create(ProductTypeCreate productTypeCreate)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                connection.ChangeDatabase(workDatabaseName);
                SqlTransaction trans = connection.BeginTransaction();

                using (SqlCommand command = new SqlCommand("",connection,trans))
                {
                    //try
                    {
                        //создание типа товара
                        command.CommandText = "INSERT INTO [type_product] " +
                                                "(category_id,view_name) " +
                                                "OUTPUT INSERTED.[id] " +
                                                "VALUES " +
                                                "((@categoryId),(@productTypeName)) ";
                        command.Parameters.Add(new SqlParameter("@categoryId", productTypeCreate.CategoryId));
                        command.Parameters.Add(new SqlParameter("@productTypeName", productTypeCreate.TypeName));

                        int productTypeId = (int)command.ExecuteScalar();

                        //создание таблицы типа товара
                        command.CommandText = "CREATE TABLE [type_product" + productTypeId + "]" +
                                                "(" +
                                                    "product_id int," +
                                                    CreateProductTypeTableQuery(productTypeCreate.AttributeDescriptions) +
                                                ")";
                        command.Parameters[0].ParameterName = "@productTypeId";
                        command.Parameters[0].Value = productTypeId;
                        command.Parameters.RemoveAt(1);
                        command.ExecuteNonQuery();

                        command.CommandText =
                            "INSERT INTO [field] ([type_product_id],[view_name],[field_type]) VALUES " +
                            CreateProductTypeFieldsQuery(productTypeId, productTypeCreate.AttributeDescriptions);
                        command.Parameters.RemoveAt(0);
                        command.ExecuteNonQuery();

                        trans.Commit();
                    }
                    ////uncomment
                    //catch (Exception ex)
                    //{
                    //    trans.Rollback();
                    //    //throw;
                    //    return 1;
                    //}
                }
            }
            return 1;
        }
        private string CreateProductTypeTableQuery(List<ProductTypeField> attributes)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < attributes.Count; i++)
            {
                sb.Append("col");
                sb.Append(i.ToString());
                //--string - 1
                //--int - 2
                //--float - 3
                //--enum - 4
                switch (attributes[i].AttributeType)
                {
                    case 1:
                        sb.Append(" nvarchar(30)");
                        break;
                    case 2:
                        sb.Append(" int");
                        break;
                    case 3:
                        sb.Append(" float");
                        break;
                    case 4:
                        sb.Append(" nvarchar(30)");
                        break;
                }
                if (i < (attributes.Count - 1))
                {
                    sb.Append(",");
                }
            }
            return sb.ToString();
        }
        private string CreateProductTypeFieldsQuery(int productTypeId, List<ProductTypeField> attributes)
        {
            //INSERT INTO [field] 
            //([type_product_id],[view_name],[field_type])
            //VALUES
            //(1,N'вес',2),
            //(1,N'цвет',1)
            var sb = new StringBuilder();
            for (int i = 0; i < attributes.Count; i++)
            {
                sb.Append("(");
                sb.Append(productTypeId);
                sb.Append(",N'");
                sb.Append(attributes[i].AttributeName);
                sb.Append("',");
                sb.Append(attributes[i].AttributeType);
                sb.Append(")");
                if (i < (attributes.Count - 1))
                {
                    sb.Append(",");
                }
            }
            return sb.ToString();
        }

        public ProductTypeCreate GetProductTypeCreateById(int typeId)
        {
            var productTypeCreate = new ProductTypeCreate();
            productTypeCreate.AttributeDescriptions = new List<ProductTypeField>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                connection.ChangeDatabase(workDatabaseName);

                DataTable dataTable = new DataTable();

                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "SELECT [view_name],[category_id] FROM [type_product] WHERE [id]=(@productTypeId)";
                    command.Parameters.Add(new SqlParameter("@productTypeId", typeId));//Здесь это id товара

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        reader.Read();
                        productTypeCreate.TypeName = reader.GetString(0);
                        productTypeCreate.CategoryId = reader.GetInt32(1);

                        command.CommandText = "SELECT [view_name],[field_type] FROM [field] WHERE [type_product_id]=(@productTypeId) ORDER BY [id] ASC";
                    }

                    using (SqlDataReader reader = command.ExecuteReader())//тут а - русское)
                    {
                        while (reader.Read())
                        {
                            var productTypeField = new ProductTypeField();
                            productTypeField.AttributeName = reader.GetString(0);
                            productTypeField.AttributeType = (short)reader.GetInt32(1);
                            productTypeCreate.AttributeDescriptions.Add(productTypeField);
                        }
                    }
                }
            }
            return productTypeCreate;
        }

        public IEnumerable<ProductType> GetTypesByCategory(int categoryId)
        {
            var productTypes = new List<ProductType>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                connection.ChangeDatabase(workDatabaseName);

                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "SELECT [id],[view_name] FROM [type_product] WHERE [category_id]=(@categoryId)";
                    command.Parameters.Add(new SqlParameter("@categoryId", categoryId));//Здесь это id категории

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var productType = new ProductType();
                            productType.Id = reader.GetInt32(0);
                            productType.Name = reader.GetString(1);
                            productTypes.Add(productType);
                        }
                    }
                }
            }
            return productTypes;
        }
    }
}
