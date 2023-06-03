namespace Common;

/// <summary>
/// filter for dogs
/// </summary>
/// <param name="Attribute">can be Name, Color, TailLength or Weight</param>
/// <param name="Order">can be desk or asc</param>
/// <param name="PageNumber">pade number, by default is 1</param>
/// <param name="PageSize">page size, by default is 10</param>
public record DogFilter(string? Attribute, string? Order, int PageNumber = 1, int PageSize = 10);
