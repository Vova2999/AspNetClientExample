using AspNetClientExample.Api.Clients.Departments;
using AspNetClientExample.Api.Clients.Diseases;
using AspNetClientExample.Api.Clients.DoctorExaminations;
using AspNetClientExample.Api.Clients.Doctors;
using AspNetClientExample.Api.Clients.Examinations;
using AspNetClientExample.Api.Clients.Interns;
using AspNetClientExample.Api.Clients.Professors;
using AspNetClientExample.Api.Clients.Wards;
using AspNetClientExample.Common.Extensions;
using AspNetClientExample.Domain.Dtos;

namespace AspNetClientExample.Helpers;

public static class CreateDatabaseDataHelper
{
	public static async Task RecreateDatabaseDataAsync(
		IDepartmentsClient departmentsClient,
		IDiseasesClient diseasesClient,
		IDoctorExaminationsClient doctorExaminationsClient,
		IDoctorsClient doctorsClient,
		IExaminationsClient examinationsClient,
		IInternsClient internsClient,
		IProfessorsClient professorsClient,
		IWardsClient wardsClient)
	{
		var random = new Random(1);

		await ClearTablesAsync(
			departmentsClient,
			diseasesClient,
			doctorExaminationsClient,
			doctorsClient,
			examinationsClient,
			internsClient,
			professorsClient,
			wardsClient);

		var departments = await CreateDepartmentsAsync(departmentsClient, random);
		var wards = await CreateWardsAsync(wardsClient, random, departments);
		var diseases = await CreateDiseasesAsync(diseasesClient);
		var examinations = await CreateExaminationsAsync(examinationsClient);
		var doctors = await CreateDoctorsAsync(doctorsClient, random);
		var (interns, professors) = await CreateInternsAndProfessorsAsync(internsClient, professorsClient, random, doctors);
		var doctorsExaminations = await CreateDoctorsExaminationAsync(doctorExaminationsClient, random, doctors, examinations, diseases, wards);
	}

	private static async Task ClearTablesAsync(
		IDepartmentsClient departmentsClient,
		IDiseasesClient diseasesClient,
		IDoctorExaminationsClient doctorExaminationsClient,
		IDoctorsClient doctorsClient,
		IExaminationsClient examinationsClient,
		IInternsClient internsClient,
		IProfessorsClient professorsClient,
		IWardsClient wardsClient)
	{
		var doctorExaminations = await doctorExaminationsClient.GetAsync();
		foreach (var doctorExamination in doctorExaminations)
			await doctorExaminationsClient.DeleteAsync(doctorExamination.Id);

		var interns = await internsClient.GetAsync();
		foreach (var intern in interns)
			await internsClient.DeleteAsync(intern.Id);

		var professors = await professorsClient.GetAsync();
		foreach (var professor in professors)
			await professorsClient.DeleteAsync(professor.Id);

		var doctors = await doctorsClient.GetAsync();
		foreach (var doctor in doctors)
			await doctorsClient.DeleteAsync(doctor.Id);

		var examinations = await examinationsClient.GetAsync();
		foreach (var examination in examinations)
			await examinationsClient.DeleteAsync(examination.Id);

		var diseases = await diseasesClient.GetAsync();
		foreach (var disease in diseases)
			await diseasesClient.DeleteAsync(disease.Id);

		var wards = await wardsClient.GetAsync();
		foreach (var ward in wards)
			await wardsClient.DeleteAsync(ward.Id);

		var departments = await departmentsClient.GetAsync();
		foreach (var department in departments)
			await departmentsClient.DeleteAsync(department.Id);
	}

	private static async Task<DepartmentDto[]> CreateDepartmentsAsync(
		IDepartmentsClient departmentsClient,
		Random random)
	{
		var departments = Enumerable.Range(1, 5)
			.Select(index =>
				new DepartmentDto
				{
					Building = index,
					Financing = random.Next(10, 31) * 1000,
					Name = $"Department{index}"
				})
			.ToArray();

		for (var index = 0; index < departments.Length; index++)
			departments[index] = await departmentsClient.CreateAsync(departments[index]);

		return departments;
	}

