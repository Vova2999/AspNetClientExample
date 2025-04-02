using System.Collections.Specialized;
using AspNetClientExample.Api.Clients.Interns.Requests;
using AspNetClientExample.Api.Extensions;
using AspNetClientExample.Domain.Dtos;

namespace AspNetClientExample.Api.Clients.Interns;

public class InternsClient : HttpClientBase, IInternsClient
{
	public InternsClient(string address) : base(address)
	{
	}

	public Task<InternDto> GetAsync(int id)
	{
		return SendAsync<InternDto>(
			HttpMethod.Get,
			$"/interns/{id}",
			null,
			null);
	}

	public Task<InternDto[]> GetAsync(GetInternsRequest? request = null)
	{
		return SendAsync<InternDto[]>(
			HttpMethod.Get,
			"/interns",
			new NameValueCollection()
				.AddValuesIfNotNull("doctorName", request?.DoctorNames),
			null);
	}

	public Task<InternDto> CreateAsync(InternDto intern)
	{
		return SendAsync<InternDto, InternDto>(
			HttpMethod.Post,
			"/interns",
			null,
			null,
			intern);
	}

	public Task<InternDto> UpdateAsync(int id, InternDto intern)
	{
		return SendAsync<InternDto, InternDto>(
			HttpMethod.Put,
			$"/interns/{id}",
			null,
			null,
			intern);
	}

	public Task DeleteAsync(int id)
	{
		return SendAsync(
			HttpMethod.Delete,
			$"/interns/{id}",
			null,
			null);
	}
}