﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Windows.Markup;
using System.Xml;
using TheAirline.Model.AirportModel;
using TheAirline.Model.AirlineModel;
using TheAirline.Model.AirlinerModel;
using TheAirline.Model.AirlinerModel.RouteModel;
using TheAirline.Model.GeneralModel.StatisticsModel;
using TheAirline.GraphicsModel.SkinsModel;
using TheAirline.Model.GeneralModel.Helpers;
using TheAirline.Model.PassengerModel;
using TheAirline.Model.GeneralModel.CountryModel;

namespace TheAirline.Model.GeneralModel
{
    /*! Setup class.
     * This class is used for configuring the games environment.
     * The class needs no instantiation, because all methods are declared static.
     */
    public class Setup
    {
        /*! private static variable rnd.
         * holds a random number.
         */
        private static Random rnd = new Random();

        /*! public static method SetupGame().
         * Tries to create game´s environment and base configuration.
         */
        public static void SetupGame()
        {
            try
            {
                GeneralHelpers.CreateBigImageCanvas();
                ClearLists();

                LoadRegions();
                LoadCountries();
                LoadTemporaryCountries();
                LoadUnions();
                LoadAirports();
                LoadAirportLogos();
                LoadAirportMaps();
                LoadAirportFacilities();
                LoadAirlineFacilities();
                LoadManufacturers();
                LoadManufacturerLogos();
                LoadAirliners();
                LoadAirlinerFacilities();
                LoadFlightRestrictions();

                SetupStatisticsTypes();

                CreateAdvertisementTypes();
                CreateTimeZones();
                CreateFeeTypes();
                CreateFlightFacilities();

                LoadAirlines();

                Skins.Init();
            }
            catch (Exception e)
            {
                string s = e.ToString();
            }
        }

        /*! private static method ClearLists().
         * Resets game´s environment.
         */
        private static void ClearLists()
        {
            AdvertisementTypes.Clear();
            TimeZones.Clear();
            TemporaryCountries.Clear();
            Airports.Clear();
            AirportFacilities.Clear();
            AirlineFacilities.Clear();
            Manufacturers.Clear();
            Airliners.Clear();
            Airlines.Clear();
            AirlinerFacilities.Clear();
            RouteFacilities.Clear();
            StatisticsTypes.Clear();
            Regions.Clear();
            Countries.Clear();
            Unions.Clear();
            AirlinerTypes.Clear();
            Skins.Clear();
            FeeTypes.Clear();
            Alliances.Clear();
            FlightRestrictions.Clear();
        }
        /*! creates the Advertisement types
         */
        private static void CreateAdvertisementTypes()
        {
            AdvertisementTypes.AddAdvertisementType(new AdvertisementType(AdvertisementType.AirlineAdvertisementType.Internet, "No Advertisement", 0, 0));
            AdvertisementTypes.AddAdvertisementType(new AdvertisementType(AdvertisementType.AirlineAdvertisementType.Internet, "National", 10000, 1));
            AdvertisementTypes.AddAdvertisementType(new AdvertisementType(AdvertisementType.AirlineAdvertisementType.Newspaper, "No Advertisement", 0, 0));
            AdvertisementTypes.AddAdvertisementType(new AdvertisementType(AdvertisementType.AirlineAdvertisementType.Newspaper, "National", 10000, 1));
            AdvertisementTypes.AddAdvertisementType(new AdvertisementType(AdvertisementType.AirlineAdvertisementType.Radio, "No Advertisement", 0, 0));
            AdvertisementTypes.AddAdvertisementType(new AdvertisementType(AdvertisementType.AirlineAdvertisementType.Radio, "National", 10000, 1));
            AdvertisementTypes.AddAdvertisementType(new AdvertisementType(AdvertisementType.AirlineAdvertisementType.TV, "No Advertisement", 0, 0));
            AdvertisementTypes.AddAdvertisementType(new AdvertisementType(AdvertisementType.AirlineAdvertisementType.TV, "National", 10000, 1));
        }

