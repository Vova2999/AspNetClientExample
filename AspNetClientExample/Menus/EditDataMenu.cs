using AspNetClientExample.Api.Clients.Departments;
using AspNetClientExample.Api.Clients.Departments.Requests;
using AspNetClientExample.Api.Clients.Wards;
using AspNetClientExample.Api.Clients.Wards.Requests;
using AspNetClientExample.Domain.Dtos;
using AspNetClientExample.Helpers;
using AspNetClientExample.Menus.Logic;

namespace AspNetClientExample.Menus;

public static class EditDataMenu
{
	[StaticConsoleMenuCommand("Добавить Department")]
	public static async Task AddDepartment()
	{
		var departmentsClient = Locator.Current.Locate<IDepartmentsClient>();

		var name = await ConsoleReadUniqueDepartmentName(departmentsClient);

		Console.WriteLine("Введите Building");
		var building = ConsoleReadHelper.ReadInt(" => ", 1, 5);

		Console.WriteLine("Введите Financing");
		var financing = ConsoleReadHelper.ReadInt(" => ", 0);

		Console.WriteLine();
		Console.WriteLine("Введенные данные:");
		Console.WriteLine($"Name: {name}");
		Console.WriteLine($"Building: {building}");
		Console.WriteLine($"Financing: {financing}");

		Console.WriteLine();
		Console.Write("Продолжить сохранение? y/n => ");
		var answer = Console.ReadLine();
		if (answer != "y")
			return;

		await departmentsClient.CreateAsync(
			new DepartmentDto
			{
				Name = name,
				Building = building,
				Financing = financing
			});
	}

	[StaticConsoleMenuCommand("Изменить Department")]
	public static async Task EditDepartment()
	{
		var departmentsClient = Locator.Current.Locate<IDepartmentsClient>();

		var departments = await departmentsClient.GetAsync();

		ConsoleMenuHelper.PrintCommands(departments.Select(department => department.Name));
		var selector = ConsoleReadHelper.ReadInt(" => ", 0, departments.Length);
		if (selector == 0)
			return;

		var editingDepartment = departments[selector - 1];
		var name = await ConsoleReadUniqueDepartmentName(departmentsClient, editingDepartment);

		Console.WriteLine("Введите Building");
		var building = ConsoleReadHelper.ReadInt(" => ", 1, 5);

		Console.WriteLine("Введите Financing");
		var financing = ConsoleReadHelper.ReadInt(" => ", 0);

		Console.WriteLine();
		Console.WriteLine("Введенные данные:");
		Console.WriteLine($"Name: {name}");
		Console.WriteLine($"Building: {building}");
		Console.WriteLine($"Financing: {financing}");

		Console.WriteLine();
		Console.Write("Продолжить сохранение? y/n => ");
		var answer = Console.ReadLine();
		if (answer != "y")
			return;

		await departmentsClient.UpdateAsync(
			editingDepartment.Id,
            new DepartmentDto
			{
				Name = name,
				Building = building,
				Financing = financing
			});
	}

	[StaticConsoleMenuCommand("Удалить Department")]
	public static async Task DeleteDepartment()
	{
		var departmentsClient = Locator.Current.Locate<IDepartmentsClient>();

		var departments = await departmentsClient.GetAsync();

		ConsoleMenuHelper.PrintCommands(departments.Select(department => department.Name));
		var selector = ConsoleReadHelper.ReadInt(" => ", 0, departments.Length);
		if (selector == 0)
			return;

		Console.WriteLine();
		Console.WriteLine("Продолжить удаление? y/n => ");
		var answer = Console.ReadLine();
		if (answer != "y")
			return;

		await departmentsClient.DeleteAsync(departments[selector - 1].Id);
	}

