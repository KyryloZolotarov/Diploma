using System.ComponentModel.DataAnnotations;

namespace Basket.Host.Models;

public class AddRequest
{
    [Required] public string Data { get; set; } = null!;
}