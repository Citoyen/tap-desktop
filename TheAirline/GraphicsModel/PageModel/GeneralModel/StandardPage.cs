﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Navigation;
using TheAirline.Model.GeneralModel;
using TheAirline.GraphicsModel.PageModel.GeneralModel;
using TheAirline.GraphicsModel.Converters;
using TheAirline.Model.PassengerModel;
using TheAirline.Model.AirlineModel;


namespace TheAirline.GraphicsModel.PageModel.GeneralModel
{
    public abstract class StandardPage : Page
    {
        private PageHeader PageHeader;
        private PageContent PageContent;
        private Panel panelNavigation;
        private Panel mainPanel;
        private Frame frameTopMenu, frameBottomMenu, frameInformation;

        private Button btnPrevious, btnNext, btnPause, btnStart;
        public StandardPage()
        {
            this.Width = SystemParameters.PrimaryScreenWidth;
            this.Height = SystemParameters.PrimaryScreenHeight;

            int sideMargin = 200;
            int regularMargin = 25;
            int menuHeight = 25;

            ImageBrush imgBackground = new ImageBrush();

            BitmapImage img = (BitmapImage)App.Current.Resources["BackgroundImage"];

            imgBackground.ImageSource = (BitmapImage)FindResource("BackgroundImage");
            imgBackground.Viewport = new Rect(0, 0, this.Width, this.Height);
            imgBackground.ViewportUnits = BrushMappingMode.Absolute;
            imgBackground.TileMode = TileMode.Tile;

            this.Background = imgBackground;

            mainPanel = new Canvas();

            frameTopMenu = new Frame();
            frameTopMenu.Height = menuHeight;
            frameTopMenu.Width = this.Width;
            frameTopMenu.NavigationUIVisibility = NavigationUIVisibility.Hidden;
            frameTopMenu.Navigate(new PageStandardMenuTop());

            Canvas.SetTop(frameTopMenu, 0);
            Canvas.SetLeft(frameTopMenu, 0);
            mainPanel.Children.Add(frameTopMenu);

            panelNavigation = new DockPanel();
            panelNavigation.Margin = new Thickness(0, 5, 0, 0);

            btnPrevious = new Button();
            btnPrevious.SetResourceReference(Button.StyleProperty, "RoundedButton");
            btnPrevious.Height = 24;
            btnPrevious.Width = 32;
            btnPrevious.Content = "<-";
            btnPrevious.Margin = new Thickness(2, 0, 0, 0);
            btnPrevious.SetResourceReference(Button.BackgroundProperty, "ButtonBrush");
            btnPrevious.Click += new RoutedEventHandler(btnPrevious_Click);
            panelNavigation.Children.Add(btnPrevious);

            btnNext = new Button();
            btnNext.SetResourceReference(Button.StyleProperty, "RoundedButton");
            btnNext.Height = 24;
            btnNext.Margin = new Thickness(2, 0, 0, 0);
            btnNext.Width = 32;
            btnNext.Content = "->";
            btnNext.SetResourceReference(Button.BackgroundProperty, "ButtonBrush");
            btnNext.Click += new RoutedEventHandler(btnNext_Click);
            panelNavigation.Children.Add(btnNext);

            btnPause = new Button();
            btnPause.SetResourceReference(Button.StyleProperty, "RoundedButton");
            btnPause.Height = 24;
            btnPause.Width = 32;
            btnPause.Margin = new Thickness(2, 0, 0, 0);
            btnPause.Content = "||";
            btnPause.Visibility = GameTimer.GetInstance().isPaused() ? Visibility.Collapsed : Visibility.Visible;
            btnPause.SetResourceReference(Button.BackgroundProperty, "ButtonBrush");
            btnPause.Click += new RoutedEventHandler(btnPause_Click);
            panelNavigation.Children.Add(btnPause);

            btnStart = new Button();
            btnStart.SetResourceReference(Button.StyleProperty, "RoundedButton");
            btnStart.Height = 24;
            btnStart.Width = 32;
            btnStart.Visibility = GameTimer.GetInstance().isPaused() ? Visibility.Visible : System.Windows.Visibility.Collapsed;
            btnStart.Margin = new Thickness(2, 0, 0, 0);
            btnStart.Content = ">";
            btnStart.SetResourceReference(Button.BackgroundProperty, "ButtonBrush");
            btnStart.Click += new RoutedEventHandler(btnStart_Click);
            panelNavigation.Children.Add(btnStart);

            Canvas.SetTop(panelNavigation, frameTopMenu.Height);
            Canvas.SetLeft(panelNavigation, 0);
            mainPanel.Children.Add(panelNavigation);


            Frame frameTop = new Frame();
            frameTop.Height = 75;
            frameTop.Width = this.Width - 2 * sideMargin;
            frameTop.NavigationUIVisibility = NavigationUIVisibility.Hidden;
            this.PageHeader = new PageHeader();
            frameTop.Navigate(this.PageHeader);

            Canvas.SetTop(frameTop, frameTopMenu.Height + regularMargin);
            Canvas.SetRight(frameTop, sideMargin);
            mainPanel.Children.Add(frameTop);

            Frame frameContent = new Frame();

            frameContent.SizeChanged += new SizeChangedEventHandler(frameContent_SizeChanged);
            frameContent.Height = this.Height - Canvas.GetTop(frameTop) - 150;
            frameContent.Width = this.Width - 2 * sideMargin;
            frameContent.NavigationUIVisibility = NavigationUIVisibility.Hidden;
            this.PageContent = new PageContent();
            frameContent.Navigate(this.PageContent);

            Canvas.SetTop(frameContent, Canvas.GetTop(frameTop) + frameTop.Height + regularMargin);
            Canvas.SetLeft(frameContent, sideMargin);
            mainPanel.Children.Add(frameContent);

            frameInformation = new Frame();
            frameInformation.Height = frameTopMenu.Height;
            frameInformation.Width = 100;
            frameInformation.NavigationUIVisibility = NavigationUIVisibility.Hidden;
            frameInformation.Navigate(new PageInformation());

            Canvas.SetTop(frameInformation,0);
            Canvas.SetRight(frameInformation, 0);
            mainPanel.Children.Add(frameInformation);

            frameBottomMenu = new Frame();
            frameBottomMenu.Height = menuHeight;
            frameBottomMenu.Width = this.Width;
            frameBottomMenu.NavigationUIVisibility = NavigationUIVisibility.Hidden;
            frameBottomMenu.Navigate(new PageBottomMenu());

            Canvas.SetBottom(frameBottomMenu, 0);
            Canvas.SetLeft(frameBottomMenu, 0);
            mainPanel.Children.Add(frameBottomMenu);

            this.Content = this.mainPanel;
        }

