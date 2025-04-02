namespace AspNetClientExample.Api.Clients.Departments.Requests;

public class GetDepartmentsRequest
{
    public int[]? Buildings { get; set; }
    public decimal? FinancingFrom { get; set; }
    public decimal? FinancingTo { get; set; }
    public string[]? Names { get; set; }
}