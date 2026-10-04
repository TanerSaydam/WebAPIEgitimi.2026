namespace WebAPIEgitimi.HastaKabulWebAPI.DTOS;

public record PatientCreateDto(
    string TCNo,
    string FirstName,
    string LastName,
    string PhoneNumber);