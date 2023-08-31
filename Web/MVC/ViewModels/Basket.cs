namespace MVC.ViewModels
{
    public record Basket
    {
        public int Count { get; init; }
        public List<BasketItem> Data { get; init; }
    }
}
