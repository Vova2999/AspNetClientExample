using AspNetClientExample.Api.Clients.DoctorExaminations.Requests;
using AspNetClientExample.Domain.Dtos;

namespace AspNetClientExample.Api.Clients.DoctorExaminations;

public interface IDoctorExaminationsClient
{
    Task<DoctorExaminationDto> GetAsync(
        int id,
        string? token = default,
        CancellationToken cancellationToken = default);

    Task<DoctorExaminationDto[]> GetAsync(
        GetDoctorExaminationsRequest? request = null,
        string? token = default,
        CancellationToken cancellationToken = default);

    Task<DoctorExaminationDto> CreateAsync(
        DoctorExaminationDto doctorExamination,
        string? token = default,
        CancellationToken cancellationToken = default);

    Task<DoctorExaminationDto> UpdateAsync(
        int id,
        DoctorExaminationDto doctorExamination,
        string? token = default,
        CancellationToken cancellationToken = default);

    Task DeleteAsync(
        int id,
        string? token = default,
        CancellationToken cancellationToken = default);
}