namespace WebAPIEgitimi.HastaKabulWebAPI.DTOS;

public record PatientUpdateDto(
    Guid Id,
    string TCNo,
    string FirstName,
    string LastName,
    string PhoneNumber);