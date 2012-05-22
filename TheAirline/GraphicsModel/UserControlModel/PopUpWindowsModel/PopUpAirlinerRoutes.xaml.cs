﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TheAirline.Model.AirlinerModel;
using TheAirline.GraphicsModel.PageModel.GeneralModel;
using TheAirline.Model.AirlinerModel.RouteModel;
using TheAirline.Model.GeneralModel;
using TheAirline.Model.AirportModel;
using TheAirline.GraphicsModel.UserControlModel.MessageBoxModel;
using TheAirline.GraphicsModel.Converters;
using System.Globalization;

namespace TheAirline.GraphicsModel.UserControlModel.PopUpWindowsModel
{
    /// <summary>
    /// Interaction logic for PopUpAirlinerRoutes.xaml
    /// </summary>
    public partial class PopUpAirlinerRoutes : PopUpWindow
    {
        private FleetAirliner Airliner;
        private ComboBox cbHour, cbMinute, cbRoute, cbDay, cbFlightCode;
        private TextBlock txtFlightTime;
        private ListBox lbFlights;
        private Dictionary<Route, List<RouteTimeTableEntry>> Entries;
        private Dictionary<Route, List<RouteTimeTableEntry>> EntriesToDelete;
        private Boolean IsEditable;
        public static object ShowPopUp(FleetAirliner airliner,Boolean isEditable)
        {
            PopUpWindow window = new PopUpAirlinerRoutes(airliner,isEditable);
            window.ShowDialog();

