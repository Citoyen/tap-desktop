﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheAirline.Model.GeneralModel;

namespace TheAirline.Model.AirlinerModel
{
     
     //the class for a type of airliner
    public abstract class AirlinerType
    {
        public string Name { get; set; }
        public double CruisingSpeed { get; set; }
        public long Range { get; set; }
        public double Length { get; set; }
        public double Wingspan { get; set; }
        public int CockpitCrew { get; set; }
        public long Price { get; set; }
        public ProductionPeriod Produced { get; set; }
        public double FuelConsumption { get; set; }
        public Manufacturer Manufacturer { get; set; }
        public enum BodyType {Narrow_Body, Single_Aisle,Wide_Body} 
        public enum TypeRange {Regional, Short_Range, Medium_Range, Long_Range}
        public enum EngineType {Jet, Turboprop}
        public enum TypeOfAirliner {Passenger, Cargo, Mixed}
        public BodyType Body { get; set; }
        public TypeRange RangeType { get; set; }
        public EngineType Engine { get; set; }
        public long MinRunwaylength { get; set; }
        public TypeOfAirliner TypeAirliner { get; set; }
        public AirlinerType(Manufacturer manufacturer,TypeOfAirliner typeOfAirliner, string name, int cockpitCrew, double speed, long range, double wingspan, double length, double consumption, long price,long minRunwaylength, BodyType body, TypeRange rangeType, EngineType engine, ProductionPeriod produced)
        {
            this.TypeAirliner = typeOfAirliner;
            this.Manufacturer = manufacturer;
            this.Name = name;
            this.CruisingSpeed = speed;
            this.Range = range;
            this.Wingspan = wingspan;
            this.Length = length;
            this.CockpitCrew = cockpitCrew;
            this.Price = price;
            this.FuelConsumption = consumption;
            this.Produced = produced;
            this.Engine = engine;
            this.Body = body;
            this.RangeType = rangeType;
            this.MinRunwaylength = minRunwaylength;
        }
       
        //returns the montly maintenance
        public long getMaintenance()
        {
            double maintenance = 0.0013 * (double)this.Price;
            return Convert.ToInt64(maintenance);
        }
        // chs, 2011-10-10 changed the leasing price
        //returns the leasing price based on 5 years with 6% in rate 
        public long getLeasingPrice()
        {
            double months = 5 * 12;
            double rate = 1.06;
            double leasingPrice = this.Price / months * rate;
            return Convert.ToInt64(leasingPrice);

        }
       
    }
    //the class for a passenger airliner type
    public class AirlinerPassengerType : AirlinerType
    {
        public int MaxSeatingCapacity { get; set; }
        public int CabinCrew { get; set; }
        public int MaxAirlinerClasses { get; set; }
        public AirlinerPassengerType(Manufacturer manufacturer, string name, int seating, int cockpitcrew, int cabincrew, double speed, long range, double wingspan, double length, double consumption, long price, int maxAirlinerClasses, long minRunwaylength, BodyType body, TypeRange rangeType, EngineType engine, ProductionPeriod produced) : base(manufacturer,TypeOfAirliner.Passenger,name,cockpitcrew,speed,range,wingspan,length,consumption,price,minRunwaylength,body,rangeType,engine,produced)
        {
            this.MaxSeatingCapacity = seating;
            this.CabinCrew = cabincrew;
            this.MaxAirlinerClasses = maxAirlinerClasses;
        }
    }
    //the class for a cargo airliner type
    public class AirlinerCargoType : AirlinerType
    {
        public double CargoSize { get; set; }
        public AirlinerCargoType(Manufacturer manufacturer, string name,  int cockpitcrew, double cargoSize,  double speed, long range, double wingspan, double length, double consumption, long price, long minRunwaylength, BodyType body, TypeRange rangeType, EngineType engine, ProductionPeriod produced) : base(manufacturer,TypeOfAirliner.Cargo,name,cockpitcrew,speed,range,wingspan,length,consumption,price,minRunwaylength,body,rangeType,engine,produced)
        {
            this.CargoSize = cargoSize;
        }
    }
    //the class for a production period (year from - year to)
    public class ProductionPeriod
    {
        public int From { get; set; }
        public int To { get; set; }
        public ProductionPeriod(int from, int to)
        {
            this.From = from;
            this.To = to;
        }
        public override string ToString()
        {
            return string.Format("{0}-{1}", this.From > GameObject.GetInstance().GameTime.Year ? "?" : this.From.ToString(), this.To > GameObject.GetInstance().GameTime.Year ? "?" : this.To.ToString());
        }
    }
    //the collection of airliner types
    public class AirlinerTypes
    {
        private static Dictionary<string, AirlinerType> types = new Dictionary<string, AirlinerType>();
        //clears the list
        public static void Clear()
        {
            types = new Dictionary<string, AirlinerType>();
        }
        public static void AddType(AirlinerType type)
        {
            if (types.ContainsKey(type.Name))
                Console.WriteLine(type.Name);
            types.Add(type.Name, type);
        }
        public static AirlinerType GetType(string name)
        {
            return types[name];
        }
        public static List<AirlinerType> GetTypes()
        {
            return types.Values.ToList();
        }
        
    }
}
