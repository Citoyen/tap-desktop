﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheAirline.Model.AirlineModel;
using TheAirline.Model.AirlinerModel;

namespace TheAirline.Model.PilotModel
{
    //the class for a pilot
    public class Pilot
    {
        public enum PilotRating { A=3, B=4, C=5, D=7, E=10 } 
        public PilotRating Rating { get; set; }
        public PilotProfile Profile { get; set; }
        public Airline Airline { get; set; }
        public DateTime AirlineSignedDate { get; set; }
        public DateTime EducationTime { get; set; }
        public FleetAirliner Airliner { get; set; }
         public Pilot(PilotProfile profile, DateTime educationTime, PilotRating rating)
        {
            this.Profile = profile;
            this.EducationTime = educationTime;
            this.Rating = rating;
    
        }
        //sets the airline for a pilot
        public void setAirline(Airline airline, DateTime signDate)
        {
            this.Airline = airline;
            this.AirlineSignedDate = signDate;
        }
    }
    //the list of pilots
    public class Pilots
    {
        private static List<Pilot> pilots = new List<Pilot>();
        //adds a pilot to the list
        public static void AddPilot(Pilot pilot)
        {
            pilots.Add(pilot);
        }
        //clears the list of pilots
        public static void Clear()
        {
            pilots.Clear();
        }
        //returns all unassigned pilots
        public static List<Pilot> GetUnassignedPilots()
        {
            return pilots.FindAll(p => p.Airline == null);
        }
    }
}
