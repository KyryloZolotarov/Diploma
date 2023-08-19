using System.ComponentModel.DataAnnotations;

namespace Catalog.Host.Models.Requests.UpdateRequsts
{
    public class UpdateTypeRequest
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(40)]
        public string TypeName { get; set; } = string.Empty;
    }
}
