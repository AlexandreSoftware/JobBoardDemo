using System.Data;

namespace JobBoardRepository.Interface;

public interface IDapperWrapper
{
    public IDbConnection GetConnection();

    public IEnumerable<T> Query<T>(string sql);

    public void Execute(string sql);
    public void ExecuteParams<T>(string sql, T obj);

    public Task<IEnumerable<T>> QueryAsync<T>(string sql);
    public Task<IEnumerable<T>> QueryAsyncParams<T>(string sql, object obj);
}