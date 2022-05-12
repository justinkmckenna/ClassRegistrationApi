using ClassRegistrationApi.Domain;
using ClassRegistrationApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace ClassRegistrationApi.Controllers;

[Route("registrations")]
[ApiController]
public class RegistrationController : ControllerBase
{
    private readonly ILookupCourseSchedules _scheduleLookup;
    private readonly ICreateReservations _reservationCenter;

    public RegistrationController(ILookupCourseSchedules scheduleLookup, ICreateReservations createReservations)
    {
        _scheduleLookup = scheduleLookup;
        _reservationCenter = createReservations;
    }

    [HttpPost]
    public async Task<ActionResult<Models.Registration>> AddARegistration([FromBody] RegistrationRequest request)
    {
        var dateOfCourse = request.DateOfCourse!.Value;
        bool courseIsAvailableOnThatDate = await _scheduleLookup.CourseAvailabeAsync(request.Course, dateOfCourse);
        if (!courseIsAvailableOnThatDate)
        {
            return BadRequest("Sorry, that course isn't available then.");
        }
        var response = await _reservationCenter.CreateReservationForAsync(request);
        return Ok(response);
    }
}
