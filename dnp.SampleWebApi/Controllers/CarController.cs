using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using dnp.SampleWebApi.App_Data;
using dnp.SampleWebApi.Models;

namespace dnp.SampleWebApi.Controllers
{
    public class CarController : ApiController
    {
    	readonly Cars _cars = new Cars();

        public IEnumerable<Car> Get()
        {
            return _cars.GetAllCars();
        }

        public Car Get(Guid id)
        {
            return _cars.WithId(id);
        }

        public void Post(Car car)
        {
        	_cars.Save(car);
        }

        public void Put(Car car)
        {
			_cars.Save(car);
        }

        public void Delete(Guid id)
        {
        	_cars.Delete(id);
        }
    }
}
