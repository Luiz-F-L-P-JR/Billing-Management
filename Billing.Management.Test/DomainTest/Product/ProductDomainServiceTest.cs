
using Billing.Management.Domain.Product.Service;
using Billing.Management.Domain.UnitOfWork.Interface;
using FluentAssertions;
using Moq;
using System.Net;

namespace Billing.Management.Test.DomainTest.Product
{
    public class ProductDomainServiceTest
    {
        private readonly Mock<IUnitOfWork<Domain.Product.Model.Product>>? _mockUnitOfWork;

        private Guid VALID_ID = new Guid("a543fdc0-27e8-4787-8f81-cf7ea5227229");
        private Guid INVALID_ID = new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd");

        public ProductDomainServiceTest()
        {
            _mockUnitOfWork = new();

            _mockUnitOfWork?.Setup(s => s.ProductRepository.GetAllAsync(10000, 10000))
                 .ThrowsAsync(new HttpRequestException("No data found. Try again.", null, HttpStatusCode.NotFound));

            _mockUnitOfWork?.Setup(s => s.ProductRepository.GetAllAsync(1, 2))
                 .ReturnsAsync
                 (
                    new List<Domain.Product.Model.Product>()
                    {
                        new()
                        {
                            Id = VALID_ID,
                            Name = "ProductTeste 01"
                        },

                        new()
                        {
                            Id = VALID_ID,
                            Name = "ProductTeste 00"
                        }
                    }
                 );

            _mockUnitOfWork?.Setup(s => s.RepositoryGeneric.CreateAsync(It.IsAny<Domain.Product.Model.Product>()));

            _mockUnitOfWork?.Setup(s => s.RepositoryGeneric.UpdateAsync(It.IsAny<Domain.Product.Model.Product>()));

            _mockUnitOfWork?.Setup(s => s.RepositoryGeneric.DeleteAsync(It.IsAny<Guid>()));
        }

        [Fact]
        public async Task Get_All_Products_Success()
        {
            //Arrenge
            ProductDomainService service = new(_mockUnitOfWork?.Object);

            //Act
            var result = await service.GetAllAsync(1, 2);

            //Assert
            result.Should().NotBeEmpty()
            .And.HaveCount(2).Equals
            (
                new List<Domain.Product.Model.Product>()
                {
                    new()
                    {
                        Id = VALID_ID,
                        Name = "ProductTeste 00"
                    },

                    new()
                    {
                        Id = VALID_ID,
                        Name = "ProductTeste 01"
                    }
                }
            );
        }

        [Fact]
        public async Task Get_All_Products_Failure()
        {
            //Arrenge
            ProductDomainService service = new(_mockUnitOfWork?.Object);

            //Act
            Func<Task> result = async () => await service.GetAllAsync(10000, 10000);

            //Assert
            await result.Should().ThrowAsync<HttpRequestException>().WithMessage("No data found. Try again.");
        }

        [Fact]
        public async Task Get_Product_Success()
        {
            var Product = new Domain.Product.Model.Product()
            {
                Id = VALID_ID,
                Name = "ProductTeste 00"
            };

            //Arrenge
            _mockUnitOfWork?.Setup(s => s.RepositoryGeneric.GetAsync(VALID_ID))
                .ReturnsAsync(Product);

            ProductDomainService service = new(_mockUnitOfWork?.Object);

            //Act
            var result = await service.GetAsync(VALID_ID);

            //Assret
            result.Should().Be(Product);
        }

        [Fact]
        public async Task Get_Product_Failure()
        {
            //Arrenge
            _mockUnitOfWork?.Setup(s => s.RepositoryGeneric.GetAsync(INVALID_ID))
                  .ThrowsAsync(new HttpRequestException("No data found. Try again.", null, HttpStatusCode.NotFound));

            ProductDomainService service = new(_mockUnitOfWork?.Object);

            //Act
            Func<Task> result = async () => await service.GetAsync(INVALID_ID);

            //Assret
            await result.Should().ThrowAsync<HttpRequestException>().WithMessage("No data found. Try again.");
        }

        [Fact]
        public async Task Create_Product_Success()
        {
            //Arrenge
            ProductDomainService service = new(_mockUnitOfWork?.Object);

            //Act
            await service.CreateAsync(It.IsAny<Domain.Product.Model.Product>());

            //Assret
            _mockUnitOfWork?.Verify(x => x.RepositoryGeneric.CreateAsync(It.IsAny<Domain.Product.Model.Product>()), Times.Once);
        }

        [Fact]
        public async Task Update_Product_Success()
        {
            //Arrenge
            ProductDomainService service = new(_mockUnitOfWork?.Object);

            //Act
            await service.UpdateAsync(It.IsAny<Domain.Product.Model.Product>());

            //Assret
            _mockUnitOfWork?.Verify(x => x.RepositoryGeneric.UpdateAsync(It.IsAny<Domain.Product.Model.Product>()), Times.Once);
        }

        [Fact]
        public async Task Delete_Product_Success()
        {
            //Arrenge
            ProductDomainService service = new(_mockUnitOfWork?.Object);

            //Act
            await service.DeleteAsync(It.IsAny<Guid>());

            //Assret
            _mockUnitOfWork?.Verify(x => x.RepositoryGeneric.DeleteAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async Task Create_Product_Failure()
        {
            //Arrenge
            _mockUnitOfWork?.Setup(s => s.RepositoryGeneric.CreateAsync(null))
                  .ThrowsAsync(new HttpRequestException("Data can not be created.", null, HttpStatusCode.BadRequest));

            ProductDomainService service = new(_mockUnitOfWork?.Object);

            //Act
            Func<Task> result = async () => await service.CreateAsync(null);

            //Assret
            await result.Should().ThrowAsync<HttpRequestException>().WithMessage("Data can not be created.");
        }

        [Fact]
        public async Task Update_Product_Failure()
        {
            //Arrenge
            _mockUnitOfWork?.Setup(s => s.RepositoryGeneric.UpdateAsync(null))
                  .ThrowsAsync(new HttpRequestException("Data can not be updated.", null, HttpStatusCode.BadRequest));

            ProductDomainService service = new(_mockUnitOfWork?.Object);

            //Act
            Func<Task> result = async () => await service.UpdateAsync(null);

            //Assret
            await result.Should().ThrowAsync<HttpRequestException>().WithMessage("Data can not be updated.");
        }

        [Fact]
        public async Task Delete_Product_Failure()
        {
            //Arrenge
            _mockUnitOfWork?.Setup(s => s.RepositoryGeneric.DeleteAsync(INVALID_ID))
                  .ThrowsAsync(new HttpRequestException("Data can not be deleted.", null, HttpStatusCode.BadRequest));

            ProductDomainService service = new(_mockUnitOfWork?.Object);

            //Act
            Func<Task> result = async () => await service.DeleteAsync(INVALID_ID);

            //Assret
            await result.Should().ThrowAsync<HttpRequestException>().WithMessage("Data can not be deleted.");
        }
    }
}
