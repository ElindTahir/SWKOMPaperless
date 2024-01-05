using NPaperless.OCRLibrary;
using NPaperless.Services.MinIO;

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

    public WorkerOCR(ILogger<WorkerOCR> logger, 
        IQueueConsumer queueConsumer,
        IOcrClient ocrClient,
        FileUpload fileUpload)
    {
        _logger = logger;
        _queueConsumer = queueConsumer;
        _ocrClient = ocrClient;
        _fileUpload = fileUpload; // Speichern Sie die Referenz zum Service
        _queueConsumer.OnReceived += OnReceived;
        
    }
    
    private async void OnReceived(object sender, QueueReceivedEventArgs e)
    {
        // Herunterladen der Datei als Stream
        try
        {
            using (var fileStream = await _fileUpload.DownloadFileAsync(e.Content))
            {
                // Führen Sie hier die OCR-Verarbeitung durch
                var ocrResult = _ocrClient.OcrPdf(fileStream);
                // Führen Sie die gewünschten Aktionen mit dem OCR-Ergebnis durch
                _logger.LogInformation(ocrResult);
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