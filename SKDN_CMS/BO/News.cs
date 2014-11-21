using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GafinCMS.Entities {
    public class News {
        private string _title = string.Empty;
        private string _sapo = string.Empty;
        private string _content = string.Empty;
        private DateTime _date = DateTime.Now;


        public News() { }

        public DateTime Date {
            get {
                return _date;
            }
            set {
                _date = value;
            }
        }

        public string Title {
            get {
                return _title;
            }
            set {
                _title = value;
            }
        }

        public string Sapo {
            get {
                return _sapo;
            }
            set {
                _sapo = value;
            }
        }

        public string Content {
            get {
                return _content;
            }
            set {
                _content = value;
            }
        }

        private string _source = string.Empty;

        public string Source {
            get { return _source; }
            set { _source = value; }
        }


    }
}
