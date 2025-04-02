using AspNetClientExample.Api.Clients.Departments;
using AspNetClientExample.Api.Clients.Departments.Requests;
using AspNetClientExample.Api.Clients.Diseases;
using AspNetClientExample.Api.Clients.DoctorExaminations;
using AspNetClientExample.Api.Clients.DoctorExaminations.Requests;
using AspNetClientExample.Api.Clients.Doctors;
using AspNetClientExample.Api.Clients.Doctors.Requests;
using AspNetClientExample.Api.Clients.Interns;
using AspNetClientExample.Api.Clients.Professors;
using AspNetClientExample.Api.Clients.Wards;
using AspNetClientExample.Api.Clients.Wards.Requests;
using AspNetClientExample.Common.Extensions;
using AspNetClientExample.Menus.Logic;

namespace AspNetClientExample.Menus;

public static class TasksMenu
{
    [StaticConsoleMenuCommand("Вывести названия и вместимости палат, расположенных в 5-м корпусе, вместимостью 5 и более мест")]
    public static async Task Task1()
    {
        var wardsClient = Locator.Current.Locate<IWardsClient>();
        var departmentClient = Locator.Current.Locate<IDepartmentsClient>();

        var departments = await departmentClient.GetAsync(new GetDepartmentsRequest { Buildings = new[] { 5 } });
        var wards = await wardsClient.GetAsync(new GetWardsRequest { PlacesFrom = 5, DepartmentNames = departments.Select(department => department.Name).ToArray() });

        wards.Select(ward => $"{ward.Name}: {ward.Places}").ForEach(Console.WriteLine);
    }

    [StaticConsoleMenuCommand("Вывести названия отделений в которых проводилось хотя бы одно обследование за последнюю неделю")]
    public static async Task Task2()
    {
        var wardsClient = Locator.Current.Locate<IWardsClient>();
        var doctorExaminationsClient = Locator.Current.Locate<IDoctorExaminationsClient>();

        var startDate = DateTime.Now.AddDays(-7).ToDateOnly();
        var doctorExaminations = await doctorExaminationsClient.GetAsync(new GetDoctorExaminationsRequest { DateFrom = startDate });
        var wards = await wardsClient.GetAsync(new GetWardsRequest { Names = doctorExaminations.Select(doctorExamination => doctorExamination.WardName).Distinct().ToArray() });

        wards.Select(ward => ward.DepartmentName).Distinct().ForEach(Console.WriteLine);
    }

    [StaticConsoleMenuCommand("Вывести названия заболеваний, для которых не проводятся обследования")]
    public static async Task Task3()
    {
        var diseasesClient = Locator.Current.Locate<IDiseasesClient>();
        var doctorExaminationsClient = Locator.Current.Locate<IDoctorExaminationsClient>();

        var diseases = await diseasesClient.GetAsync();
        var doctorExaminations = await doctorExaminationsClient.GetAsync();

        var diseaseNames = diseases
            .Where(disease => doctorExaminations.All(doctorExamination => doctorExamination.DiseaseName != disease.Name))
            .Select(disease => disease.Name)
            .ToArray();

        diseaseNames.ForEach(Console.WriteLine);
    }

    [StaticConsoleMenuCommand("Вывести полные имена врачей, которые не проводят обследования")]
    public static async Task Task4()
    {
        var doctorsClient = Locator.Current.Locate<IDoctorsClient>();
        var doctorExaminationsClient = Locator.Current.Locate<IDoctorExaminationsClient>();

        var doctors = await doctorsClient.GetAsync();
        var doctorExaminations = await doctorExaminationsClient.GetAsync();

        var doctorNames = doctors
            .Where(doctor => doctorExaminations.All(doctorExamination => doctorExamination.DoctorName != doctor.Name))
            .Select(doctor => doctor.Name)
            .ToArray();

        doctorNames.ForEach(Console.WriteLine);
    }

    [StaticConsoleMenuCommand("Вывести названия отделений, в которых не проводятся обследования")]
    public static async Task Task5()
    {
        var wardsClient = Locator.Current.Locate<IWardsClient>();
        var departmentsClient = Locator.Current.Locate<IDepartmentsClient>();
        var doctorExaminationsClient = Locator.Current.Locate<IDoctorExaminationsClient>();

        var wards = await wardsClient.GetAsync();
        var departments = await departmentsClient.GetAsync();
        var doctorExaminations = await doctorExaminationsClient.GetAsync();

        var wardsWithDoctorExaminations = wards
            .Where(ward => doctorExaminations.All(de => de.WardName != ward.Name));

        var departmentNames = departments
            .Where(department => wardsWithDoctorExaminations.All(ward => ward.DepartmentName != department.Name))
            .Select(department => department.Name)
            .ToArray();

        departmentNames.ForEach(Console.WriteLine);
    }

