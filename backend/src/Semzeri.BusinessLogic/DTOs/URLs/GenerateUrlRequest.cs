using System.ComponentModel.DataAnnotations;

namespace Semzeri.BusinessLogic.DTOs.URLs;

public class GenerateUrlRequest
{
    [Required]
    public string OriginalUrl { get; set; } = string.Empty;
}