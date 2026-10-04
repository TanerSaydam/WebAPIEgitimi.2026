using Ardalis.SmartEnum;
using WebAPIEgitimi.HastaKabulWebAPI.Abstractions;

namespace WebAPIEgitimi.HastaKabulWebAPI.Models;

public sealed class Muayene : Entity
{
    public Guid PatientId { get; set; }
    public Patient? Patient { get; set; }
    public PatientStatusEnum PatientStatus { get; set; } = PatientStatusEnum.Bekliyor;
    public DateTimeOffset? TaburcuDate { get; set; }
    public string? Epikriz { get; set; }
}

public sealed class PatientStatusEnum : SmartEnum<PatientStatusEnum, string>
{
    public static readonly PatientStatusEnum Bekliyor = new("Bekliyor", "Bekliyor");
    public static readonly PatientStatusEnum MuayeneOluyor = new("MuayeneOluyor", "Muayene Oluyor");
    public static readonly PatientStatusEnum TaburcuOldu = new("TaburcuOldu", "Taburcu Oldu");

    private PatientStatusEnum(string name, string value) : base(name, value)
    {
    }
}
