using Newtonsoft.Json;
using System.Net.Http.Json;
using xUnit.Common.Dtos;
using xUnit.DAL.Models;

namespace xUnit.IntegrationTests.Tests
{
    public class CustomerTests : BaseIntegrationTest
    {
        public CustomerTests(CustomWebAppFactory factory) : base(factory)
        {
        }

        [Fact]
        public async Task Create_Customer()
        {
            // Arrange
            var expected = new CreateCustomerDto
            {
                CustomerName = "Test",
                Address = "Address test"
            };

            // Act
            var response = await httpClient.PostAsJsonAsync("/api/Customers", expected);

            // Assert
            string content = await response.Content.ReadAsStringAsync();
            var customerId = JsonConvert.DeserializeObject<Guid>(content);
            var actual = dbContext.Customers.FirstOrDefault(x => x.Id == customerId);

            Assert.NotNull(actual);
            Assert.Equal(expected.CustomerName, actual.Name);
            Assert.Equal(expected.Address, actual.Address);
        }
        [Fact]
        public async Task Get_Customer()
        {
            // Arrange
            var expected = new List<Customer>
            {
                new Customer
                {
                    Id = Guid.NewGuid(),
                    Name = "Test",
                    Address = "Address test"
                },
                new Customer
                {
                    Id = Guid.NewGuid(),
                    Name = "Test2",
                    Address = "Address test2"
                },
                new Customer
                {
                    Id = Guid.NewGuid(),
                    Name = "Test3",
                    Address = "Address test3"
                },
                new Customer
                {
                    Id = Guid.NewGuid(),
                    Name = "Test4",
                    Address = "Address test4"
                },
            };
            await dbContext.Customers.AddRangeAsync(expected);
            await dbContext.SaveChangesAsync();

            // Act
            var actual = await httpClient.GetFromJsonAsync<List<CustomerDto>>("/api/Customers");

            // Assert
            Assert.NotNull(actual);

            expected.Sort((x, y) => x.Id.CompareTo(y.Id));
            actual.Sort((x, y) => x.CustomerId.CompareTo(y.CustomerId));
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.NotNull(actual[i]);
                Assert.Equal(expected[i].Id, actual[i].CustomerId);
                Assert.Equal(expected[i].Name, actual[i].CustomerName);
                Assert.Equal(expected[i].Address, actual[i].Address);
            }
        }
    }
}