        /*! creates the time zones.
         */
        private static void CreateTimeZones()
        {
            TimeZones.AddTimeZone(new GameTimeZone("Baker Island Time", "BIT", new TimeSpan(-12, 0, 0)));
            TimeZones.AddTimeZone(new GameTimeZone("Samoa Standard Time", "SST", new TimeSpan(-11, 0, 0)));
            TimeZones.AddTimeZone(new GameTimeZone("Hawaiian Standard Time", "HAST", new TimeSpan(-10, 0, 0)));
            TimeZones.AddTimeZone(new GameTimeZone("Alaskan Standard Time", "MIT", new TimeSpan(-9, 0, 0)));
            TimeZones.AddTimeZone(new GameTimeZone("Pacific Standard Time", "PST", new TimeSpan(-8, 0, 0)));
            TimeZones.AddTimeZone(new GameTimeZone("Mountain Standard Time", "MST", new TimeSpan(-7, 0, 0)));
            TimeZones.AddTimeZone(new GameTimeZone("Central Standard Time", "CST", new TimeSpan(-6, 0, 0)));
            TimeZones.AddTimeZone(new GameTimeZone("Eastern Standard Time", "EST", new TimeSpan(-5, 0, 0)));
            TimeZones.AddTimeZone(new GameTimeZone("Venezuelan Standard Time", "VET", new TimeSpan(-4, -30, 0)));
            TimeZones.AddTimeZone(new GameTimeZone("Atlantic Standard Time", "AST", new TimeSpan(-4, 0, 0)));
            TimeZones.AddTimeZone(new GameTimeZone("Newfoundland Standard Time", "NST", new TimeSpan(-3, -30, 0)));
            TimeZones.AddTimeZone(new GameTimeZone("South America Standard Time", "SAST", new TimeSpan(-3, 0, 0)));
            TimeZones.AddTimeZone(new GameTimeZone("Mid-Atlantic Standard Time", "MAST", new TimeSpan(-2, 0, 0)));
            TimeZones.AddTimeZone(new GameTimeZone("Cape Verde Time", "CVT", new TimeSpan(-1, 0, 0)));
            TimeZones.AddTimeZone(new GameTimeZone("Greenwich Mean Time", "GMT", new TimeSpan(0, 0, 0)));
            TimeZones.AddTimeZone(new GameTimeZone("Central European Time", "CET", new TimeSpan(1, 0, 0)));
            TimeZones.AddTimeZone(new GameTimeZone("Eastern European Time", "EET", new TimeSpan(2, 0, 0)));
            TimeZones.AddTimeZone(new GameTimeZone("East Africa Time", "EAT", new TimeSpan(3, 0, 0)));
            TimeZones.AddTimeZone(new GameTimeZone("Iran Standard Time", "IRST", new TimeSpan(3, 30, 0)));
            TimeZones.AddTimeZone(new GameTimeZone("Gulf Standard Time", "GST", new TimeSpan(4, 0, 0)));
            TimeZones.AddTimeZone(new GameTimeZone("Afghanistan Time", "AFT", new TimeSpan(4, 30, 0)));
            TimeZones.AddTimeZone(new GameTimeZone("West Asia Standard Time", "WAST", new TimeSpan(5, 0, 0)));
            TimeZones.AddTimeZone(new GameTimeZone("Indian Standard Time", "IST", new TimeSpan(5, 30, 0)));
            TimeZones.AddTimeZone(new GameTimeZone("Asia Standard Time", "ASST", new TimeSpan(6, 0, 0)));
            TimeZones.AddTimeZone(new GameTimeZone("Central Asia Standard Time", "CEST", new TimeSpan(7, 0, 0)));
            TimeZones.AddTimeZone(new GameTimeZone("China Standard Time", "CST", new TimeSpan(8, 0, 0)));
            TimeZones.AddTimeZone(new GameTimeZone("Japan Standard Time", "JST", new TimeSpan(9, 0, 0)));
            TimeZones.AddTimeZone(new GameTimeZone("Australia Standard Time", "AST", new TimeSpan(9, 30, 0)));
            TimeZones.AddTimeZone(new GameTimeZone("Australian Eastern Standard Time", "AEST", new TimeSpan(10, 0, 0)));
            TimeZones.AddTimeZone(new GameTimeZone("Australian Central Standard Time", "ACST", new TimeSpan(10, 30, 0)));
            TimeZones.AddTimeZone(new GameTimeZone("Central Pacific Standard Time", "CPST", new TimeSpan(11, 0, 0)));
            TimeZones.AddTimeZone(new GameTimeZone("New Zealand Standard Time", "NZST", new TimeSpan(12, 0, 0)));
            TimeZones.AddTimeZone(new GameTimeZone("Tonga Standard Time", "TST", new TimeSpan(13, 0, 0)));
        }

        /*! loads the airline facilities.
         */
        private static void LoadAirlineFacilities()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(AppSettings.getDataPath() + "\\airlinefacilities.xml");
            XmlElement root = doc.DocumentElement;

            XmlNodeList facilitiesList = root.SelectNodes("//airlinefacility");

            foreach (XmlElement element in facilitiesList)
            {
                string section = root.Name;
                string uid = element.Attributes["uid"].Value;
                double price = XmlConvert.ToDouble(element.Attributes["price"].Value);
                double monthlycost = XmlConvert.ToDouble(element.Attributes["monthlycost"].Value);
                int fromyear = XmlConvert.ToInt16(element.Attributes["fromyear"].Value);

                XmlElement levelElement = (XmlElement)element.SelectSingleNode("level");
                int service = Convert.ToInt32(levelElement.Attributes["service"].Value);
                int luxury = Convert.ToInt32(levelElement.Attributes["luxury"].Value);

                AirlineFacilities.AddFacility(new AirlineFacility(section, uid, price, monthlycost, fromyear, service, luxury));

                if (element.SelectSingleNode("translations") != null)
                    Translator.GetInstance().addTranslation(root.Name, element.Attributes["uid"].Value, element.SelectSingleNode("translations"));
            }
        }

        /*! loads the regions.
         */
        private static void LoadRegions()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(AppSettings.getDataPath() + "\\regions.xml");
            XmlElement root = doc.DocumentElement;

