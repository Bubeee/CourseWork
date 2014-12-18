using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DalAlexey.Repositories;
using System.Data.SqlClient;
using System.Data;
using Interfaces.Entities;

namespace Test
{
    public class GenerateData
    {
        ProductRepository productRepository = new ProductRepository();
        ProductTypeRepository productTypeRepository = new ProductTypeRepository();
        CategoriesRepository categoryTypeRepository = new CategoriesRepository();

        public static string connectionString = @"Data Source=BUMBLEBEE\SQLEXPRESS;Integrated Security=true";
        public static string workDatabaseName = "kur";

        const string picturePath = "Content/Pictures/picture.jpg";

        static int manufacturerStartId = 1;
        static int manufacturerCount = 40;

        static int deliveryStartId = 1;
        static int deliveryCount = 40;

        static int categoryStartId = 1;
        static int categoryCount = 40;

        static int typeProductStartId = 1;
        static int typeProductCount = 40;

        static int productStartId = 1;
        static int productCount = 40;

        public void Generate()
        {
            //ClearBase();
            //GetStartIds();
            GenerateManufacturers();
            GenerateDelivery();
            GenerateCategory();
            GenerateTypeProduct();
            GenerateProduct();
        }
        public void ClearBase()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                connection.ChangeDatabase(workDatabaseName);

                using (SqlCommand command = new SqlCommand("", connection))
                {

                    //for (int i = 1; i <= 500; i++)
                    //{
                    //    try
                    //    {
                    //        command.CommandText = "DROP TABLE type_product" + i;
                    //        command.ExecuteNonQuery();
                    //    }
                    //    catch (Exception ex) { }
                    //}



                    command.CommandText = "SELECT MIN([id]) AS min_id FROM [type_product]";
                    object result = command.ExecuteScalar();
                    if (DBNull.Value != result)
                    {

                        int min = Convert.ToInt32(result);

                        command.CommandText = "SELECT MAX([id]) AS max_id FROM [type_product]";
                        int max = Convert.ToInt32(command.ExecuteScalar());

                        for (int i = min; i <= max; i++)
                        {
                            try
                            {
                                command.CommandText = "DROP TABLE type_product" + i;
                                command.ExecuteNonQuery();
                            }
                            catch (Exception ex) { }
                        }

                    }

                    command.CommandText =
                        "DELETE FROM [log] " +
                        "DELETE FROM product " +
                        "DELETE FROM field " +
                        "DELETE FROM [type_product] " +
                        "DELETE FROM [user] " +
                        "DELETE FROM manufacturer " +
                        "DELETE FROM category " +
                        "DELETE FROM delivery " +
                        "DELETE FROM storage " +
                        "DELETE FROM enum_view ";
                    command.ExecuteNonQuery();
                }
            }
        }

        public void GetStartIds()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                connection.ChangeDatabase(workDatabaseName);

                using (SqlCommand command = new SqlCommand("", connection))
                {
                    command.CommandText = "SELECT IDENT_CURRENT( 'manufacturer' )";
                    object asd = command.ExecuteScalar();
                    manufacturerStartId = Convert.ToInt32(command.ExecuteScalar()) + 1;

                    command.CommandText = "SELECT IDENT_CURRENT( 'delivery' )";
                    deliveryStartId = Convert.ToInt32(command.ExecuteScalar()) + 1;

                    command.CommandText = "SELECT IDENT_CURRENT( 'category' )";
                    categoryStartId = Convert.ToInt32(command.ExecuteScalar()) + 1;

                    command.CommandText = "SELECT IDENT_CURRENT( 'type_product' )";
                    typeProductStartId = Convert.ToInt32(command.ExecuteScalar()) + 1;

                    command.CommandText = "SELECT IDENT_CURRENT( 'product' )";
                    productStartId = Convert.ToInt32(command.ExecuteScalar()) + 1;


                }
            }
        }

        public void GenerateManufacturers()
        {
            for (int i = 0; i < manufacturerCount; i++)
            {
                productRepository.AddManufacturer("manufacturer" + (manufacturerStartId + i), "manufacturer info");
            }
        }
        public void GenerateDelivery()
        {
            for (int i = 0; i < deliveryCount; i++)
            {
                productRepository.AddDelivery("delivery" + (deliveryStartId + i));
            }
        }

        public void GenerateCategory()
        {
            for (int i = 0; i < categoryCount; i++)
            {
                Category category = new Category() { Name = "category" + (deliveryStartId + i), Image = picturePath };
                categoryTypeRepository.Create(category);
            }
        }

        public void GenerateTypeProduct()
        {
            for (int i = 0; i < typeProductCount; i++)
            {
                var productTypeCreate = new ProductTypeCreate() { TypeName = "type name" + (typeProductStartId + i), CategoryId = categoryStartId };
                productTypeCreate.AttributeDescriptions = new List<ProductTypeField>();
                for (int j = 0; j < 5; j++)
                {
                    //--string - 1
                    //--int - 2
                    //--float - 3
                    var productTypeField = new ProductTypeField { AttributeName = "хар-ка" + i + "-" + j, AttributeType = 1 };
                    productTypeCreate.AttributeDescriptions.Add(productTypeField);
                }
                productTypeRepository.Create(productTypeCreate);
            }
        }

        public void GenerateProduct()
        {
            for (int i = 0; i < productCount; i++)
            {
                var productCreate = new ProductCreate()
                {
                    TypeId = typeProductStartId,
                    Name = "model" + productStartId,
                    ManufacturerId = manufacturerStartId,
                    Price = 30,
                    Warranty = "12",
                    DeliveryId = deliveryStartId,
                    Picture = picturePath,
                    Count = 10
                };
                for (int j = 0; j < 5; j++)
                {
                    productCreate.Attributes.Add("значение" + i + "-" + j);
                }
                productCreate.ProductType = productTypeRepository.GetProductTypeCreateById(typeProductStartId);
                productRepository.Create(productCreate);
            }
        }

    }
}
