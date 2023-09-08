using MVC.Models.Responses;
using MVC.ViewModels.CatalogViewModels;

namespace MVC.ViewModels.OrderViewModels
{
    public class OrderItemFordDisplay
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int CatalogSubTypeId { get; set; }
        public CatalogSubType CatalogSubType { get; set; }
        public int CatalogModelId { get; set; }
        public CatalogModel CatalogModel { get; set; }
        public int Count { get; set; }
        public int OrderId { get; set; }
        public OrderForDisplay Order { get; set; }
    }
}