        private void frameContent_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Size s = ((Frame)sender).RenderSize;

            GraphicsHelpers.SetContentHeight(s.Height -100);
            GraphicsHelpers.SetContentWidth(s.Width / 2);
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            this.btnStart.Visibility = System.Windows.Visibility.Collapsed;
            this.btnPause.Visibility = System.Windows.Visibility.Visible;
            GameTimer.GetInstance().start();
        }

        private void btnPause_Click(object sender, RoutedEventArgs e)
        {
            this.btnStart.Visibility = System.Windows.Visibility.Visible;
            this.btnPause.Visibility = System.Windows.Visibility.Collapsed;
            GameTimer.GetInstance().pause();
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            PageNavigator.NavigateForward();
        }

        private void btnPrevious_Click(object sender, RoutedEventArgs e)
        {
            PageNavigator.NavigateBack();
        }
      
        //hides the bottom menu
        public void hideBottomMenu()
        {
            frameBottomMenu.Visibility = System.Windows.Visibility.Collapsed;
            frameInformation.Visibility = System.Windows.Visibility.Collapsed;
        }
        //sets the top menu page
        public void setTopMenu(Page page)
        {
            frameTopMenu.Navigate(page);
        }
        //shows a page
        public void showPage(Page page)
        {
            page.Content = this.mainPanel;
        }
       
