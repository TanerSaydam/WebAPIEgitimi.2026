namespace WebAPIEgitimi.HastaKabulWebAPI.DTOS;

public class MuayeneDto
{
    public Guid Id { get; set; }
    public Guid PatientId { get; set; }
    public string PatientName { get; set; } = default!;
    public DateTimeOffset CreatedAt { get; set; }
    public string Status { get; set; } = default!;
    public DateTimeOffset? TaburcuDate { get; set; }
}
