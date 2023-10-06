using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using RestApp.BL.Interfaces;
using RestApp.Common.Constants.MemoryCache;
using RestApp.Common.Dtos;
using RestApp.Dal.Interfaces;
using RestApp.DemoMigrations;
using RestApp.Domain.Models;

namespace RestApp.BL.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMemoryCache _memoryCache;
        private readonly IDistributedCache _distributedCache;
        private readonly DemoDatabaseContext _dbContext;

        public CustomerService(
            ICustomerRepository customerRepository, 
            IMemoryCache memoryCache,
            IDistributedCache distributedCache,
            DemoDatabaseContext dbContext) 
        {
            _customerRepository = customerRepository;
            _memoryCache = memoryCache;
            _distributedCache = distributedCache;
            _dbContext = dbContext;
        }

        public async Task<List<CustomerDto>> GetAllCustomersAsync()
        {
            var isCached = _memoryCache.TryGetValue(MemoryCacheValues.Customers, out List<CustomerDto>? result);

            if (!isCached)
            {
                result = await _customerRepository.GetCustomersAsync();

                //If the cache entry isn't accessed for more than 3 seconds, it gets evicted from the cache.
                //Each time the cache entry is accessed, it remains in the cache for a further 3 seconds.
                //If decided to use this, set an abosulute time for expiration, to garuantee the eviction of the cache.
                //var cacheEntryOptions = new MemoryCacheEntryOptions()
                //    .SetSlidingExpiration(TimeSpan.FromSeconds(3));

                _memoryCache.Set(MemoryCacheValues.Customers, result, TimeSpan.FromSeconds(10));
            }

            return result;
        }

        public async Task<List<CustomerDto>> GetAllCustomersRedisAsync()
        {
            string? cachedMember = await _distributedCache.GetStringAsync(MemoryCacheValues.Customers);
            List<CustomerDto> result;

            if (string.IsNullOrEmpty(cachedMember))
            {
                result = await _customerRepository.GetCustomersAsync();

                await _distributedCache.SetStringAsync(MemoryCacheValues.Customers, JsonConvert.SerializeObject(result));
                return result;
            }

            result = JsonConvert.DeserializeObject<List<CustomerDto>>(cachedMember);
            return result;
        }
        public async Task<Customer> GetCustomerByIdAsync(int customerId)
        {
            var result = await _customerRepository.GetCustomerByIdAsync(customerId);

            return result;
        }
    }
}
