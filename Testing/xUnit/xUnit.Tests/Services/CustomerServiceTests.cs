using AutoFixture.Xunit2;
using FakeItEasy;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using xUnit.BL.Services;
using xUnit.Common.Dtos;
using xUnit.DAL.Interfaces;

namespace xUnit.Tests.Services
{
    public class CustomerServiceTests
    {
        [Theory, AutoMoqData]
        public async Task GetAllCustomersAsync_ShouldNotBeNull_ReturnsListOfCustomers(
            [Frozen] Mock<ICustomerRepository> mockRepository,
            CustomerService sut,
            List<CustomerDto> customerDtos)
        {
            // Arrange
            mockRepository.Setup(x => x.GetCustomersAsync())
                .ReturnsAsync(customerDtos);

            // Act
            var result = await sut.GetAllCustomersAsync();

            // Assert
            Assert.NotNull(result);
            result.Should().NotBeNullOrEmpty().And.BeEquivalentTo(customerDtos);
        }

        [Theory, AutoMoqData]
        public async Task GetCustomerByIdAsync_ShouldReturnCustomerBasedOnItsId_ReturnsCustomer(
            [Frozen] Mock<ICustomerRepository> mockRepository,
            CustomerService sut,
            CustomerDto customer)
        {
            // Arrange
            mockRepository.Setup(x => x.GetCustomerByIdAsync(customer.CustomerId))
                .ReturnsAsync(customer);

            // Act
            var result = await sut.GetCustomerByIdAsync(customer.CustomerId);

            // Assert
            Assert.Equal(customer, result);
            mockRepository.Verify(x => x.GetCustomerByIdAsync(It.IsAny<Guid>()), Moq.Times.Exactly(1));
            result.Should().BeEquivalentTo(customer).And.BeOfType<CustomerDto>();
        }

        [Fact]
        public async Task Fakeiteasy_GetAllCustomersAsync_ShouldNotBeNull_ReturnsListOfCustomers()
        {
            // Arrange
            var fakeLogger = A.Fake<ILogger<CustomerService>>();
            var fakeRepo = A.Fake<ICustomerRepository>();
            var fakeListCustomers = A.CollectionOfFake<CustomerDto>(5).ToList();
            var fakeCustomer = A.Fake<CustomerDto>();
            fakeListCustomers.Add(fakeCustomer);
            A.CallTo(() => fakeRepo.GetCustomersAsync()).Returns(fakeListCustomers);
            var sut = new CustomerService(fakeRepo, fakeLogger);

            // Act
            var result = await sut.GetAllCustomersAsync();

            // Assert
            result.Should().NotBeNullOrEmpty().And.HaveCount(6).And.Contain(fakeCustomer);
        }

        [Theory, AutoFakeItEasyData]
        public async Task Fakeiteasy_GetCustomerByIdAsync_CustomerById_ReturnsCustomer(
            [Frozen] Fake<ICustomerRepository> fakeRepository,
            CustomerService sut,
            CustomerDto expected)
        {
            // Arrange
            fakeRepository.CallsTo(x => x.GetCustomerByIdAsync(expected.CustomerId)).Returns(expected);

            // Act
            var result = await sut.GetCustomerByIdAsync(expected.CustomerId);

            // Assert
            result.Should().BeOfType<CustomerDto>().And.BeSameAs(expected);
            A.CallTo(() => fakeRepository.FakedObject.GetCustomerByIdAsync(expected.CustomerId)).MustHaveHappened();
        }
    }
}
