using ClassRegistrationApi.Domain;
using ClassRegistrationApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace ClassRegistrationApi.Controllers;

[Route("registrations")]
[ApiController]
public class RegistrationController : ControllerBase
{
    private readonly ILookupCourseSchedules _scheduleLookup;

    public RegistrationController(ILookupCourseSchedules scheduleLookup)
    {
        _scheduleLookup = scheduleLookup;
    }

    [HttpPost]
    public async Task<ActionResult<Registration>> AddARegistration([FromBody] RegistrationRequest request)
    {
        var dateOfCourse = request.DateOfCourse!.Value;
        bool courseIsAvailableOnThatDate = await _scheduleLookup.CourseAvailabeAsync(request.Course, dateOfCourse);
        if (!courseIsAvailableOnThatDate)
        {
            return BadRequest("Sorry, that course isn't available then.");
        }
        var response = new Registration("99", request);
        return Ok(response);
    }
}
