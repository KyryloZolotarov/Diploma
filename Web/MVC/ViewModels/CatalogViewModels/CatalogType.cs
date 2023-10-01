namespace MVC.ViewModels.CatalogViewModels;

public record CatalogType
{
    public int Id { get; set; }

    public string Type { get; set; } = null!;
}