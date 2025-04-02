using System.Collections.Specialized;
using AspNetClientExample.Api.Clients.DoctorExaminations.Requests;
using AspNetClientExample.Api.Extensions;
using AspNetClientExample.Domain.Dtos;

namespace AspNetClientExample.Api.Clients.DoctorExaminations;

public class DoctorExaminationsClient : HttpClientBase, IDoctorExaminationsClient
{
	public DoctorExaminationsClient(string address) : base(address)
	{
	}

	public Task<DoctorExaminationDto> GetAsync(int id)
	{
		return SendAsync<DoctorExaminationDto>(
			HttpMethod.Get,
			$"/doctorExaminations/{id}",
			null,
			null);
	}

	public Task<DoctorExaminationDto[]> GetAsync(GetDoctorExaminationsRequest? request = null)
	{
		return SendAsync<DoctorExaminationDto[]>(
			HttpMethod.Get,
			"/doctorExaminations",
			new NameValueCollection()
				.AddValueIfNotNull("dateFrom", request?.DateFrom)
				.AddValueIfNotNull("dateTo", request?.DateTo)
				.AddValuesIfNotNull("diseaseName", request?.DiseaseNames)
				.AddValuesIfNotNull("doctorName", request?.DoctorNames)
				.AddValuesIfNotNull("examinationName", request?.ExaminationNames)
				.AddValuesIfNotNull("wardName", request?.WardNames),
			null);
	}

	public Task<DoctorExaminationDto> CreateAsync(DoctorExaminationDto doctorExamination)
	{
		return SendAsync<DoctorExaminationDto, DoctorExaminationDto>(
			HttpMethod.Post,
			"/doctorExaminations",
			null,
			null,
			doctorExamination);
	}

	public Task<DoctorExaminationDto> UpdateAsync(int id, DoctorExaminationDto doctorExamination)
	{
		return SendAsync<DoctorExaminationDto, DoctorExaminationDto>(
			HttpMethod.Put,
			$"/doctorExaminations/{id}",
			null,
			null,
			doctorExamination);
	}

	public Task DeleteAsync(int id)
	{
		return SendAsync(
			HttpMethod.Delete,
			$"/doctorExaminations/{id}",
			null,
			null);
	}
}