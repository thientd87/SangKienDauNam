using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SolrNet;
using log4net;

namespace SolrIndexer {
    public class LoggingConnection : ISolrConnection {
        private readonly ISolrConnection connection;

        public LoggingConnection(ISolrConnection connection) {
            this.connection = connection;
        }

        public string ServerURL {
            get { return connection.ServerURL; }
            set { connection.ServerURL = value; }
        }

        public string Version {
            get { return connection.Version; }
            set { connection.Version = value; }
        }

        public string Post(string relativeUrl, string s) {
            logger.DebugFormat("POSTing '{0}' to '{1}'", s, relativeUrl);
            return connection.Post(relativeUrl, s);
        }

        public string Get(string relativeUrl, IEnumerable<KeyValuePair<string, string>> parameters) {
            var stringParams = string.Join(", ", parameters.Select(p => string.Format("{0}={1}", p.Key, p.Value)).ToArray());
            logger.DebugFormat("GETting '{0}' from '{1}'", stringParams, relativeUrl);
            return connection.Get(relativeUrl, parameters);
        }

        private static readonly ILog logger = LogManager.GetLogger(typeof(LoggingConnection));
    }
}
