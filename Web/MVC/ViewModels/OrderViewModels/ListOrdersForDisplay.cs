namespace MVC.ViewModels.OrderViewModels;

public class ListOrdersForDisplay
{
    public List<OrderForDisplay> Orders { get; set; }

    public OrderUserForDisplay User { get; set; }
}