using System.Collections.Specialized;
using AspNetClientExample.Api.Clients.Diseases.Requests;
using AspNetClientExample.Api.Extensions;
using AspNetClientExample.Domain.Dtos;

namespace AspNetClientExample.Api.Clients.Diseases;

public class DiseasesClient : HttpClientBase, IDiseasesClient
{
	public DiseasesClient(string address) : base(address)
	{
	}

	public Task<DiseaseDto> GetAsync(int id)
	{
		return SendAsync<DiseaseDto>(
			HttpMethod.Get,
			$"/diseases/{id}",
			null,
			null);
	}

	public Task<DiseaseDto[]> GetAsync(GetDiseasesRequest? request = null)
	{
		return SendAsync<DiseaseDto[]>(
			HttpMethod.Get,
			"/diseases",
			new NameValueCollection()
				.AddValuesIfNotNull("name", request?.Names),
			null);
	}

	public Task<DiseaseDto> CreateAsync(
		DiseaseDto disease)
	{
		return SendAsync<DiseaseDto, DiseaseDto>(
			HttpMethod.Post,
			"/diseases",
			null,
			null,
			disease);
	}

	public Task<DiseaseDto> UpdateAsync(
		int id,
		DiseaseDto disease)
	{
		return SendAsync<DiseaseDto, DiseaseDto>(
			HttpMethod.Put,
			$"/diseases/{id}",
			null,
			null,
			disease);
	}

	public Task DeleteAsync(
		int id)
	{
		return SendAsync(
			HttpMethod.Delete,
			$"/diseases/{id}",
			null,
			null);
	}
}