using Dapper;
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Akgunler.Models
{
	public abstract class EntityBase : EntityBase<int>
	{

    }

	public abstract class EntityBase<TId> : EntityBaseWithTypedId<TId>
	{
		#region Init

		public static IDbConnection GetConnection() => new SqlConnection(ConnectionStr);

		private static string ConnectionStr { get; set; }

		public static void InitConnection(string connectionString)
		{
			ConnectionStr = connectionString;
		}

		#endregion

		#region Methods
		public static int Exec(string sql, object param = null)
		{
			int result = 0;

			using (IDbConnection conn = GetConnection())
			{
				try
				{
					if (conn.State != ConnectionState.Open) conn.Open();

					result = conn.Execute(sql, param);
				}
				catch (Exception e)
				{
					System.Console.WriteLine(e);
				}
				finally
				{
					conn.Close();
				}
			}

			return result;
		}

		public static T Scalar<T>(string sql, object param = null)
		{
			T result;

			using (IDbConnection conn = GetConnection())
			{
				try
				{
					if (conn.State != ConnectionState.Open) conn.Open();

					result = conn.ExecuteScalar<T>(sql, param);
				}
				finally
				{
					conn.Close();
				}
			}

			return result;
		}

		public static T One<T>(string sql, object param = null)
		{
			T result;

			using (IDbConnection conn = GetConnection())
			{
				try
				{
					if (conn.State != ConnectionState.Open) conn.Open();

					result = conn.QueryFirstOrDefault<T>(sql, param);
				}
				finally
				{
					conn.Close();
				}
			}
			return result;
		}

		public static List<T> All<T>(string sql, object param = null)
		{
			var result = new List<T>();

			using (IDbConnection conn = GetConnection())
			{
				try
				{
					if (conn.State != ConnectionState.Open) conn.Open();

					result = conn.Query<T>(sql, param).AsList();
				}
				finally
				{
					conn.Close();
				}
			}
			return result;
		}

		public static T Get<T>(int id) where T : class
		{
			T result;

			using (IDbConnection conn = GetConnection())
			{
				try
				{
					if (conn.State != ConnectionState.Open) conn.Open();

					result = conn.Get<T>(id);
				}
				finally
				{
					conn.Close();
				}
			}

			return result;
		}

		public static List<T> GetAll<T>() where T : class
		{
			List<T> result = new List<T>();

			using (IDbConnection conn = GetConnection())
			{
				try
				{
					if (conn.State != ConnectionState.Open) conn.Open();

					result = conn.GetAll<T>().ToList();
				}
				finally
				{
					conn.Close();
				}
			}

			return result;
		}

/*
		public static long Insert<T>(T value) where T : class
		{
			long result;

			using (IDbConnection conn = GetConnection())
			{
				try
				{
					if (conn.State != ConnectionState.Open) conn.Open();

					result = conn.Insert(value);
				}
				catch (Exception e)
				{
					throw new Exception(e.Message);
				}
				finally
				{
					conn.Close();
				}
			}

			return result;
		}

		public static bool Update<T>(T value) where T : class
		{
			bool result;

			using (IDbConnection conn = GetConnection())
			{
				try
				{
					if (conn.State != ConnectionState.Open) conn.Open();

					result = conn.Update(value);
				}
				catch (Exception e)
				{
					throw new Exception(e.Message);
				}
				finally
				{
					conn.Close();
				}
			}

			return result;
		}
*/
		public static bool Delete<T>(T value) where T : class
		{
			bool result;

			using (IDbConnection conn = GetConnection())
			{
				try
				{
					if (conn.State != ConnectionState.Open) conn.Open();

					result = conn.Delete(value);
				}
				finally
				{
					conn.Close();
				}
			}

			return result;
		}

		public static (int, List<T>) Page<T>(int page, int count, string primaryKey, string sql, object param = null)
		{
			string itemsQuery = "SELECT COUNT(" + primaryKey + ") AS WiseTotalCount FROM ( " + sql + " ) AS WiseQuery;SELECT * FROM ( " + sql + " ) AS WiseQuery ORDER BY " + primaryKey + " OFFSET " + ((page > 0 ? (page - 1) : 0) * count).ToString() + " ROWS FETCH NEXT " + count.ToString() + " ROWS ONLY;";

			List<T> result;
			int totalCount;

			using (IDbConnection conn = GetConnection())
			{
				try
				{
					if (conn.State != ConnectionState.Open) conn.Open();

					var query = conn.QueryMultiple(itemsQuery, param);
					totalCount = query.Read<int>().First();
					result = conn.Query<T>(itemsQuery, param: param).ToList();
				}
				finally
				{
					conn.Close();
				}
			}

			return (totalCount, result);
		}

		#endregion
		
		#region Async Methods

		public static async Task<int> ExecAsync(string sql, object param = null)
		{
			int result = 0;

			using (IDbConnection conn = GetConnection())
			{
				try
				{
					if (conn.State != ConnectionState.Open) conn.Open();

					result = await conn.ExecuteAsync(sql, param);
				}
				catch (Exception e)
				{
					System.Console.WriteLine(e);
				}
				finally
				{
					conn.Close();
				}
			}

			return result;
		}

		public static async Task<T> ScalarAsync<T>(string sql, object param = null)
		{
			T result;

			using (IDbConnection conn = GetConnection())
			{
				try
				{
					if (conn.State != ConnectionState.Open) conn.Open();

					result = await conn.ExecuteScalarAsync<T>(sql, param);
				}
				finally
				{
					conn.Close();
				}
			}

			return result;
		}

		public static async Task<T> OneAsync<T>(string sql, object param = null)
		{
			T result;

			using (IDbConnection conn = GetConnection())
			{
				try
				{
					if (conn.State != ConnectionState.Open) conn.Open();

					result = await conn.QueryFirstOrDefaultAsync<T>(sql, param);
				}
				finally
				{
					conn.Close();
				}
			}
			return result;
		}

		public static async Task<List<T>> AllAsync<T>(string sql, object param = null)
		{
			var result = new List<T>();

			using (IDbConnection conn = GetConnection())
			{
				try
				{
					if (conn.State != ConnectionState.Open) conn.Open();
					var items = await conn.QueryAsync<T>(sql, param);
					result = items.AsList();
				}
				finally
				{
					conn.Close();
				}
			}
			return result;
		}

		public static async Task<T> GetByIdAsync<T>(int id) where T : class
		{
			T result;

			using (IDbConnection conn = GetConnection())
			{
				try
				{
					if (conn.State != ConnectionState.Open) conn.Open();

					result = await conn.GetAsync<T>(id);
				}
				finally
				{
					conn.Close();
				}
			}

			return result;
		}

		public static async Task<List<T>> GetAllAsync<T>() where T : class
		{
			List<T> result = new List<T>();

			using (IDbConnection conn = GetConnection())
			{
				try
				{
					if (conn.State != ConnectionState.Open) conn.Open();

					var tresult = await conn.GetAllAsync<T>();
					result = tresult.AsList();
				}
				finally
				{
					conn.Close();
				}
			}

			return result;
		}

		public static async Task<long> InsertAsync<T>(T value) where T : class
		{
			long result;

			using (IDbConnection conn = GetConnection())
			{
				try
				{
					if (conn.State != ConnectionState.Open) conn.Open();

					result = await conn.InsertAsync(value);
				}
				catch (Exception e)
				{
					throw new Exception(e.Message);
				}
				finally
				{
					conn.Close();
				}
			}

			return result;
		}

		public static async Task<bool> UpdateAsync<T>(T value) where T : class
		{
			bool result;

			using (IDbConnection conn = GetConnection())
			{
				try
				{
					if (conn.State != ConnectionState.Open) conn.Open();

					result = await conn.UpdateAsync(value);
				}
				catch (Exception e)
				{
					throw new Exception(e.Message);
				}
				finally
				{
					conn.Close();
				}
			}

			return result;
		}

		public static async Task<bool> DeleteAsync<T>(T value) where T : class
		{
			bool result;

			using (IDbConnection conn = GetConnection())
			{
				try
				{
					if (conn.State != ConnectionState.Open) conn.Open();

					result = await conn.DeleteAsync(value);
				}
				finally
				{
					conn.Close();
				}
			}

			return result;
		}

		public static async Task<(int, List<T>)> PageAsync<T>(int page, int count, string primaryKey, string sql, object param = null)
		{
			string itemsQuery = "SELECT COUNT(" + primaryKey + ") AS WiseTotalCount FROM ( " + sql + " ) AS WiseQuery;SELECT * FROM ( " + sql + " ) AS WiseQuery ORDER BY " + primaryKey + " OFFSET " + ((page > 0 ? (page - 1) : 0) * count).ToString() + " ROWS FETCH NEXT " + count.ToString() + " ROWS ONLY;";

			List<T> result;
			int totalCount;

			using (IDbConnection conn = GetConnection())
			{
				try
				{
					if (conn.State != ConnectionState.Open) conn.Open();

					var query = await conn.QueryMultipleAsync(itemsQuery, param);
					var items = await query.ReadAsync<int>();
					totalCount = items.First();

					var tresult = await conn.QueryAsync<T>(itemsQuery, param: param);
					result = tresult.AsList();
				}
				finally
				{
					conn.Close();
				}
			}

			return (totalCount, result);
		}
		#endregion
	}
}