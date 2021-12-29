using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Threading.Tasks.Dataflow;
using Dapper;
using JobBoardRepository.Interface;

namespace JobBoardRepository;

public class DapperWrapper:IDapperWrapper
{
    public string connectionString;
    public IDbConnection GetConnection()
    {
        return new SqlConnection(connectionString);
    }

    public IEnumerable<T> Query<T>(string sql)
    {
        using (var connection = GetConnection())
        {
            return connection.Query<T>(sql);
        }
    }
}