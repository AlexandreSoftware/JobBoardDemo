namespace JobBoardRepository.Interface;

public interface ISqlReader
{
    public Task<string> ReadFile(string path);
}