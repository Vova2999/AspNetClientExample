using AspNetClientExample.Api.Clients.DoctorExaminations.Requests;
using AspNetClientExample.Domain.Dtos;

namespace AspNetClientExample.Api.Clients.DoctorExaminations;

public interface IDoctorExaminationsClient
{
    Task<DoctorExaminationDto> GetAsync(
        int id,
        CancellationToken cancellationToken = default);

    Task<DoctorExaminationDto[]> GetAsync(
        GetDoctorExaminationsRequest? request = null,
        CancellationToken cancellationToken = default);

    Task<DoctorExaminationDto> CreateAsync(
        DoctorExaminationDto doctorExamination,
        CancellationToken cancellationToken = default);

    Task<DoctorExaminationDto> UpdateAsync(
        int id,
        DoctorExaminationDto doctorExamination,
        CancellationToken cancellationToken = default);

    Task DeleteAsync(
        int id,
        CancellationToken cancellationToken = default);
}