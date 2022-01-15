using System.Data.SqlTypes;
using JobBoardRepository.Interface;

namespace JobBoardRepository;

public class SqlReader:ISqlReader

{
    public async Task<string> ReadFile(string path)
    { 
        return await File.ReadAllTextAsync("../JobBoardRepository/Sql/"+path+".sql");
    }
}