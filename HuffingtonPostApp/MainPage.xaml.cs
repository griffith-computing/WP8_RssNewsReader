using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using HuffingtonPostApp.Resources;
using System.Xml.Linq;
using Windows.Phone.Speech.Synthesis;
using Windows.Foundation;
using HuffingtonPostApp.services;
using HuffingtonPostApp.models;
using HuffingtonPostApp.events;
using HuffingtonPostApp.views;
using Microsoft.Phone.Scheduler;

namespace HuffingtonPostApp
{
    public partial class MainPage : PhoneApplicationPage
    {
        protected String rssFeedUrl = "http://feeds.bbci.co.uk/news/rss.xml";
        protected RssArticleService service = new RssArticleService();
        protected PeriodicTask task;

        // Constructor
        public MainPage()
        {
            InitializeComponent();

            service.articlesObtained += new EventHandler<RssArticleEventArgs>(handleArticlesObtained);
            service.getFeedArticles(rssFeedUrl);
        }

        private void handleArticlesObtained(object sender, RssArticleEventArgs e)
        {
            List<ArticleVo> articles = e.articles;

            list1.ItemsSource = articles;
            list1.SelectionChanged += new SelectionChangedEventHandler(handleListSelectionChanged);  
        }

        private void handleListSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            NavigationService.Navigated += new NavigatedEventHandler(handleNavigatedToArticleView);
            NavigationService.Navigate(new Uri("/views/ArticleView.xaml", UriKind.Relative));
        }

        private void handleNavigatedToArticleView(object sender, NavigationEventArgs e)
        {
            NavigationService.Navigated -= new NavigatedEventHandler(handleNavigatedToArticleView);
            ArticleView view = (ArticleView)e.Content;
            view.data = (ArticleVo)list1.SelectedItem;
        }

        /*private void handleDownloadStringComplete(object sender, DownloadStringCompletedEventArgs e)
        {
            if (Microsoft.Phone.Net.NetworkInformation.DeviceNetworkInformation.IsNetworkAvailable)
            {
                XNamespace media = XNamespace.Get("http://search.yahoo.com/mrss/");
                titles = XElement.Parse(e.Result).Descendants("title")
                    .Where(m => m.Parent.Name == "item")
                    .Select(m => m.Value)
                    .ToArray();

                descriptions = XElement.Parse(e.Result).Descendants("description")
                    .Where(m => m.Parent.Name == "item")
                    .Select(m => m.Value)
                    .ToArray();

                images = XElement.Parse(e.Result).Descendants(media + "thumbnail")
                    .Where(m => m.Parent.Name == "item")
                    .Select(m => m.Attribute("url").Value)
                    .ToArray();

                
                UpdatePrimaryTile(titles[0], images[0]);
                
            }
            else
            {
                MessageBox.Show("No network is available...");
            }
        }*/

        // TODO: figure out how to update the tile every minute...
        public static void UpdatePrimaryTile(string content, string image)
        {
            FlipTileData primaryTileData = new FlipTileData();
            primaryTileData.BackContent = content;
            primaryTileData.BackgroundImage = new Uri(image, UriKind.Absolute);

            ShellTile primaryTile = ShellTile.ActiveTiles.First();
            primaryTile.Update(primaryTileData);
        }

        // Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
    }
}