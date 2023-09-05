using Catalog.Host.Models.Dtos;

namespace Catalog.Host.Data
{
    public class BasketItems<T>
    {
        public List<T> Items { get; set; }
    }
}
