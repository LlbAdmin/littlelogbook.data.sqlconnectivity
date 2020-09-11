using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace LittleLogBook.Data.SqlConnectivity
{
    public interface IDataHandler
    {
        SqlConnection Connection { get; }

        SqlCommand CreateCommand();

        SqlCommand CreateCommand(string sqlCommand);

        SqlCommand CreateCommand(string sqlCommand, CommandType commandType);

        SqlCommand AddParameters(SqlCommand command, IEnumerable<SqlParameter> parameters);

        SqlCommand AddParameter(SqlCommand command, string name, object value, DbType type);

        SqlCommand AddParameter(SqlCommand command, string name, object value, DbType type, ParameterDirection direction);

        SqlDataReader OpenReader(SqlCommand Command);

        void Dispose();
    }
}
