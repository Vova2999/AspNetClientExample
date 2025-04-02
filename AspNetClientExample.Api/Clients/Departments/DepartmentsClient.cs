using System.Collections.Specialized;
using AspNetClientExample.Api.Clients.Departments.Requests;
using AspNetClientExample.Api.Extensions;
using AspNetClientExample.Domain.Dtos;

namespace AspNetClientExample.Api.Clients.Departments;

public class DepartmentsClient : HttpClientBase, IDepartmentsClient
{
	public DepartmentsClient(string address) : base(address)
	{
	}

	public Task<DepartmentDto> GetAsync(int id)
	{
		return SendAsync<DepartmentDto>(
			HttpMethod.Get,
			$"/departments/{id}",
			null,
			null);
	}

	public Task<DepartmentDto[]> GetAsync(GetDepartmentsRequest? request = null)
	{
		return SendAsync<DepartmentDto[]>(
			HttpMethod.Get,
			"/departments",
			new NameValueCollection()
				.AddValuesIfNotNull("building", request?.Buildings)
				.AddValueIfNotNull("financingFrom", request?.FinancingFrom)
				.AddValueIfNotNull("financingTo", request?.FinancingTo)
				.AddValuesIfNotNull("name", request?.Names),
			null);
	}

	public Task<DepartmentDto> CreateAsync(DepartmentDto department)
	{
		return SendAsync<DepartmentDto, DepartmentDto>(
			HttpMethod.Post,
			"/departments",
			null,
			null,
			department);
	}

	public Task<DepartmentDto> UpdateAsync(int id, DepartmentDto department)
	{
		return SendAsync<DepartmentDto, DepartmentDto>(
			HttpMethod.Put,
			$"/departments/{id}",
			null,
			null,
			department);
	}

	public Task DeleteAsync(int id)
	{
		return SendAsync(
			HttpMethod.Delete,
			$"/departments/{id}",
			null,
			null);
	}
}