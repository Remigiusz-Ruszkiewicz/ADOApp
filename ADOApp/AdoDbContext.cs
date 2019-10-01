using ADOApp.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADOApp
{
    public class AdoDbContext
    {
        string conn = "Data Source=PK3-6;Initial Catalog=CarRental;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public List<Car> GetCars()
        {
            List<Car> cars = new List<Car>();
            using (SqlConnection connection = new SqlConnection(conn))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("Select * from dbo.Cars", connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            cars.Add(new Car
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Name = reader["Name"].ToString(),
                                ModelId = Convert.ToInt32(reader["ModelId"])
                            });
                        }
                    }
                }
            }
            return cars;
        }
    }
}
