using System.ComponentModel.DataAnnotations;

namespace Catalog.Host.Models.Requests.AddRequsts
{
    public class AddTypeRequest
    {
        [Required]
        [MaxLength(40)]
        public string TypeName { get; set; } = string.Empty;
    }
}
