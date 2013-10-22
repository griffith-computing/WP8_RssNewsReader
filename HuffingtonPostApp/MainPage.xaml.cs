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

namespace HuffingtonPostApp
{
    public partial class MainPage : PhoneApplicationPage
    {
        protected String huffPostFullFeedUrl = "http://feeds.bbci.co.uk/news/rss.xml";
        protected String[] titles;
        protected String[] images;
        protected String[] descriptions;

        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // refactor to a service which has an event when the download is complete...
            WebClient wc = new WebClient();
            wc.DownloadStringCompleted += new DownloadStringCompletedEventHandler(handleDownloadStringComplete);
            wc.DownloadStringAsync(new Uri(huffPostFullFeedUrl));

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        private void handleDownloadStringComplete(object sender, DownloadStringCompletedEventArgs e)
        {
            if (Microsoft.Phone.Net.NetworkInformation.DeviceNetworkInformation.IsNetworkAvailable)
            {
                XNamespace media = XNamespace.Get("http://search.yahoo.com/mrss/");
                titles = System.Xml.Linq.XElement.Parse(e.Result).Descendants("title")
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

                SpeechSynthesizer synth = new SpeechSynthesizer();
                synth.SpeakTextAsync(titles[0]).Completed += new AsyncActionCompletedHandler(delegate(IAsyncAction action, AsyncStatus status)
                    {
                        synth.SpeakTextAsync(descriptions[0]);
                    });
                UpdatePrimaryTile(titles[0], images[0]);
                
            }
            else
            {
                MessageBox.Show("No network is available...");
            }
        }

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