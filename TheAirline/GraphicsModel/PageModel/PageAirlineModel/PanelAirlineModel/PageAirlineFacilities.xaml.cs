﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TheAirline.Model.AirlineModel;
using TheAirline.GraphicsModel.PageModel.GeneralModel;
using TheAirline.GraphicsModel.UserControlModel.MessageBoxModel;
using TheAirline.Model.GeneralModel;

namespace TheAirline.GraphicsModel.PageModel.PageAirlineModel.PanelAirlineModel
{
    /// <summary>
    /// Interaction logic for PageAirlineFacilities.xaml
    /// </summary>
    public partial class PageAirlineFacilities : Page
    {
        private Airline Airline;
        private ListBox lbNewFacilities, lbFacilities, lbAdvertisement;
        private Dictionary<AdvertisementType.AirlineAdvertisementType, ComboBox> cbAdvertisements;
        public PageAirlineFacilities(Airline airline)
        {
            cbAdvertisements = new Dictionary<AdvertisementType.AirlineAdvertisementType, ComboBox>();

            this.Language = XmlLanguage.GetLanguage(new CultureInfo(GameObject.GetInstance().getLanguage().CultureInfo, true).IetfLanguageTag); 


            InitializeComponent();

            this.Airline = airline;

            InitializeComponent();

            StackPanel panelFacilities = new StackPanel();
            panelFacilities.Margin = new Thickness(0, 10, 50, 0);

            TextBlock txtHeaderFacilities = new TextBlock();
            txtHeaderFacilities.Margin = new Thickness(0, 0, 0, 0);
            txtHeaderFacilities.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
            txtHeaderFacilities.SetResourceReference(TextBlock.BackgroundProperty, "HeaderBackgroundBrush2");
            txtHeaderFacilities.FontWeight = FontWeights.Bold;
            txtHeaderFacilities.Text = "Airline Facilities";
            panelFacilities.Children.Add(txtHeaderFacilities);


      
            lbFacilities = new ListBox();
            lbFacilities.ItemContainerStyleSelector = new ListBoxItemStyleSelector();
            lbFacilities.ItemTemplate = this.Resources["FacilityItem"] as DataTemplate;
            lbFacilities.MaxHeight = (GraphicsHelpers.GetContentHeight() - 100) / 3;
            panelFacilities.Children.Add(lbFacilities);

         
            TextBlock txtNewAirlineFacilities = new TextBlock();
            txtNewAirlineFacilities.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
            txtNewAirlineFacilities.SetResourceReference(TextBlock.BackgroundProperty, "HeaderBackgroundBrush2");
            txtNewAirlineFacilities.FontWeight = FontWeights.Bold;
            txtNewAirlineFacilities.Text = "Purchase Facilities";
            txtNewAirlineFacilities.Margin = new Thickness(0, 5, 0, 0);
            txtNewAirlineFacilities.Visibility = this.Airline.IsHuman ? Visibility.Visible : System.Windows.Visibility.Collapsed;
            panelFacilities.Children.Add(txtNewAirlineFacilities);

            lbNewFacilities = new ListBox();
            lbNewFacilities.ItemContainerStyleSelector = new ListBoxItemStyleSelector();
            lbNewFacilities.ItemTemplate = this.Resources["FacilityNewItem"] as DataTemplate;
            lbNewFacilities.MaxHeight = (GraphicsHelpers.GetContentHeight() - 100) / 3;
            panelFacilities.Children.Add(lbNewFacilities);

        
            lbNewFacilities.Visibility = this.Airline.IsHuman ? Visibility.Visible : Visibility.Collapsed;

            TextBlock txtHeaderAdvertisement = new TextBlock();
            txtHeaderAdvertisement.Margin = new Thickness(0, 5, 0, 0);
            txtHeaderAdvertisement.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
            txtHeaderAdvertisement.SetResourceReference(TextBlock.BackgroundProperty, "HeaderBackgroundBrush2");
            txtHeaderAdvertisement.FontWeight = FontWeights.Bold;
            txtHeaderAdvertisement.Text = "Airline Advertisement";
            panelFacilities.Children.Add(txtHeaderAdvertisement);

            lbAdvertisement = new ListBox();
            lbAdvertisement.ItemContainerStyleSelector = new ListBoxItemStyleSelector();
            lbAdvertisement.MaxHeight = (GraphicsHelpers.GetContentHeight() - 100) / 3;
            lbAdvertisement.SetResourceReference(ListBox.ItemTemplateProperty, "QuickInfoItem");
            panelFacilities.Children.Add(lbAdvertisement);
      
            // chs, 2011-17-10 changed so it is only advertisement types which has been invented which are shown
            foreach (AdvertisementType.AirlineAdvertisementType type in Enum.GetValues(typeof(AdvertisementType.AirlineAdvertisementType)))
            {
                if (GameObject.GetInstance().GameTime.Year >= (int)type)
                    lbAdvertisement.Items.Add(new QuickInfoValue(type.ToString(), createAdvertisementTypeItem(type)));
            }

            Button btnSave = new Button();
            btnSave.SetResourceReference(Button.StyleProperty, "RoundedButton");
            btnSave.Height = 16;
            btnSave.Width = 200;
            btnSave.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            btnSave.Margin = new Thickness(0, 5, 0, 0);
            btnSave.Click += new RoutedEventHandler(btnSave_Click);
            btnSave.Content = "Save Advertisements";
            btnSave.SetResourceReference(Button.BackgroundProperty, "ButtonBrush");
            btnSave.Visibility = this.Airline.IsHuman ? Visibility.Visible : System.Windows.Visibility.Collapsed;
            panelFacilities.Children.Add(btnSave);


            this.Content = panelFacilities;

            showFacilities();

        }
        // chs, 2011-14-10 sets the airline advertisement items
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            foreach (AdvertisementType.AirlineAdvertisementType type in Enum.GetValues(typeof(AdvertisementType.AirlineAdvertisementType)))
            {
                ComboBox cbAdvertisement = cbAdvertisements[type];

                AdvertisementType aType = (AdvertisementType)cbAdvertisement.SelectedItem;
                this.Airline.setAirlineAdvertisement(aType);
            }
        }
        //creates an item for an advertisering type
        private UIElement createAdvertisementTypeItem(AdvertisementType.AirlineAdvertisementType type)
        {
            if (this.Airline.IsHuman)
            {
                ComboBox cbType = new ComboBox();
                cbType.ItemTemplate = this.Resources["AdvertisementItem"] as DataTemplate;
                cbType.SetResourceReference(ComboBox.StyleProperty, "ComboBoxTransparentStyle");
                cbType.Width = 200;


                cbAdvertisements.Add(type, cbType);

                foreach (AdvertisementType aType in AdvertisementTypes.GetTypes(type))
                    cbType.Items.Add(aType);

                cbType.SelectedItem = this.Airline.getAirlineAdvertisement(type);

                return cbType;
            }
            // chs, 2011-17-10 changed so it is not possible to change the advertisement type for a CPU airline
            else
            {
                return UICreator.CreateTextBlock(this.Airline.getAirlineAdvertisement(type).Name);
            }
        }
        //shows the list of facilities
        private void showFacilities()
        {
            lbFacilities.Items.Clear();
            lbNewFacilities.Items.Clear();

            foreach (AirlineFacility facility in this.Airline.Facilities)
                lbFacilities.Items.Add(new AirlineFacilityItem(this.Airline,facility));

            List<AirlineFacility> facilitiesNew = AirlineFacilities.GetFacilities();

            facilitiesNew.RemoveAll((delegate(AirlineFacility af) { return this.Airline.Facilities.Contains(af); }));

            foreach (AirlineFacility facility in facilitiesNew)
                lbNewFacilities.Items.Add(facility);


        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            AirlineFacility facility = (AirlineFacility)((Button)sender).Tag;

            if (facility.Price > GameObject.GetInstance().HumanAirline.Money)
                WPFMessageBox.Show("Not enough money", "You don't have any money to buy these facilities", WPFMessageBoxButtons.Ok);
            else
            {

                WPFMessageBoxResult result = WPFMessageBox.Show("Buy facility", string.Format("Are you sure you want to buy {0} as an airline facility?", facility.Name), WPFMessageBoxButtons.YesNo);

                if (result == WPFMessageBoxResult.Yes)
                {

                    this.Airline.addFacility(facility);

                    //this.Airline.Money -= facility.Price;

                    this.Airline.addInvoice(new Invoice(GameObject.GetInstance().GameTime, Invoice.InvoiceType.Purchases, -facility.Price));


                    showFacilities();
                }
            }

        }

        private void ButtonRemove_Click(object sender, RoutedEventArgs e)
        {
          
            AirlineFacility facility = (AirlineFacility)((Button)sender).Tag;

            WPFMessageBoxResult result = WPFMessageBox.Show("Remove facility", string.Format("Are you sure you want to remove {0} as an airline facility?", facility.Name), WPFMessageBoxButtons.YesNo);

            if (result == WPFMessageBoxResult.Yes)
            {

                this.Airline.removeFacility(facility);

                showFacilities();
            }


        }
        //the item for a facility for an airline
        private class AirlineFacilityItem
        {
            public Airline Airline { get; set; }
            public AirlineFacility Facility { get; set; }
            public AirlineFacilityItem(Airline airline, AirlineFacility facility)
            {
                this.Airline = airline;
                this.Facility = facility;
            }
        }
    }
}
