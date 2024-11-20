using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace TicketingApi
{
	public class DataAccess
	{
		public List<T> LoadData<T, U>(string sqlStatement, U parameters, string connectionString)
		{
			using IDbConnection connection = new SqlConnection(connectionString);
			List<T> rows = connection.Query<T>(sqlStatement, parameters).ToList();
			return rows;
		}

		public void SaveData<T>(string sqlStatement, T parameters, string connectionString)
		{
			using IDbConnection connection = new SqlConnection(connectionString);
			connection.Execute(sqlStatement, parameters);
		}
	}
}
