using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using HuffingtonPostApp.models;
using Windows.Phone.Speech.Synthesis;
using Windows.Foundation;

namespace HuffingtonPostApp.views
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ArticleView : PhoneApplicationPage
    {
        /// <summary>
        /// 
        /// </summary>
        private ArticleVo _data;
        public ArticleVo data
        {
            get
            {
                return _data;
            }
            set
            {
                _data = value;
                updateControl();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        protected void updateControl()
        {
            title.Text = data.title;
            description.Text = data.description;
        }

        /// <summary>
        /// 
        /// </summary>
        protected void finishInitializingComponent()
        {
            speakArticleBtn.Click += new RoutedEventHandler(handleSpeakArticleBtnClick);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void handleSpeakArticleBtnClick(object sender, RoutedEventArgs e)
        {
            SpeechSynthesizer synth = new SpeechSynthesizer();
            synth.SpeakTextAsync(data.title).Completed += new AsyncActionCompletedHandler(delegate(IAsyncAction action, AsyncStatus status)
            {
                synth.SpeakTextAsync(data.description);
            });
        }

        /// <summary>
        /// 
        /// </summary>
        public ArticleView()
        {
            InitializeComponent();
            finishInitializingComponent();
        }
    }
}