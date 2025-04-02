using System.Collections.Specialized;
using AspNetClientExample.Api.Clients.Examinations.Requests;
using AspNetClientExample.Api.Extensions;
using AspNetClientExample.Domain.Dtos;

namespace AspNetClientExample.Api.Clients.Examinations;

public class ExaminationsClient : HttpClientBase, IExaminationsClient
{
	public ExaminationsClient(string address) : base(address)
	{
	}

	public Task<ExaminationDto> GetAsync(int id)
	{
		return SendAsync<ExaminationDto>(
			HttpMethod.Get,
			$"/examinations/{id}",
			null,
			null);
	}

	public Task<ExaminationDto[]> GetAsync(GetExaminationsRequest? request = null)
	{
		return SendAsync<ExaminationDto[]>(
			HttpMethod.Get,
			"/examinations",
			new NameValueCollection()
				.AddValuesIfNotNull("name", request?.Names),
			null);
	}

	public Task<ExaminationDto> CreateAsync(ExaminationDto examination)
	{
		return SendAsync<ExaminationDto, ExaminationDto>(
			HttpMethod.Post,
			"/examinations",
			null,
			null,
			examination);
	}

	public Task<ExaminationDto> UpdateAsync(int id, ExaminationDto examination)
	{
		return SendAsync<ExaminationDto, ExaminationDto>(
			HttpMethod.Put,
			$"/examinations/{id}",
			null,
			null,
			examination);
	}

	public Task DeleteAsync(int id)
	{
		return SendAsync(
			HttpMethod.Delete,
			$"/examinations/{id}",
			null,
			null);
	}
}