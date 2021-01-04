using AutoVersionsDB.DB.Contract;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;

namespace AutoVersionsDB.DB.SqlServer
{
    public class SqlServerConnection : IDBConnection
    {
        private readonly SqlConnection _sqlDBConnection;

        public bool IsDisposed { get; private set; }

        public int Timeout { get; private set; }

        public string ConnectionString { get; private set; }


        private readonly object _openCloseSync = new object();

        public string DataBaseName
        {
            get
            {
                string outStr = "";

                if (_sqlDBConnection != null)
                {
                    if (string.IsNullOrWhiteSpace(_sqlDBConnection.ConnectionString)
                        && !string.IsNullOrWhiteSpace(ConnectionString))
                    {
                        _sqlDBConnection.ConnectionString = ConnectionString;
                    }

                    outStr = _sqlDBConnection.Database;
                }

                return outStr;
            }
        }



        public SqlServerConnection(string connectionString, int timeout)
        {
            _sqlDBConnection = new SqlConnection();
            ConnectionString = connectionString;
            Timeout = timeout;
        }


        public void Open()
        {
            lock (_openCloseSync)
            {
                if (_sqlDBConnection.State != ConnectionState.Open)
                {
                    if (string.IsNullOrWhiteSpace(_sqlDBConnection.ConnectionString))
                    {
                        _sqlDBConnection.ConnectionString = ConnectionString;
                    }


                    _sqlDBConnection.Open();
                }
            }
        }

        public void Close()
        {
            lock (_openCloseSync)
            {
                //            _sqlDBConnection.Open();

                _sqlDBConnection.Close();
                SqlConnection.ClearPool(_sqlDBConnection);
                //    _sqlDBConnection.Dispose(); 
            }
        }

        public bool CheckConnection(out string outErrorMseeage)
        {
            outErrorMseeage = "";

            bool outVal;
            try
            {
                Open();
                Close();

                outVal = true;
            }
            catch (Exception ex)
            {
                outErrorMseeage = ex.Message;
                outVal = false;
            }


            return outVal;
        }






        public void ExecSQLCommandStr(string commandStr)
        {
            List<string> splitedCommandStr = SplitSqlStatements(commandStr).ToList();

            foreach (string currScriptStr in splitedCommandStr)
            {
                using (SqlDataAdapter myDataAdapter = CreateDataAdapter(CommandType.Text, currScriptStr))
                {
                    try
                    {
                        myDataAdapter.SelectCommand.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception($"Error Message: '{ex.Message}', Script: {currScriptStr}", ex);
                    }
                }
            }

        }



