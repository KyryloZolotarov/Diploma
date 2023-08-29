namespace MVC.ViewModels
{
    public class CatalogModel
    {
        public int Id { get; set; }

        public string Model { get; set; }

        public CatalogBrand CatalogBrand { get; set; }
    }
}
