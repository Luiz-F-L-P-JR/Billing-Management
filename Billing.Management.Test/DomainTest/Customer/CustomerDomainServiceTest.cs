
using Billing.Management.Domain.Customer.Service;
using Billing.Management.Domain.UnitOfWork.Interface;
using FluentAssertions;
using Moq;
using NPOI.SS.Formula.Functions;
using System.Net;

namespace Billing.Management.Test.DomainTest.Customer
{
    public class CustomerDomainServiceTest
    {
        private readonly Mock<IUnitOfWork<Domain.Customer.Model.Customer>>? _mockUnitOfWork;

        private Guid VALID_ID = new Guid("a543fdc0-27e8-4787-8f81-cf7ea5227229");
        private Guid INVALID_ID = new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd");

        public CustomerDomainServiceTest()
        {
            _mockUnitOfWork = new();

            _mockUnitOfWork?.Setup(s => s.CustomerRepository.GetAllAsync(10000, 10000))
                 .ThrowsAsync(new HttpRequestException("No data found. Try again.", null, HttpStatusCode.NotFound));

            _mockUnitOfWork?.Setup(s => s.CustomerRepository.GetAllAsync(1, 2))
                 .ReturnsAsync
                 (
                    new List<Domain.Customer.Model.Customer>()
                    {
                        new()
                        {
                            Id = VALID_ID,
                            Name = "CustomerTeste 01",
                            Email = "Test@email.com",
                            Address = "Address Test 222"
                        },

                        new()
                        {
                            Id = VALID_ID,
                            Name = "CustomerTeste 00",
                            Email = "Test@email.com",
                            Address = "Address Test 123"
                        }
                    }
                 );

            _mockUnitOfWork?.Setup(s => s.RepositoryGeneric.CreateAsync(It.IsAny<Domain.Customer.Model.Customer>()));

            _mockUnitOfWork?.Setup(s => s.RepositoryGeneric.UpdateAsync(It.IsAny<Domain.Customer.Model.Customer>()));

            _mockUnitOfWork?.Setup(s => s.RepositoryGeneric.DeleteAsync(It.IsAny<Guid>()));
        }

        [Fact]
        public async Task Get_All_Customers_Success()
        {
            //Arrenge
            CustomerDomainService service = new(_mockUnitOfWork?.Object);

            //Act
            var result = await service.GetAllAsync(1, 2);

            //Assert
            result.Should().NotBeEmpty()
            .And.HaveCount(2).Equals
            (
                new List<Domain.Customer.Model.Customer>()
                {
                    new()
                    {
                        Id = VALID_ID,
                        Name = "CustomerTeste 00",
                        Email = "Test@email.com",
                        Address = "Address Test 123"
                    },

                    new()
                    {
                        Id = VALID_ID,
                        Name = "CustomerTeste 01",
                        Email = "Test@email.com",
                        Address = "Address Test 222"
                    }
                }
            );
        }

        [Fact]
        public async Task Get_All_Customers_Failure()
        {
            //Arrenge
            CustomerDomainService service = new(_mockUnitOfWork?.Object);

            //Act
            Func<Task> result = async () => await service.GetAllAsync(10000, 10000);

            //Assert
            await result.Should().ThrowAsync<HttpRequestException>().WithMessage("No data found. Try again.");
        }

        [Fact]
        public async Task Get_Customer_Success()
        {
            var customer = new Domain.Customer.Model.Customer()
            {
                Id = VALID_ID,
                Name = "CustomerTeste 00",
                Email = "Test@email.com",
                Address = "Address Test 123"
            };

            //Arrenge
            _mockUnitOfWork?.Setup(s => s.RepositoryGeneric.GetAsync(VALID_ID))
                .ReturnsAsync(customer);

            CustomerDomainService service = new(_mockUnitOfWork?.Object);

            //Act
            var result = await service.GetAsync(VALID_ID);

            //Assret
            result.Should().Be(customer);
        }

        [Fact]
        public async Task Get_Customer_Failure()
        {
            //Arrenge
            _mockUnitOfWork?.Setup(s => s.RepositoryGeneric.GetAsync(INVALID_ID))
                  .ThrowsAsync(new HttpRequestException("No data found. Try again.", null, HttpStatusCode.NotFound));

            CustomerDomainService service = new(_mockUnitOfWork?.Object);

            //Act
            Func<Task> result = async () => await service.GetAsync(INVALID_ID);

            //Assret
            await result.Should().ThrowAsync<HttpRequestException>().WithMessage("No data found. Try again.");
        }

        [Fact]
        public async Task Create_Customer_Success()
        {
            //Arrenge
            CustomerDomainService service = new(_mockUnitOfWork?.Object);

            //Act
            await service.CreateAsync(It.IsAny<Domain.Customer.Model.Customer>());

            //Assret
            _mockUnitOfWork?.Verify(x => x.RepositoryGeneric.CreateAsync(It.IsAny<Domain.Customer.Model.Customer>()), Times.Once);
        }

        [Fact]
        public async Task Update_Customer_Success()
        {
            //Arrenge
            CustomerDomainService service = new(_mockUnitOfWork?.Object);

            //Act
            await service.UpdateAsync(It.IsAny<Domain.Customer.Model.Customer>());

            //Assret
            _mockUnitOfWork?.Verify(x => x.RepositoryGeneric.UpdateAsync(It.IsAny<Domain.Customer.Model.Customer>()), Times.Once);
        }

        [Fact]
        public async Task Delete_Customer_Success()
        {
            //Arrenge
            CustomerDomainService service = new(_mockUnitOfWork?.Object);

            //Act
            await service.DeleteAsync(It.IsAny<Guid>());

            //Assret
            _mockUnitOfWork?.Verify(x => x.RepositoryGeneric.DeleteAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async Task Create_Customer_Failure()
        {
            //Arrenge
            _mockUnitOfWork?.Setup(s => s.RepositoryGeneric.CreateAsync(null))
                  .ThrowsAsync(new HttpRequestException("Data can not be created.", null, HttpStatusCode.BadRequest));

            CustomerDomainService service = new(_mockUnitOfWork?.Object);

            //Act
            Func<Task> result = async () => await service.CreateAsync(null);

            //Assret
            await result.Should().ThrowAsync<HttpRequestException>().WithMessage("Data can not be created.");
        }

        [Fact]
        public async Task Update_Customer_Failure()
        {
            //Arrenge
            _mockUnitOfWork?.Setup(s => s.RepositoryGeneric.UpdateAsync(null))
                  .ThrowsAsync(new HttpRequestException("Data can not be updated.", null, HttpStatusCode.BadRequest));

            CustomerDomainService service = new(_mockUnitOfWork?.Object);

            //Act
            Func<Task> result = async () => await service.UpdateAsync(null);

            //Assret
            await result.Should().ThrowAsync<HttpRequestException>().WithMessage("Data can not be updated.");
        }

        [Fact]
        public async Task Delete_Customer_Failure()
        {
            //Arrenge
            _mockUnitOfWork?.Setup(s => s.RepositoryGeneric.DeleteAsync(INVALID_ID))
                  .ThrowsAsync(new HttpRequestException("Data can not be deleted.", null, HttpStatusCode.BadRequest));

            CustomerDomainService service = new(_mockUnitOfWork?.Object);

            //Act
            Func<Task> result = async () => await service.DeleteAsync(INVALID_ID);

            //Assret
            await result.Should().ThrowAsync<HttpRequestException>().WithMessage("Data can not be deleted.");
        }
    }
}
