using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace LittleLogBook.Data.SqlConnectivity
{
    public class DataHandler : IDataHandler, IDisposable
	{
		private readonly string _connectionString;
		
		private SqlConnection _connection;

		public SqlConnection Connection
		{
			get
			{
				if (_connection == null)
				{
					_connection = new SqlConnection(_connectionString);

					_connection.Open();
				}

				return _connection;
			}
		}

        public DataHandler(string connectionString)
		{
			_connectionString = connectionString;
		}

		public SqlCommand CreateCommand()
		{
			var returnValue = Connection.CreateCommand();

			returnValue.Connection = Connection;

			return returnValue;
		}

		public SqlCommand CreateCommand(string sqlCommand)
		{
			return CreateCommand(sqlCommand, CommandType.StoredProcedure);
		}

		public SqlCommand CreateCommand(string sqlCommand, CommandType type)
		{
			var returnValue = CreateCommand();

			returnValue.CommandText = sqlCommand;
			returnValue.CommandType = type;

			return returnValue;
		}

		public SqlCommand AddParameter(SqlCommand command, string name, object value, DbType type, ParameterDirection direction)
		{
			return command.AddParameter(name, value, type, direction);
		}

		public SqlCommand AddParameter(SqlCommand command, string name, object value, DbType type)
		{
			return AddParameter(command, name, value, type, ParameterDirection.Input);
		}

		public SqlCommand AddParameters(SqlCommand command, IEnumerable<SqlParameter> parameters)
		{
			return command.AddParameters(parameters);
		}

		public SqlDataReader OpenReader(SqlCommand command)
		{
			return command.OpenReader();
		}

		public void Dispose()
		{
			Dispose(true);

			GC.SuppressFinalize(this);
		}

		~DataHandler()
		{
			// Finalizer calls Dispose(false)  
			Dispose(false);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
                if (_connection != null)
                {
                    if (_connection.State != ConnectionState.Closed)
                    {
						_connection.Close();
                    }

					_connection.Dispose();
					_connection = null;
                }
            }
		}
	}
}