    [StaticConsoleMenuCommand("Вывести фамилии врачей, которые являются интернами")]
    public static async Task Task6()
    {
        var doctorsClient = Locator.Current.Locate<IDoctorsClient>();
        var internsClient = Locator.Current.Locate<IInternsClient>();

        var interns = await internsClient.GetAsync();
        var doctors = await doctorsClient.GetAsync(new GetDoctorsRequest { Names = interns.Select(intern => intern.DoctorName).ToArray() });

        doctors.Select(doctor => doctor.Surname).ForEach(Console.WriteLine);
    }

    [StaticConsoleMenuCommand("Вывести фамилии интернов, ставки которых больше, чем ставка хотя бы одного из врачей")]
    public static async Task Task7()
    {
        var doctorsClient = Locator.Current.Locate<IDoctorsClient>();
        var internsClient = Locator.Current.Locate<IInternsClient>();
        var professorsClient = Locator.Current.Locate<IProfessorsClient>();

        var doctors = await doctorsClient.GetAsync();
        var interns = await internsClient.GetAsync();
        var professors = await professorsClient.GetAsync();

        var minSalary = doctors
            .Where(doctor =>
                interns.All(intern => intern.DoctorName != doctor.Name) &&
                professors.All(professor => professor.DoctorName != doctor.Name))
            .Min(doctor => doctor.Salary);

        var doctorSurnames = doctors
            .Where(doctor =>
                interns.Any(intern => intern.DoctorName == doctor.Name) &&
                doctor.Salary > minSalary)
            .Select(doctor => doctor.Surname)
            .ToArray();

        doctorSurnames.ForEach(Console.WriteLine);
    }

    [StaticConsoleMenuCommand("Вывести названия палат, чья вместимость больше, чем вместимость каждой палаты, находящейся в 3-м корпусе")]
    public static async Task Task8()
    {
        var wardsClient = Locator.Current.Locate<IWardsClient>();
        var departmentClient = Locator.Current.Locate<IDepartmentsClient>();

        var departments = await departmentClient.GetAsync(new GetDepartmentsRequest { Buildings = new[] { 3 } });
        var wardsWithSelectedBuildings = await wardsClient.GetAsync(new GetWardsRequest { DepartmentNames = departments.Select(department => department.Name).ToArray() });
        var maxPlaces = wardsWithSelectedBuildings.Max(ward => ward.Places);

        var wards = await wardsClient.GetAsync(new GetWardsRequest { PlacesFrom = maxPlaces + 1 });

        wards.Select(ward => ward.Name).ForEach(Console.WriteLine);
    }

    [StaticConsoleMenuCommand("Вывести фамилии врачей, проводящих обследования в отделениях Department1 и Department3")]
    public static async Task Task9()
    {
        var wardsClient = Locator.Current.Locate<IWardsClient>();
        var doctorsClient = Locator.Current.Locate<IDoctorsClient>();
        var doctorExaminationsClient = Locator.Current.Locate<IDoctorExaminationsClient>();

        var wards = await wardsClient.GetAsync(new GetWardsRequest { DepartmentNames = new[] { "Department1", "Department3" } });
        var doctorExaminations = await doctorExaminationsClient.GetAsync(new GetDoctorExaminationsRequest { WardNames = wards.Select(ward => ward.Name).ToArray() });
        var doctors = await doctorsClient.GetAsync(new GetDoctorsRequest { Names = doctorExaminations.Select(doctorExamination => doctorExamination.DoctorName).Distinct().ToArray() });

        doctors.Select(doctor => doctor.Surname).ForEach(Console.WriteLine);
    }

