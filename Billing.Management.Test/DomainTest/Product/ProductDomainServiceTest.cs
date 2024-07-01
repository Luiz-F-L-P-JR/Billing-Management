
using Billing.Management.Domain.Product.Service;
using Billing.Management.Domain.UnitOfWork.Interface;
using FluentAssertions;
using Moq;
using System.Net;

namespace Billing.Management.Test.DomainTest.Product
{
    public class ProductDomainServiceTest
    {
        private readonly Mock<IUnitOfWork<Domain.Product.Model.Product>>? _mock;

        private Guid VALID_ID = new Guid();
        private Guid INVALID_ID = Guid.Parse("87878787877");

        public ProductDomainServiceTest()
        {
            _mock = new(MockBehavior.Strict);

            _mock.Setup(s => s.ProductRepository.GetAllAsync(1, 10))
                 .ThrowsAsync(new HttpRequestException("No data found. Try again.", null, HttpStatusCode.NotFound));

            _mock.Setup(s => s.ProductRepository.GetAllAsync(1, 2))
                 .ReturnsAsync
                 (
                    new List<Domain.Product.Model.Product>()
                    {
                        new ()
                        {
                            Id = Guid.NewGuid(),
                            Name = "ProductTeste 01"
                        },

                        new ()
                        {
                            Id = Guid.NewGuid(),
                            Name = "ProductTeste 02"
                        }
                    }
                 );

            var product = new Domain.Product.Model.Product()
            {
                Id = new Guid(),
                Name = "ProductTeste 00"
            };

            _mock.Setup(s => s.ProductRepository.GetAsync(It.IsAny<Guid>()))
                 .ThrowsAsync(new HttpRequestException("No data found. Try again.", null, HttpStatusCode.NotFound));

            _mock.Setup(s => s.ProductRepository.GetAsync(VALID_ID))
                 .ReturnsAsync(product);
        }

        [Fact]
        public void Get_All_Products_Success()
        {
            ProductDomainService service = new(_mock.Object);

            service
                .GetAllAsync(1, 2)
                .Should()
                .Be(new List<Domain.Product.Model.Product>());
        }

        [Fact]
        public void Get_All_Products_Failure()
        {
            ProductDomainService service = new(_mock.Object);

            service
                .GetAllAsync(1, 10)
                .Should()
                .Be(new HttpRequestException("No data found. Try again.", null, HttpStatusCode.NotFound));
        }

        [Fact]
        public void Get_Product_Success()
        {
            ProductDomainService service = new(_mock.Object);

            service
                .GetAsync(VALID_ID)
                .Should()
                .Be(new Domain.Product.Model.Product());
        }

        [Fact]
        public void Get_Product_Failure()
        {
            ProductDomainService service = new(_mock.Object);

            service
                .GetAsync(INVALID_ID)
                .Should()
                .Be(new HttpRequestException("No data found. Try again.", null, HttpStatusCode.NotFound));
        }
    }
}
