namespace BackGroundJobsWithHangfire.Jobs;

public class JobsWithDI
{
    private readonly ILogger<JobsWithDI> _logger;

    public JobsWithDI(ILogger<JobsWithDI> logger)
    {
        _logger = logger;
    }

    public void WriteLogMessage(string message)
    {
        _logger.LogInformation("{datetime} {message}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss tt"), message);
    }
}
