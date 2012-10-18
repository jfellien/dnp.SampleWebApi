using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;

namespace dnp.SampleWebApi.Console
{
	class CarsWebService
	{
		const string BaseAddress = "http://localhost:8951";
		static readonly HttpClient WebApiClient;
 
		static CarsWebService()
		{
			WebApiClient = new HttpClient {BaseAddress = new Uri(BaseAddress)};
			WebApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
		}

		public static IEnumerable<Car> GetAllCars()
		{
			var response = WebApiClient.GetAllCars();

			TalkWhatsHappen(response);

			return response.Cars; 
		}

		public static Car GetCar(Guid id)
		{
			var response = WebApiClient.GetCar(id);

			TalkWhatsHappen(response);

			return response.Car; 
		}

		public static void AddNewCar(Car car)
		{
			var response = WebApiClient.AddCar(car);

			TalkWhatsHappen(response);
		}

		public static void ChangeCar(Car car)
		{
			var response = WebApiClient.ChangeCar(car);

			TalkWhatsHappen(response);
		}

		public static void RemoveCar(Guid id)
		{
			var response = WebApiClient.DeleteCar(id);

			TalkWhatsHappen(response);
		}

		private static void TalkWhatsHappen(CarWebApiResponse response)
		{
			if(response.IsNotSuccessful)
				System.Console.WriteLine("{0} ({1})", response.StatusCode, response.Reason);
			else
				System.Console.WriteLine("succeeded");
		}
	}

	static class HttpClientExtensions
	{
		static readonly List<Car> EmptyListOfCars = new List<Car>();
		static readonly Car EmptyCar = new Car();

		public static CarWebApiResponse DeleteCar(this HttpClient source, Guid id)
		{
			var result = source.DeleteAsync("api/car/" + id).Result;

			return ApiResponse(result);
		}

		public static CarWebApiResponse ChangeCar(this HttpClient source, Car car)
		{
			var result = source.PutAsJsonAsync("api/car/" + car.ID, car).Result;

			return ApiResponse(result);
		}

		public static CarWebApiResponse AddCar(this HttpClient source, Car car)
		{
			var result = source.PostAsJsonAsync("api/car", car).Result;

			return ApiResponse(result);
		}

		public static CarWebApiResponse GetCar(this HttpClient source, Guid id)
		{
			var result = source.GetAsync("api/car/" + id).Result;

			return GetCarResponse(result);
		}

		public static CarWebApiResponse GetAllCars(this HttpClient source)
		{
			var result = source.GetAsync("api/car").Result;

			return GetAllCarsResponse(result);
		}

		static CarWebApiResponse ApiResponse(HttpResponseMessage result)
		{
			return new CarWebApiResponse
				{
					IsNotSuccessful = !result.IsSuccessStatusCode,
					StatusCode = result.StatusCode,
					Reason = result.ReasonPhrase
				};
		}

		static CarWebApiResponse GetCarResponse(HttpResponseMessage result)
		{
			var response = ApiResponse(result);

			response.Car = result.IsSuccessStatusCode 
				? result.Content.ReadAsAsync<Car>().Result 
				: EmptyCar;

			return response;
		}

		static CarWebApiResponse GetAllCarsResponse(HttpResponseMessage result)
		{
			var response = ApiResponse(result);

			response.Cars = result.IsSuccessStatusCode 
				? result.Content.ReadAsAsync<IEnumerable<Car>>().Result 
				: EmptyListOfCars;

			return response;
		}
	}
}