            return window.Selected;
        }
        public PopUpAirlinerRoutes(FleetAirliner airliner, Boolean isEditable)
        {
            this.Entries = new Dictionary<Route, List<RouteTimeTableEntry>>();
            this.EntriesToDelete = new Dictionary<Route, List<RouteTimeTableEntry>>();

            this.Airliner = airliner;
            this.IsEditable = isEditable;

            InitializeComponent();

            this.Title = this.Airliner.Name;

            this.Width = 1200;

            this.Height = this.IsEditable ? 325 : 250;

            this.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;

            StackPanel mainPanel = new StackPanel();
            mainPanel.Margin = new Thickness(10, 10, 10, 10);

            Grid grdFlights = UICreator.CreateGrid(2);
            grdFlights.ColumnDefinitions[1].Width = new GridLength(200);
            mainPanel.Children.Add(grdFlights);

            lbFlights = new ListBox();
            lbFlights.ItemContainerStyleSelector = new ListBoxItemStyleSelector();
            lbFlights.SetResourceReference(ListBox.ItemTemplateProperty, "QuickInfoItem");
            lbFlights.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;

            Grid.SetColumn(lbFlights, 0);
            grdFlights.Children.Add(lbFlights);

            ScrollViewer panelRoutes = createRoutesPanel();
         
            Grid.SetColumn(panelRoutes, 1);
            grdFlights.Children.Add(panelRoutes);
 
            if (this.IsEditable)
            {
                mainPanel.Children.Add(createNewEntryPanel());
                mainPanel.Children.Add(createButtonsPanel());
            }

            this.Content = mainPanel;

            showFlights();
        }
        //creates the panel for the routes
        private ScrollViewer createRoutesPanel()
        {
            ScrollViewer scroller = new ScrollViewer();
            scroller.MaxHeight = 200;
            scroller.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            scroller.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;

            StackPanel panelRoutes = new StackPanel();
            panelRoutes.Margin = new Thickness(5, 0, 0, 0);

            foreach (Route route in this.Airliner.Airliner.Airline.Routes.FindAll(r => this.Airliner.Airliner.Type.Range > MathHelpers.GetDistance(r.Destination1.Profile.Coordinates, r.Destination2.Profile.Coordinates)))
            {
                Border brdRoute = new Border();
                brdRoute.BorderBrush = Brushes.Black;
                brdRoute.BorderThickness = new Thickness(1);
                brdRoute.Margin = new Thickness(0, 0, 0, 2);
                brdRoute.Width = 100;
                brdRoute.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;

                ContentControl ccRoute = new ContentControl();
                ccRoute.ContentTemplate = this.Resources["RouteItem"] as DataTemplate;
                ccRoute.Content = route;

                brdRoute.Child = ccRoute;

                panelRoutes.Children.Add(brdRoute);
            }

            scroller.Content = panelRoutes;

            return scroller;

        }
        //creates the buttons panel
        private WrapPanel createButtonsPanel()
        {
            WrapPanel buttonsPanel = new WrapPanel();
            buttonsPanel.Margin = new Thickness(0, 10, 0, 0);

            Button btnOk = new Button();
            btnOk.Uid = "100";
            btnOk.SetResourceReference(Button.StyleProperty, "RoundedButton");
            btnOk.Height = Double.NaN;
            btnOk.Width = Double.NaN;
            btnOk.Content = Translator.GetInstance().GetString("General", btnOk.Uid);
            btnOk.Click += new RoutedEventHandler(btnOk_Click);
            btnOk.SetResourceReference(Button.BackgroundProperty, "ButtonBrush");

            buttonsPanel.Children.Add(btnOk);

            Button btnCancel = new Button();
            btnCancel.Uid = "101";
            btnCancel.SetResourceReference(Button.StyleProperty, "RoundedButton");
            btnCancel.Height = Double.NaN;
            btnCancel.Margin = new Thickness(5, 0, 0, 0);
            btnCancel.Width = Double.NaN;
            btnCancel.Click += new RoutedEventHandler(btnCancel_Click);
            btnCancel.Content = Translator.GetInstance().GetString("General", btnCancel.Uid);
            btnCancel.SetResourceReference(Button.BackgroundProperty, "ButtonBrush");

            buttonsPanel.Children.Add(btnCancel);

            Button btnUndo = new Button();
            btnUndo.Uid = "103";
            btnUndo.SetResourceReference(Button.StyleProperty, "RoundedButton");
            btnUndo.Height = Double.NaN;
            btnUndo.Width = Double.NaN;
            btnUndo.Margin = new Thickness(5, 0, 0, 0);
            btnUndo.Content = Translator.GetInstance().GetString("General", btnUndo.Uid);
            btnUndo.SetResourceReference(Button.BackgroundProperty, "ButtonBrush");
            btnUndo.Click += new RoutedEventHandler(btnUndo_Click);

            buttonsPanel.Children.Add(btnUndo);

            Button btnClear = new Button();
            btnClear.Uid = "108";
            btnClear.SetResourceReference(Button.StyleProperty, "RoundedButton");
            btnClear.Height = Double.NaN;
            btnClear.Width = Double.NaN;
            btnClear.Margin = new Thickness(5, 0, 0, 0);
            btnClear.Content = Translator.GetInstance().GetString("General", btnClear.Uid);
            btnClear.Click += new RoutedEventHandler(btnClear_Click);
            btnClear.SetResourceReference(Button.BackgroundProperty, "ButtonBrush");

            buttonsPanel.Children.Add(btnClear);

            return buttonsPanel;
        }
        //creates the panel for adding a new entry
        private StackPanel createNewEntryPanel()
        {
            StackPanel newEntryPanel = new StackPanel();
            newEntryPanel.Margin = new Thickness(0, 10, 0, 0);

            WrapPanel entryPanel = new WrapPanel();

            newEntryPanel.Children.Add(entryPanel);

            cbRoute = new ComboBox();

            cbRoute.ItemTemplate = this.Resources["RouteItem"] as DataTemplate;
            cbRoute.SetResourceReference(ComboBox.StyleProperty, "ComboBoxTransparentStyle");
            cbRoute.SelectionChanged += new SelectionChangedEventHandler(cbRoute_SelectionChanged);
        
            foreach (Route route in this.Airliner.Airliner.Airline.Routes.FindAll(r=>this.Airliner.Airliner.Type.Range>MathHelpers.GetDistance(r.Destination1.Profile.Coordinates,r.Destination2.Profile.Coordinates)))
            {
                ComboBoxItem item1 = new ComboBoxItem();
                item1.Tag = new KeyValuePair<Route, Airport>(route, route.Destination2);
                item1.Content = string.Format("{0}-{1}", route.Destination1.Profile.IATACode, route.Destination2.Profile.IATACode);
                cbRoute.Items.Add(item1);

                ComboBoxItem item2 = new ComboBoxItem();
                item2.Tag = new KeyValuePair<Route, Airport>(route, route.Destination1);
                item2.Content = string.Format("{0}-{1}", route.Destination2.Profile.IATACode, route.Destination1.Profile.IATACode);
                cbRoute.Items.Add(item2);
            }

          
            entryPanel.Children.Add(cbRoute);

            cbDay = new ComboBox();
            cbDay.SetResourceReference(ComboBox.StyleProperty, "ComboBoxTransparentStyle");
            cbDay.Width = 100;
            cbDay.Margin = new Thickness(10, 0, 0, 0);
            cbDay.Items.Add("Daily");

            foreach (DayOfWeek day in Enum.GetValues(typeof(DayOfWeek)))
                cbDay.Items.Add(day);

            cbDay.SelectedIndex = 0;

            entryPanel.Children.Add(cbDay);

            cbHour = new ComboBox();
            cbHour.SetResourceReference(ComboBox.StyleProperty, "ComboBoxTransparentStyle");
            cbHour.ItemTemplate = this.Resources["HourItem"] as DataTemplate;//ItemStringFormat = "{0:D2}";
            cbHour.Margin = new Thickness(5, 0, 0, 0);

            for (int i = 0; i < 24; i++)
                cbHour.Items.Add(i);

            cbHour.SelectedIndex = 0;

            entryPanel.Children.Add(cbHour);

            entryPanel.Children.Add(UICreator.CreateTextBlock(":"));

            cbMinute = new ComboBox();
            cbMinute.SetResourceReference(ComboBox.StyleProperty, "ComboBoxTransparentStyle");
            cbMinute.ItemStringFormat = "{0:D2}";

        
            for (int i = 0; i < 60; i += 15)
                cbMinute.Items.Add(i);

            cbMinute.SelectedIndex = 0;

            entryPanel.Children.Add(cbMinute);

            cbFlightCode = new ComboBox();
            cbFlightCode.SetResourceReference(ComboBox.StyleProperty, "ComboBoxTransparentStyle");

            foreach (string flightCode in this.Airliner.Airliner.Airline.getFlightCodes())
                cbFlightCode.Items.Add(flightCode);

            cbFlightCode.SelectedIndex = 0;

            entryPanel.Children.Add(cbFlightCode);

            Button btnAdd = new Button();
            btnAdd.Uid = "104";
            btnAdd.SetResourceReference(Button.StyleProperty, "RoundedButton");
            btnAdd.Height = Double.NaN;
            btnAdd.Width = Double.NaN;
            btnAdd.Click += new RoutedEventHandler(btnAdd_Click);
            btnAdd.Margin = new Thickness(5, 0, 0, 0);
            btnAdd.Content = Translator.GetInstance().GetString("General", btnAdd.Uid);
            btnAdd.IsEnabled = cbRoute.Items.Count > 0;
            btnAdd.VerticalAlignment = System.Windows.VerticalAlignment.Bottom;
            btnAdd.SetResourceReference(Button.BackgroundProperty, "ButtonBrush");


            entryPanel.Children.Add(btnAdd);

            txtFlightTime = new TextBlock();
            txtFlightTime.Text = "Flight time: ";

            newEntryPanel.Children.Add(txtFlightTime);
          
            cbRoute.SelectedIndex = 0;

            return newEntryPanel;
        }


