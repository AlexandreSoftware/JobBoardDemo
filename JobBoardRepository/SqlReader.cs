using System.Data.SqlTypes;

namespace JobBoardRepository;

public class SqlReader
{
    public async static Task<string> ReadFile(string path)
    { 
        return await File.ReadAllTextAsync("../JobBoardRepository/sql/"+path+".sql");
    }
}