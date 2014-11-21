using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Portal.Core.DAL {
    public class StoredProcedures_New {
        private MainDB _db;

        public StoredProcedures_New(MainDB db) {
            _db = db;
        }

        public MainDB Database {
            get { return _db; }
        }
        


    }
}
