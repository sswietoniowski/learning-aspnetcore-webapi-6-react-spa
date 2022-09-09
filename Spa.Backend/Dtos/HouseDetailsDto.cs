using System.ComponentModel.DataAnnotations;

public record HouseDetailsDto(int Id, 
    [property: Required]string? Address, [property: Required]string? Country, 
    string? Description, int Price, string? Photo);
