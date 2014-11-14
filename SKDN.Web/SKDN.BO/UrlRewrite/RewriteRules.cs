using System;
using System.Collections;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Caching;
using System.Xml;

namespace BO.UrlRewrite
{
    public class RewriteRules : CollectionBase {
        public static RewriteRules GetCurrentRewriteRules() {
            string cacheName = "CommonConfiguration_RewriteRules";
            if (null != HttpContext.Current.Cache[cacheName]) {
                try {
                    return (RewriteRules)HttpContext.Current.Cache[cacheName];
                }
                catch {
                    return new RewriteRules();
                }
            }
            else {
                try {
                    string configFilePath = HttpContext.Current.Server.MapPath("/Config/RewriteRules.config"); //@"D:\Running projects\VC Corporation\Dantri\Dantri.Cached\CacheSettings.config";
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(configFilePath);

                    RewriteRules rules = new RewriteRules();

                    XmlNodeList nlstRules = xmlDoc.DocumentElement.SelectNodes("//rules/rule");

                    for (int i = 0; i < nlstRules.Count; i++) {
                        RewriteRule rule = new RewriteRule();
                        rule.Url = nlstRules[i].SelectSingleNode("url").InnerText;
                        rule.Rewrite = nlstRules[i].SelectSingleNode("rewrite").InnerText;

                        rules.List.Add(rule);
                    }

                    XmlNode nodeFileSettingCacheExpire = xmlDoc.DocumentElement.SelectSingleNode("//Configuration/RewriteRulesFile");
                    //long fileSettingCacheExpire = Lib.Object2Long(nodeFileSettingCacheExpire.Attributes["cacheExpire"].Value);
                    //if (fileSettingCacheExpire <= 0) {
                    long fileSettingCacheExpire = 3600;// default 1h
                    //}

                    CacheDependency fileDependency = new CacheDependency(configFilePath);
                    HttpContext.Current.Cache.Insert(cacheName, rules, fileDependency, DateTime.Now.AddSeconds(fileSettingCacheExpire), TimeSpan.Zero, CacheItemPriority.Normal, null);

                    return rules;
                }
                catch {
                    return new RewriteRules();
                }
            }
        }

        public string GetMatchingRewrite(string url) {
            Regex rex;

            for (int i = 0; i < List.Count; i++) {
                RewriteRule rule = (RewriteRule)List[i];
                rex = new Regex(rule.Url, RegexOptions.IgnoreCase);
                Match match = rex.Match(url);

                if (match.Success) {
                    return rex.Replace(url, rule.Rewrite);
                }

            }

            return string.Empty;
        }

        public RewriteRule GetMatchingRule(string url) {
            Regex rex;

            for (int i = 0; i < List.Count; i++) {
                RewriteRule rule = (RewriteRule)List[i];
                rex = new Regex(rule.Url, RegexOptions.IgnoreCase);
                Match match = rex.Match(url);

                if (match.Success) {
                    return rule;
                }

            }

            return new RewriteRule();
        }

        public RewriteRule this[int index] {
            get {
                return (RewriteRule)List[index];
            }
        }

        public struct RewriteRule {
            public RewriteRule(string url, string rewrite) {
                this.Url = url;
                this.Rewrite = rewrite;
            }
            public string Url, Rewrite;
        }
    }
}