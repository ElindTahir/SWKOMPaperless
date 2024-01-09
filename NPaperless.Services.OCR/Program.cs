using System.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NPaperless.QueueLibrary; // Ersetzen Sie dies durch den tatsächlichen Namensraum Ihres QueueLibrary
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NPaperless.DataAccess.Sql;
using NPaperless.OCRLibrary;
using NPaperless.SearchLibrary;
using NPaperless.Services.MinIO;
using NPaperless.Services.OCR;
using Document = NPaperless.DataAccess.Entities.Document;


IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        // Elasticsearch
        services.AddScoped<ElasticSearchIndex>();
        
        // Konfigurieren Sie QueueOptions mit den Einstellungen aus der appsettings.json
        services.Configure<QueueOptions>(hostContext.Configuration.GetSection("Queue"));

        // Laden der Konfiguration für OcrOptions
        services.Configure<OcrOptions>(hostContext.Configuration.GetSection("OCR"));

        // Fügen Sie den QueueConsumer-Dienst hinzu
        services.AddSingleton<IQueueConsumer, QueueConsumer>();
        
        services
            .AddDbContext<NPaperlessDbContext>(options =>
            {
                options.UseNpgsql(hostContext.Configuration.GetConnectionString("NPaperlessDatabase"));
            });
        
        // ... innerhalb der ConfigureServices-Methode ...
        services.AddScoped<IRepository<Document>, DocumentRepository>();
        
        // Registrierung des OcrClient-Dienstes
        services.AddSingleton<IOcrClient, OcrClient>(sp => 
        {
            // Abrufen der OcrOptions aus IOptions
            var ocrOptions = sp.GetRequiredService<IOptions<OcrOptions>>().Value;
            return new OcrClient(ocrOptions);
        });

        services.AddSingleton<FileUpload>(); // Ihr Service zum Herunterladen der Dateien

        // Registrierung des WorkerOCR-Dienstes
        services.AddHostedService<WorkerOCR>();
    })
    .Build();

await host.RunAsync();