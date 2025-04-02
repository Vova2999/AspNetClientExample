using System.Collections.Specialized;
using AspNetClientExample.Api.Clients.Professors.Requests;
using AspNetClientExample.Api.Extensions;
using AspNetClientExample.Domain.Dtos;

namespace AspNetClientExample.Api.Clients.Professors;

public class ProfessorsClient : HttpClientBase, IProfessorsClient
{
	public ProfessorsClient(string address) : base(address)
	{
	}

	public Task<ProfessorDto> GetAsync(int id)
	{
		return SendAsync<ProfessorDto>(
			HttpMethod.Get,
			$"/professors/{id}",
			null,
			null);
	}

	public Task<ProfessorDto[]> GetAsync(GetProfessorsRequest? request = null)
	{
		return SendAsync<ProfessorDto[]>(
			HttpMethod.Get,
			"/professors",
			new NameValueCollection()
				.AddValuesIfNotNull("doctorName", request?.DoctorNames),
			null);
	}

	public Task<ProfessorDto> CreateAsync(ProfessorDto professor)
	{
		return SendAsync<ProfessorDto, ProfessorDto>(
			HttpMethod.Post,
			"/professors",
			null,
			null,
			professor);
	}

	public Task<ProfessorDto> UpdateAsync(int id, ProfessorDto professor)
	{
		return SendAsync<ProfessorDto, ProfessorDto>(
			HttpMethod.Put,
			$"/professors/{id}",
			null,
			null,
			professor);
	}

	public Task DeleteAsync(int id)
	{
		return SendAsync(
			HttpMethod.Delete,
			$"/professors/{id}",
			null,
			null);
	}
}