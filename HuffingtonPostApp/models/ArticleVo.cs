using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuffingtonPostApp.models
{
    /// <summary>
    /// Vo for the Articles parses from the RSS feed
    /// </summary>
    public class ArticleVo
    {
        /// <summary>
        /// 
        /// </summary>
        private String _title = "";
        public String title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private String _description = "";
        public String description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private String _imageUrl = "";
        public String imageUrl
        {
            get
            {
                return _imageUrl;
            }
            set
            {
                _imageUrl = value;
            }
        }
    }
}
