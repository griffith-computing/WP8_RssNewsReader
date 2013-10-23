using HuffingtonPostApp.events;
using HuffingtonPostApp.models;
using HuffingtonPostApp.parsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HuffingtonPostApp.services
{
    /// <summary>
    /// Service to obtain RSS feed items
    /// </summary>
    public class RssArticleService
    {
        protected RssArticleParser parser;
        protected WebClient webClient;

        public event EventHandler<RssArticleEventArgs> articlesObtained;

        public void getFeedArticles(String url)
        {            
            webClient.DownloadStringAsync(new Uri(url));
        }

        protected void handleWebClientDownloadStringComplete(object sender, DownloadStringCompletedEventArgs e)
        {
            EventHandler<RssArticleEventArgs> handler = articlesObtained;
            handler(this, new RssArticleEventArgs(parser.parseArticles(e.Result)));
        }

        public RssArticleService()
        {
            // TODO: figure out how to inject the parser
            this.parser = new RssArticleParser();

            webClient = new WebClient();
            webClient.DownloadStringCompleted += new DownloadStringCompletedEventHandler(handleWebClientDownloadStringComplete);
        }
    }
}
