namespace MVC.ViewModels
{
    public record CatalogModel
    {
        public int Id { get; set; }

        public string Model { get; set; }

        public CatalogBrand CatalogBrand { get; set; }
    }
}
