using Infrastructure.Services.Interfaces;
using MVC.Models.Requests;
using MVC.Models.Responses;
using MVC.Repositories.Interfaces;
using MVC.Services.Interfaces;
using MVC.ViewModels.CatalogViewModels;
using MVC.ViewModels.OrderViewModels;

namespace MVC.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly ICatalogRepository _catalogRepository;
    private readonly IBasketRepository _basketRepository;

    public OrderService(IBasketRepository basket,  ICatalogRepository catalog, IOrderRepository orderRepository)
    {
        _basketRepository = basket;
        _catalogRepository = catalog;
        _orderRepository = orderRepository;
    }

    public async Task<bool> AddOrder(ListOrderItemsFordDisplay order)
    {
        var orderForDb = new ListOrderItemsRequest { Items = new List<OrderItemRequest>(), DateTime = order.DateTime };

        var result2 = await _catalogRepository.ChangeAvailableItems(orderForDb);

        if(result2 == false) { return false; }
        foreach (var item in order.Items)
            orderForDb.Items.Add(new OrderItemRequest
            {
                Id = item.Id,
                Name = item.Name,
                Count = item.Count,
                CatalogModelId = item.CatalogModelId,
                CatalogSubTypeId = item.CatalogSubTypeId,
                OrderId = item.OrderId,
                Price = item.Price,
                Order = new OrderToDb
                {
                    DateTime = order.DateTime
                }
            });
        var result = await _orderRepository.AddOrder(orderForDb);
        if (result) await _basketRepository.ClearBasket();
        return result;
    }

    public async Task<ListOrderItemsFordDisplay> GetOrder(int id)
    {
        var result = await _orderRepository.GetOrder(id);

        var orderFordDisplay = new ListOrderItemsFordDisplay
            { Items = new List<OrderItemFordDisplay>(), DateTime = result.Order.DateTime };
        var modelIds = new CatalogModelForOrderRequest { Id = new List<int>() };

        foreach (var item in result.Items)
        {
            modelIds.Id.Add(item.CatalogModelId);
            orderFordDisplay.Items.Add(new OrderItemFordDisplay
            {
                Id = item.Id,
                Name = item.Name,
                Count = item.Count,
                CatalogModelId = item.CatalogModelId,
                CatalogSubTypeId = item.CatalogSubTypeId,
                OrderId = item.OrderId, Price = item.Price,
                Order = new OrderForDisplay
                {
                    Id = item.Order.Id,
                    UserId = item.Order.UserId,
                    DateTime = item.Order.DateTime
                }
            });
        }

        var modelsResult = await _catalogRepository.GetItemsForOrder(modelIds);

        foreach (var item in orderFordDisplay.Items)
        {
            var tempModel = modelsResult.Models.FirstOrDefault(x => x.Id == item.CatalogModelId);
            if (tempModel != null)
            {
                item.CatalogModel = tempModel;
                continue;
            }

            item.CatalogModel = new CatalogModel();
        }

        return orderFordDisplay;
    }

    public async Task<ListOrdersForDisplay> GetOrderList()
    {
        var result = await _orderRepository.GetOrderList();
        var ordersForDisplay = new ListOrdersForDisplay { Orders = new List<OrderForDisplay>() };
        if (result == null) return ordersForDisplay;
        foreach (var item in result.Orders)
            ordersForDisplay.Orders.Add(new OrderForDisplay
                { Id = item.Id, UserId = item.UserId, DateTime = item.DateTime });
        return ordersForDisplay;
    }
}