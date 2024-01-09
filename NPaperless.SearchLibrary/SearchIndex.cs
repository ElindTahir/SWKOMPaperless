namespace NPaperless.SearchLibrary;

using System.Diagnostics;
using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.QueryDsl;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

public class ElasticSearchIndex : ISearchIndex
{
    private readonly Uri _uri;
    private readonly ILogger<ElasticSearchIndex> _logger;
    private readonly ElasticsearchClient _elasticClient;

    public ElasticSearchIndex(IConfiguration configuration, ILogger<ElasticSearchIndex> logger)
    {
        this._uri = new Uri(configuration.GetConnectionString("ElasticSearch") ?? "http://elastic_search:9200/");
        this._logger = logger;
        this._elasticClient = new ElasticsearchClient(_uri);
    }
    public void AddDocumentAsync(Document document)
    {

        if (!_elasticClient.Indices.Exists("documents").Exists)
            _elasticClient.Indices.Create("documents");

        var indexResponse = _elasticClient.Index(document, "documents");
        if (!indexResponse.IsSuccess())
        {
            // Handle errors
            _logger.LogError($"Failed to index document: {indexResponse.DebugInformation}\n{indexResponse.ElasticsearchServerError}");

            throw new Exception($"Failed to index document: {indexResponse.DebugInformation}\n{indexResponse.ElasticsearchServerError}");
        }
    }

    public IEnumerable<Document> SearchDocumentAsync(string searchTerm, int limit)
    {
        var searchResponse = _elasticClient.Search<Document>(s => s
            .Index("documents")
            .Query(q => q.QueryString(qs => qs.DefaultField(p => p.Content).Query($"*{searchTerm}*")))
        );

        return searchResponse.Documents;
    }
}


