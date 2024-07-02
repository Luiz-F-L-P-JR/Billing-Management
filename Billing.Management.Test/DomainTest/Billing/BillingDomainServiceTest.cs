
using Billing.Management.Domain.Billing.Models;
using Billing.Management.Domain.Billing.Services;
using Billing.Management.Domain.UnitOfWork.Interface;
using FluentAssertions;
using Moq;
using NPOI.SS.Formula.Functions;
using System.Net;

namespace Billing.Management.Test.DomainTest.Billing
{
    public class BillingDomainServiceTest
    {
        private readonly Mock<IUnitOfWork<Domain.Billing.Models.Billing>>? _mockUnitOfWork;

        private Guid VALID_ID = new Guid("a543fdc0-27e8-4787-8f81-cf7ea5227229");
        private Guid INVALID_ID = new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd");

        public BillingDomainServiceTest()
        {
            _mockUnitOfWork = new();

            _mockUnitOfWork?.Setup(s => s.BillingRepository.GetAllAsync(10000, 10000))
                 .ThrowsAsync(new HttpRequestException("No data found. Try again.", null, HttpStatusCode.NotFound));

            _mockUnitOfWork?.Setup(s => s.BillingRepository.GetAllAsync(1, 2))
                 .ReturnsAsync
                 (
                    new List<Domain.Billing.Models.Billing>()
                    {
                        new()
                        {
                            Id = It.IsAny<Guid>(),
                            InvoiceNumber = "INV-000",
                            CustomerId = It.IsAny<Guid>(),
                            Customer = new()
                            {
                                Id = VALID_ID,
                                Name = "CustomerTeste 00",
                                Email = "Test@email.com",
                                Address = "Address Test 123"
                            },
                            Date = It.IsAny<DateTime>(),
                            DueDate = It.IsAny<DateTime>(),
                            TotalAmount = 100,
                            Currency = "BRL",
                            Lines = new List<BillingLine>
                            {
                                new()
                                {
                                    ProductId = It.IsAny<Guid>(),
                                    Description = "Produto ",
                                    Quantity = 5,
                                    UnitPrice = 500,
                                    Subtotal = 2500
                                },

                                new()
                                {
                                    ProductId = It.IsAny<Guid>(),
                                    Description = "Produto 2",
                                    Quantity = 1,
                                    UnitPrice = 50,
                                    Subtotal = 50
                                }
                            }

                        },

                        new()
                        {
                            Id = It.IsAny<Guid>(),
                            InvoiceNumber = "INV-000",
                            CustomerId = It.IsAny<Guid>(),
                            Customer = new()
                            {
                                Id = VALID_ID,
                                Name = "CustomerTeste 00",
                                Email = "Test@email.com",
                                Address = "Address Test 123"
                            },
                            Date = It.IsAny<DateTime>(),
                            DueDate = It.IsAny<DateTime>(),
                            TotalAmount = 100,
                            Currency = "BRL",
                            Lines = new List<BillingLine>
                            {
                                new()
                                {
                                    ProductId = It.IsAny<Guid>(),
                                    Description = "Produto ",
                                    Quantity = 5,
                                    UnitPrice = 500,
                                    Subtotal = 2500
                                },

                                new()
                                {
                                    ProductId = It.IsAny<Guid>(),
                                    Description = "Produto 2",
                                    Quantity = 1,
                                    UnitPrice = 50,
                                    Subtotal = 50
                                }
                            }

                        }
                    }
                 );

            _mockUnitOfWork?.Setup(s => s.RepositoryGeneric.CreateAsync(It.IsAny<Domain.Billing.Models.Billing>()));

            _mockUnitOfWork?.Setup(s => s.RepositoryGeneric.UpdateAsync(It.IsAny<Domain.Billing.Models.Billing>()));

            _mockUnitOfWork?.Setup(s => s.BillingLinesRepository.DeleteAsync(It.IsAny<Guid>()));

            _mockUnitOfWork?.Setup(s => s.RepositoryGeneric.DeleteAsync(It.IsAny<Guid>()));

        }

