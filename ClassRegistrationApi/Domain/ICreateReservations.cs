using ClassRegistrationApi.Models;

namespace ClassRegistrationApi.Domain;

public interface ICreateReservations
{
    public Task<Models.Registration> CreateReservationForAsync(RegistrationRequest request);
}
