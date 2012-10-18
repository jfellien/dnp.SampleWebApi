using System;
using System.Collections.Generic;
using System.Linq;
using dnp.SampleWebApi.Models;

namespace dnp.SampleWebApi.App_Data
{
	internal class Cars
	{
		readonly static List<Car> AllCars;

		static Cars()
		{
			AllCars = new List<Car>{ 
				new Car
					{
						ID = new Guid("{3AF3E804-EBA2-41B9-B0FF-E29D55108274}"),
						Kategorie = "Mini",
						Typ = "VW",
						Kennzeichen = "A-BC 1234",
						Kilometerstand = 6000,
						NächsteUntersuchung = new DateTime(2012, 09, 01)
					}, 
				new Car{
					ID = new Guid("{65E19EC1-FB16-4CF7-802D-32E3C3F1A4F1}"),
					Kategorie = "Mini",
					Typ = "Toyota",
					Kennzeichen = "A-BC 5555",
					Kilometerstand = 10000,
					NächsteUntersuchung = new DateTime(2013, 06, 01)
				},
				new Car{
					ID = new Guid("{E61C43B6-5FBF-49E8-830F-887705DD6199}"),
					Kategorie = "Maxi",
					Typ = "Ford",
					Kennzeichen = "A-BC 7777",
					Kilometerstand = 1000,
					NächsteUntersuchung = new DateTime(2014, 12, 01)
				}, 
			};
		}

		public IEnumerable<Car> GetAllCars()
		{
			return AllCars;
		}

		public Car WithId(Guid id)
		{
			return AllCars.SingleOrDefault(x => x.ID == id);
		}

		public void Save(Car car)
		{
			var carToChange = WithId(car.ID);
			
			if(carToChange != null)
				Delete(car.ID);
			
			AllCars.Add(car);

		}

		public void Delete(Guid id)
		{
			AllCars.RemoveAll(x => x.ID == id);
		}
	}
}