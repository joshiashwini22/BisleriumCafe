using BisleriumCafe.Data.Utils;
using BisleriumCafe.Data.Enums;
using BisleriumCafe.Data.Models;
using System.Text.Json;
using System.Data;

namespace BisleriumCafe.Data.Services;

public static class SalesService
{
    private static void SaveAll(List<Sales> sales)
    {
        string appDataDirectoryPath = Explorer.GetAppDirectoryPath();
        string salesFilePath = Explorer.GetSalesFilePath();

        if (!Directory.Exists(appDataDirectoryPath))
        {
            Directory.CreateDirectory(appDataDirectoryPath);
        }

        var json = JsonSerializer.Serialize(sales);
        File.WriteAllText(salesFilePath, json);
    }

    public static List<Sales> GetAll()
    {
        string salesFilePath = Explorer.GetSalesFilePath();
        if (!File.Exists(salesFilePath))
        {
            return new List<Sales>();
        }

        var json = File.ReadAllText(salesFilePath);

        return JsonSerializer.Deserialize<List<Sales>>(json);
    }


    public static List<Sales> CreateNormalSale(string number, DateTime orderDate, List<OrderItem> orderItems, float orderTotal)
    {

        List<Sales> sales = GetAll();

        sales.Add(
            new Sales
            {
                Number = number,
                OrderDate = orderDate,
                OrderTotal = orderTotal,
                OrderItems = orderItems
            }
        );
        SaveAll(sales);
        return sales;
    }

    public static List<Sales> CreateMemberSale(Guid memberId, string number, DateTime orderDate, List<OrderItem> orderItems, float orderTotal)
    {

        List<Sales> sales = GetAll();

        sales.Add(
            new Sales
            {
                MemberId = memberId,
                Number = number,
                OrderDate = orderDate,
                OrderTotal = orderTotal,
                OrderItems = orderItems
            }
        );

        SaveAll(sales);
        return sales;
    }

    public static Sales GetSalesByID(string saleid)
    {
        List<Sales> sales = GetAll();

        return sales.FirstOrDefault(x => x.SalesId.ToString() == saleid);
    }

    public static IEnumerable<Sales> GetSalesItemByProductType(Enum productType)
    {
        List<Sales> sales = GetAll();

        return sales
            .Where(s => s.OrderItems.Any(item => item.ItemType == productType.ToString()))
            .Select(s => new Sales
            {
                SalesId = s.SalesId,
                OrderDate = s.OrderDate,
                MemberId = s.MemberId,
                Number = s.Number,
                OrderTotal = s.OrderTotal,
                OrderItems = s.OrderItems.Where(item => item.ItemType == productType.ToString()).ToList()
            });
    }
    public static bool IsRegularMember(string phoneNumber)
    {
        List<Sales> sales = GetAll();

        var currentMonthOrders = sales
            .Where(sale => sale.Number is null ? false : sale.Number.Equals(phoneNumber) && sale.OrderDate.Month.Equals(DateTime.Now.Month))
            .ToList();


        if (currentMonthOrders.Count >= 15)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private static float GetSalesRevenue(List<Sales> sales)
    {
        float revenue = 0f;
        foreach (var sale in sales)
        {
            revenue += sale.OrderTotal;
        }

        return revenue;
    }

    //private static List<Sales> GetTopFiveDrinks(List<Sales> sales) 
    //{ 
    //    return sales
    //    .SelectMany(s => s.OrderItems)
    //    .Where(item => item.ItemType.Equals("Drink"))
    //    .GroupBy(item => item.ItemName)
    //    .Select(group => new OrderItem
    //    {
    //        ItemName = group.Key,
    //        Quantity = group.Sum(item => item.Quantity)
    //    })
    //    .OrderByDescending(item => item.Quantity)
    //    .Take(5)
    //    .ToList();
    //}
}