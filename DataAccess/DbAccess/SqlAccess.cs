using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace DataAccess.DbAccess;

public class SqlAccess : ISqlAccess
{
    private readonly IConfiguration _config;
    public SqlAccess(IConfiguration config)
    {
        _config = config;
    }

    public async Task<IEnumerable<T>> LoadData<T, U>(string storedProcedure, U parameters, string connectionString = "Default") 
    {
        using (IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionString)))
        {
            return await connection.QueryAsync<T>(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
        };
    }

    public async Task SaveData<T>(string storedProcedure, T parameters, string connectionString = "Default")
    {
        using (IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionString)))
        {
            await connection.ExecuteAsync(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
        };
    }
}