        //creates the time indicator header
        private WrapPanel createTimeHeaderPanel()
        {
            string format = System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.PMDesignator == "" ? "HH" : "hh";
            int hourFactor = 1;

            WrapPanel panelHeader = new WrapPanel();
            for (int i = 0; i < 24; i++)
            {
                Border brdHour = new Border();
                brdHour.BorderThickness = new Thickness(1, 0, 1, 0);
                brdHour.BorderBrush = Brushes.Black;
                
                TextBlock txtHour = new TextBlock();
                txtHour.Text = string.Format("{0}-{1}", new DateTime(2000, 1, 1, i, 0, 0).ToString(format), new DateTime(2000, 1, 1, i+1==24 ? 0 : i+1, 0, 0).ToString(format));
                txtHour.FontWeight = FontWeights.Bold;
                txtHour.Width = (60 / hourFactor) - 2;
                txtHour.TextAlignment = TextAlignment.Center;

                brdHour.Child = txtHour;
                panelHeader.Children.Add(brdHour);
            }

            return panelHeader;
        }
        //creates the panel for a route for a day
        private Border createRoutePanel(DayOfWeek day)
        {
            int hourFactor = 1;

            Border brdDay = new Border();
            brdDay.BorderThickness = new Thickness(1, 1, 1, 1);
            brdDay.BorderBrush = Brushes.Black;

            Canvas cnvFlights = new Canvas();
            cnvFlights.Width = 24 * 60 / hourFactor;
            cnvFlights.Height = 20;
            brdDay.Child = cnvFlights;

            List<RouteTimeTableEntry> entries = this.Airliner.Routes.SelectMany(r => r.TimeTable.Entries.FindAll(e => e.Airliner == this.Airliner && e.Day == day)).ToList(); //&& e.Time.Hours<12)).ToList();
            entries.AddRange(this.Entries.Keys.SelectMany(r => this.Entries[r].FindAll(e => e.Day == day)));
            entries.RemoveAll(e => this.EntriesToDelete.Keys.SelectMany(r => this.EntriesToDelete[r]).ToList().Find(te => te == e) == e);

            foreach (RouteTimeTableEntry e in entries)
            {
                double maxTime = new TimeSpan(24, 0, 0).Subtract(e.Time).TotalMinutes;

                TimeSpan flightTime = MathHelpers.GetFlightTime(e.TimeTable.Route.Destination1.Profile.Coordinates, e.TimeTable.Route.Destination2.Profile.Coordinates, this.Airliner.Airliner.Type);

                ContentControl ccFlight = new ContentControl();
                ccFlight.ContentTemplate = this.Resources["FlightItem"] as DataTemplate;
                ccFlight.Content = new KeyValuePair<double, RouteTimeTableEntry>(Math.Min(flightTime.TotalMinutes / hourFactor, maxTime / hourFactor), e);
                int hours = e.Time.Hours;

                Canvas.SetLeft(ccFlight, 60 * hours / hourFactor + e.Time.Minutes / hourFactor);

                cnvFlights.Children.Add(ccFlight);
            }

            List<RouteTimeTableEntry> dayBeforeEntries = this.Airliner.Routes.SelectMany(r => r.TimeTable.Entries.FindAll(e => e.Airliner == this.Airliner && (e.Day == day - 1 || ((int)day) == 0 && ((int)e.Day) == 6))).ToList();
            dayBeforeEntries.AddRange(this.Entries.Keys.SelectMany(r => this.Entries[r].FindAll(e => e.Day == day - 1 || ((int)day) == 0 && ((int)e.Day) == 6)));
            dayBeforeEntries.RemoveAll(e => this.EntriesToDelete.Keys.SelectMany(r => this.EntriesToDelete[r]).ToList().Find(te => te == e) == e);

            foreach (RouteTimeTableEntry e in dayBeforeEntries)
            {
                TimeSpan flightTime = MathHelpers.GetFlightTime(e.TimeTable.Route.Destination1.Profile.Coordinates, e.TimeTable.Route.Destination2.Profile.Coordinates, this.Airliner.Airliner.Type);

                TimeSpan endTime = e.Time.Add(flightTime);
                if (endTime.Days == 1)
                {
                    ContentControl ccFlight = new ContentControl();
                    ccFlight.ContentTemplate = this.Resources["FlightItem"] as DataTemplate;
                    ccFlight.Content = new KeyValuePair<double, RouteTimeTableEntry>(endTime.Subtract(new TimeSpan(1, 0, 0, 0)).TotalMinutes / 2, e);

                    Canvas.SetLeft(ccFlight, 0);

                    cnvFlights.Children.Add(ccFlight);
                }
            }

            return brdDay;
        }
        //checks if an entry is valid
        private Boolean isRouteEntryValid(RouteTimeTableEntry entry, Boolean showMessageBoxOnError)
        {
           

            TimeSpan flightTime = MathHelpers.GetFlightTime(entry.DepartureAirport.Profile.Coordinates, entry.Destination.Airport.Profile.Coordinates, this.Airliner.Airliner.Type.CruisingSpeed).Add(RouteTimeTable.MinTimeBetweenFlights);

            TimeSpan startTime = new TimeSpan((int)entry.Day, entry.Time.Hours, entry.Time.Minutes, entry.Time.Seconds);

            TimeSpan endTime = startTime.Add(flightTime);
            if (endTime.Days == 7)
                endTime = new TimeSpan(0, endTime.Hours, endTime.Minutes, endTime.Seconds);

            List<RouteTimeTableEntry> airlinerEntries = this.Airliner.Routes.SelectMany(r => r.TimeTable.Entries.FindAll(e=>e.Airliner==this.Airliner)).ToList();
            airlinerEntries.AddRange(this.Entries.Keys.SelectMany(r => this.Entries[r]));
            airlinerEntries.RemoveAll(e=>this.EntriesToDelete.Keys.SelectMany(r => this.Entries[r]).Contains(e));
            airlinerEntries.AddRange(entry.TimeTable.Entries.FindAll(e => e.Destination.Airport == entry.Destination.Airport));
                   
            foreach (RouteTimeTableEntry e in airlinerEntries)
            {
                TimeSpan eStartTime = new TimeSpan((int)e.Day, e.Time.Hours, e.Time.Minutes, e.Time.Seconds);

                TimeSpan eFlightTime = MathHelpers.GetFlightTime(e.DepartureAirport.Profile.Coordinates, e.Destination.Airport.Profile.Coordinates, this.Airliner.Airliner.Type.CruisingSpeed).Add(RouteTimeTable.MinTimeBetweenFlights);

                TimeSpan eEndTime = eStartTime.Add(eFlightTime);

                double diffStartTime = Math.Abs(eStartTime.Subtract(startTime).TotalMinutes);
                double diffEndTime = Math.Abs(eEndTime.Subtract(endTime).TotalMinutes);
                          
                if (eEndTime.Days == 7)
                    eEndTime = new TimeSpan(0, endTime.Hours, endTime.Minutes, endTime.Seconds);

                if ((eStartTime >= startTime && endTime >= eStartTime) || (eEndTime >= startTime && endTime >= eEndTime) || (endTime >= eStartTime && eEndTime >= endTime) || (startTime >= eStartTime && eEndTime >= startTime))
                {
                    if (e.Airliner == this.Airliner || diffEndTime < 60 || diffStartTime < 60)
                    {
                        if (showMessageBoxOnError)
                        {
                            if (e.Airliner == this.Airliner)
                                WPFMessageBox.Show("Already in route", "The plane is already in route at that time", WPFMessageBoxButtons.Ok);
                            else
                            {
                                WPFMessageBox.Show("Already in route", "The route do already has an airliner at that time", WPFMessageBoxButtons.Ok);
                            }
                        }

                        return false;
                    }
                }
            }
            double minutesPerWeek = 7 * 24*60;

            RouteTimeTableEntry nextEntry = getNextEntry(entry);

            RouteTimeTableEntry previousEntry = getPreviousEntry(entry);

            if (nextEntry != null && previousEntry != null)
            {
             
                TimeSpan flightTimeNext = MathHelpers.GetFlightTime(entry.Destination.Airport.Profile.Coordinates, nextEntry.DepartureAirport.Profile.Coordinates, this.Airliner.Airliner.Type.CruisingSpeed).Add(RouteTimeTable.MinTimeBetweenFlights);
                TimeSpan flightTimePrevious = MathHelpers.GetFlightTime(entry.DepartureAirport.Profile.Coordinates, previousEntry.Destination.Airport.Profile.Coordinates, this.Airliner.Airliner.Type.CruisingSpeed).Add(RouteTimeTable.MinTimeBetweenFlights);
        

                TimeSpan prevDate = new TimeSpan((int)previousEntry.Day,previousEntry.Time.Hours,previousEntry.Time.Minutes,previousEntry.Time.Seconds);
                TimeSpan nextDate = new TimeSpan((int)nextEntry.Day,nextEntry.Time.Hours,nextEntry.Time.Minutes,nextEntry.Time.Seconds);
                TimeSpan currentDate = new TimeSpan((int)entry.Day, entry.Time.Hours, entry.Time.Minutes, entry.Time.Seconds);

                        
                double timeToNext = currentDate.Subtract(nextDate).TotalMinutes > 0 ? minutesPerWeek - currentDate.Subtract(nextDate).TotalMinutes : Math.Abs(currentDate.Subtract(nextDate).TotalMinutes);
                double timeFromPrev = prevDate.Subtract(currentDate).TotalMinutes > 0 ? minutesPerWeek - prevDate.Subtract(currentDate).TotalMinutes : Math.Abs(prevDate.Subtract(currentDate).TotalMinutes);

                if (timeFromPrev > previousEntry.getFlightTime().TotalMinutes + flightTimePrevious.TotalMinutes && timeToNext > entry.getFlightTime().TotalMinutes + flightTimeNext.TotalMinutes)
                    return true;
                else
                {
                    if (showMessageBoxOnError)
                        WPFMessageBox.Show("Not matching", "The plane can't reach the destination from this entry", WPFMessageBoxButtons.Ok);

                    return false;
                }
            }
            else
                return true;

        }
        //returns the previous entry before a specific entry
        private RouteTimeTableEntry getPreviousEntry(RouteTimeTableEntry entry)
        {
            TimeSpan tsEntry = new TimeSpan((int)entry.Day, entry.Time.Hours, entry.Time.Minutes, entry.Time.Seconds);
            DayOfWeek eDay = entry.Day;

            int counter = 0;

            while (counter < 7)
            {

                List<RouteTimeTableEntry> entries = this.Airliner.Routes.SelectMany(r => r.TimeTable.Entries.FindAll(e => e.Airliner == this.Airliner && e.Day == eDay)).ToList();
                entries.AddRange(this.Entries.Keys.SelectMany(r => this.Entries[r].FindAll(e => e.Day == eDay)));
                entries.RemoveAll(e => this.EntriesToDelete.Keys.SelectMany(r => this.EntriesToDelete[r]).ToList().Find(te => te == e) == e);

                entries = (from e in entries orderby e.Time descending select e).ToList();

                foreach (RouteTimeTableEntry dEntry in entries)
                {
                    TimeSpan ts = new TimeSpan((int)eDay, dEntry.Time.Hours, dEntry.Time.Minutes, dEntry.Time.Seconds);
                    if (ts < tsEntry)
                        return dEntry;

                }
                counter++;

                eDay--;

                if (((int)eDay) == -1)
                {
                    eDay = (DayOfWeek)6;
                    tsEntry = new TimeSpan(7, tsEntry.Hours, tsEntry.Minutes, tsEntry.Seconds);
                }

            }

            return null;

        }
        //returns the next entry after a specific entry
        private RouteTimeTableEntry getNextEntry(RouteTimeTableEntry entry)
        {

            DayOfWeek day = entry.Day;

            int counter = 0;

            while (counter < 7)
            {

                List<RouteTimeTableEntry> entries = this.Airliner.Routes.SelectMany(r => r.TimeTable.Entries.FindAll(e => e.Airliner == this.Airliner && e.Day == day)).ToList();
                entries.AddRange(this.Entries.Keys.SelectMany(r => this.Entries[r].FindAll(e => e.Day == day)));
                entries.RemoveAll(e=>this.EntriesToDelete.Keys.SelectMany(r=>this.EntriesToDelete[r]).ToList().Find(te=>te == e)==e);

                entries = (from e in entries orderby e.Time select e).ToList();


                foreach (RouteTimeTableEntry dEntry in entries)
                {
                    if (!((dEntry.Day == entry.Day && dEntry.Time <= entry.Time)))
                        return dEntry;
                }
                day++;

                if (day == (DayOfWeek)7)
                    day = (DayOfWeek)0;

                counter++;

            }

            return null;

        }

