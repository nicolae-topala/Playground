using Elastic.Clients.Elasticsearch;
using ES.BLL.Interfaces;

namespace ES.BLL.Services
{
    public class ElasticsearchService<TDocument> : IElasticsearchService<TDocument>
    {
        protected readonly ElasticsearchClient _client;

        public ElasticsearchService(ElasticsearchClient elasticsearchClient)
        {
            _client = elasticsearchClient;
        }

        public async Task<GetResponse<TDocument>> GetByIdAsync(string documentId) =>
            await _client.GetAsync<TDocument>(documentId);

        public async Task<CreateResponse> CreateAsync(TDocument document) =>
            await _client.CreateAsync(new CreateRequestDescriptor<TDocument>(document));

        public async Task<DeleteResponse> DeleteByIdAsync(Guid productId) =>
            await _client.DeleteAsync<TDocument>(productId);

        public async Task<UpdateResponse<TDocument>> UpdateByIdAsync(Guid productId, TDocument document) =>
            await _client.UpdateAsync(new UpdateRequestDescriptor<TDocument, object>(productId).Doc(document));
    }
}
