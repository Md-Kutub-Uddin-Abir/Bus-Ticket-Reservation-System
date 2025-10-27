using System;

using Xunit;
using Moq;
using Application.Contracts.Interfaces;
using Application.Contracts.DTOs;
using Application.Services;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Tests.Services
{
    public class TicketServiceTests
    {
        private readonly Mock<ITicketRepository> _ticketRepoMock;
        private readonly Mock<IBusRepository> _busRepoMock;
        private readonly TicketService _ticketService;

        public TicketServiceTests()
        {
            _ticketRepoMock = new Mock<ITicketRepository>();
            _busRepoMock = new Mock<IBusRepository>();
            _ticketService = new TicketService(_ticketRepoMock.Object, _busRepoMock.Object);
        }

        [Fact]
        public async Task BookTicket_ShouldBookAvailableSeats()
        {
            // Arrange
            var dto = new BookTicketDto
            {
                BusScheduleId = 1,
                SeatNumbers = new List<int> { 1, 2 },
                PassengerName = "Abir",
                PassengerMobile = "01711111111"
            };

            var schedule = new BusSchedule { Id = 1 };
            var tickets = new List<Ticket>
            {
                new Ticket { SeatNo = 1, Status = SeatStatus.Available },
                new Ticket { SeatNo = 2, Status = SeatStatus.Available }
            };

            _busRepoMock.Setup(r => r.GetBusByIdAsync(dto.BusScheduleId))
                        .ReturnsAsync(new Bus { Id = 1 });

            _ticketRepoMock.Setup(r => r.GetTicketBySeatAsync(dto.BusScheduleId, 1))
                           .ReturnsAsync(tickets[0]);
            _ticketRepoMock.Setup(r => r.GetTicketBySeatAsync(dto.BusScheduleId, 2))
                           .ReturnsAsync(tickets[1]);

            _ticketRepoMock.Setup(r => r.UpdateTicketAsync(It.IsAny<Ticket>()))
                           .Returns(Task.CompletedTask);

            // Act
            var result = await _ticketService.BookTicketAsync(dto);

            // Assert
            Assert.Equal(2, result.Count);
            Assert.All(result, t => Assert.Equal(SeatStatus.Booked, t.Status));
        }

        [Fact]
        public async Task BookTicket_ShouldThrowError_IfSeatAlreadyBooked()
        {
            // Arrange
            var dto = new BookTicketDto
            {
                BusScheduleId = 1,
                SeatNumbers = new List<int> { 3 },
                PassengerName = "Test",
                PassengerMobile = "01722222222"
            };

            var bookedTicket = new Ticket { SeatNo = 3, Status = SeatStatus.Booked };

            _busRepoMock.Setup(r => r.GetBusByIdAsync(dto.BusScheduleId))
                        .ReturnsAsync(new Bus { Id = 1 });

            _ticketRepoMock.Setup(r => r.GetTicketBySeatAsync(dto.BusScheduleId, 3))
                           .ReturnsAsync(bookedTicket);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _ticketService.BookTicketAsync(dto));
        }
    }
}