        //shows the flights for the airliner
        private void showFlights()
        {
            lbFlights.Items.Clear();

            lbFlights.Items.Add(new QuickInfoValue("Day", createTimeHeaderPanel()));

            foreach (DayOfWeek day in Enum.GetValues(typeof(DayOfWeek)))
            {
                lbFlights.Items.Add(new QuickInfoValue(day.ToString(), createRoutePanel(day)));

            }
        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxItem item = (ComboBoxItem)cbRoute.SelectedItem;

            Route route = ((KeyValuePair<Route, Airport>)item.Tag).Key;

            Airport airport = ((KeyValuePair<Route, Airport>)item.Tag).Value;

            TimeSpan time = new TimeSpan((int)cbHour.SelectedItem, (int)cbMinute.SelectedItem, 0);

            string day = cbDay.SelectedItem.ToString();

            if (!this.Entries.ContainsKey(route))
                this.Entries.Add(route, new List<RouteTimeTableEntry>());

            string flightCode = cbFlightCode.SelectedItem.ToString();

          
            if (day == "Daily")
            {
                Boolean showMessageBoxOnError = true;
                foreach (DayOfWeek dayOfWeek in Enum.GetValues(typeof(DayOfWeek)))
                {
                    RouteTimeTableEntry entry = new RouteTimeTableEntry(route.TimeTable, dayOfWeek, time, new RouteEntryDestination(airport, flightCode));
                    entry.Airliner = this.Airliner;
                    if (isRouteEntryValid(entry, showMessageBoxOnError))
                        this.Entries[route].Add(entry);
                    else
                        showMessageBoxOnError = false;
                }

            }
            else
            {
                RouteTimeTableEntry entry = new RouteTimeTableEntry(route.TimeTable, (DayOfWeek)cbDay.SelectedItem, time, new RouteEntryDestination(airport,flightCode));
                entry.Airliner = this.Airliner;
                if (isRouteEntryValid(entry,true))
                    this.Entries[route].Add(entry);

            }



            showFlights();
        }
        private void btnUndo_Click(object sender, RoutedEventArgs e)
        {
            this.Entries.Clear();

            this.EntriesToDelete.Clear();

            showFlights();
        }
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            this.Entries.Clear();