	private static async Task<WardDto[]> CreateWardsAsync(
		IWardsClient wardsClient,
		Random random,
		IEnumerable<DepartmentDto> departments)
	{
		var wards = departments
			.SelectMany(department =>
				Enumerable.Range(1, random.Next(5, 21))
					.Select(index => new WardDto
					{
						Name = $"Ward{department.Building}{index:D2}",
						Places = department.Building / 2 + random.Next(1, 5),
						DepartmentName = department.Name
					}))
			.ToArray();

		for (var index = 0; index < wards.Length; index++)
			wards[index] = await wardsClient.CreateAsync(wards[index]);

		return wards;
	}

	private static async Task<DiseaseDto[]> CreateDiseasesAsync(
		IDiseasesClient diseasesClient)
	{
		var diseases = Enumerable.Range(1, 20)
			.Select(index =>
				new DiseaseDto
                {
					Name = $"Disease{index}"
				})
			.ToArray();

		for (var index = 0; index < diseases.Length; index++)
			diseases[index] = await diseasesClient.CreateAsync(diseases[index]);

		return diseases;
	}

	private static async Task<ExaminationDto[]> CreateExaminationsAsync(
		IExaminationsClient examinationsClient)
	{
		var examinations = Enumerable.Range(1, 20)
			.Select(index =>
				new ExaminationDto
                {
					Name = $"Examination{index}"
				})
			.ToArray();

		for (var index = 0; index < examinations.Length; index++)
			examinations[index] = await examinationsClient.CreateAsync(examinations[index]);

		return examinations;
	}

	private static async Task<DoctorDto[]> CreateDoctorsAsync(
		IDoctorsClient doctorsClient,
		Random random)
	{
		var doctors = Enumerable.Range(1, 15)
			.Select(index =>
				new DoctorDto
                {
					Name = $"Name{index}",
					Salary = random.Next(15, 41) * 1000,
					Surname = $"Surname{index}"
				})
			.ToArray();

		doctors[10].Salary = 20000;

		for (var index = 0; index < doctors.Length; index++)
			doctors[index] = await doctorsClient.CreateAsync(doctors[index]);

		return doctors;
	}

	private static async Task<(InternDto[], ProfessorDto[])> CreateInternsAndProfessorsAsync(
		IInternsClient internsClient,
		IProfessorsClient professorsClient,
		Random random,
		IEnumerable<DoctorDto> doctors)
	{
		var internsAndProfessors = doctors
			.Select(doctor =>
			{
				return random.Next(1, 100) switch
				{
					<= 30 => new InternDto { DoctorName = doctor.Name },
					<= 50 => new ProfessorDto { DoctorName = doctor.Name },
					_ => (object?) null
				};
			})
			.ToArray();

		var interns = (InternDto[]) internsAndProfessors
			.Select(obj => obj as InternDto)
			.Where(intern => intern != null)
			.ToArray();

		var professors = (ProfessorDto[]) internsAndProfessors
			.Select(obj => obj as ProfessorDto)
			.Where(professor => professor != null)
			.ToArray();

		for (var index = 0; index < interns.Length; index++)
			interns[index] = await internsClient.CreateAsync(interns[index]);

		for (var index = 0; index < professors.Length; index++)
			professors[index] = await professorsClient.CreateAsync(professors[index]);

        return (interns, professors);
	}

	private static async Task<DoctorExaminationDto[]> CreateDoctorsExaminationAsync(
		IDoctorExaminationsClient doctorExaminationsClient,
		Random random,
		IReadOnlyList<DoctorDto> doctors,
		IReadOnlyList<ExaminationDto> examinations,
		IReadOnlyList<DiseaseDto> diseases,
		IReadOnlyList<WardDto> wards)
	{
		var doctorExaminations = Enumerable.Range(1, 100)
			.Select(_ => new DoctorExaminationDto
            {
				Date = DateTime.Now.AddDays(-random.Next(0, 200)).ToDateOnly(),
				DiseaseName = diseases[random.Next(diseases.Count - 3)].Name,
				DoctorName = doctors[random.Next(doctors.Count - 3)].Name,
				ExaminationName = examinations[random.Next(examinations.Count)].Name,
				WardName = wards[random.Next(wards.Count - 20)].Name
			})
			.ToArray();

		for (var index = 0; index < doctorExaminations.Length; index++)
			doctorExaminations[index] = await doctorExaminationsClient.CreateAsync(doctorExaminations[index]);

        return doctorExaminations;
	}
}