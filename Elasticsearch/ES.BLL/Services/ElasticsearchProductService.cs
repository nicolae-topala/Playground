using Elastic.Clients.Elasticsearch;
using ES.BLL.Interfaces;
using ES.Common.Models;
using Elastic.Clients.Elasticsearch.QueryDsl;

namespace ES.BLL.Services
{
    public class ElasticsearchProductService : ElasticsearchService<Product>, IElasticsearchProductService
    {
        public ElasticsearchProductService(ElasticsearchClient elasticsearchClient) : base(elasticsearchClient)
        {
        }

        public async Task<SearchResponse<Product>> GetProductByDescriptionTermAsync(string descriptionTerm) =>
            await _client.SearchAsync<Product>(x => x
                .From(0)
                .Size(10)
                .Query(q => q
                    .Term(t => t.Description, descriptionTerm)
                )
                .MinScore(0.1)
            );

        public async Task<SearchResponse<Product>> GetProductByDescriptionPhraseAsync(string descriptionFullText) =>
            await _client.SearchAsync<Product>(x => x
                .From(0)
                .Size(10)
                .Query(q => q
                    .MatchPhrase(m => m.Field(f => f.Description).Query(descriptionFullText))
                )
                .MinScore(0.1)
            );

        public async Task<SearchResponse<Product>> GetProductByTextAsync(string fullText, double price) =>
            await _client.SearchAsync<Product>(x => x
                .From(0)
                .Size(10)
                .Query(q => q
                    .Bool(b => b
                        .Should(m => m
                            .Match(mp => mp.Field(f => f.Name).Query(fullText).Operator(Operator.Or).Boost(2)), m => m
                            .Match(mp => mp.Field(f => f.Description).Query(fullText).Operator(Operator.Or))
                         )
                        .Filter(fi => fi
                            .Range(r => r.NumberRange(nr => nr
                                    .Field(f => f.Price)
                                    .Gt(price))
                            )
                        )
                    )
                )
                .MinScore(0.1)
            );
    }
}
