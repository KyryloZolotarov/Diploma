namespace MVC.ViewModels;

public record CatalogBrand
{
    public int Id { get; set; }

    public string Brand { get; set; } = null!;
}