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

namespace BisleriumCafe.Data.Services
{
    public static class PDFService
    {
        public static void SavePDF()
        {
            var filePath = Explorer.GetSalesFilePath();
            if (File.Exists(filePath))
            {
                var json = File.ReadAllText(filePath);
                if (json.Trim().Length > 0)
                {
                    var deserializedData = JsonSerializer.Deserialize<List<Sales>>(json);
                    if (deserializedData != null)
                    {
                        var appPath = Explorer.GetAppDirectoryPath();
                        Document.Create(container =>
                        {
                            container.Page(page =>
                            {
                                page.Header().Text("Sales");

                                page.Content().Table(table =>
                                {
                                    table.ColumnsDefinition(column =>
                                    {
                                        column.RelativeColumn();
                                        column.RelativeColumn();
                                    });

                                    table.Header(header =>
                                    {
                                        header.Cell().Text("Total");
                                        header.Cell().Text("Salesid");
                                    });

                                    foreach (var item in deserializedData)
                                    {
                                        table.Cell().Text(item.OrderTotal);
                                        table.Cell().Text(item.ToString());
                                    }
                                });

                                page.Footer().Text(text =>
                                {
                                    text.Span("Page :");
                                    text.CurrentPageNumber();
                                });
                            });
                        }).GeneratePdf(Path.Combine(appPath, "Users.pdf"));
                    }
                }
            }
        }
    }
}
