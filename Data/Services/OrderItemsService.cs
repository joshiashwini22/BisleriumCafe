using BisleriumCafe.Data.Models;
using BisleriumCafe.Data.Utils;
using BisleriumCafe.Data.Enums;
using BisleriumCafe.Data.Models;
using System.Text.Json;

namespace BisleriumCafe.Data.Services
{
    public static class OrderItemsService
    {
        public static void AddItemInOrderItemList(List<OrderItem> orderItems, Guid itemId, string itemName, string itemType, float itemPrice)
        {
            OrderItem orderItem = orderItems.FirstOrDefault(x => x.ItemId.ToString() == itemId.ToString() && x.ItemType == itemType);
            if (orderItem != null)
            {
                orderItem.Quantity++;
                float tp = (float)orderItem.Quantity * itemPrice;
                orderItem.TotalPrice = tp;

                return;
            }

            orderItem = new()
            {
                ItemId = itemId,
                ItemType = itemType,
                ItemName = itemName,
                ItemPrice = itemPrice,
                Quantity = 1,
                TotalPrice = itemPrice

            };
            orderItems.Add(orderItem);
        }
        private static void SaveAll(List<OrderItem> orderItems)
        {
            string appDataDirectoryPath = Explorer.GetAppDirectoryPath();
            string orderItemsFilePath = Explorer.GetOrderItemsFilePath();

            if (!Directory.Exists(appDataDirectoryPath))
            {
                Directory.CreateDirectory(appDataDirectoryPath);
            }

            var json = JsonSerializer.Serialize(orderItems);
            File.WriteAllText(orderItemsFilePath, json);
        }

        public static List<OrderItem> GetAll()
        {
            string orderItemsFilePath = Explorer.GetOrderItemsFilePath();
            if (!File.Exists(orderItemsFilePath))
            {
                return new List<OrderItem>();
            }

            var json = File.ReadAllText(orderItemsFilePath);

            return JsonSerializer.Deserialize<List<OrderItem>>(json);
        }
        public static void DeleteItemInCart(List<OrderItem> _orderItems, String itemId)
        {
            OrderItem item = _orderItems.FirstOrDefault(x => x.OrderItemId.ToString() == itemId);

            if (item != null)
            {
                _orderItems.Remove(item);
            }
        }
        
        public static float CalculateItemListTotal(IEnumerable<OrderItem> Elements)
        {
            float listTotal = 0;
            foreach (var element in Elements)
            {
                listTotal += element.TotalPrice;
            }
            return listTotal;
        }
    }
}
