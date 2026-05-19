using System;
using System.Data;
using System.IO;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace ConsoleApp2
{
    public class ProductDataAccess
    {
        private readonly string _connectionString;

        public ProductDataAccess()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            _connectionString = config.GetConnectionString("DefaultConnection");
        }

        public void InsertProduct(Product p)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("usp_InsertProduct", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProductName", p.ProductName);
                cmd.Parameters.AddWithValue("@Category", p.Category);
                cmd.Parameters.AddWithValue("@Price", p.Price);

                con.Open();
                cmd.ExecuteNonQuery();
                Console.WriteLine("Product inserted successfully.");
            }
        }

        public void GetAllProducts()
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                // Using the View Stored Procedure
                SqlDataAdapter adapter = new SqlDataAdapter("usp_GetAllProducts", con);
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;

                DataTable dt = new DataTable();
                adapter.Fill(dt);

                foreach (DataRow row in dt.Rows)
                {
                    Console.WriteLine($"{row["ProductId"]} | {row["ProductName"]} | {row["Category"]} | {row["Price"]}");
                }
            }
        }

        public void GetProductById(int id)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("usp_GetProductById", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProductId", id);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    Console.WriteLine($"{row["ProductId"]} | {row["ProductName"]} | {row["Category"]} | {row["Price"]}");
                }
                else
                {
                    Console.WriteLine("Product not found.");
                }
            }
        }

        public void UpdateProduct(Product p)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("usp_UpdateProduct", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProductId", p.ProductId);
                cmd.Parameters.AddWithValue("@ProductName", p.ProductName);
                cmd.Parameters.AddWithValue("@Category", p.Category);
                cmd.Parameters.AddWithValue("@Price", p.Price);

                con.Open();
                int rows = cmd.ExecuteNonQuery();
                if (rows > 0) Console.WriteLine("Product updated successfully.");
                else Console.WriteLine("Product not found.");
            }
        }

        public void DeleteProduct(int id)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("usp_DeleteProduct", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProductId", id);

                con.Open();
                int rows = cmd.ExecuteNonQuery();
                if (rows > 0) Console.WriteLine("Product deleted successfully.");
                else Console.WriteLine("Product not found.");
            }
        }
    }
}
