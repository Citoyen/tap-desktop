﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using TheAirline.Model.GeneralModel;
using TheAirline.Model.GeneralModel.CountryModel;

namespace TheAirline.GUIModel.HelpersModel
{
    //the class for the different converters
    //the converter for a string to a brush
    public class StringToBrushConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string color = (string)value;
            try
            {
                TypeConverter colorConverter = new ColorConverter();
                Color c = (Color)colorConverter.ConvertFromString(color);

                return new SolidColorBrush(c);
            }
            catch
            {
                return Brushes.White;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    //the converter for airline color
    public class AirlineColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string color = (string)value;

            try
            {
                TypeConverter colorConverter = new ColorConverter();
                Color baseColor = (Color)colorConverter.ConvertFromString(color);

                Color c2 = Color.FromArgb(25, baseColor.R, baseColor.G, baseColor.B);

                LinearGradientBrush colorBrush = new LinearGradientBrush();
                colorBrush.StartPoint = new Point(0, 0);
                colorBrush.EndPoint = new Point(0, 1);
                colorBrush.GradientStops.Add(new GradientStop(c2, 0.15));
                colorBrush.GradientStops.Add(new GradientStop(baseColor, 0.85));
                colorBrush.GradientStops.Add(new GradientStop(c2, 1));

                return colorBrush;
            }
            catch (Exception)
            {
                return Brushes.DarkRed;
            }

        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    //the converter for a value to the current currency
    public class ValueCurrencyConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                double v = Double.Parse(value.ToString());


                if (GameObject.GetInstance().CurrencyCountry == null || Double.IsInfinity(v))
                {
                    return string.Format("{0:C}", value);
                }
                else
                {
                    CountryCurrency currency = GameObject.GetInstance().CurrencyCountry.getCurrency(GameObject.GetInstance().GameTime);


                    if (currency == null)
                    {
                        if (Settings.GetInstance().CurrencyShorten)
                        {
                            if (v >= 1000000000 || v <= -1000000000)
                                return string.Format("{0:C} {1}", v / 1000000000, Translator.GetInstance().GetString("General", "2001"));
                            if (v >= 1000000 || v <= -1000000)
                                return string.Format("{0:C} {1}", v / 1000000, Translator.GetInstance().GetString("General", "2000"));
                            return string.Format("{0:C}", value);
                        }
                        else
                            return string.Format("{0:C}", value);
                    }
                    else
                    {
                        double currencyValue = v * currency.Rate;

                        if (Settings.GetInstance().CurrencyShorten)
                        {
                            if (currencyValue >= 1000000 || currencyValue <= -1000000)
                            {
                                double sValue = currencyValue / 1000000;
                                string sFormat = Translator.GetInstance().GetString("General", "2000");

                                if (currencyValue >= 1000000000 || currencyValue <= -1000000000)
                                {
                                    sValue = currencyValue / 1000000000;
                                    sFormat = Translator.GetInstance().GetString("General", "2001");
                                }

                                if (currency.Position == CountryCurrency.CurrencyPosition.Right)
                                    return string.Format("{0:#,0.00} {2} {1}", sValue, currency.CurrencySymbol, sFormat);
                                else
                                    return string.Format("{1}{0:#,0.00} {2}", sValue, currency.CurrencySymbol, sFormat);

                            }
                            else
                            {
                                if (currency.Position == CountryCurrency.CurrencyPosition.Right)
                                    return string.Format("{0:#,0.00} {1}", currencyValue, currency.CurrencySymbol);
                                else
                                    return string.Format("{1}{0:#,0.00}", currencyValue, currency.CurrencySymbol);

                            }
                        }
                        else
                        {

                            if (currency.Position == CountryCurrency.CurrencyPosition.Right)
                                return string.Format("{0:#,0.00} {1}", currencyValue, currency.CurrencySymbol);
                            else
                                return string.Format("{1}{0:#,0.00}", currencyValue, currency.CurrencySymbol);
                        }
                    }
                }
            }
            catch (Exception)
            {
                return string.Format("{0:C}", value);

            }
        }
        public object Convert(object value)
        {
            return this.Convert(value, null, null, null);
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
   
    //the converter for a boolean to visibility
    public class BooleanToVisibility : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Boolean negation = false;
            if (parameter != null && (parameter.ToString() == "!"))
                negation = true;

            Visibility rv = Visibility.Collapsed;
            try
            {
                var x = bool.Parse(value.ToString());

                if (negation) x = !x;
                if (x)
                {
                    rv = Visibility.Visible;
                }
                else
                {
                    rv = Visibility.Collapsed;
                }
            }
            catch (Exception)
            {
            }
            return rv;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }
    }
    //the converter for translations
    public class TranslatorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                string[] values = parameter.ToString().Split(' ');

                return Translator.GetInstance().GetString(values[0], values[1]);
            }
            catch (Exception e)
            {
                return "";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    //the converter for a text with _ to text without
    public class TextUnderscoreConverter : IValueConverter
    {
        public object Convert(object value)
        {
            return this.Convert(value, null, null, null);
        }
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value.ToString().Replace('_', ' ');
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