            foreach (Route route in this.Airliner.Routes)
            {
                foreach (RouteTimeTableEntry entry in route.TimeTable.Entries)
                {
                    if (!this.EntriesToDelete.ContainsKey(route))
                    {
                        this.EntriesToDelete.Add(route, new List<RouteTimeTableEntry>());
                        this.EntriesToDelete[route].Add(entry);
                    }
                    else
                    {
                        if (!this.EntriesToDelete[route].Contains(entry))
                            this.EntriesToDelete[route].Add(entry);
                    }
                }
                    
            }

            showFlights();

        }
        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            foreach (Route route in this.Entries.Keys)
            {
                foreach (RouteTimeTableEntry entry in this.Entries[route])
                    route.TimeTable.addEntry(entry);

                if (!this.Airliner.Routes.Contains(route))
                    this.Airliner.addRoute(route);
            }
            foreach (Route route in this.EntriesToDelete.Keys)
            {
                foreach (RouteTimeTableEntry entry in this.EntriesToDelete[route])
                    route.TimeTable.removeEntry(entry);

                if (route.TimeTable.getEntries(this.Airliner).Count == 0)
                    this.Airliner.removeRoute(route);
           
            }

            this.Close();
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void cbRoute_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem item = (ComboBoxItem)cbRoute.SelectedItem;

