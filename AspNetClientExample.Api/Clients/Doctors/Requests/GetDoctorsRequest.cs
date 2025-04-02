namespace AspNetClientExample.Api.Clients.Doctors.Requests;

public class GetDoctorsRequest
{
	public string[]? Names { get; set; }
    public decimal? SalaryFrom { get; set; }
    public decimal? SalaryTo { get; set; }
    public string[]? Surnames { get; set; }
}