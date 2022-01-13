using System.Data;
using Dapper;
using JobBoardRepository.Interface;
using MySqlConnector;
using Serilog;

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
        string templateLog = "[JobBoardDemoApi] [DapperWrapper] [GetConnection]";
        Log.Information($"{templateLog} Getting Connection");
        var res = new MySqlConnection(connectionString);
        Log.Information($"{templateLog} Sucessfully got Connection, returning");
        return res;
    }

    public IEnumerable<T> Query<T>(string sql)
    {
        string templateLog = "[JobBoardDemoApi] [DapperWrapper] [Query]";
        Log.Information($"{templateLog} Started Query, calling GetConnection");
        using (var connection = GetConnection())
        {
            Log.Information($"{templateLog} Got Connection, Querying");
            var res = connection.Query<T>(sql);
            Log.Information($"{templateLog} Sucess Querying, returning");
            return res;
        }
    }
    
    public async Task<IEnumerable<T>> QueryAsync<T>(string sql)
    {
        string templateLog = "[JobBoardDemoApi] [DapperWrapper] [QueryAsync]";
        Log.Information($"{templateLog} Started Query, calling GetConnection");
        using (var connection = GetConnection())
        {
            Log.Information($"{templateLog} Got Connection, Querying with params");
            var res = await connection.QueryAsync<T>(sql);
            Log.Information($"{templateLog} Sucess Querying, returning");
            return res;
        }
    }
    public async Task<dynamic> QueryAsyncDynamic(string sql)
    {
        string templateLog = "[JobBoardDemoApi] [DapperWrapper] [QueryAsync]";
        Log.Information($"{templateLog} Started Query, calling GetConnection");
        using (var connection = GetConnection())
        {
            Log.Information($"{templateLog} Got Connection, Querying with params");
            var res = await connection.QueryAsync<dynamic>(sql);
            Log.Information($"{templateLog} Sucess Querying, returning");
            return res;
        }
    }
    
    public async Task<IEnumerable<T>> QueryAsyncParams<T>(string sql, object obj)
    {
        string templateLog = "[JobBoardDemoApi] [DapperWrapper] [QueryAsyncParams]";
        Log.Information($"{templateLog} Started Query, calling GetConnection");
        using (var connection = GetConnection())
        {
            Log.Information($"{templateLog} Got Connection, Querying with params");
            var res = await connection.QueryAsync<T>(sql, obj);
            Log.Information($"{templateLog} Sucess Querying, returning");
            return res;
        }    
    }
    public void Execute(string sql)
    {
        string templateLog = "[JobBoardDemoApi] [DapperWrapper] [Execute]";
        Log.Information($"{templateLog} Started Execution, calling GetConnection");
        using (var connection = GetConnection())
        {
            Log.Information($"{templateLog} Got Connection, Executing");
            connection.Execute(sql);
            Log.Information($"{templateLog} Sucessfully Executed Sql");
        }
    }
    
    public async void ExecuteParamsAsync<T>(string sql,T obj){
        string templateLog = "[JobBoardDemoApi] [DapperWrapper] [ExecuteParams]";
        Log.Information($"{templateLog} Started Execution, calling GetConnection");
        using (var connection = GetConnection())
        {
            Log.Information($"{templateLog} Got Connection, Executing with Params");
            await connection.ExecuteAsync(sql,obj);
            Log.Information($"{templateLog} Sucessfully Executed Sql with params");
        }
    }
}