            Route route = ((KeyValuePair<Route, Airport>)item.Tag).Key;

            Airport airport = ((KeyValuePair<Route, Airport>)item.Tag).Value;

            TimeSpan flightTime = MathHelpers.GetFlightTime(route.Destination1.Profile.Coordinates, route.Destination2.Profile.Coordinates, this.Airliner.Airliner.Type);

            txtFlightTime.Text = string.Format("Flight time: {0:hh\\:mm}", flightTime);

            cbFlightCode.Items.Clear();

            List<string> codes = new List<string>(this.Airliner.Airliner.Airline.getFlightCodes());
            codes.AddRange((from entry in this.Entries.Keys.SelectMany(r => this.Entries[r]) select entry.Destination.FlightCode).Distinct());

            foreach (string eEntry in (from entry in this.Entries.Keys.SelectMany(r => this.Entries[r]) select entry.Destination.FlightCode).Distinct())
                codes.Remove(eEntry);
      
            foreach (string flightCode in codes)
                cbFlightCode.Items.Add(flightCode);

            string tFlightCode = null;

            if (route.TimeTable.Entries.Find(entry => entry.Destination.Airport == airport && entry.Airliner == Airliner) != null)
                tFlightCode = route.TimeTable.Entries.Find(entry => entry.Destination.Airport == airport && entry.Airliner == Airliner).Destination.FlightCode;
            else if (this.Entries.ContainsKey(route) && this.Entries[route].Find(entry => entry.Destination.Airport == airport) != null)
                tFlightCode = this.Entries[route].Find(entry => entry.Destination.Airport == airport).Destination.FlightCode;

