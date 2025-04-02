namespace AspNetClientExample.Api.Clients.Wards.Requests;

public class GetWardsRequest
{
	public string[]? Names { get; set; }
    public int? PlacesFrom { get; set; }
    public int? PlacesTo { get; set; }
    public string[]? DepartmentNames { get; set; }
}