using BisleriumCafe.Data.Utils;
using BisleriumCafe.Data.Models;
using QuestPDF.Fluent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using MudBlazor;
using QuestPDF.Helpers;
using static MudBlazor.CategoryTypes;

namespace BisleriumCafe.Data.Services
{
    public static class PdfGenerationServices
    {
        public static void SaveDailyPDF(List<Sales> deserializedData, string reportDate, List<SalesService.DrinkSales> topFiveDrinkSales, List<SalesService.AddInSales> topFiveAddInSales, float grandTotal)
        {
            var filePath = Explorer.GetSalesFilePath();
            if (File.Exists(filePath))
            {
                var json = File.ReadAllText(filePath);
                if (json.Trim().Length > 0)
                {
                    if (deserializedData != null)
                    {
                        var appPath = Explorer.GetAppDirectoryPath();
                        Document.Create(container =>
                        {
                            container.Page(page =>
                            {
                                page.Size(PageSizes.A4);
                                page.Margin(20);
                                page.DefaultTextStyle(x => x.FontSize(10));
                                page.Header().Text($"Sales Report: {reportDate}");


                                page.Content().Column(column =>
                                {
                                    column.Item().Row(row =>
                                    {
                                        // Top 5 selling coffees
                                        row.RelativeItem().Column(column =>
                                        {
                                            column.Item().Text("Top 5 selling coffees");
                                            column.Item().Table(table =>
                                            {
                                                table.ColumnsDefinition(column =>
                                                {
                                                    column.RelativeColumn();
                                                    column.RelativeColumn();
                                                });

                                                table.Header(header =>
                                                {
                                                    header.Cell().Text("Drink Name");
                                                    header.Cell().Text("Total Sales Count");

                                                    
                                                });

                                                foreach (var drink in topFiveDrinkSales)
                                                {
                                                    table.Cell().Text(drink.DrinkName);
                                                    table.Cell().Text(drink.Quantity.ToString());
                                                }

                                                table.Footer(footer =>
                                                {
                                                    footer.Cell().Text("Total");
                                                 });
                                            });
                                        });

                                        // Top 5 selling addins
                                        row.RelativeItem().Column(column =>
                                        {
                                            column.Item().Text("Top 5 selling addins");
                                        column.Item().Table(table =>
                                        {
                                            table.ColumnsDefinition(column =>
                                            {
                                                column.RelativeColumn();
                                                column.RelativeColumn();
                                            });

                                            table.Header(header =>
                                            {
                                                header.Cell().Text("Addin Name");
                                                header.Cell().Text("Total Sales Count");
                                            });

                                            foreach (var topSeller in topFiveAddInSales)
                                            {
                                                table.Cell().Text(topSeller.AddInName);
                                                table.Cell().Text(topSeller.Quantity.ToString());
                                            }

                                            table.Footer(footer =>
                                            {
                                                footer.Cell().Text("Total");
                                            });
                                        });
                                    });
                                });
                                    // All sales report
                                    column.Item().Table(table =>
                                    {
                                        table.ColumnsDefinition(column =>
                                        {
                                            column.RelativeColumn();
                                            column.RelativeColumn();
                                            column.RelativeColumn();
                                            column.RelativeColumn();
                                            column.RelativeColumn();
                                        });

                                        table.Header(header =>
                                        {
                                            header.Cell().Text("Drink Name");
                                            header.Cell().Text("Member");
                                            header.Cell().Text("Phone Number");
                                            header.Cell().Text("Date");
                                            header.Cell().Text("Total Amount");
                                        });

                                        foreach (var item in deserializedData)
                                        {
                                            table.Cell().Text(item.SalesId);
                                            table.Cell().Text(item.MemberId);
                                            table.Cell().Text(item.Number);
                                            table.Cell().Text(item.OrderDate.ToString("yyyy-mm-dd"));
                                            table.Cell().Text(item.OrderTotal);
                                        }
                                    });

                                });


                                page.Footer().Text(text =>
                                {
                                    text.Span($"Revenue: {grandTotal}");
                                    text.CurrentPageNumber();
                                });
                            });
                        }).GeneratePdf(Path.Combine(appPath,$"{reportDate}_dailyReport.pdf"));
                    }
                }
            }
        }

