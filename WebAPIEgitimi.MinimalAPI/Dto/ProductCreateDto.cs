namespace WebAPIEgitimi.MinimalAPI.Dto;

public sealed record ProductCreateDto(
    string Name,
    Guid CategoryId
    );

public sealed record ProductDto(
    Guid Id,
    string Name,
    Guid CategoryId,
    string CategoryName
    );