using HuffingtonPostApp.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace HuffingtonPostApp.parsers
{
    /// <summary>
    /// 
    /// </summary>
    public class RssArticleParser
    {
        public List<ArticleVo> parseArticles(string result)
        {
            List<ArticleVo> articles = new List<ArticleVo>();
            XNamespace media = XNamespace.Get("http://search.yahoo.com/mrss/");
            foreach (XElement element in XElement.Parse(result).Descendants("item"))
            {
                ArticleVo article = new ArticleVo();
                article.title = element.Descendants("title").Select(m => m.Value).ToArray()[0];
                article.description = element.Descendants("description").Select(m => m.Value).ToArray()[0];
                article.imageUrl = element.Descendants(media + "thumbnail").Select(m => m.Attribute("url").Value).ToArray()[0];
                articles.Add(article);
            }

            return articles;
        }
    }
}
