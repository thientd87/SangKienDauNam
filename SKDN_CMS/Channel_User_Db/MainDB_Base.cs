
using System;
using System.Data;
using System.Collections;

namespace DFISYS.User.Db {
    public abstract class MainDB_Base : IDisposable {
        private IDbConnection _connection;
        private IDbTransaction _transaction;

        // Table and view fields
        private CategoryCollection _category;
        private ChannelCollection _channel;
        private Channel_UserCollection _channel_User;
        private Channel_User_RoleCollection _channel_User_Role;
        private PermissionCollection _permission;
        private RoleCollection _role;
        private Role_PermissionCollection _role_Permission;
        private UserCollection _user;
        private User_CategoryCollection _user_Category;
        private User_PermissionCollection _user_Permission;

        private StoreProcedure _storeProcedure;
        private Box_Permission _BoxPer;
        protected MainDB_Base()
            : this(true) {
            // EMPTY
        }

        protected MainDB_Base(bool init) {
            if (init)
                InitConnection();
        }

        protected void InitConnection() {
            _connection = CreateConnection();
            _connection.Open();
        }

        protected abstract IDbConnection CreateConnection();
        protected internal abstract string CreateSqlParameterName(string paramName);
        protected internal virtual IDataReader ExecuteReader(IDbCommand command) {
            return command.ExecuteReader();
        }

        internal IDbDataParameter AddParameter(IDbCommand cmd, string paramName,
                                                DbType dbType, object value) {
            IDbDataParameter parameter = cmd.CreateParameter();
            parameter.ParameterName = CreateCollectionParameterName(paramName);
            parameter.DbType = dbType;
            parameter.Value = null == value ? DBNull.Value : value;
            cmd.Parameters.Add(parameter);
            return parameter;
        }

        protected abstract string CreateCollectionParameterName(string baseParamName);
        public IDbConnection Connection {
            get { return _connection; }
        }

        public CategoryCollection CategoryCollection {
            get {
                if (null == _category)
                    _category = new CategoryCollection((MainDB)this);
                return _category;
            }
        }

        public ChannelCollection ChannelCollection {
            get {
                if (null == _channel)
                    _channel = new ChannelCollection((MainDB)this);
                return _channel;
            }
        }

        public Box_Permission Box_Permission {
            get {
                if (null == _BoxPer)
                    _BoxPer = new Box_Permission((MainDB)this);
                return _BoxPer;
            }
        }
        public Channel_UserCollection Channel_UserCollection {
            get {
                if (null == _channel_User)
                    _channel_User = new Channel_UserCollection((MainDB)this);
                return _channel_User;
            }
        }

        public Channel_User_RoleCollection Channel_User_RoleCollection {
            get {
                if (null == _channel_User_Role)
                    _channel_User_Role = new Channel_User_RoleCollection((MainDB)this);
                return _channel_User_Role;
            }
        }

        public PermissionCollection PermissionCollection {
            get {
                if (null == _permission)
                    _permission = new PermissionCollection((MainDB)this);
                return _permission;
            }
        }

        public RoleCollection RoleCollection {
            get {
                if (null == _role)
                    _role = new RoleCollection((MainDB)this);
                return _role;
            }
        }

        public Role_PermissionCollection Role_PermissionCollection {
            get {
                if (null == _role_Permission)
                    _role_Permission = new Role_PermissionCollection((MainDB)this);
                return _role_Permission;
            }
        }
        
        public UserCollection UserCollection {
            get {
                if (null == _user)
                    _user = new UserCollection((MainDB)this);
                return _user;
            }
        }

        public User_CategoryCollection User_CategoryCollection {
            get {
                if (null == _user_Category)
                    _user_Category = new User_CategoryCollection((MainDB)this);
                return _user_Category;
            }
        }

        public StoreProcedure StoreProcedure {
            get {
                if (null == _storeProcedure)
                    _storeProcedure = new StoreProcedure((MainDB)this);
                return _storeProcedure;
            }
        }

        public User_PermissionCollection User_PermissionCollection {
            get {
                if (null == _user_Permission)
                    _user_Permission = new User_PermissionCollection((MainDB)this);
                return _user_Permission;
            }
        }

        public IDbTransaction BeginTransaction() {
            CheckTransactionState(false);
            _transaction = _connection.BeginTransaction();
            return _transaction;
        }

        public IDbTransaction BeginTransaction(IsolationLevel isolationLevel) {
            CheckTransactionState(false);
            _transaction = _connection.BeginTransaction(isolationLevel);
            return _transaction;
        }
        public void CommitTransaction() {
            CheckTransactionState(true);
            _transaction.Commit();
            _transaction = null;
        }

        public void RollbackTransaction() {
            CheckTransactionState(true);
            _transaction.Rollback();
            _transaction = null;
        }


        private void CheckTransactionState(bool mustBeOpen) {
            if (mustBeOpen) {
                if (null == _transaction)
                    throw new InvalidOperationException("Transaction is not open.");
            }
            else {
                if (null != _transaction)
                    throw new InvalidOperationException("Transaction is already open.");
            }
        }

        internal IDbCommand CreateCommand(string sqlText) {
            return CreateCommand(sqlText, false);
        }

        internal IDbCommand CreateCommand(string sqlText, bool procedure) {
            IDbCommand cmd = _connection.CreateCommand();
            cmd.CommandText = sqlText;
            cmd.Transaction = _transaction;
            if (procedure)
                cmd.CommandType = CommandType.StoredProcedure;
            return cmd;
        }
        public virtual void Close() {
            if (null != _connection)
                _connection.Close();
        }
        public virtual void Dispose() {
            Close();
            if (null != _connection)
                _connection.Dispose();
        }
    }
    
}
