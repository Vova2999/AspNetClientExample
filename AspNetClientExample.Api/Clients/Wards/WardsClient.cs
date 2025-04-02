using System.Collections.Specialized;
using AspNetClientExample.Api.Clients.Wards.Requests;
using AspNetClientExample.Api.Extensions;
using AspNetClientExample.Domain.Dtos;

namespace AspNetClientExample.Api.Clients.Wards;

public class WardsClient : HttpClientBase, IWardsClient
{
	public WardsClient(string address) : base(address)
	{
	}

	public Task<WardDto> GetAsync(int id)
	{
		return SendAsync<WardDto>(
			HttpMethod.Get,
			$"/wards/{id}",
			null,
			null);
	}

	public Task<WardDto[]> GetAsync(GetWardsRequest? request = null)
	{
		return SendAsync<WardDto[]>(
			HttpMethod.Get,
			"/wards",
			new NameValueCollection()
				.AddValuesIfNotNull("name", request?.Names)
				.AddValueIfNotNull("placesFrom", request?.PlacesFrom)
				.AddValueIfNotNull("placesTo", request?.PlacesTo)
				.AddValuesIfNotNull("departmentName", request?.DepartmentNames),
			null);
	}

	public Task<WardDto> CreateAsync(WardDto ward)
	{
		return SendAsync<WardDto, WardDto>(
			HttpMethod.Post,
			"/wards",
			null,
			null,
			ward);
	}

	public Task<WardDto> UpdateAsync(int id, WardDto ward)
	{
		return SendAsync<WardDto, WardDto>(
			HttpMethod.Put,
			$"/wards/{id}",
			null,
			null,
			ward);
	}

	public Task DeleteAsync(int id)
	{
		return SendAsync(
			HttpMethod.Delete,
			$"/wards/{id}",
			null,
			null);
	}
}