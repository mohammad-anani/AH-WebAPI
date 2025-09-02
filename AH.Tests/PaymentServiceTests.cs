using AH.Application.DTOs.Create;
using AH.Application.DTOs.Response;
using AH.Application.IRepositories;
using AH.Application.Services;
using AH.Domain.Entities;
using AutoFixture;
using FluentAssertions;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace AH.Tests
{
    public class PaymentServiceTests
    {
        private readonly PaymentService paymentService;
        private readonly Mock<IPaymentRepository> mock;

        public PaymentServiceTests()
        {
            mock = new Mock<IPaymentRepository>();
            paymentService = new PaymentService(mock.Object);
        }

        [Fact]
        public async Task DeleteAsync_WhenRepositorySucceeds_ReturnsTrueAndNoException()
        {
            // Arrange
            mock.Setup(r => r.DeleteAsync(It.IsAny<int>()))
                .ReturnsAsync(new DeleteResponseDTO(true, null));

            // Act
            var result = await paymentService.DeleteAsync(123);

            // Assert
            result.Data.Should().BeTrue();
            result.StatusCode.Should().Be(200);
            mock.Verify(r => r.DeleteAsync(123), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_WhenRepositoryFails_ReturnsFalseAndException()
        {
            // Arrange
            var ex = new Exception("delete failed");
            mock.Setup(r => r.DeleteAsync(It.IsAny<int>()))
                .ReturnsAsync(new DeleteResponseDTO(false, ex));

            // Act
            var result = await paymentService.DeleteAsync(456);

            // Assert
            result.Data.Should().BeFalse();
            result.StatusCode.Should().NotBe(200);
            mock.Verify(r => r.DeleteAsync(456), Times.Once);
        }

        [Fact]
        public async Task AddAsync_UsingAutoFixture_MapsDtoToPayment_AndReturnsId()
        {
            var random = new Random();

            // Arrange
            var fixture = new Fixture();
            var dto = fixture.Build<CreatePaymentDTO>().With(x => x.Method, random.Next(1, 3)) // ensure valid method
                             .Create();

            Payment? captured = null;
            mock.Setup(r => r.AddAsync(It.IsAny<Payment>()))
                .Callback<Payment>(p => captured = p)
                .ReturnsAsync(new CreateResponseDTO(42, null));

            // Act
            var result = await paymentService.AddAsync(dto);

            // Assert - result
            result.Data.Should().Be(42);
            result.StatusCode.Should().Be(200);

            // Assert - mapping
            captured.Should().NotBeNull();
            captured!.Bill.ID.Should().Be(dto.BillID);
            captured.Amount.Should().Be(dto.Amount);
            captured.Method.Should().Be(Payment.GetMethod(dto.Method));
            captured.CreatedByReceptionist.ID.Should().Be(dto.CreatedByReceptionistID);

            mock.Verify(r => r.AddAsync(It.IsAny<Payment>()), Times.Once);
        }
    }
}