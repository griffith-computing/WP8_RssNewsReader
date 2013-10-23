using HuffingtonPostApp.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuffingtonPostApp.events
{
    /// <summary>
    /// 
    /// </summary>
    public class RssArticleEventArgs : EventArgs
    {
        /// <summary>
        /// 
        /// </summary>
        private List<ArticleVo> _articles;
        public List<ArticleVo> articles
        {
            get
            {
                return _articles;
            }
            set
            {
                _articles = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="articles"></param>
        public RssArticleEventArgs(List<ArticleVo> articles)
        {
            this.articles = articles;
        }
    }
}
