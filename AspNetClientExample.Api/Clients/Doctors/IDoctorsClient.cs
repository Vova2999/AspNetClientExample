using AspNetClientExample.Api.Clients.Doctors.Requests;
using AspNetClientExample.Domain.Dtos;

namespace AspNetClientExample.Api.Clients.Doctors;

public interface IDoctorsClient
{
    Task<DoctorDto> GetAsync(
        int id,
        string? token = default,
        CancellationToken cancellationToken = default);

    Task<DoctorDto[]> GetAsync(
        GetDoctorsRequest? request = null,
        string? token = default,
        CancellationToken cancellationToken = default);

    Task<DoctorDto> CreateAsync(
        DoctorDto doctor,
        string? token = default,
        CancellationToken cancellationToken = default);

    Task<DoctorDto> UpdateAsync(
        int id,
        DoctorDto doctor,
        string? token = default,
        CancellationToken cancellationToken = default);

    Task DeleteAsync(
        int id,
        string? token = default,
        CancellationToken cancellationToken = default);
}