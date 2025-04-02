// ReSharper disable UnusedMember.Global

using AspNetClientExample.Api.Clients.Account;
using AspNetClientExample.Api.Clients.Departments;
using AspNetClientExample.Api.Clients.Diseases;
using AspNetClientExample.Api.Clients.DoctorExaminations;
using AspNetClientExample.Api.Clients.Doctors;
using AspNetClientExample.Api.Clients.Examinations;
using AspNetClientExample.Api.Clients.Interns;
using AspNetClientExample.Api.Clients.Professors;
using AspNetClientExample.Api.Clients.Wards;
using AspNetClientExample.Api.Token;
using AspNetClientExample.Domain.Dtos;
using AspNetClientExample.Helpers;
using AspNetClientExample.Menus.Logic;

namespace AspNetClientExample.Menus;

public static class MainMenu
{
    [StaticConsoleMenuCommand("Авторизоваться")]
    public static async Task Login()
    {
        var tokenProvider = Locator.Current.Locate<ITokenProvider>();

        Console.WriteLine("Введите логин");
        var login = ConsoleReadHelper.ReadString(" => ");

        Console.WriteLine("Введите пароль");
        var password = ConsoleReadHelper.ReadString(" => ");

        await tokenProvider.LoginAsync(new LoginDto { Login = login, Password = password });
    }

    [StaticConsoleMenuCommand("Зарегистрироваться")]
    public static async Task Register()
    {
        var accountClient = Locator.Current.Locate<IAccountClient>();
        var tokenProvider = Locator.Current.Locate<ITokenProvider>();

        Console.WriteLine("Введите логин");
        var login = ConsoleReadHelper.ReadString(" => ");

        Console.WriteLine("Введите пароль");
        var password = ConsoleReadHelper.ReadString(" => ");

        await accountClient.RegisterAsync(new RegisterDto { Login = login, Password = password });
        await tokenProvider.LoginAsync(new LoginDto { Login = login, Password = password });
    }

    [StaticConsoleMenuCommand("Выполнить задачи")]
    public static async Task Tasks()
    {
        await StaticConsoleMenu.RunAsync(typeof(TasksMenu));
    }

    [StaticConsoleMenuCommand("Редактировать данные")]
    public static async Task EditData()
    {
        await StaticConsoleMenu.RunAsync(typeof(EditDataMenu));
    }

    [StaticConsoleMenuCommand("Пересоздать данные бд")]
    public static async Task RecreateDatabaseData()
    {
        var departmentsClient = Locator.Current.Locate<IDepartmentsClient>();
        var diseasesClient = Locator.Current.Locate<IDiseasesClient>();
        var doctorExaminationsClient = Locator.Current.Locate<IDoctorExaminationsClient>();
        var doctorsClient = Locator.Current.Locate<IDoctorsClient>();
        var examinationsClient = Locator.Current.Locate<IExaminationsClient>();
        var internsClient = Locator.Current.Locate<IInternsClient>();
        var professorsClient = Locator.Current.Locate<IProfessorsClient>();
        var wardsClient = Locator.Current.Locate<IWardsClient>();

        await CreateDatabaseDataHelper.RecreateDatabaseDataAsync(
            departmentsClient,
            diseasesClient,
            doctorExaminationsClient,
            doctorsClient,
            examinationsClient,
            internsClient,
            professorsClient,
            wardsClient);
    }
}