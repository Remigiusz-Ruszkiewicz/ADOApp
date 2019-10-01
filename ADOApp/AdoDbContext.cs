using ADOApp.Models1;
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
        public Car GetCar(int id)
        {
            string conn = "Data Source=PK3-6;Initial Catalog=CarRental;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            Car car = new Car();
            using (SqlConnection connection = new SqlConnection(conn))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand($"Select top(1)* from dbo.Cars where Id={id}", connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            car = new Car
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Name = reader["Name"].ToString(),
                                ModelId = Convert.ToInt32(reader["ModelId"])
                            };
                        }
                    }
                }
            }
            return car;
        }
        public int GetLastId()
        {
            return GetCars().Count != 0 ? GetCars().LastOrDefault().Id : 0;
        }

        internal int Add(Car car)
        {
            using (SqlConnection connection = new SqlConnection(conn))
            {
                connection.Open();
                using (SqlCommand command =
                    new SqlCommand($"Insert into dbo.Cars Values(@name,@modeld)", connection))
                {
                    SqlParameter name = new SqlParameter("@name", car.Name);
                    SqlParameter modelId = new SqlParameter("@modelId", car.ModelId);
                    command.Parameters.Add(name);
                    command.Parameters.Add(modelId);
                    command.ExecuteNonQuery();
                }
            }
            return GetLastId();
        }
        public bool Update(Car car)
        {
            Car dbCar = GetCar(car.Id);
            if (dbCar == null)
                return false;
            {
                using (SqlConnection connection = new SqlConnection(conn))
                {
                    connection.Open();
                    using (SqlCommand command =
                        new SqlCommand($"Update dbo.Cars Set Name=@Name,ModelId=@ModelId where Id={car.Id}", connection))
                    {
                        SqlParameter name = new SqlParameter("@name", car.Name);
                        SqlParameter modelId = new SqlParameter("@modelId", car.ModelId);
                        command.Parameters.Add(name);
                        command.Parameters.Add(modelId);
                        command.ExecuteNonQuery();
                    }
                }
            }
            return true;
        }
        public bool Delete(int id)
        {
            Car dbCar = GetCar(id);
            if (dbCar == null)
                return false;
            {
                using (SqlConnection connection = new SqlConnection(conn))
                {
                    connection.Open();
                    using (SqlCommand command =
                        new SqlCommand($"Delete dbo.Cars where Id={id}", connection))
                    {

                        command.ExecuteNonQuery();
                    }
                }
            }
            return true;
        }
    }
}
