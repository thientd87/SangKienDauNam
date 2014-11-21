using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using SolrNet;
using Microsoft.Practices.ServiceLocation;
using SolrNet.Exceptions;
using SolrNet.Impl;
using System.IO;
using log4net.Config;
using System.Web;

namespace SolrIndexer {
    public class Indexer {
        public Indexer() {
            if (Configuration.IndexerServer == string.Empty)
                throw new Exception("No indexer server was found");

            if (Configuration.EnableLogging) {
                XmlConfigurator.ConfigureAndWatch(new FileInfo(Path.Combine(HttpContext.Current.Server.MapPath("/"), "log4net.config")));

                var connection = new SolrConnection(Configuration.IndexerServer);
                var loggingConnection = new LoggingConnection(connection);

                Startup.Init<NewsRepository>(loggingConnection);
            }
            else {
                Startup.Init<NewsRepository>(Configuration.IndexerServer);
            }
        }

        public static bool AddToIndex(NewsRepository entity) {
            bool added = false;
            string log = string.Empty;

            var solr = ServiceLocator.Current.GetInstance<ISolrOperations<NewsRepository>>();

            if (entity != null && entity != default(NewsRepository)) {
                try {
                    solr.Add(entity);
                    solr.Commit();
                    solr.BuildSpellCheckDictionary();
                    added = true;
                }
                catch (SolrConnectionException se) {
                    log = "Cannot connect to Solr server \n" + se.Message;
                }
            }

            return added;
        }

        public static string Delete(string NewsID) {
            string log = string.Empty;
            try {
                var solr = ServiceLocator.Current.GetInstance<ISolrOperations<NewsRepository>>();
                solr.Delete(NewsID);
                solr.Commit();
            }
            catch (SolrConnectionException se) {
                log = "Cannot connect to Solr server \n" + se.Message;
            }
            return log;
        }

        public static string Delete(NewsRepository doc) {
            string log = string.Empty;
            try {
                var solr = ServiceLocator.Current.GetInstance<ISolrOperations<NewsRepository>>();
                solr.Delete(doc);
                solr.Commit();
            }
            catch (SolrConnectionException se) {
                log = "Cannot connect to Solr server \n" + se.Message;
            }
            return log;
        }

        public static void DeleteAll() {
            try {
                var solr = ServiceLocator.Current.GetInstance<ISolrOperations<NewsRepository>>();

                solr.Delete(SolrQuery.All);

                solr.Commit();
            }
            catch (SolrConnectionException se) {
                throw se;
            }
        }

        public static void GetAll() {
            try {
                var solr = ServiceLocator.Current.GetInstance<ISolrOperations<NewsRepository>>();
                ISolrQueryResults<NewsRepository> list = null;
                list = solr.Query(SolrQuery.All);

                foreach (NewsRepository news in list) {
                    HttpContext.Current.Response.Write(string.Format("{0}: {1} <br />", news.NewsID, news.NewsTitle));
                }
            }
            catch (SolrConnectionException se) {
                throw se;
            }
        }

    }
}
