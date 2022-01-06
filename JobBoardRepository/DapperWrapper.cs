using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Threading.Tasks.Dataflow;
using Dapper;
using JobBoardRepository.Interface;
using MySqlConnector;

namespace JobBoardRepository;

public class DapperWrapper : IDapperWrapper
{
    public string connectionString;

    public DapperWrapper(string cs)
    {
        this.connectionString = cs;
    }

    public IDbConnection GetConnection()
    {
        return new MySqlConnection(connectionString);
    }

    public IEnumerable<T> Query<T>(string sql)
    {
        using (var connection = GetConnection())
        {
            return connection.Query<T>(sql);
        }
    }

    public void Execute(string sql)
    {
        using (var connection = GetConnection())
        {
            connection.ExecuteAsync(sql);
        }
    }

    public async void ExecuteParams<T>(string sql,T obj){
        using (var connection = GetConnection())
        {
           await connection.ExecuteAsync(sql:sql, param:obj);
        }
    }
    public async Task<IEnumerable<T>> QueryAsync<T>(string sql)
    {
        using (var connection = GetConnection())
        {
             return await connection.QueryAsync<T>(sql);
        }
    }

    public async Task<IEnumerable<T>> QueryAsyncParams<T>(string sql, object obj)
    {
        using (var connection = GetConnection())
        {
            return await connection.QueryAsync<T>(sql, obj);
        }    }
}