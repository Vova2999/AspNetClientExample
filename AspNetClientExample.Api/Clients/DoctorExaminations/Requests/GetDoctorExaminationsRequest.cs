namespace AspNetClientExample.Api.Clients.DoctorExaminations.Requests;

public class GetDoctorExaminationsRequest
{
    public DateOnly? DateFrom { get; set; }
    public DateOnly? DateTo { get; set; }
    public string[]? DiseaseNames { get; set; }
    public string[]? DoctorNames { get; set; }
    public string[]? ExaminationNames { get; set; }
    public string[]? WardNames { get; set; }
}