            if (tFlightCode != null)
            {
                cbFlightCode.Items.Add(tFlightCode);
                cbFlightCode.SelectedItem = tFlightCode;
            }
            else
                cbFlightCode.SelectedIndex = 0;

    

        }

        private void txtFlightEntry_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.RightButton == MouseButtonState.Pressed && this.IsEditable)
            {
                RouteTimeTableEntry entry = (RouteTimeTableEntry)((TextBlock)sender).Tag;

                if (this.Entries.ContainsKey(entry.TimeTable.Route) && this.Entries[entry.TimeTable.Route].Find(te => te == entry) != null)
                {
                    this.Entries[entry.TimeTable.Route].Remove(entry);
                }
                else
                {
                    if (!this.EntriesToDelete.ContainsKey(entry.TimeTable.Route))
                        this.EntriesToDelete.Add(entry.TimeTable.Route, new List<RouteTimeTableEntry>());

                    this.EntriesToDelete[entry.TimeTable.Route].Add(entry);
                }

                showFlights();
            }
        }

    }
    //the converter for getting the color for a route
    public class RouteColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Route route = (Route)value;
   
            Guid g2 = new Guid(route.Id);

            byte[] bytes = g2.ToByteArray();

            byte red = bytes[0];
            byte green = bytes[1];
            byte blue = bytes[2];

            SolidColorBrush brush = new SolidColorBrush(Color.FromRgb(red, green, blue));
            brush.Opacity = 0.2;
            return brush;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }
    //the converter for getting the local time of a flight
    public class FlightLocalTimeConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            RouteTimeTableEntry e = (RouteTimeTableEntry)value;
            
            TimeSpan flightTime = MathHelpers.GetFlightTime(e.TimeTable.Route.Destination1.Profile.Coordinates, e.TimeTable.Route.Destination2.Profile.Coordinates, e.Airliner.Airliner.Type);

            Airport airport = parameter.ToString() == "D" ? e.DepartureAirport : e.Destination.Airport;
            
            TimeSpan time = parameter.ToString() == "D" ? e.Time : e.Time.Add(flightTime);

            return new TimeSpanConverter().Convert(MathHelpers.ConvertTimeSpanToLocalTime(time, airport.Profile.TimeZone));

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    //the converter for the hour
    public class HourConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            int hour = (int)value;

            if (hour == 24)
                hour = 0;

            if (System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.PMDesignator == "")

                return new DateTime(2000, 1, 1, hour, 0, 0).ToString("HH");
            else
                return new DateTime(2000, 1, 1, hour, 0, 0).ToString("hh tt");


        }
        public object Convert(object value)
        {
            return this.Convert(value, null, null, null);
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    //the converter for getting the end time for a route entry
    public class RouteEntryEndTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {

            RouteTimeTableEntry e = (RouteTimeTableEntry)value;
            TimeSpan flightTime = MathHelpers.GetFlightTime(e.TimeTable.Route.Destination1.Profile.Coordinates, e.TimeTable.Route.Destination2.Profile.Coordinates, e.Airliner.Airliner.Type);

            return new TimeSpanConverter().Convert(e.Time.Add(flightTime));
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }

}