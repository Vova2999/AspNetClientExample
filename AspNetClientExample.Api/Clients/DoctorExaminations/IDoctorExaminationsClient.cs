using AspNetClientExample.Api.Clients.DoctorExaminations.Requests;
using AspNetClientExample.Domain.Dtos;

namespace AspNetClientExample.Api.Clients.DoctorExaminations;

public interface IDoctorExaminationsClient
{
	Task<DoctorExaminationDto> GetAsync(int id);
	Task<DoctorExaminationDto[]> GetAsync(GetDoctorExaminationsRequest request = null);
	Task<DoctorExaminationDto> CreateAsync(DoctorExaminationDto doctorExamination);
	Task<DoctorExaminationDto> UpdateAsync(int id, DoctorExaminationDto doctorExamination);
	Task DeleteAsync(int id);
}