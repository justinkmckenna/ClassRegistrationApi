namespace ClassRegistrationApi.Domain;

public class TrivialCourseScheduleLookup : ILookupCourseSchedules
{
    public Task<bool> CourseAvailabeAsync(string course, DateTime dateOfCourse)
    {
        return dateOfCourse > DateTime.Now;
    }
}