	[StaticConsoleMenuCommand("Добавить Ward")]
	public static async Task AddWard()
	{
		var wardsClient = Locator.Current.Locate<IWardsClient>();
		var departmentsClient = Locator.Current.Locate<IDepartmentsClient>();

		var departments = await departmentsClient.GetAsync();

		var name = await ConsoleReadUniqueWardName(wardsClient);

		Console.WriteLine("Введите Places");
		var places = ConsoleReadHelper.ReadInt(" => ", 1);

		Console.WriteLine("Выберите Department");
		ConsoleMenuHelper.PrintCommands(departments.Select(department => department.Name), isSkipFirstEmptyLine: true);
		var selector = ConsoleReadHelper.ReadInt(" => ", 0, departments.Length);
		if (selector == 0)
			return;

		var department = departments[selector - 1];

		Console.WriteLine();
		Console.WriteLine("Введенные данные:");
		Console.WriteLine($"Name: {name}");
		Console.WriteLine($"Places: {places}");
		Console.WriteLine($"Department Name: {department.Name}");

		Console.WriteLine();
		Console.Write("Продолжить сохранение? y/n => ");
		var answer = Console.ReadLine();
		if (answer != "y")
			return;

		await wardsClient.CreateAsync(
			new WardDto
			{
				Name = name,
				Places = places,
				DepartmentName = department.Name
			});
	}

	[StaticConsoleMenuCommand("Изменить Ward")]
	public static async Task EditWard()
	{
		var wardsClient = Locator.Current.Locate<IWardsClient>();
		var departmentsClient = Locator.Current.Locate<IDepartmentsClient>();

		var wards = await wardsClient.GetAsync();

		ConsoleMenuHelper.PrintCommands(wards.Select(department => department.Name));
		var selector = ConsoleReadHelper.ReadInt(" => ", 0, wards.Length);
		if (selector == 0)
			return;

		var departments = await departmentsClient.GetAsync();

		var editingWard = wards[selector - 1];
		var name = await ConsoleReadUniqueWardName(wardsClient, editingWard);

		Console.WriteLine("Введите Places");
		var places = ConsoleReadHelper.ReadInt(" => ", 1);

		Console.WriteLine("Выберите Department");
		ConsoleMenuHelper.PrintCommands(departments.Select(department => department.Name), isSkipFirstEmptyLine: true);
		var selectorDepartment = ConsoleReadHelper.ReadInt(" => ", 0, departments.Length);
		if (selectorDepartment == 0)
			return;

		var department = departments[selectorDepartment - 1];

		Console.WriteLine();
		Console.WriteLine("Введенные данные:");
		Console.WriteLine($"Name: {name}");
		Console.WriteLine($"Places: {places}");
		Console.WriteLine($"Department Name: {department.Name}");

		Console.WriteLine();
		Console.Write("Продолжить сохранение? y/n => ");
		var answer = Console.ReadLine();
		if (answer != "y")
			return;

		await wardsClient.UpdateAsync(
			editingWard.Id,
			new WardDto
			{
				Name = name,
				Places = places,
				DepartmentName = department.Name
			});
	}

	[StaticConsoleMenuCommand("Удалить Ward")]
	public static async Task DeleteWard()
	{
		var wardsClient = Locator.Current.Locate<IWardsClient>();

		var wards = await wardsClient.GetAsync();

		ConsoleMenuHelper.PrintCommands(wards.Select(department => department.Name));
		var selector = ConsoleReadHelper.ReadInt(" => ", 0, wards.Length);
		if (selector == 0)
			return;

		Console.WriteLine();
		Console.WriteLine("Продолжить удаление? y/n => ");
		var answer = Console.ReadLine();
		if (answer != "y")
			return;

		await wardsClient.DeleteAsync(wards[selector - 1].Id);
	}

	private static async Task<string> ConsoleReadUniqueDepartmentName(
		IDepartmentsClient departmentsClient,
		DepartmentDto? currentDepartment = null)
	{
		Console.WriteLine("Введите Name");

		while (true)
		{
			var name = ConsoleReadHelper.ReadString(" => ");

			var departments = await departmentsClient.GetAsync(new GetDepartmentsRequest { Names = new[] { name } });

			var isExists = currentDepartment == null
				? departments.Any()
				: departments.Any(department => department.Id != currentDepartment.Id);

			if (!isExists)
				return name;

			Console.WriteLine("Вы ввели не уникальное значение");
		}
	}

	private static async Task<string> ConsoleReadUniqueWardName(
		IWardsClient wardsClient,
		WardDto? currentWard = null)
	{
		Console.WriteLine("Введите Name");

		while (true)
		{
			var name = ConsoleReadHelper.ReadString(" => ");

			var wards = await wardsClient.GetAsync(new GetWardsRequest { Names = new[] { name } });

            var isExists = currentWard == null
				? wards.Any()
				: wards.Any(ward => ward.Id != currentWard.Id);

            if (!isExists)
				return name;

			Console.WriteLine("Вы ввели не уникальное значение");
		}
	}
}