        private static IEnumerable<string> SplitSqlStatements(string sqlScript)
        {
            // Split by "GO" statements
            var statements = Regex.Split(
                    sqlScript,
                    @"^[\t\r\n]*GO[\t\r\n]*\d*[\t\r\n]*(?:--.*)?$",
                    RegexOptions.Multiline |
                    RegexOptions.IgnorePatternWhitespace |
                    RegexOptions.IgnoreCase);

            // Remove empties, trim, and return
            return statements
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Select(x => x.Trim(' ', '\r', '\n'));
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Security", "CA2100:Review SQL queries for security vulnerabilities", Justification = "<Pending>")]
        public int UpdateDataTableWithUpdateIdentityOnInsert(DataTable dataTable)
        {
            //Comment: dont work here with get changes and merge becase the UpdateIdentityOnInsert duplicate the inserted DataRows


            //OnLogMessage("UpdateDataTableWithUpdateIdentityOnInsert - Start");



            int outVal = -1;

            if (dataTable != null)
            {

                string currTableName = dataTable.TableName;


                using (SqlDataAdapter myDataAdapter = new SqlDataAdapter())
                {
                    myDataAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;

                    myDataAdapter.RowUpdated += new SqlRowUpdatedEventHandler(OnRowUpdate_SetIdentityFromDB);

                    //if (_timeout > 0)
                    //{
                    //    myDataAdapter.SelectCommand.CommandTimeout = _timeout;
                    //}


                    string currTableSelectSql = $"Select * from {currTableName}";

                    SqlCommandBuilder currCommandBuilder = new SqlCommandBuilder(myDataAdapter);

                    myDataAdapter.SelectCommand = new SqlCommand(currTableSelectSql, _sqlDBConnection);
                    myDataAdapter.UpdateCommand = currCommandBuilder.GetUpdateCommand();
                    myDataAdapter.DeleteCommand = currCommandBuilder.GetDeleteCommand();

                    // now we modify the INSERT command, first we clone it and then modify
                    SqlCommand cmd = currCommandBuilder.GetInsertCommand().Clone();

                    // adds the call to SCOPE_IDENTITY
                    cmd.CommandText += " SET @ID = SCOPE_IDENTITY()";

                    //log


                    // the SET command writes to an output parameter "@ID"
                    SqlParameter parm = new SqlParameter
                    {
                        Direction = ParameterDirection.Output,
                        //parm.Size = 4;
                        SqlDbType = SqlDbType.Int,
                        ParameterName = "@ID"
                    };
                    // parm.DbType = DbType.Int32;

                    // adds parameter to command
                    cmd.Parameters.Add(parm);

                    // adds our customized insert command to DataAdapter
                    myDataAdapter.InsertCommand = cmd;

                    //string commandObjStr = getCommandObjAsStr(myDataAdapter.InsertCommand);
                    //OnLogMessage(string.Format(" before call resolveNewRowsIDsConfilctWithDB: {0}", commandObjStr));


                    ResolveNewRowsIDsConfilctWithDB(dataTable);

                    //             OnLogMessage("UpdateDataTableWithUpdateIdentityOnInsert - before call update");


                    outVal = myDataAdapter.Update(dataTable);


                    currCommandBuilder.Dispose();
                }


            }


            //     OnLogMessage("UpdateDataTableWithUpdateIdentityOnInsert - End");


            return outVal;
        }

        private static void ResolveNewRowsIDsConfilctWithDB(DataTable dataTable)
        {
            int currNewRowsKey = -1;
            foreach (DataRow rowItem in dataTable.Rows)
            {
                if (rowItem.RowState == DataRowState.Added)
                {
                    foreach (DataColumn currCol in rowItem.Table.Columns)
                    {
                        if (currCol.AutoIncrement)
                        {
                            currCol.ReadOnly = false;
                            rowItem[currCol] = currNewRowsKey;
                            currCol.ReadOnly = true;
                            currNewRowsKey--;
                            break; // there can be only one identity column
                        }
                    }
                }
            }
        }

        private void OnRowUpdate_SetIdentityFromDB(object sender, SqlRowUpdatedEventArgs args)
        {
            if (args.StatementType == StatementType.Insert)
            {



                // reads the identity value from the output parameter @ID
                object newKeyValue = args.Command.Parameters["@ID"].Value;



                // updates the identity column (autoincrement)
                foreach (DataColumn currCol in args.Row.Table.Columns)
                {
                    if (currCol.AutoIncrement)
                    {
                        currCol.ReadOnly = false;
                        args.Row[currCol] = newKeyValue;
                        currCol.ReadOnly = true;
                        break; // there can be only one identity column
                    }
                }

                args.Row.AcceptChanges();


                //logMessage = "onRowUpdate_SetIdentityFromDB --> End";
                //OnLogMessage(logMessage);

            }
        }




        public DataTable GetSelectCommand(string sqlSelectCmd, int overrideTimeout = 0)
        {
            DataTable outDt = new DataTable();

            using (SqlDataAdapter myDataAdapter = CreateDataAdapter(CommandType.Text, sqlSelectCmd, overrideTimeout))
            {
                myDataAdapter.Fill(outDt);
            }

            return outDt;
        }




        private SqlDataAdapter CreateDataAdapter(CommandType commandType, string commandText, int overrideTimeout = 0)
        {
            return CreateDataAdapter(commandType, commandText, new Dictionary<string, object>(), overrideTimeout);
        }
        private SqlDataAdapter CreateDataAdapter(CommandType commandType, string commandText, Dictionary<string, object> argsDic, int overrideTimeout = 0)
        {

            SqlDataAdapter myDataAdapter = new SqlDataAdapter
            {
                MissingSchemaAction = MissingSchemaAction.AddWithKey
            };

            SqlCommand cmd = CreateSqlCommand(commandType, commandText, overrideTimeout);

            if (argsDic.Count > 0)
            {
                foreach (KeyValuePair<string, object> currParamKeyValue in argsDic)
                {
                    SqlParameter currParam = new SqlParameter
                    {
                        ParameterName = currParamKeyValue.Key
                    };
                    System.ComponentModel.TypeConverter tc = System.ComponentModel.TypeDescriptor.GetConverter(currParam.DbType);

                    currParam.Value = currParamKeyValue.Value;

                    if (currParamKeyValue.Value != null)
                    {
                        currParam.DbType = (DbType)tc.ConvertFrom(currParamKeyValue.Value.GetType().Name);
                    }

                    cmd.Parameters.Add(currParam);
                }
            }


            myDataAdapter.SelectCommand = cmd;
            myDataAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            return myDataAdapter;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Security", "CA2100:Review SQL queries for security vulnerabilities", Justification = "<Pending>")]
        private SqlCommand CreateSqlCommand(CommandType commandType, string commandText, int overrideTimeout)
        {
            SqlCommand cmd = new SqlCommand
            {
                Connection = _sqlDBConnection,
                CommandType = commandType,
                CommandText = commandText
            };

            if (overrideTimeout > 0)
            {
                cmd.CommandTimeout = overrideTimeout;
            }
            else if (Timeout > 0)
            {
                cmd.CommandTimeout = Timeout;
            }

            return cmd;
        }









        #region IDisposable
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~SqlServerConnection()
        {
            Dispose(false);
        }

        // The bulk of the clean-up code is implemented in Dispose(bool)
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // free managed resources


                if (_sqlDBConnection.State == ConnectionState.Open)
                {
                    _sqlDBConnection.Close();
                }
                _sqlDBConnection.Dispose();

                IsDisposed = true;

            }
            // free native resources here if there are any
        }

        #endregion

    }
}
