using Elastic.Clients.Elasticsearch;
using ES.Common.Models;

namespace ES.BLL.Interfaces
{
    public interface IElasticsearchProductService : IElasticsearchService<Product>
    {
        Task<SearchResponse<Product>> GetProductByDescriptionTermAsync(string descriptionTerm);
        Task<SearchResponse<Product>> GetProductByDescriptionPhraseAsync(string descriptionFullText);
        Task<SearchResponse<Product>> GetProductByTextAsync(string fullText, double price);
    }
}