        //sets the content
        protected void setContent(UIElement content)
        {
            this.PageContent.setContent(content);
        }
        //sets the content for the header panel
        protected void setHeaderContent(string text)
        {
            
            if (text != null)
                this.PageHeader.Text = text;
        }
        //hides the navigator
        public void hideNavigator()
        {
            panelNavigation.Visibility = System.Windows.Visibility.Collapsed;
        }
        //pauses the page
        public void pausePage()
        {
            this.PageContent.IsEnabled = false;
            this.panelNavigation.IsEnabled = false;
        }
        public virtual void updatePage()
        {
        }

    }
}

//the standard content page
public class StandardContentPanel : Grid
{
    private StackPanel panelLeft;
    private StackPanel panelRight;
    public enum ContentLocation { Right, Left }
    public StandardContentPanel()
    {

        for (int i = 0; i < 2; i++)
        {
            ColumnDefinition columnDef = new ColumnDefinition();
            this.ColumnDefinitions.Add(columnDef);
        }

        panelLeft = new StackPanel();

        Grid.SetColumn(panelLeft, 0);
        this.Children.Add(panelLeft);

        panelRight = new StackPanel();

        Grid.SetColumn(panelRight, 1);
        this.Children.Add(panelRight);


    }
    //sets the content page
    public void setContentPage(UIElement contentPanel, ContentLocation location)
    {
        if (location == ContentLocation.Left)
        {
            panelLeft.Children.Clear();
            panelLeft.Children.Add(contentPanel);
        }
        if (location == ContentLocation.Right)
        {
            panelRight.Children.Clear();
            panelRight.Children.Add(contentPanel);
        }
    }
}
public class PageInformation : Page
{
    private TextBlock txtGasPrice;

    public PageInformation()
    {
        this.Background = Brushes.Transparent;

        WrapPanel panelContent = new WrapPanel();
        panelContent.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
        panelContent.VerticalAlignment = System.Windows.VerticalAlignment.Center;
        panelContent.Margin = new Thickness(0, 0, 5, 0);

      
        Image imgLogo = new Image();
        imgLogo.Source = new BitmapImage(new Uri(@"/Data/images/gas-white.png", UriKind.RelativeOrAbsolute));
        imgLogo.Height = 16;
        RenderOptions.SetBitmapScalingMode(imgLogo, BitmapScalingMode.HighQuality);

        panelContent.Children.Add(imgLogo);

        txtGasPrice = UICreator.CreateTextBlock(string.Format("{0:c}/{1}.", new FuelUnitConverter().Convert(GameObject.GetInstance().FuelPrice), new StringToLanguageConverter().Convert("ltr")));
        txtGasPrice.VerticalAlignment = System.Windows.VerticalAlignment.Bottom;
        txtGasPrice.FontWeight = FontWeights.Bold;
        txtGasPrice.Margin = new Thickness(5, 0, 0, 0);

        panelContent.Children.Add(txtGasPrice);

        this.Content = panelContent;

        GameTimer.GetInstance().OnTimeChanged += new GameTimer.TimeChanged(PageInformation_OnTimeChanged);

        this.Unloaded += new RoutedEventHandler(PageInformation_Unloaded);
    }

   
    private void PageInformation_Unloaded(object sender, RoutedEventArgs e)
    {
        GameTimer.GetInstance().OnTimeChanged -= new GameTimer.TimeChanged(PageInformation_OnTimeChanged);

    }

    private void PageInformation_OnTimeChanged()
    {
       
        if (this.IsLoaded)
        {
            txtGasPrice.Text = string.Format("{0:c}/{1}.", new FuelUnitConverter().Convert(GameObject.GetInstance().FuelPrice), new StringToLanguageConverter().Convert("ltr"));
            
     

        }
     }
}
