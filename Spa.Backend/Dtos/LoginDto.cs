using System.ComponentModel.DataAnnotations;

public record LoginDto([Required]string UserName, [Required]string Password, bool RememberLogin = false);