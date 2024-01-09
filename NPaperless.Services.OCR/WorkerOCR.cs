using System.Text.Json;
using NPaperless.OCRLibrary;
using NPaperless.Services.MinIO;
using NPaperless.DataAccess.Sql;
using NPaperless.SearchLibrary;
using Document = NPaperless.DataAccess.Entities.Document;

namespace NPaperless.Services.OCR;

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using NPaperless.QueueLibrary;

public class WorkerOCR : BackgroundService
{
    private readonly ILogger<WorkerOCR> _logger;
    private readonly IQueueConsumer _queueConsumer;
    private readonly IOcrClient _ocrClient;
    private readonly FileUpload _fileUpload; // Ihr Service zum Herunterladen der Datei
    private readonly IRepository<Document> _documentRepository;
    // add elastic search
    private readonly ElasticSearchIndex _elasticSearchIndex;

    public WorkerOCR(ILogger<WorkerOCR> logger,
        IQueueConsumer queueConsumer,
        IOcrClient ocrClient,
        FileUpload fileUpload,
        IRepository<Document> documentRepository,
        ElasticSearchIndex elasticSearchIndex)
    {
        _logger = logger;
        _queueConsumer = queueConsumer;
        _ocrClient = ocrClient;
        _fileUpload = fileUpload; // Speichern Sie die Referenz zum Service
        _documentRepository = documentRepository;
        _queueConsumer.OnReceived += OnReceived;
        _elasticSearchIndex = elasticSearchIndex;
    }
    
    private async void OnReceived(object sender, QueueReceivedEventArgs e)
    {
        // Herunterladen der Datei als Stream
        try
        {
            using (var fileStream = await _fileUpload.DownloadFileAsync(e.Content))
            {
                var ocrResult = _ocrClient.OcrPdf(fileStream);
                
                _logger.LogInformation(ocrResult);
                
                var fileName = e.Content;

                _documentRepository.UpdateContentByFileName(fileName, ocrResult);
                
                _elasticSearchIndex.AddDocumentAsync(new NPaperless.SearchLibrary.Document
                {
                    Content = ocrResult,
                    Title = fileName
                });
                _logger.LogInformation($"OCR erfolgreich für Datei: {fileName}");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Fehler beim Herunterladen oder Verarbeiten der Datei.");
        }
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
        _queueConsumer.StartReceive();
        while (!stoppingToken.IsCancellationRequested)
        {
            // Worker Logik hier. Zum Beispiel, überwachen Sie die Queue für neue Nachrichten.
            await Task.Delay(1000, stoppingToken);
        }
        _queueConsumer.StopReceive();
    }
}

public class MyMessageFormat
{
    public string FileName { get; set; }
    // Fügen Sie hier weitere Eigenschaften hinzu, die Ihre Nachrichtenstruktur erfordert.
}