using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ADOApp.Models1;

namespace ADOApp
{
    public class EfRepository
    {
        public List<Car> GetCars()
        {
            using (CarRentalEntities db = new CarRentalEntities())
            {
                return db.Cars.Select(c => new Car
                {
                    Id = c.Id,
                    Name = c.Name,
                    ModelId = (int)c.ModelId
                }).ToList();
            }
        }
        public Car GetCar(int id)
        {
            using (CarRentalEntities db = new CarRentalEntities())
            {
                return db.Cars
                    .Where(c => c.Id == id)
                    .Select(c => new Car
                    {
                        Id = c.Id,
                        Name = c.Name,
                        ModelId = (int)c.ModelId
                    }).FirstOrDefault();
            }
        }
        public Cars GetDbCar(int id,CarRentalEntities db)
        {
                return db.Cars
                    .Where(c => c.Id == id)
                    .FirstOrDefault();
        }

        internal int Add(Car car)
        {
            using (CarRentalEntities db = new CarRentalEntities())
            {
                var dbCar = new Cars
                {
                    Name = car.Name,
                    ModelId = car.ModelId
                };
                db.Cars.Add(dbCar);
                db.SaveChanges();
                return dbCar.Id;
            }
        }

        internal object Update(Car car)
        {
            using (CarRentalEntities db = new CarRentalEntities())
            {
                var dbCar = GetDbCar(car.Id,db);
                if (dbCar==null)
                    return false;
                dbCar.Name = car.Name;
                dbCar.ModelId = car.ModelId;
                db.Entry(dbCar).State=System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return true;
            }
        }

        internal bool Delete(int id)
        {
            using (CarRentalEntities db = new CarRentalEntities())
            {
                var dbCar = GetDbCar(id,db);
                if (dbCar == null)
                    return false;
                db.Cars.Remove(dbCar);
                db.SaveChanges();
                return true;
            }
        }
    }
}
