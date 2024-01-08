using BisleriumCafe.Data.Utils;
using BisleriumCafe.Data.Enums;
using BisleriumCafe.Data.Models;
using System.Text.Json;
using System.Data;

namespace BisleriumCafe.Data.Services;

public static class SalesService
{
    private static void SaveAll(IEnumerable<Sales> sales)
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

    public static IEnumerable<Sales> GetAll()
    {
        string salesFilePath = Explorer.GetSalesFilePath();
        if (!File.Exists(salesFilePath))
        {
            return new List<Sales>();
        }

        var json = File.ReadAllText(salesFilePath);

        return JsonSerializer.Deserialize<IEnumerable<Sales>>(json);
    }


    public static IEnumerable<Sales> CreateNormalSale(string number, DateTime orderDate, List<OrderItem> orderItems, float orderTotal)
    {

        IEnumerable<Sales> sales = GetAll();
        
        sales = sales.Append(
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

    public static IEnumerable<Sales> CreateMemberSale(Guid memberId, string number, DateTime orderDate, List<OrderItem> orderItems, float orderTotal)
    {

        IEnumerable<Sales> sales = GetAll();

        sales = sales.Append(
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
        IEnumerable<Sales> sales = GetAll();

        return sales.FirstOrDefault(x => x.SalesId.ToString() == saleid);
    }


}
