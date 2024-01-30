using Microsoft.Extensions.Logging;
using Moq;
using xUnit.BL.Services;
using xUnit.Common.Dtos;
using xUnit.DAL.Interfaces;

namespace Specflow.Specs.StepDefinitions
{
    [Binding]
    public class CustomerServiceStepDefinitions
    {
        private readonly CustomerService _sut;
        private readonly Mock<ICustomerRepository> _mock;
        private CustomerDto _result;

        public CustomerServiceStepDefinitions()
        {
            ILogger<CustomerService> logger = new Mock<ILogger<CustomerService>>().Object;
            _mock = new Mock<ICustomerRepository>();
            _sut = new(_mock.Object, logger);
        }

        [Given(@"there is a customer called (.*)")]
        public async void GivenThereIsACustomerCalled(string customerName)
        {
            var customerDto = new CreateCustomerDto
            {
                CustomerName = customerName
            };

            await _sut.CreateCustomerAsync(customerDto);
        }

        [When(@"I search for a customer called (.*)")]
        public async void WhenISearchForACustomerCalled(string customerName)
        {
            var customerDto = new CustomerDto
            {
                CustomerName = customerName
            };

            _mock.Setup(x => x.GetCustomerByNameAsync(It.IsAny<string>())).ReturnsAsync(customerDto);
            var result = await _sut.GetCustomerByNameAsync(customerName);
            _result = result;
        }

        [Then(@"the name of the customer returned should be (.*)")]
        public void ThenTheNameOfTheCustomerReturnedShouldBe(string customerName)
        {
            _result.CustomerName.Should().Be(customerName);
        }
    }
}
