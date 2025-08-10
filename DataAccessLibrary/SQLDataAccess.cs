using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DataAccessLibrary
{
	public class SqlDataAccess
	{
		public async Task<IEnumerable<T>> LoadDataAsync<T, U>(string sqlStatement, U parameters, string connectionString)
		{
			using SqlConnection connection = new SqlConnection(connectionString);
			await connection.OpenAsync();
			IEnumerable<T> rows = await connection.QueryAsync<T>(sqlStatement, parameters);
			return rows;
		}

		public async Task<T?> LoadSingleDataAsync<T, U>(string sqlStatement, U parameters, string connectionString)
		{
			using SqlConnection connection = new SqlConnection(connectionString);
			await connection.OpenAsync();
			return await connection.QueryFirstOrDefaultAsync<T>(sqlStatement, parameters);
		}

		public async Task SaveDataAsync<T>(string sqlStatement, T parameters, string connectionString)
		{
			using SqlConnection connection = new SqlConnection(connectionString);
			await connection.OpenAsync();
			await connection.ExecuteAsync(sqlStatement, parameters);
		}

		public async Task<int> SaveDataWithReturnAsync<T>(string sqlStatement, T parameters, string connectionString)
		{
			using SqlConnection connection = new SqlConnection(connectionString);
			await connection.OpenAsync();
			return await connection.ExecuteScalarAsync<int>(sqlStatement, parameters);
		}
	}
}
