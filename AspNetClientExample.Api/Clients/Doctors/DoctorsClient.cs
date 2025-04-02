using System.Collections.Specialized;
using AspNetClientExample.Api.Clients.Doctors.Requests;
using AspNetClientExample.Api.Extensions;
using AspNetClientExample.Domain.Dtos;

namespace AspNetClientExample.Api.Clients.Doctors;

public class DoctorsClient : HttpClientBase, IDoctorsClient
{
	public DoctorsClient(string address) : base(address)
	{
	}

	public Task<DoctorDto> GetAsync(int id)
	{
		return SendAsync<DoctorDto>(
			HttpMethod.Get,
			$"/doctors/{id}",
			null,
			null);
	}

	public Task<DoctorDto[]> GetAsync(GetDoctorsRequest? request = null)
	{
		return SendAsync<DoctorDto[]>(
			HttpMethod.Get,
			"/doctors",
			new NameValueCollection()
				.AddValuesIfNotNull("name", request?.Names)
				.AddValueIfNotNull("salaryFrom", request?.SalaryFrom)
				.AddValueIfNotNull("salaryTo", request?.SalaryTo)
				.AddValuesIfNotNull("surname", request?.Surnames),
			null);
	}

	public Task<DoctorDto> CreateAsync(DoctorDto doctor)
	{
		return SendAsync<DoctorDto, DoctorDto>(
			HttpMethod.Post,
			"/doctors",
			null,
			null,
			doctor);
	}

	public Task<DoctorDto> UpdateAsync(int id, DoctorDto doctor)
	{
		return SendAsync<DoctorDto, DoctorDto>(
			HttpMethod.Put,
			$"/doctors/{id}",
			null,
			null,
			doctor);
	}

	public Task DeleteAsync(int id)
	{
		return SendAsync(
			HttpMethod.Delete,
			$"/doctors/{id}",
			null,
			null);
	}
}