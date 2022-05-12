namespace ClassRegistrationApi.Adapters;

public class ScheduleHttpAdapter
{
    private readonly HttpClient _httpClient;
    private readonly string path;

    public ScheduleHttpAdapter(HttpClient httpClient, IConfiguration config, ILogger<ScheduleHttpAdapter> logger)
    {
        _httpClient = httpClient;
        _httpClient.DefaultRequestHeaders.Add("User-Agent", "registration-api");
        path = config.GetValue<string>("scheduleApiSchedulePath");
        logger.LogInformation("Using the path: {0}", path);
    }

    public async Task<ScheduleResponse?> GetScheduleForCourse(string courseId)
    {
        var response = await _httpClient.GetAsync($"{path}{courseId}");
        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return null;
        } 
        else
        {
            response.EnsureSuccessStatusCode(); // anything other than 200-299 throws an exception
            var schedule = await response.Content.ReadFromJsonAsync<ScheduleResponse>();
            return schedule!;
        }
    }
}

public record ScheduleResponse
{
    public List<ScheduleResponseItem> data { get; set; } = new List<ScheduleResponseItem>();
}



public record ScheduleResponseItem
{
    public DateTime startDate { get; set; }
    public DateTime endDate { get; set; }
    public int numberOfDays { get; set; }
}