        public static void SaveMonthlyPDF(List<Sales> deserializedData, string reportDate, List<SalesService.DrinkSales> topFiveDrinkSales, List<SalesService.AddInSales> topFiveAddInSales, float grandTotal)
        {
            var filePath = Explorer.GetSalesFilePath();
            if (File.Exists(filePath))
            {
                var json = File.ReadAllText(filePath);
                if (json.Trim().Length > 0)
                {
                    if (deserializedData != null)
                    {
                        var appPath = Explorer.GetAppDirectoryPath();
                        Document.Create(container =>
                        {
                            container.Page(page =>
                            {
                                page.Size(PageSizes.A4);
                                page.Margin(20);
                                page.DefaultTextStyle(x => x.FontSize(10));
                                page.Header().Text($"Sales Report: {reportDate}");


                                page.Content().Column(column =>
                                {
                                    column.Item().Row(row =>
                                    {
                                        // Top 5 selling coffees
                                        row.RelativeItem().Column(column =>
                                        {
                                            column.Item().Text("Top 5 selling coffees");
                                            column.Item().Table(table =>
                                            {
                                                table.ColumnsDefinition(column =>
                                                {
                                                    column.RelativeColumn();
                                                    column.RelativeColumn();
                                                });

                                                table.Header(header =>
                                                {
                                                    header.Cell().Text("Drink Name");
                                                    header.Cell().Text("Total Sales Count");


                                                });

                                                foreach (var drink in topFiveDrinkSales)
                                                {
                                                    table.Cell().Text(drink.DrinkName);
                                                    table.Cell().Text(drink.Quantity.ToString());
                                                }

                                                table.Footer(footer =>
                                                {
                                                    footer.Cell().Text("Total");
                                                });
                                            });
                                        });

                                        // Top 5 selling addins
                                        row.RelativeItem().Column(column =>
                                        {
                                            column.Item().Text("Top 5 selling addins");
                                            column.Item().Table(table =>
                                            {
                                                table.ColumnsDefinition(column =>
                                                {
                                                    column.RelativeColumn();
                                                    column.RelativeColumn();
                                                });

                                                table.Header(header =>
                                                {
                                                    header.Cell().Text("Addin Name");
                                                    header.Cell().Text("Total Sales Count");
                                                });

                                                foreach (var topSeller in topFiveAddInSales)
                                                {
                                                    table.Cell().Text(topSeller.AddInName);
                                                    table.Cell().Text(topSeller.Quantity.ToString());
                                                }

                                                table.Footer(footer =>
                                                {
                                                    footer.Cell().Text("Total");
                                                });
                                            });
                                        });
                                    });
                                    // All sales report
                                    column.Item().Table(table =>
                                    {
                                        table.ColumnsDefinition(column =>
                                        {
                                            column.RelativeColumn();
                                            column.RelativeColumn();
                                            column.RelativeColumn();
                                            column.RelativeColumn();
                                            column.RelativeColumn();
                                        });

                                        table.Header(header =>
                                        {
                                            header.Cell().Text("Drink Name");
                                            header.Cell().Text("Member");
                                            header.Cell().Text("Phone Number");
                                            header.Cell().Text("Date");
                                            header.Cell().Text("Total Amount");
                                        });

                                        foreach (var item in deserializedData)
                                        {
                                            table.Cell().Text(item.SalesId);
                                            table.Cell().Text(item.MemberId);
                                            table.Cell().Text(item.Number);
                                            table.Cell().Text(item.OrderDate.ToString("yyyy-mm-dd"));
                                            table.Cell().Text(item.OrderTotal);
                                        }
                                    });

                                });


                                page.Footer().Text(text =>
                                {
                                    text.Span($"Revenue: {grandTotal}");
                                    text.CurrentPageNumber();
                                });
                            });
                        }).GeneratePdf(Path.Combine(appPath, $"{reportDate}_monthlyReport.pdf"));
                    }
                }
            }
        }

    }
}