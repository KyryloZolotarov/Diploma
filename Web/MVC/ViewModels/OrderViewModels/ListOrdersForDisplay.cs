namespace MVC.ViewModels.OrderViewModels;

public class ListOrdersForDisplay
{
    public List<OrderForDisplay> Orders { get; set; }

    public CurrentUser User { get; set; }
}