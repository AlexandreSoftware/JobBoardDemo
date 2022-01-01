using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Threading.Tasks.Dataflow;
using Dapper;
using JobBoardRepository.Interface;
using MySqlConnector;

namespace JobBoardRepository;

public class DapperWrapper:IDapperWrapper
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
    public void QueryNoReturn(string sql)
    {
        using (var connection = GetConnection())
        {
            connection.Query(sql);
        }
    }
    
    public Task<IEnumerable<T>> QueryAsync<T>(string sql)
    {
        using (var connection = GetConnection())
        {
            return connection.QueryAsync<T>(sql);
        }
    }
    public async void QueryNoReturnAsync(string sql)
    {
        using (var connection = GetConnection())
        {
            
            Console.WriteLine( await connection.QueryAsync(sql));
        }
    }
}