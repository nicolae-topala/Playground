using Elastic.Clients.Elasticsearch;

namespace ES.BLL.Interfaces
{
    public interface IElasticsearchService<TDocument>
    {
        Task<GetResponse<TDocument>> GetByIdAsync(string documentId);
        Task<CreateResponse> CreateAsync(TDocument document);
        Task<DeleteResponse> DeleteByIdAsync(Guid productId);
        Task<UpdateResponse<TDocument>> UpdateByIdAsync(Guid productId, TDocument document);
    }
}
