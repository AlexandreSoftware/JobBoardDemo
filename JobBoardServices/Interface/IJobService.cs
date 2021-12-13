using JobBoardServices.View;

namespace JobBoardServices.Interface;

public interface IJobService
{
    public Job Get();
    public Job Post();
    public Job Delete();
    public Job Put();
    public Job Options();

}