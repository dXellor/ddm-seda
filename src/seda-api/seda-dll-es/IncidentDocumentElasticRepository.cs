using Microsoft.Extensions.Configuration;
using Elastic.Clients.Elasticsearch;
using seda_dll_es.Contracts;
using seda_dll_es.Models;

namespace seda_dll_es;

public class IncidentDocumentElasticRepository: IElasticRepository<ESIncidentDocument>
{
    private readonly ElasticsearchClient _client;
    private readonly IConfiguration _configuration;
    private const string _indexName = "incident_documents";

    public IncidentDocumentElasticRepository( IConfiguration configuration )
    {
        _configuration = configuration;
        _client = new ElasticsearchClient( new Uri( _configuration["ES:Host"]! ) );
    }
    
    public async Task<bool> IndexDocumentAsync(ESIncidentDocument esIncidentDocument)
    {
        await CreateIndexIfDoesntExist();
        var result = await _client.IndexAsync( esIncidentDocument, (IndexName) _indexName );
        return result.IsValidResponse;
    }

    public async Task<IEnumerable<ESIncidentDocument>> QueryDocuments()
    {
        throw new NotImplementedException();
    }
    
    private async Task CreateIndexIfDoesntExist()
    {
        await _client.Indices.CreateAsync(_indexName);
    }
}