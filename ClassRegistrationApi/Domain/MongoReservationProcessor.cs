using ClassRegistrationApi.Adapters;
using ClassRegistrationApi.Models;

namespace ClassRegistrationApi.Domain;

// this will be a transient or scoped service since we are injecting a 
public class MongoReservationProcessor : ICreateReservations
{
    private readonly RegistrationMongoAdapter _adapter;

    public MongoReservationProcessor(RegistrationMongoAdapter adapter)
    {
        _adapter = adapter;
    }

    async Task<Models.Registration> ICreateReservations.CreateReservationForAsync(RegistrationRequest request)
    {
        var registration = new Registration
        {
            Details = new RegistrationDetails
            {
                Course = request.Course,
                DateOfCourse = request.DateOfCourse!.Value,
                Name = request.Name
            }
        };
        await _adapter.GetRegistrationCollection().InsertOneAsync(registration);
        return new Models.Registration(registration.Id.ToString(), request);
    }
}
