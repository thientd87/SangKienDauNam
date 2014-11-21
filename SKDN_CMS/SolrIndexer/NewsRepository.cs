using System;
using System.Text;
using SolrNet.Attributes;  

namespace SolrIndexer {
    public class NewsRepository {
        private long _NewsID = 0;

        [SolrUniqueKey("id")] 
        public long NewsID {
            get { return _NewsID; }
            set { _NewsID = value; }
        }

        private string _NewsTitle = string.Empty;

        [SolrField("title")]
        public string NewsTitle {
            get { return _NewsTitle; }
            set { _NewsTitle = value; }
        }

        private string _NewsInitContent = string.Empty;

        [SolrField("initcontent")]
        public string NewsInitContent {
            get { return _NewsInitContent; }
            set { _NewsInitContent = value; }
        }

        private string _NewsContent = string.Empty;

        [SolrField("content")]
        public string NewsContent {
            get { return _NewsContent; }
            set { _NewsContent = value; }
        }

        private DateTime _NewsPublishDate;

        [SolrField("newsdate")]
        public DateTime NewsPublishDate {
            get { return _NewsPublishDate; }
            set { _NewsPublishDate = value; }
        }

        private string _NewsUrl = string.Empty;

        [SolrField("newsurl")]
        public string NewsUrl {
            get { return _NewsUrl; }
            set { _NewsUrl = value; }
        }
    }
}
