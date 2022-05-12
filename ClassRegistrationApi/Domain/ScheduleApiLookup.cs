using ClassRegistrationApi.Adapters;

namespace ClassRegistrationApi.Domain;

public class ScheduleApiLookup : ILookupCourseSchedules
{

    private readonly ScheduleHttpAdapter _adapter;

    public ScheduleApiLookup(ScheduleHttpAdapter adapter)
    {
        _adapter = adapter;
    }

    public async Task<bool> CourseAvailabeAsync(string course, DateTime dateOfCourse)
    {
        var response = await _adapter.GetScheduleForCourse(course);
        if (response == null)
        {
            return false;
        }
        return response.data.Any(c => c.startDate == dateOfCourse);
    }
}
