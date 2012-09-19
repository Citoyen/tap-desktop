﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheAirline.Model.AirportModel;
using TheAirline.Model.GeneralModel;
using TheAirline.Model.GeneralModel.StatisticsModel;
using TheAirline.Model.AirlineModel;
using TheAirline.Model.GeneralModel.InvoicesModel;

namespace TheAirline.Model.AirlinerModel.RouteModel
{
    //the class for a route
    public class Route
    {
        public string Id { get; set; }
        public Airport Destination1 { get; set; }
        public Airport Destination2 { get; set; }
        //public FleetAirliner Airliner { get; set; }
        public List<RouteAirlinerClass> Classes { get; set; }
        public RouteTimeTable TimeTable { get; set; }
        public Invoices Invoices { get; set; }
        public RouteStatistics Statistics { get; set; }
        public Boolean Banned { get; set; }
        public double Balance { get { return getBalance(); } set { ;} }
        public double FillingDegree { get { return getFillingDegree(); } set { ;} }
        public double IncomePerPassenger { get { return getIncomePerPassenger(); } set { ;} }
        public DateTime LastUpdated { get; set; }
        public Boolean HasAirliner { get { return getAirliners().Count > 0; } set { ;} }
        public Weather.Season Season { get; set; }
        public Airline Airline { get; set; }
        public Route(string id,Airport destination1, Airport destination2, double farePrice)
        {
    
            this.Id = id;
            this.Destination1 = destination1;
            this.Destination2 = destination2;
            this.TimeTable = new RouteTimeTable(this);
            this.Invoices = new Invoices();
            this.Statistics = new RouteStatistics();
            this.Banned = false;

             this.Season = Weather.Season.All_Year;

            this.Classes = new List<RouteAirlinerClass>();

            foreach (AirlinerClass.ClassType type in Enum.GetValues(typeof(AirlinerClass.ClassType)))
            {
                RouteAirlinerClass cl = new RouteAirlinerClass(type, RouteAirlinerClass.SeatingType.Reserved_Seating, farePrice);
                
                this.Classes.Add(cl);
            }

        }
      
        //returns if the route contains a specific destination
        public Boolean containsDestination(Airport destination)
        {
            return this.Destination1 == destination || this.Destination2 == destination;

        }
        //adds a route airliner class to the route
        public void addRouteAirlinerClass(RouteAirlinerClass aClass)
        {
            this.Classes.Add(aClass);
        }
        //returns the route airliner class for a specific class type
        public RouteAirlinerClass getRouteAirlinerClass(AirlinerClass.ClassType type)
        {
            RouteAirlinerClass rac = this.Classes.Find(cl => cl.Type == type);
            return this.Classes.Find((delegate(RouteAirlinerClass c) { return c.Type == type; }));
        }
        //returns the total number of cabin crew for the route based on airliner
        public int getTotalCabinCrew()
        {
            int cabinCrew = getAirliners().Max(c => ((AirlinerPassengerType)c.Airliner.Type).CabinCrew);
          
            return cabinCrew;
        }
        //returns all invoices for the route
        public Invoices getInvoices()
        {
            return this.Invoices;
        }
        //adds an invoice for a route 
        public void addRouteInvoice(Invoice invoice)
        {
            this.Invoices.addInvoice(invoice);

        }
        //sets the invoice to the route
        public void setRouteInvoice(Invoice.InvoiceType type, int year, int month, double amount)
        {
            this.Invoices.addInvoice(type, year, month, amount);
        }
        //returns invoices amount for a specific type for a route
        public double getRouteInvoiceAmount(Invoice.InvoiceType type)
        {
            return this.Invoices.getAmount(type);
           
        }
        //returns the invoices amount for a specific type for a period
        public double getRouteInvoiceAmount(Invoice.InvoiceType type, DateTime startTime, DateTime endTime)
        {
            int startYear = startTime.Year;
            int endYear = endTime.Year;

            int startMonth = startTime.Month;
            int endMonth = endTime.Month;

            int totalMonths = (endMonth - startMonth) + 12 * (endYear - startYear);

            double totalAmount = 0;

            DateTime date = new DateTime(startYear, startMonth, 1);

            for (int i = 0; i < totalMonths; i++)
            {
                if (type == Invoice.InvoiceType.Total)
                    totalAmount += this.Invoices.getAmount(date.Year, date.Month);
                else
                    totalAmount += this.Invoices.getAmount(type, date.Year, date.Month);
                
                date = date.AddMonths(1);
            }

            return totalAmount;
        }
        //returns the list of invoice types for a route
        public List<Invoice.InvoiceType> getRouteInvoiceTypes()
        {
            List<Invoice.InvoiceType> types = new List<Invoice.InvoiceType>();

            types.Add(Invoice.InvoiceType.Tickets);
            types.Add(Invoice.InvoiceType.OnFlight_Income);
            types.Add(Invoice.InvoiceType.Fees);

            types.Add(Invoice.InvoiceType.Maintenances);
            types.Add(Invoice.InvoiceType.Flight_Expenses);
            types.Add(Invoice.InvoiceType.Wages);
         
            types.Add(Invoice.InvoiceType.Total);

            return types;
        }
        //returns the balance for the route for a period
        public double getBalance(DateTime startTime, DateTime endTime)
        {
            return getRouteInvoiceAmount(Invoice.InvoiceType.Total, startTime, endTime);
        }
        //get the balance for the route 
        private double getBalance()
        {
            return getRouteInvoiceAmount(Invoice.InvoiceType.Total);
        }
        //get the degree of filling
        private double getFillingDegree()
        {
            double passengers = Convert.ToDouble(this.Statistics.getTotalValue(StatisticsTypes.GetStatisticsType("Passengers")));

            double passengerCapacity = Convert.ToDouble(this.Statistics.getTotalValue(StatisticsTypes.GetStatisticsType("Capacity")));
               
            return passengers / passengerCapacity;
        }
        //gets the income per passenger
        private double getIncomePerPassenger()
        {
            double totalPassengers = Convert.ToDouble(this.Statistics.getTotalValue(StatisticsTypes.GetStatisticsType("Passengers")));

            return getBalance() / totalPassengers;
        }
        //returns all airliners assigned to the route
        public List<FleetAirliner> getAirliners()
        {
            return (from e in this.TimeTable.Entries where e.Airliner!=null select e.Airliner).Distinct().ToList();
        }
        //returns the current airliner on the route
        public FleetAirliner getCurrentAirliner()
        {
            return getAirliners().Find(f => f.CurrentFlight != null && f.CurrentFlight.Entry.TimeTable.Route == this);
        }

    }
   
}
