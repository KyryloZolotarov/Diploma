namespace MVC.ViewModels
{
    public record BasketItem
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public CatalogSubType CatalogSubType { get; set; }

        public CatalogModel CatalogModel { get; set; }
    }
}
