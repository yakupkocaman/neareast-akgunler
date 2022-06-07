using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Akgunler.Data;

namespace Akgunler.Extensions
{
    public static class SqlExtention
    {
        public static string ExecuteNonQuery(this string query, string connectionString)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    var error = "";

                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        error = ex.Message;
                    }
                    connection.Close();

                    return error;
                }
            }
        }

        public static T ExecuteScalar<T>(this string query, string connectionString)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();

                    var result = (T)command.ExecuteScalar();

                    connection.Close();

                    return result;
                }
            }
        }
    }
}
