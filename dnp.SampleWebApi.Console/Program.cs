using System;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;

namespace dnp.SampleWebApi.Console
{
	class Program
	{
		static void Main(string[] args)
		{

			do
			{
				PrintGlobalFunctions();
				var choice = GetMenuChoice();

				DoMenuChoice(choice);

			} while (!ExitMenu());

		}

		static void PrintGlobalFunctions()
		{
			System.Console.Clear();
			System.Console.ForegroundColor = ConsoleColor.Green;
			System.Console.WriteLine("Wählen Sie bitte eine Funktion");
			System.Console.ForegroundColor = ConsoleColor.White;
			System.Console.WriteLine("[{0}]nzeigen aller Fahzeuge", Menu.A);
			System.Console.WriteLine("[{0}]öschen eines Fahrzeuges", Menu.L);
			System.Console.WriteLine("[{0}]inzufügen eines Fahrzeuges", Menu.H);
			System.Console.WriteLine("[{0}]ndern eiens Fahrzeuges", Menu.Ä);
		}

		static string GetMenuChoice()
		{
			System.Console.ForegroundColor = ConsoleColor.Green;
			System.Console.Write("Auswahl: ");
			System.Console.ForegroundColor = ConsoleColor.White;

			var cInfo = System.Console.ReadKey();

			return cInfo.KeyChar.ToString(CultureInfo.InvariantCulture).ToUpperInvariant();
		}

		static void DoMenuChoice(string choice)
		{
			if (choice == Menu.A) ShowAllCars();

			if (choice == Menu.L) RemoveCar();

			if (choice == Menu.H) AddNewCar();

			if (choice == Menu.Ä) ChangeCar();
		}

		static void ChangeCar()
		{
			System.Console.WriteLine();
			System.Console.ForegroundColor = ConsoleColor.Yellow;
			System.Console.WriteLine("======= FAHRZEUG BEARBEITEN ============================");
			System.Console.WriteLine("Bitte geben Sie die ID Der Fahrzeuges an, das geändert werden soll");
			System.Console.ForegroundColor = ConsoleColor.Green;
			System.Console.Write("ID: \t");
			var carID = System.Console.ReadLine();
			System.Console.ForegroundColor = ConsoleColor.Yellow;
			System.Console.WriteLine();

			var car = CarsWebService.GetCar(new Guid(carID));

			if (car.ID == Guid.Empty)
			{
				System.Console.ForegroundColor = ConsoleColor.Red;
				System.Console.WriteLine("Kein Fahrzeug zur ID {0} gefunden", carID);
				System.Console.ForegroundColor = ConsoleColor.White;
				return;
			}

			System.Console.WriteLine("Bitte geben Sie die geänderten Daten für das zu beabeitende Fahrzeug ein");
			System.Console.ForegroundColor = ConsoleColor.White;

			System.Console.WriteLine("ID: \t\t{0}", car.ID);

			System.Console.Write("Kategorie: \t\t{0} === Neu: ", car.Kategorie);
			var kategorie = System.Console.ReadLine();
			car.Kategorie = String.IsNullOrEmpty(kategorie) 
				? car.Kategorie 
				: kategorie;

			System.Console.Write("Typ: \t\t\t{0} === Neu: ", car.Typ);
			var typ = System.Console.ReadLine();
			car.Typ = String.IsNullOrEmpty(typ) 
				? car.Typ 
				: typ;

			System.Console.Write("Kennzeichen: \t\t{0} === Neu: ", car.Kennzeichen);
			var kennzeichen = System.Console.ReadLine();
			car.Kennzeichen = String.IsNullOrEmpty(kennzeichen) 
				? car.Kennzeichen 
				: kennzeichen;

			System.Console.Write("Kilometerstand: \t{0} === Neu: ", car.Kilometerstand);
			var kilometerstand = System.Console.ReadLine();
			car.Kilometerstand = String.IsNullOrEmpty(kilometerstand) 
				? car.Kilometerstand 
				: Int32.Parse(kilometerstand);

			System.Console.Write("Nächste Untersucheung: \t{0} === Neu: ", car.NächsteUntersuchung);
			var nächsteUntersuchung = System.Console.ReadLine();
			car.NächsteUntersuchung = String.IsNullOrEmpty(nächsteUntersuchung) 
				? car.NächsteUntersuchung 
				: DateTime.Parse(nächsteUntersuchung);

			CarsWebService.ChangeCar(car);
		}

		static void RemoveCar()
		{

			System.Console.WriteLine();
			System.Console.ForegroundColor = ConsoleColor.Yellow;
			System.Console.WriteLine("======= FAHRZEUG ENTFERNEN =====================================");
			System.Console.WriteLine("Bitte geben Sie die ID Fahrzeuges ein, das Sie entfernen möchten");
			System.Console.ForegroundColor = ConsoleColor.White;

			System.Console.Write("ID: ");
			var id = System.Console.ReadLine();

			CarsWebService.RemoveCar(new Guid(id));
		}

		static void AddNewCar()
		{
			var newCar = new Car {ID = Guid.NewGuid()};

			System.Console.WriteLine();
			System.Console.ForegroundColor = ConsoleColor.Yellow;
			System.Console.WriteLine("======= NEUES FAHRZEUG ============================");
			System.Console.WriteLine("Bitte geben Sie die Daten für das neue Fahrzeug ein");
			System.Console.ForegroundColor = ConsoleColor.White;
			
			System.Console.Write("Kategorie: \t\t");
			newCar.Kategorie = System.Console.ReadLine();
			
			System.Console.Write("Typ: \t\t\t");
			newCar.Typ = System.Console.ReadLine();
			
			System.Console.Write("Kennzeichen: \t\t");
			newCar.Kennzeichen = System.Console.ReadLine();

			System.Console.Write("Kilometerstand: \t");
			newCar.Kilometerstand = Int32.Parse(System.Console.ReadLine());

			System.Console.Write("Nächste Untersucheung: \t");
			newCar.NächsteUntersuchung = DateTime.Parse(System.Console.ReadLine());

			CarsWebService.AddNewCar(newCar);
		}

		static void ShowAllCars()
		{
			System.Console.WriteLine();
			System.Console.WriteLine();
			System.Console.ForegroundColor = ConsoleColor.Yellow;
			System.Console.WriteLine("===== Alle Fahrzeuge im Bestand =====");
			System.Console.ForegroundColor = ConsoleColor.White;

			var allCars = CarsWebService.GetAllCars();

			allCars.ToList().ForEach(x=> System.Console.WriteLine(
				"====== " + x.ID + "=====\r\n" +
				"Kategorie: \t\t" + x.Kategorie + "\r\n" +
				"Typ: \t\t\t" + x.Typ + "\r\n" +
				"Kennzeichen: \t\t" + x.Kennzeichen + "\r\n" +
				"Kilometerstand: \t" + x.Kilometerstand + "\r\n" +
				"Nächste Untersuchung: \t" + x.NächsteUntersuchung
				));
		}

		static bool ExitMenu()
		{
			System.Console.ForegroundColor = ConsoleColor.Red;
			System.Console.WriteLine();
			System.Console.Write("Applikation beenden (J/N): ");
			System.Console.ForegroundColor = ConsoleColor.White;

			var cInfo = System.Console.ReadKey();

			return cInfo.KeyChar == 'J' | cInfo.KeyChar == 'j';
		}
	}
}