            XmlNodeList regionsList = root.SelectNodes("//region");
            foreach (XmlElement element in regionsList)
            {
                string section = root.Name;
                string uid = element.Attributes["uid"].Value;

                Regions.AddRegion(new Region(section, uid));

                if (element.SelectSingleNode("translations") != null)
                    Translator.GetInstance().addTranslation(root.Name, element.Attributes["uid"].Value, element.SelectSingleNode("translations"));
            }
        }

        /*! loads the manufacturers.
         */
        private static void LoadManufacturers()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(AppSettings.getDataPath() + "\\manufacturers.xml");
            XmlElement root = doc.DocumentElement;

            XmlNodeList manufacturersList = root.SelectNodes("//manufacturer");
            foreach (XmlElement manufacturer in manufacturersList)
            {
                string name = manufacturer.Attributes["name"].Value;
                string shortname = manufacturer.Attributes["shortname"].Value;

                Country country = Countries.GetCountry(manufacturer.Attributes["country"].Value);

                Manufacturers.AddManufacturer(new Manufacturer(name, shortname, country));
            }
        }

        /*!loads the airliners.
         */
        private static void LoadAirliners()
        {
          
            try
            {
                DirectoryInfo dir = new DirectoryInfo(AppSettings.getDataPath() + "\\addons\\airliners");

                foreach (FileInfo file in dir.GetFiles("*.xml"))
                {
                    Console.WriteLine(file.FullName);
                    LoadAirliners(file.FullName);

                }
            }
            catch (Exception e)
            {
                  string s = e.ToString();
            }
        }
        private static void LoadAirliners(string file)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(file);
            XmlElement root = doc.DocumentElement;

            XmlNodeList airlinersList = root.SelectNodes("//airliner");

            foreach (XmlElement airliner in airlinersList)
            {
                AirlinerType.TypeOfAirliner airlinerType = airliner.HasAttribute("type") ? (AirlinerType.TypeOfAirliner)Enum.Parse(typeof(AirlinerType.TypeOfAirliner), airliner.Attributes["type"].Value) : AirlinerType.TypeOfAirliner.Passenger;

                string manufacturerName = airliner.Attributes["manufacturer"].Value;
                Manufacturer manufacturer = Manufacturers.GetManufacturer(manufacturerName);

                string name = airliner.Attributes["name"].Value;
                long price = Convert.ToInt64(airliner.Attributes["price"].Value);


                XmlElement typeElement = (XmlElement)airliner.SelectSingleNode("type");
                AirlinerType.BodyType body = (AirlinerType.BodyType)Enum.Parse(typeof(AirlinerType.BodyType), typeElement.Attributes["body"].Value);
                AirlinerType.TypeRange rangeType = (AirlinerType.TypeRange)Enum.Parse(typeof(AirlinerType.TypeRange), typeElement.Attributes["rangetype"].Value);
                AirlinerType.EngineType engine = (AirlinerType.EngineType)Enum.Parse(typeof(AirlinerType.EngineType), typeElement.Attributes["engine"].Value);

                XmlElement specsElement = (XmlElement)airliner.SelectSingleNode("specs");
                double wingspan = XmlConvert.ToDouble(specsElement.Attributes["wingspan"].Value);
                double length = XmlConvert.ToDouble(specsElement.Attributes["length"].Value);
                long range = Convert.ToInt64(specsElement.Attributes["range"].Value);
                double speed = XmlConvert.ToDouble(specsElement.Attributes["speed"].Value);
                double fuel = XmlConvert.ToDouble(specsElement.Attributes["consumption"].Value);
                long runwaylenght = XmlConvert.ToInt64(specsElement.Attributes["runwaylengthrequired"].Value);



                XmlElement capacityElement = (XmlElement)airliner.SelectSingleNode("capacity");

                XmlElement producedElement = (XmlElement)airliner.SelectSingleNode("produced");
                int from = Convert.ToInt16(producedElement.Attributes["from"].Value);
                int to = Convert.ToInt16(producedElement.Attributes["to"].Value);

                if (airlinerType == AirlinerType.TypeOfAirliner.Passenger)
                {

                    int passengers = Convert.ToInt16(capacityElement.Attributes["passengers"].Value);
                    int cockpitcrew = Convert.ToInt16(capacityElement.Attributes["cockpitcrew"].Value);
                    int cabincrew = Convert.ToInt16(capacityElement.Attributes["cabincrew"].Value);
                    int maxClasses = Convert.ToInt16(capacityElement.Attributes["maxclasses"].Value);
                    AirlinerTypes.AddType(new AirlinerPassengerType(manufacturer, name, passengers, cockpitcrew, cabincrew, speed, range, wingspan, length, fuel, price, maxClasses, runwaylenght, body, rangeType, engine, new ProductionPeriod(from, to)));

                }
                if (airlinerType == AirlinerType.TypeOfAirliner.Cargo)
                {
                    int cockpitcrew = Convert.ToInt16(capacityElement.Attributes["cockpitcrew"].Value);
                    double cargo = Convert.ToDouble(capacityElement.Attributes["cargo"].Value);
                    AirlinerTypes.AddType(new AirlinerCargoType(manufacturer, name, cockpitcrew, cargo, speed, range, wingspan, length, fuel, price, runwaylenght, body, rangeType, engine, new ProductionPeriod(from, to)));
                }

            }
        }

        /*!loads the airports.
         */
        private static void LoadAirports()
        {
            //LoadAirports(AppSettings.getDataPath() + "\\airports.xml");
            try
            {
                DirectoryInfo dir = new DirectoryInfo(AppSettings.getDataPath() + "\\addons\\airports");

                foreach (FileInfo file in dir.GetFiles("*.xml"))
                {
                    LoadAirports(file.FullName);

                }
            }
            catch (Exception e)
            {
                string s = e.ToString();
            }
        }
        private static void LoadAirports(string file)
        {
            string id = "";
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(file);
                XmlElement root = doc.DocumentElement;

                XmlNodeList airportsList = root.SelectNodes("//airport");

                foreach (XmlElement airportElement in airportsList)
                {
                    string name = airportElement.Attributes["name"].Value;
                    string icao = airportElement.Attributes["icao"].Value;
                    string iata = airportElement.Attributes["iata"].Value;

                    id = name;

                    AirportProfile.AirportType type = (AirportProfile.AirportType)Enum.Parse(typeof(AirportProfile.AirportType), airportElement.Attributes["type"].Value);
                    Weather.Season season = (Weather.Season)Enum.Parse(typeof(Weather.Season), airportElement.Attributes["season"].Value);

                    XmlElement townElement = (XmlElement)airportElement.SelectSingleNode("town");
                    string town = townElement.Attributes["town"].Value;
                    string country = townElement.Attributes["country"].Value;
                    TimeSpan gmt = TimeSpan.Parse(townElement.Attributes["GMT"].Value);
                    TimeSpan dst = TimeSpan.Parse(townElement.Attributes["DST"].Value);

                    XmlElement latitudeElement = (XmlElement)airportElement.SelectSingleNode("coordinates/latitude");
                    XmlElement longitudeElement = (XmlElement)airportElement.SelectSingleNode("coordinates/longitude");
                    Coordinate latitude = Coordinate.Parse(latitudeElement.Attributes["value"].Value);
                    Coordinate longitude = Coordinate.Parse(longitudeElement.Attributes["value"].Value);

                    XmlElement sizeElement = (XmlElement)airportElement.SelectSingleNode("size");
                    AirportProfile.AirportSize size = (AirportProfile.AirportSize)Enum.Parse(typeof(AirportProfile.AirportSize), sizeElement.Attributes["value"].Value);


                    AirportProfile profile = new AirportProfile(name, iata, icao, type, town, Countries.GetCountry(country), gmt, dst, new Coordinates(latitude, longitude), size, size, season);

                    Airport airport = new Airport(profile);

                    XmlNodeList terminalList = airportElement.SelectNodes("terminals/terminal");

                    foreach (XmlElement terminalNode in terminalList)
                    {
                        string terminalName = terminalNode.Attributes["name"].Value;
                        int terminalGates = XmlConvert.ToInt32(terminalNode.Attributes["gates"].Value);

                        airport.Terminals.addTerminal(new Terminal(airport, null, terminalName, terminalGates, new DateTime(1950, 1, 1)));
                    }

                    XmlNodeList runwaysList = airportElement.SelectNodes("runways/runway");

                    foreach (XmlElement runwayNode in runwaysList)
                    {
                        string runwayName = runwayNode.Attributes["name"].Value;
                        long runwayLength = XmlConvert.ToInt32(runwayNode.Attributes["length"].Value);
                        Runway.SurfaceType surface = (Runway.SurfaceType)Enum.Parse(typeof(Runway.SurfaceType), runwayNode.Attributes["surface"].Value);

                        airport.Runways.Add(new Runway(runwayName, runwayLength, surface));

                    }
                    if (Airports.GetAirport(airport.Profile.IATACode) == null)
                        Airports.AddAirport(airport);
                }
            }
            catch (Exception e)
            {
                string i = id;
                string s = e.ToString();
            }
        }

        /*!loads the airport facilities.
         */
        private static void LoadAirportFacilities()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(AppSettings.getDataPath() + "\\airportfacilities.xml");
            XmlElement root = doc.DocumentElement;

            XmlNodeList facilitiesList = root.SelectNodes("//airportfacility");

            foreach (XmlElement element in facilitiesList)
            {
                string section = root.Name;
                string uid = element.Attributes["uid"].Value;
                string shortname = element.Attributes["shortname"].Value;
                AirportFacility.FacilityType type =
      (AirportFacility.FacilityType)Enum.Parse(typeof(AirportFacility.FacilityType), element.Attributes["type"].Value);
                int typeLevel = Convert.ToInt16(element.Attributes["typelevel"].Value);

                double price = XmlConvert.ToDouble(element.Attributes["price"].Value);
                int buildingDays = XmlConvert.ToInt32(element.Attributes["buildingdays"].Value);

                XmlElement levelElement = (XmlElement)element.SelectSingleNode("level");
                int service = Convert.ToInt32(levelElement.Attributes["service"].Value);
                int luxury = Convert.ToInt32(levelElement.Attributes["luxury"].Value);

                AirportFacilities.AddFacility(new AirportFacility(section, uid, shortname, type, buildingDays, typeLevel, price, service, luxury));

                if (element.SelectSingleNode("translations") != null)
                    Translator.GetInstance().addTranslation(root.Name, element.Attributes["uid"].Value, element.SelectSingleNode("translations"));
            }
        }

        /*!loads the countries.
         */
        private static void LoadCountries()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(AppSettings.getDataPath() + "\\countries.xml");
            XmlElement root = doc.DocumentElement;

            XmlNodeList countriesList = root.SelectNodes("//country");
            foreach (XmlElement element in countriesList)
            {
                string section = root.Name;
                string uid = element.Attributes["uid"].Value;
                string shortname = element.Attributes["shortname"].Value;
                string flag = element.Attributes["flag"].Value;
                Region region = Regions.GetRegion(element.Attributes["region"].Value);
                string tailformat = element.Attributes["tailformat"].Value;

                Country country = new Country(section, uid, shortname, region, tailformat);
                country.Flag = AppSettings.getDataPath() + "\\graphics\\flags\\" + flag + ".png";
                Countries.AddCountry(country);

                if (element.SelectSingleNode("translations") != null)
                    Translator.GetInstance().addTranslation(root.Name, element.Attributes["uid"].Value, element.SelectSingleNode("translations"));
            }


        }
        /*! loads the temporary countries
         */
        private static void LoadTemporaryCountries()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(AppSettings.getDataPath() + "\\temporary countries.xml");
            XmlElement root = doc.DocumentElement;

            XmlNodeList countriesList = root.SelectNodes("//country");
            foreach (XmlElement element in countriesList)
            {
                string section = root.Name;
                string uid = element.Attributes["uid"].Value;
                string shortname = element.Attributes["shortname"].Value;
                string flag = element.Attributes["flag"].Value;
                Region region = Regions.GetRegion(element.Attributes["region"].Value);
                string tailformat = element.Attributes["tailformat"].Value;

                XmlElement periodElement = (XmlElement)element.SelectSingleNode("period");
                DateTime startDate = Convert.ToDateTime(periodElement.Attributes["start"].Value);
                DateTime endDate = Convert.ToDateTime(periodElement.Attributes["end"].Value);

                XmlElement historyElement = (XmlElement)element.SelectSingleNode("history");
                Country before = Countries.GetCountry(historyElement.Attributes["before"].Value);
                Country after = Countries.GetCountry(historyElement.Attributes["after"].Value);

                Country country = new Country(section, uid, shortname, region, tailformat);

                if (element.SelectSingleNode("translations") != null)
                    Translator.GetInstance().addTranslation(root.Name, element.Attributes["uid"].Value, element.SelectSingleNode("translations"));

                TemporaryCountry tCountry = new TemporaryCountry(country, startDate, endDate, before, after);

                tCountry.Flag = AppSettings.getDataPath() + "\\graphics\\flags\\" + flag + ".png";


                TemporaryCountries.AddCountry(tCountry);
            }
        }
        /*! load the unions
         */
        private static void LoadUnions()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(AppSettings.getDataPath() + "\\unions.xml");
            XmlElement root = doc.DocumentElement;

            XmlNodeList unionsList = root.SelectNodes("//union");
            foreach (XmlElement element in unionsList)
            {
                try
                {
                    string section = root.Name;
                    string uid = element.Attributes["uid"].Value;
                    string shortname = element.Attributes["shortname"].Value;
                    string flag = element.Attributes["flag"].Value;


                    XmlElement periodElement = (XmlElement)element.SelectSingleNode("period");
                    DateTime creationDate = Convert.ToDateTime(periodElement.Attributes["creation"].Value);
                    DateTime obsoleteDate = Convert.ToDateTime(periodElement.Attributes["obsolete"].Value);

                    Union union = new Union(section, uid, shortname, creationDate, obsoleteDate);

                    XmlNodeList membersList = element.SelectNodes("members/member");

                    foreach (XmlElement memberNode in membersList)
                    {
                        Country country = Countries.GetCountry(memberNode.Attributes["country"].Value);
                        DateTime fromDate = Convert.ToDateTime(memberNode.Attributes["memberfrom"].Value);
                        DateTime toDate = Convert.ToDateTime(memberNode.Attributes["memberto"].Value);

                        union.addMember(new UnionMember(country, fromDate, toDate));
                    }


                    union.Flag = AppSettings.getDataPath() + "\\graphics\\flags\\" + flag + ".png";
                    Unions.AddUnion(union);

                    if (element.SelectSingleNode("translations") != null)
                        Translator.GetInstance().addTranslation(root.Name, element.Attributes["uid"].Value, element.SelectSingleNode("translations"));
                }
                catch (Exception e)
                {
                    throw new Exception("Error on reading unions");
                }
            }

        }
        /*! load the airliner facilities.
         */
        private static void LoadAirlinerFacilities()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(AppSettings.getDataPath() + "\\airlinerfacilities.xml");
            XmlElement root = doc.DocumentElement;

            XmlNodeList facilitiesList = root.SelectNodes("//airlinerfacility");
            foreach (XmlElement element in facilitiesList)
            {
                string section = root.Name;
                string uid = element.Attributes["uid"].Value;
                AirlinerFacility.FacilityType type = (AirlinerFacility.FacilityType)Enum.Parse(typeof(AirlinerFacility.FacilityType), element.Attributes["type"].Value);
                int fromyear = Convert.ToInt16(element.Attributes["fromyear"].Value);

                XmlElement levelElement = (XmlElement)element.SelectSingleNode("level");
                int service = Convert.ToInt32(levelElement.Attributes["service"].Value);

                XmlElement seatsElement = (XmlElement)element.SelectSingleNode("seats");
                double seatsPercent = XmlConvert.ToDouble(seatsElement.Attributes["percent"].Value);
                double seatsPrice = XmlConvert.ToDouble(seatsElement.Attributes["price"].Value);
                double seatuse = XmlConvert.ToDouble(seatsElement.Attributes["uses"].Value);

                AirlinerFacilities.AddFacility(new AirlinerFacility(section, uid, type, fromyear, service, seatsPercent, seatsPrice, seatuse));

                if (element.SelectSingleNode("translations") != null)
                    Translator.GetInstance().addTranslation(root.Name, element.Attributes["uid"].Value, element.SelectSingleNode("translations"));
            }
        }
        /*loads the airlines
         */
        private static void LoadAirlines()
        {
            try
            {
                DirectoryInfo dir = new DirectoryInfo(AppSettings.getDataPath() + "\\addons\\airlines");

                foreach (FileInfo file in dir.GetFiles("*.xml"))
                {
                    LoadAirline(file.FullName);
                }

                CreateAirlineLogos();

                GameObject.GetInstance().HumanAirline = Airlines.GetAllAirlines()[0];
            }
            catch (Exception e)
            {
                string s = e.ToString();
            }
        }
        /*loads an airline
         */
        private static void LoadAirline(string path)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(path);
            XmlElement root = doc.DocumentElement;

            XmlElement profileElement = (XmlElement)root.SelectSingleNode("profile");

            string name = profileElement.Attributes["name"].Value;
            string iata = profileElement.Attributes["iata"].Value;
            string color = profileElement.Attributes["color"].Value;
            Country country = Countries.GetCountry(profileElement.Attributes["country"].Value);
            string ceo = profileElement.Attributes["CEO"].Value;
            Airline.AirlineMentality mentality = (Airline.AirlineMentality)Enum.Parse(typeof(Airline.AirlineMentality), profileElement.Attributes["mentality"].Value);
            Airline.AirlineMarket market = (Airline.AirlineMarket)Enum.Parse(typeof(Airline.AirlineMarket), profileElement.Attributes["market"].Value);

            Airlines.AddAirline(new Airline(new AirlineProfile(name, iata, color, country, ceo), mentality, market));

        }
        /*loads the flight restrictions
       */
        private static void LoadFlightRestrictions()
        {

            XmlDocument doc = new XmlDocument();
            doc.Load(AppSettings.getDataPath() + "\\flightrestrictions.xml");
            XmlElement root = doc.DocumentElement;

            XmlNodeList restrictionsList = root.SelectNodes("//restriction");
            foreach (XmlElement element in restrictionsList)
            {
                FlightRestriction.RestrictionType type = (FlightRestriction.RestrictionType)Enum.Parse(typeof(FlightRestriction.RestrictionType), element.Attributes["type"].Value);

                DateTime startDate = Convert.ToDateTime(element.Attributes["start"].Value);
                DateTime endDate = Convert.ToDateTime(element.Attributes["end"].Value);

                XmlElement countriesElement = (XmlElement)element.SelectSingleNode("countries");

                BaseUnit from, to;

                if (countriesElement.Attributes["fromtype"].Value == "C")
                    from = Countries.GetCountry(countriesElement.Attributes["from"].Value);
                else
                    from = Unions.GetUnion(countriesElement.Attributes["from"].Value);

                if (countriesElement.Attributes["totype"].Value == "C")
                    to = Countries.GetCountry(countriesElement.Attributes["to"].Value);
                else
                    to = Unions.GetUnion(countriesElement.Attributes["to"].Value);

                FlightRestrictions.AddRestriction(new FlightRestriction(type, startDate, endDate, from, to));

            }
        }
        /*! sets up the statistics types.
         */
        private static void SetupStatisticsTypes()
        {
            StatisticsTypes.AddStatisticsType(new StatisticsType("Arrivals", "Arrivals"));
            StatisticsTypes.AddStatisticsType(new StatisticsType("Departures", "Departures"));
            StatisticsTypes.AddStatisticsType(new StatisticsType("Passengers", "Passengers"));
            StatisticsTypes.AddStatisticsType(new StatisticsType("Passengers per flight", "Passengers%"));
            StatisticsTypes.AddStatisticsType(new StatisticsType("Passenger Capacity", "Capacity"));
            StatisticsTypes.AddStatisticsType(new StatisticsType("Airliner Income", "Airliner_Income"));
            StatisticsTypes.AddStatisticsType(new StatisticsType("On-Time flights", "On-Time"));
            StatisticsTypes.AddStatisticsType(new StatisticsType("Flights On-Time", "On-Time%"));
        }

        /*! create a random airliner with a minimum range.
         */
        private static Airliner CreateAirliner(double minRange)
        {
            List<AirlinerType> types = AirlinerTypes.GetTypes(delegate(AirlinerType t) { return t.Range >= minRange && t.Produced.From < GameObject.GetInstance().GameTime.Year && t.Produced.To > GameObject.GetInstance().GameTime.Year - 30; });

            int typeNumber = rnd.Next(types.Count);
            AirlinerType type = types[typeNumber];

            int countryNumber = rnd.Next(Countries.Count());
            Country country = Countries.GetCountries()[countryNumber];

            int builtYear = rnd.Next(Math.Max(type.Produced.From, GameObject.GetInstance().GameTime.Year - 30), Math.Min(GameObject.GetInstance().GameTime.Year, type.Produced.To));

            Airliner airliner = new Airliner(type, country.TailNumbers.getNextTailNumber(), new DateTime(builtYear, 1, 1));

            int age = MathHelpers.CalculateAge(airliner.BuiltDate, GameObject.GetInstance().GameTime);

            long kmPerYear = rnd.Next(100000, 1000000);
            long km = kmPerYear * age;

            airliner.Flown = km;

            return airliner;
        }

        /*! create some game airliners.
         */
        public static void CreateAirliners()
        {
            int number = AirlinerTypes.GetTypes(delegate(AirlinerType t) { return t.Produced.From <= GameObject.GetInstance().GameTime.Year && t.Produced.To >= GameObject.GetInstance().GameTime.Year - 30; }).Count * 25;
            for (int i = 0; i < number; i++)
            {
                Airliners.AddAirliner(CreateAirliner(0));
            }
        }


        /*! sets up test game.
         */
        public static void SetupTestGame(int opponents)
        {
            RemoveAirlines(opponents);

            //sets all the facilities at an airport to none for all airlines
            foreach (Airport airport in Airports.GetAllAirports())
            {
                foreach (Airline airline in Airlines.GetAllAirlines())
                {
                    foreach (AirportFacility.FacilityType type in Enum.GetValues(typeof(AirportFacility.FacilityType)))
                    {
                        AirportFacility noneFacility = AirportFacilities.GetFacilities(type).Find((delegate(AirportFacility facility) { return facility.TypeLevel == 0; }));

                        airport.setAirportFacility(airline, noneFacility, GameObject.GetInstance().GameTime);
                    }
                }
            }

            foreach (Airline airline in Airlines.GetAllAirlines())
            {
                airline.Money = GameObject.GetInstance().StartMoney;
                if (!airline.IsHuman)
                    CreateComputerRoutes(airline);
                // chs, 2011-24-10 changed so the human starts with an airport with home base facilities


                List<AirportFacility> facilities = AirportFacilities.GetFacilities(AirportFacility.FacilityType.Service);

                AirportFacility facility = facilities.Find((delegate(AirportFacility f) { return f.TypeLevel == 1; }));

                airline.Airports[0].setAirportFacility(airline, facility, GameObject.GetInstance().GameTime);

            }

       
        }

        /*! removes some random airlines from the list bases on number of opponents.
         */
        private static void RemoveAirlines(int opponnents)
        {
            int count = Airlines.GetAirlines(a=>!a.IsHuman).Count;

            for (int i = 0; i < count - opponnents; i++)
            {
                List<Airline> airlines = Airlines.GetAirlines(a=>!a.IsHuman);

                Airlines.RemoveAirline(airlines[rnd.Next(airlines.Count)]);
            }
        }
        //finds the home base for a computer airline
        private static Airport FindComputerHomeBase(Airline airline)
        {
            List<Airport> airports = Airports.GetAirports(airline.Profile.Country).FindAll(a => a.Terminals.getFreeGates() > 1);

            if (airports.Count < 4)
                airports = Airports.GetAirports(airline.Profile.Country.Region).FindAll(a => a.Terminals.getFreeGates() > 1);

            Dictionary<Airport, int> list = new Dictionary<Airport, int>();
            airports.ForEach(a => list.Add(a, ((int)a.Profile.Size) * GeneralHelpers.GetAirportsNearAirport(a).Count));

            return AIHelpers.GetRandomItem(list);
        }
        /*! creates some airliners and routes for a computer airline.
         */
        private static void CreateComputerRoutes(Airline airline)
        {
            Airport airportHomeBase = FindComputerHomeBase(airline);

            Airport airportDestination = AIHelpers.GetDestinationAirport(airline, airportHomeBase);

            KeyValuePair<Airliner, Boolean>? airliner = AIHelpers.GetAirlinerForRoute(airline, airportHomeBase, airportDestination);

            if (airportDestination == null || !airliner.HasValue)
            {

                CreateComputerRoutes(airline);

            }
            else
            {
                airportHomeBase.Terminals.rentGate(airline);
                airportHomeBase.Terminals.rentGate(airline);

                airportDestination.Terminals.rentGate(airline);

                double price = PassengerHelpers.GetPassengerPrice(airportDestination, airline.Airports[0]);

                Guid id = Guid.NewGuid();

                Route route = new Route(id.ToString(), airportDestination, airline.Airports[0], price);

                FleetAirliner fAirliner = AirlineHelpers.BuyAirliner(airline, airliner.Value.Key, airportDestination);
                fAirliner.addRoute(route);
                fAirliner.Status = FleetAirliner.AirlinerStatus.To_route_start;

                route.LastUpdated = GameObject.GetInstance().GameTime;



                foreach (AirlinerClass.ClassType type in Enum.GetValues(typeof(AirlinerClass.ClassType)))
                {
                    route.getRouteAirlinerClass(type).FarePrice = price * GeneralHelpers.ClassToPriceFactor(type);
                    route.getRouteAirlinerClass(type).CabinCrew = 2;
                    route.getRouteAirlinerClass(type).DrinksFacility = RouteFacilities.GetFacilities(RouteFacility.FacilityType.Drinks)[rnd.Next(RouteFacilities.GetFacilities(RouteFacility.FacilityType.Drinks).Count)];// RouteFacilities.GetBasicFacility(RouteFacility.FacilityType.Drinks);
                    route.getRouteAirlinerClass(type).FoodFacility = RouteFacilities.GetFacilities(RouteFacility.FacilityType.Food)[rnd.Next(RouteFacilities.GetFacilities(RouteFacility.FacilityType.Food).Count)];//RouteFacilities.GetBasicFacility(RouteFacility.FacilityType.Food);
                }
                airline.addRoute(route);

                airportDestination.Terminals.getEmptyGate(airline).Route = route;
                airline.Airports[0].Terminals.getEmptyGate(airline).Route = route;

                AIHelpers.CreateRouteTimeTable(route, fAirliner);


            }
        }
        /*! loads the maps for the airports
         */
        private static void LoadAirportMaps()
        {
            DirectoryInfo dir = new DirectoryInfo(AppSettings.getDataPath() + "\\graphics\\airportmaps");

            foreach (FileInfo file in dir.GetFiles("*.png"))
            {
                string code = file.Name.Split('.')[0].ToUpper();
                Airport airport = Airports.GetAirport(code);

                if (airport != null)
                    airport.Profile.Map = file.FullName;
                else
                    code = "x";
            }
        }
        /*! loads the logos for the airports.
         */
        private static void LoadAirportLogos()
        {
            DirectoryInfo dir = new DirectoryInfo(AppSettings.getDataPath() + "\\graphics\\airportlogos");

            foreach (FileInfo file in dir.GetFiles("*.png"))
            {
                string code = file.Name.Split('.')[0].ToUpper();
                Airport airport = Airports.GetAirport(code);

                if (airport != null)
                    airport.Profile.Logo = file.FullName;
                else
                    code = "x";
            }
        }
        /*! loads the manufacturer logos
         */
        private static void LoadManufacturerLogos()
        {
            DirectoryInfo dir = new DirectoryInfo(AppSettings.getDataPath() + "\\graphics\\manufacturerlogos");

            foreach (FileInfo file in dir.GetFiles("*.png"))
            {
                string name = file.Name.Split('.')[0];
                Manufacturer manufacturer = Manufacturers.GetManufacturer(name);

                if (manufacturer != null)
                    manufacturer.Logo = file.FullName;
                else
                    name = "x";
            }
        }

        /*! creates the logos for the game airlines.
         */
        private static void CreateAirlineLogos()
        {
            foreach (Airline airline in Airlines.GetAllAirlines())
                airline.Profile.Logo = AppSettings.getDataPath() + "\\graphics\\airlinelogos\\" + airline.Profile.IATACode + ".png";
        }

        /*! private stativ method CreateFlightFacilities.
         * creates the in flight facilities.
         */
        private static void CreateFlightFacilities()
        {
            RouteFacilities.AddFacility(new RouteFacility(RouteFacility.FacilityType.Food, "No Food", 1, -50, RouteFacility.ExpenseType.Fixed, 0, null));
            RouteFacilities.AddFacility(new RouteFacility(RouteFacility.FacilityType.Food, "Buyable Snacks", 1, 10, RouteFacility.ExpenseType.Random, 0, FeeTypes.GetType("Snacks")));
            RouteFacilities.AddFacility(new RouteFacility(RouteFacility.FacilityType.Food, "Buyable Meal", 1, 25, RouteFacility.ExpenseType.Random, 0, FeeTypes.GetType("Meal")));
            RouteFacilities.AddFacility(new RouteFacility(RouteFacility.FacilityType.Food, "Basic Meal", 2, 50, RouteFacility.ExpenseType.Fixed, 20, null));
            RouteFacilities.AddFacility(new RouteFacility(RouteFacility.FacilityType.Food, "Full Dinner", 3, 100, RouteFacility.ExpenseType.Fixed, 40, null));
            RouteFacilities.AddFacility(new RouteFacility(RouteFacility.FacilityType.Drinks, "No Drinks", 1, -50, RouteFacility.ExpenseType.Fixed, 0, null));
            RouteFacilities.AddFacility(new RouteFacility(RouteFacility.FacilityType.Drinks, "Buyable Drinks", 1, 25, RouteFacility.ExpenseType.Random, 0, FeeTypes.GetType("Drinks")));
            RouteFacilities.AddFacility(new RouteFacility(RouteFacility.FacilityType.Drinks, "Free Drinks", 3, 100, RouteFacility.ExpenseType.Fixed, 30, null));
        }

        /*! creates the Fee types.
         */
        private static void CreateFeeTypes()
        {

            FeeTypes.AddType(new FeeType(FeeType.eFeeType.Wage, "Cabin kilometer rate", 0.8, 0.2, 2, 100));
            FeeTypes.AddType(new FeeType(FeeType.eFeeType.Wage, "Cockpit kilometer rate", 2, 1, 4, 100));
            FeeTypes.AddType(new FeeType(FeeType.eFeeType.Wage, "Cockpit wage", 100, 20, 200, 100));
            FeeTypes.AddType(new FeeType(FeeType.eFeeType.Wage, "Cabin wage", 50, 10, 100, 100));
            FeeTypes.AddType(new FeeType(FeeType.eFeeType.FoodDrinks, "Drinks", 2, 0.5, 6, 75));
            FeeTypes.AddType(new FeeType(FeeType.eFeeType.FoodDrinks, "Snacks", 3, 2, 4, 70));
            FeeTypes.AddType(new FeeType(FeeType.eFeeType.FoodDrinks, "Meal", 5, 4, 6, 50));
            FeeTypes.AddType(new FeeType(FeeType.eFeeType.Fee, "1 Bag", 0, 0, 30, 95));
            FeeTypes.AddType(new FeeType(FeeType.eFeeType.Fee, "2 Bags", 10, 0, 45, 25));
            FeeTypes.AddType(new FeeType(FeeType.eFeeType.Fee, "3+ Bags", 20, 0, 55, 2));
            FeeTypes.AddType(new FeeType(FeeType.eFeeType.Fee, "Pets", 0, 0, 150, 1));
        }
    }
}
