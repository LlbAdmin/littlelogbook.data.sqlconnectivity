using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace LittleLogBook.Data.SqlConnectivity
{
    public static class CommandExtensions
    {
		public static SqlCommand AddParameter(this SqlCommand command, string name, object value, DbType type, ParameterDirection direction)
		{
			var parameter = command.CreateParameter();

			parameter.ParameterName = name;
			parameter.Value = value ?? DBNull.Value;
			parameter.DbType = type;
			parameter.Direction = direction;

			command.Parameters.Add(parameter);

			return command;
		}

		public static SqlCommand AddParameter(this SqlCommand command, string name, object value, DbType type)
		{
			return AddParameter(command, name, value, type, ParameterDirection.Input);
		}

		public static SqlCommand AddParameters(this SqlCommand command, IEnumerable<IDbDataParameter> parameters)
		{
			foreach (var parameter in parameters)
			{
				command.Parameters.Add(parameter);
			}

			return command;
		}

		public static SqlDataReader OpenReader(this SqlCommand command)
		{
			if (command.Connection.State == ConnectionState.Closed)
			{
				command.Connection.Open();
			}
			
			return command.ExecuteReader();
		}
	}
}
