using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace SolrIndexer {
    public class Configuration {
        
        public static bool EnableLogging {
            get {
                bool _enable = false;

                Boolean.TryParse(ConfigurationManager.AppSettings["EnableIndexLogging"], out _enable);

                return _enable;
            }
        }

        public static string IndexerServer {
            get { 
                string _val = string.Empty;

                if (ConfigurationManager.AppSettings["IndexerServer"] != null)
                    _val = ConfigurationManager.AppSettings["IndexerServer"];

                return _val;
            }
        }
    }
}
