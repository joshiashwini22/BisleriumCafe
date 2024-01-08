using BisleriumCafe.Data.Utils;
using BisleriumCafe.Data.Enums;
using BisleriumCafe.Data.Models;
using System.Text.Json;
using System.Data;

namespace BisleriumCafe.Data.Services;

public static class ProductsService
{
    private static void SaveAll(List<Product> products)
    {
        string appDataDirectoryPath = Explorer.GetAppDirectoryPath();
        string productsFilePath = Explorer.GetProductsFilePath();

        if (!Directory.Exists(appDataDirectoryPath))
        {
            Directory.CreateDirectory(appDataDirectoryPath);
        }

        var json = JsonSerializer.Serialize(products);
        File.WriteAllText(productsFilePath, json);
    }

    public static List<Product> GetAll()
    {
        string productsFilePath = Explorer.GetProductsFilePath();
        if (!File.Exists(productsFilePath))
        {
            return new List<Product>();
        }

        var json = File.ReadAllText(productsFilePath);

        return JsonSerializer.Deserialize<List<Product>>(json);
    }


    public static List<Product> Create(string productName, ProductType productType, float price)
    {

        List<Product> products = GetAll();
        bool itemExists = products.Any(x => x.ProductName == productName);

        if (itemExists)
        {
            throw new Exception("Item already exists.");
        }

        products.Add(
            new Product
            {
                ProductName = productName,
                ProductType = productType,
                Price = price
            }
        );
        SaveAll(products);
        return products;
    }
    
    public static List<Product> Delete(Guid productId)
    {
        List<Product> products = GetAll();
        Product product = products.FirstOrDefault(x => x.ProductId == productId);

        if (product == null)
        {
            throw new Exception("Product not found.");
        }

        products.Remove(product);
        SaveAll(products);
        return products;
    }

    public static List<Product> Update(String productId, float price)
    {
        List<Product> products = GetAll();
        Product productToUpdate = products.FirstOrDefault(x => x.ProductId.ToString() == productId);

        if (productToUpdate == null)
        {
            throw new Exception("Product not found.");
        }

        productToUpdate.Price = price;
        SaveAll(products);
        return products;
    }

    public static Product GetProductByID(string productId)
    {
        List<Product> products = GetAll();

        return products.FirstOrDefault(x => x.ProductId.ToString() == productId);
    }


}
