using Xunit;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Services;
using Application.Contracts.Interfaces;
using Domain.Entities;
using Application.Contracts.DTOs;

namespace Application.Tests.Services
{
    public class SearchServiceTests
    {
        private readonly Mock<IBusRepository> _busRepoMock;
        private readonly SearchService _service;

        public SearchServiceTests()
        {
            _busRepoMock = new Mock<IBusRepository>();
            _service = new SearchService(_busRepoMock.Object);
        }

        [Fact]
        public async Task SearchBuses_ReturnsMatchingSchedules()
        {
            // Arrange
            string from = "Dhaka";
            string to = "Chittagong";
            DateTime journeyDate = new DateTime(2025, 10, 21);

            
            _busRepoMock.Setup(r => r.SearchAvailableBusesAsync(from, to, journeyDate))
                .ReturnsAsync(new List<BusSchedule>
                {
                    new BusSchedule
                    {
                        Id = 1,
                        JourneyDate = journeyDate,
                        StartTime = new TimeSpan(8, 30, 0),
                        ArrivalTime = new TimeSpan(14, 15, 0),
                        Price = 950,
                        Bus = new Bus
                        {
                            Id = 1,
                            CompanyName = "Hanif Enterprise",
                            BusName = "Hanif",
                            From = from,
                            To = to,
                            TotalSeats = 40
                        }
                    }
                });

           
            _busRepoMock.Setup(r => r.CountBookedSeatsAsync(1)).ReturnsAsync(2);

            // Act
            var result = await _service.SearchAvailableBusesAsync(from, to, journeyDate);

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal("Hanif Enterprise", result[0].CompanyName);
            Assert.Equal(38, result[0].SeatsLeft); 
        }
    }
}
