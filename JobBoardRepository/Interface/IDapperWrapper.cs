using System.Data;

namespace JobBoardRepository.Interface;

public interface IDapperWrapper
{
    public IDbConnection GetConnection();

    public IEnumerable<T> Query<T>(string sql);
}