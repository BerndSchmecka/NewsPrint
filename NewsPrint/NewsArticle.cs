using System.ServiceModel.Syndication;
using System.Xml;

namespace NewsPrint {
    public class NewsArticle {
        public string URL {get; private set;}
        public List<Article> Articles {get; private set;}

        public NewsArticle(string url){
            this.URL = url;
            this.Articles = new List<Article>();
        }

        public void getArticles(){
            XmlReader reader = XmlReader.Create(this.URL);
            SyndicationFeed feed = SyndicationFeed.Load(reader);
            reader.Close();

            foreach(SyndicationItem item in feed.Items){
                this.Articles.Add(new Article(item.Title.Text, item.Summary.Text, item.PublishDate.LocalDateTime));
            }
        }
    }

    public struct Article {
        public string Title {get; private set;}
        public string Description {get; private set;}

        public DateTime PublishDate {get; private set;}

        public Article(string title, string desc, DateTime pubdate){
            this.Title = title;
            this.Description = desc;
            this.PublishDate = pubdate;
        }
    }
}