        [Fact]
        public async Task Get_All_Billings_Success()
        {
            //Arrenge
            BillingDomainService service = new(_mockUnitOfWork?.Object);

            //Act
            var result = await service.GetAllAsync(1, 2);

            //Assert
            result.Should().NotBeEmpty()
            .And.HaveCount(2).Equals
            (
                new List<Domain.Billing.Models.Billing>()
                {
                    new()
                    {
                        Id = It.IsAny<Guid>(),
                        InvoiceNumber = "INV-000",
                        CustomerId = It.IsAny<Guid>(),
                        Customer = new()
                        {
                            Id = VALID_ID,
                            Name = "CustomerTeste 00",
                            Email = "Test@email.com",
                            Address = "Address Test 123"
                        },
                        Date = It.IsAny<DateTime>(),
                        DueDate = It.IsAny<DateTime>(),
                        TotalAmount = 100,
                        Currency = "BRL",
                        Lines = new List<BillingLine>
                        {
                            new()
                            {
                                ProductId = It.IsAny<Guid>(),
                                Description = "Produto ",
                                Quantity = 5,
                                UnitPrice = 500,
                                Subtotal = 2500
                            },

                            new()
                            {
                                ProductId = It.IsAny<Guid>(),
                                Description = "Produto 2",
                                Quantity = 1,
                                UnitPrice = 50,
                                Subtotal = 50
                            }
                        }

                    },

                    new()
                    {
                        Id = It.IsAny<Guid>(),
                        InvoiceNumber = "INV-000",
                        CustomerId = It.IsAny<Guid>(),
                        Customer = new()
                        {
                            Id = VALID_ID,
                            Name = "CustomerTeste 00",
                            Email = "Test@email.com",
                            Address = "Address Test 123"
                        },
                        Date = It.IsAny<DateTime>(),
                        DueDate = It.IsAny<DateTime>(),
                        TotalAmount = 100,
                        Currency = "BRL",
                        Lines = new List<BillingLine>
                        {
                            new()
                            {
                                ProductId = It.IsAny<Guid>(),
                                Description = "Produto ",
                                Quantity = 5,
                                UnitPrice = 500,
                                Subtotal = 2500
                            },

                            new()
                            {
                                ProductId = It.IsAny<Guid>(),
                                Description = "Produto 2",
                                Quantity = 1,
                                UnitPrice = 50,
                                Subtotal = 50
                            }
                        }

                    }
                }
            );
        }

        [Fact]
        public async Task Get_All_Billings_Failure()
        {
            //Arrenge
            BillingDomainService service = new(_mockUnitOfWork?.Object);

            //Act
            Func<Task> result = async () => await service.GetAllAsync(10000, 10000);

            //Assert
            await result.Should().ThrowAsync<HttpRequestException>().WithMessage("No data found. Try again.");
        }

        [Fact]
        public async Task Get_Billing_Success()
        {
            var billing = new Domain.Billing.Models.Billing()
            {
                Id = VALID_ID,
                InvoiceNumber = "INV-000",
                CustomerId = It.IsAny<Guid>(),
                Customer = new()
                {
                    Id = VALID_ID,
                    Name = "CustomerTeste 00",
                    Email = "Test@email.com",
                    Address = "Address Test 123"
                },
                Date = It.IsAny<DateTime>(),
                DueDate = It.IsAny<DateTime>(),
                TotalAmount = 100,
                Currency = "BRL",
                Lines = new List<BillingLine>
                {
                    new()
                    {
                        ProductId = It.IsAny<Guid>(),
                        Description = "Produto ",
                        Quantity = 5,
                        UnitPrice = 500,
                        Subtotal = 2500
                    },

                    new()
                    {
                        ProductId = It.IsAny<Guid>(),
                        Description = "Produto 2",
                        Quantity = 1,
                        UnitPrice = 50,
                        Subtotal = 50
                    }
                }

            };

            //Arrenge
            _mockUnitOfWork?.Setup(s => s.RepositoryGeneric.GetAsync(VALID_ID))
                .ReturnsAsync(billing);

            BillingDomainService service = new(_mockUnitOfWork?.Object);

            //Act
            var result = await service.GetAsync(VALID_ID);

            //Assret
            result.Should().Be(billing);
        }

        [Fact]
        public async Task Get_Billing_Failure()
        {
            //Arrenge
            _mockUnitOfWork?.Setup(s => s.RepositoryGeneric.GetAsync(INVALID_ID))
                  .ThrowsAsync(new HttpRequestException("No data found. Try again.", null, HttpStatusCode.NotFound));

            BillingDomainService service = new(_mockUnitOfWork?.Object);

            //Act
            Func<Task> result = async () => await service.GetAsync(INVALID_ID);

            //Assret
            await result.Should().ThrowAsync<HttpRequestException>().WithMessage("No data found. Try again.");
        }

        [Fact]
        public async Task Update_Billing_Success()
        {
            //Arrenge
            BillingDomainService service = new(_mockUnitOfWork?.Object);

            //Act
            await service.UpdateAsync(It.IsAny<Domain.Billing.Models.Billing>());

            //Assret
            _mockUnitOfWork?.Verify(x => x.RepositoryGeneric.UpdateAsync(It.IsAny<Domain.Billing.Models.Billing>()), Times.Once);
        }

        [Fact]
        public async Task Update_Billing_Failure()
        {
            //Arrenge
            _mockUnitOfWork?.Setup(s => s.RepositoryGeneric.UpdateAsync(null))
                  .ThrowsAsync(new HttpRequestException("Data can not be updated.", null, HttpStatusCode.BadRequest));

            BillingDomainService service = new(_mockUnitOfWork?.Object);

            //Act
            Func<Task> result = async () => await service.UpdateAsync(null);

            //Assret
            await result.Should().ThrowAsync<HttpRequestException>().WithMessage("Data can not be updated.");
        }
    }
}
