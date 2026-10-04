using WebAPIEgitimi.HastaKabulWebAPI.Abstractions;

namespace WebAPIEgitimi.HastaKabulWebAPI.Models;

public sealed class Patient : Entity
{
    public string TCNo { get; set; } = default!;
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
}