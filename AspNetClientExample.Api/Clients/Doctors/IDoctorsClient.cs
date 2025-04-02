using AspNetClientExample.Api.Clients.Doctors.Requests;
using AspNetClientExample.Domain.Dtos;

namespace AspNetClientExample.Api.Clients.Doctors;

public interface IDoctorsClient
{
	Task<DoctorDto> GetAsync(int id);
	Task<DoctorDto[]> GetAsync(GetDoctorsRequest? request = null);
	Task<DoctorDto> CreateAsync(DoctorDto doctor);
	Task<DoctorDto> UpdateAsync(int id, DoctorDto doctor);
	Task DeleteAsync(int id);
}