    [StaticConsoleMenuCommand("Вывести названия отделений, в которых работают интерны и профессоры")]
    public static async Task Task10()
    {
        var wardsClient = Locator.Current.Locate<IWardsClient>();
        var internsClient = Locator.Current.Locate<IInternsClient>();
        var professorsClient = Locator.Current.Locate<IProfessorsClient>();
        var doctorExaminationsClient = Locator.Current.Locate<IDoctorExaminationsClient>();

        var interns = await internsClient.GetAsync();
        var professors = await professorsClient.GetAsync();

        var doctorNames = interns.Select(intern => intern.DoctorName).Concat(professors.Select(professor => professor.DoctorName)).ToArray();
        var doctorExaminations = await doctorExaminationsClient.GetAsync(new GetDoctorExaminationsRequest { DoctorNames = doctorNames });
        var wards = await wardsClient.GetAsync(new GetWardsRequest { Names = doctorExaminations.Select(doctorExamination => doctorExamination.WardName).Distinct().ToArray() });

        wards.Select(ward => ward.DepartmentName).Distinct().ForEach(Console.WriteLine);
    }

    [StaticConsoleMenuCommand("Вывести полные имена врачей и отделения, если отделение имеет фонд финансирования более 20000")]
    public static async Task Task11()
    {
        var wardsClient = Locator.Current.Locate<IWardsClient>();
        var doctorsClient = Locator.Current.Locate<IDoctorsClient>();
        var departmentsClient = Locator.Current.Locate<IDepartmentsClient>();
        var doctorExaminationsClient = Locator.Current.Locate<IDoctorExaminationsClient>();

        var departments = await departmentsClient.GetAsync(new GetDepartmentsRequest { FinancingFrom = 20000 });
        var wards = await wardsClient.GetAsync(new GetWardsRequest { DepartmentNames = departments.Select(department => department.Name).ToArray() });
        var doctorExaminations = await doctorExaminationsClient.GetAsync(new GetDoctorExaminationsRequest { WardNames = wards.Select(ward => ward.Name).ToArray() });
        var doctors = await doctorsClient.GetAsync(new GetDoctorsRequest { Names = doctorExaminations.Select(doctorExamination => doctorExamination.DoctorName).Distinct().ToArray() });

        var doctorsWithDepartments = doctors
            .Select(doctor => new
            {
                FullName = $"{doctor.Name} {doctor.Surname}",
                DepartmentNames = departments
                    .Where(department => wards
                        .Where(ward => doctorExaminations
                            .Where(doctorExamination => doctorExamination.DoctorName == doctor.Name)
                            .Any(doctorExamination => doctorExamination.WardName == ward.Name))
                        .Any(ward => ward.DepartmentName == department.Name))
                    .Select(department => department.Name)
                    .ToArray()
            })
            .ToArray();

        doctorsWithDepartments
            .SelectMany(doctor => doctor.DepartmentNames
                .Select(departmentName => $"{doctor.FullName}: {departmentName}"))
            .ForEach(Console.WriteLine);
    }

    [StaticConsoleMenuCommand("Вывести название отделения, в котором проводит обследования врач с наибольшей ставкой")]
    public static async Task Task12()
    {
        var wardsClient = Locator.Current.Locate<IWardsClient>();
        var doctorsClient = Locator.Current.Locate<IDoctorsClient>();
        var doctorExaminationsClient = Locator.Current.Locate<IDoctorExaminationsClient>();

        var doctors = await doctorsClient.GetAsync();

        var maxSalary = doctors.Max(doctor => doctor.Salary);

        var doctorsWithMaxSalary = doctors.Where(doctor => doctor.Salary == maxSalary);

        var doctorExaminations = await doctorExaminationsClient.GetAsync(new GetDoctorExaminationsRequest { DoctorNames = doctorsWithMaxSalary.Select(doctor => doctor.Name).ToArray() });
        var wards = await wardsClient.GetAsync(new GetWardsRequest { Names = doctorExaminations.Select(doctorExamination => doctorExamination.WardName).ToArray() });

        wards.Select(ward => ward.DepartmentName).Distinct().ForEach(Console.WriteLine);
    }

    [StaticConsoleMenuCommand("Вывести названия заболеваний и количество проводимых по ним обследований")]
    public static async Task Task13()
    {
        var diseasesClient = Locator.Current.Locate<IDiseasesClient>();
        var doctorExaminationsClient = Locator.Current.Locate<IDoctorExaminationsClient>();

        var diseases = await diseasesClient.GetAsync();
        var doctorExaminations = await doctorExaminationsClient.GetAsync();

        var diseasesCounts = diseases
            .Select(disease => new
            {
                disease.Name,
                Count = doctorExaminations.Count(doctorExamination => doctorExamination.DiseaseName == disease.Name)
            })
            .ToArray();

        diseasesCounts.Select(disease => $"{disease.Name}: {disease.Count}").ForEach(Console.WriteLine);
    }
}