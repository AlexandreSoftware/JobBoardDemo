namespace JobBoardServices.View;

public class Job
{
    public int id { get; set; }
    public string title { get; set; }
    public string subTitle { get; set; }

    public double minPay { get; set; }
    public double maxPay { get; set; }
    public string description { get; set; }
}