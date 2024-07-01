
using Billing.Management.Domain.Customer.Service;
using Billing.Management.Domain.UnitOfWork.Interface;
using FluentAssertions;
using Moq;
using System.Net;

namespace Billing.Management.Test.DomainTest.Customer
{
    public class CustomerDomainServiceTest
    {
        private readonly Mock<IUnitOfWork<Domain.Customer.Model.Customer>>? _mock;

        private Guid VALID_ID = Guid.NewGuid();
        private Guid INVALID_ID = Guid.Parse("87878787877");

        public CustomerDomainServiceTest()
        {
            _mock = new(MockBehavior.Strict);

            _mock.Setup(s => s.CustomerRepository.GetAllAsync(1, 10))
                 .ThrowsAsync(new HttpRequestException("No data found. Try again.", null, HttpStatusCode.NotFound));

            _mock.Setup(s => s.CustomerRepository.GetAllAsync(1, 2))
                 .ReturnsAsync
                 (
                    new List<Domain.Customer.Model.Customer>()
                    {
                        new ()
                        {
                            Id = Guid.NewGuid(),
                            Name = "CustomerTeste 01"
                        },

                        new ()
                        {
                            Id = Guid.NewGuid(),
                            Name = "CustomerTeste 02"
                        }
                    }
                 );

            var Customer = new Domain.Customer.Model.Customer()
            {
                Id = Guid.NewGuid(),
                Name = "CustomerTeste 00"
            };

            _mock.Setup(s => s.CustomerRepository.GetAsync(INVALID_ID))
                 .ThrowsAsync(new HttpRequestException("No data found. Try again.", null, HttpStatusCode.NotFound));

            _mock.Setup(s => s.CustomerRepository.GetAsync(Guid.NewGuid()))
                 .ReturnsAsync(Customer);
        }

        [Fact]
        public void Get_All_Customers_Success()
        {
            CustomerDomainService service = new(_mock.Object);

            service
                .GetAllAsync(1, 2)
                .Should()
                .Be(new List<Domain.Customer.Model.Customer>());
        }

        [Fact]
        public void Get_All_Customers_Failure()
        {
            CustomerDomainService service = new(_mock.Object);

            service
                .GetAllAsync(1, 10)
                .Should()
                .Be(new HttpRequestException("No data found. Try again.", null, HttpStatusCode.NotFound));
        }

        [Fact]
        public void Get_Customer_Success()
        {
            CustomerDomainService service = new(_mock.Object);

            service
                .GetAsync(Guid.NewGuid())
                .Should()
                .Be(new Domain.Customer.Model.Customer());
        }

        [Fact]
        public void Get_Customer_Failure()
        {
            CustomerDomainService service = new(_mock.Object);

            service
                .GetAsync(INVALID_ID)
                .Should()
                .Be(new HttpRequestException("No data found. Try again.", null, HttpStatusCode.NotFound));
        }
    }
}
