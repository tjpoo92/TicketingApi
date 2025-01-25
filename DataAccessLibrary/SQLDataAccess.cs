using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DataAccessLibrary
{
	public class SqlDataAccess
	{
		public async Task<IEnumerable<T>> LoadDataAsync<T, U>(string sqlStatement, U parameters, string connectionString)
		{
			using IDbConnection connection = new SqlConnection(connectionString);
			IEnumerable<T> rows = await connection.QueryAsync<T>(sqlStatement, parameters);
			return rows;
		}

		public async Task SaveDataAsync<T>(string sqlStatement, T parameters, string connectionString)
		{
			using IDbConnection connection = new SqlConnection(connectionString);
			await connection.ExecuteAsync(sqlStatement, parameters);
		}
	}
}
