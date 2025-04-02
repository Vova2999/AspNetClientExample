using AspNetClientExample.Api.Clients.Doctors.Requests;
using AspNetClientExample.Domain.Dtos;

namespace AspNetClientExample.Api.Clients.Doctors;

public interface IDoctorsClient
{
    Task<DoctorDto> GetAsync(
        int id,
        CancellationToken cancellationToken = default);

    Task<DoctorDto[]> GetAsync(
        GetDoctorsRequest? request = null,
        CancellationToken cancellationToken = default);

    Task<DoctorDto> CreateAsync(
        DoctorDto doctor,
        CancellationToken cancellationToken = default);

    Task<DoctorDto> UpdateAsync(
        int id,
        DoctorDto doctor,
        CancellationToken cancellationToken = default);

    Task DeleteAsync(
        int id,
        CancellationToken cancellationToken = default);
}