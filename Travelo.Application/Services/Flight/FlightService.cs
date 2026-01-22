using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Travelo.Application.DTOs.Flight;
using Travelo.Application.Interfaces;
using Travelo.Domain.Models.Entities;

namespace Travelo.Application.Services.Flight
{
    public class FlightService : IFlightService
    {
        private readonly IFlightRepository _flightRepository;

        public FlightService(IFlightRepository flightRepository)
        {
            _flightRepository = flightRepository;
        }

        // 1️⃣ Get all flights
        public async Task<List<FlightDto>> GetAllFlightsAsync()
        {
            var flights = await _flightRepository.GetAllAsync();
            return flights.Select(f => MapToDto(f)).ToList();
        }

        // 2️⃣ Get flight by Id
        public async Task<FlightDto?> GetFlightByIdAsync(int id)
        {
            var f = await _flightRepository.GetByIdAsync(id);
            if (f == null) return null;
            return MapToDto(f);
        }

        // 3️⃣ Create flight
        public async Task<FlightDto> CreateFlightAsync(FlightDto flightDto)
        {
            var flight = new Travelo.Domain.Models.Entities.Flight
            {
                Airline = flightDto.Airline,
                FlightNumber = flightDto.FlightNumber,
                FromAirport = flightDto.FromAirport,
                ToAirport = flightDto.ToAirport,
                DepartureDateTime = flightDto.DepartureDateTime,
                ArrivalDateTime = flightDto.ArrivalDateTime,
                Price = flightDto.Price
            };

            await _flightRepository.AddAsync(flight);

            flightDto.Id = flight.Id;
            return flightDto;
        }

        // 4️⃣ Update flight
        public async Task<FlightDto?> UpdateFlightAsync(int id, FlightDto flightDto)
        {
            var flight = await _flightRepository.GetByIdAsync(id);
            if (flight == null) return null;

            flight.Airline = flightDto.Airline;
            flight.FlightNumber = flightDto.FlightNumber;
            flight.FromAirport = flightDto.FromAirport;
            flight.ToAirport = flightDto.ToAirport;
            flight.DepartureDateTime = flightDto.DepartureDateTime;
            flight.ArrivalDateTime = flightDto.ArrivalDateTime;
            flight.Price = flightDto.Price;

            await _flightRepository.UpdateAsync(flight);

            return MapToDto(flight);
        }

        // 5️⃣ Delete flight
        public async Task<bool> DeleteFlightAsync(int id)
        {
            var flight = await _flightRepository.GetByIdAsync(id);
            if (flight == null) return false;

            await _flightRepository.DeleteAsync(id);
            return true;
        }

        // Helper method to map Flight entity to DTO
        private FlightDto MapToDto(Travelo.Domain.Models.Entities.Flight f)
        {
            return new FlightDto
            {
                Id = f.Id,
                Airline = f.Airline!,
                FlightNumber = f.FlightNumber!,
                FromAirport = f.FromAirport!,
                ToAirport = f.ToAirport!,
                DepartureDateTime = f.DepartureDateTime,
                ArrivalDateTime = f.ArrivalDateTime,
                Price = f.Price
            };
        }
    }
}
