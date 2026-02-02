using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travelo.Domain.Models.Entities;

namespace Travelo.Application.Interfaces
{
    public interface IBookingRepository
    {
        Task AddGeneralAsync(GeneralBooking booking);

        Task<GeneralBooking?> GetGeneralByIdAsync(int id);

        Task<List<GeneralBooking>> GetByUserIdAsync(string userId);

        Task AddAsync(Booking booking);
        Task<Booking?> GetByIdAsync(int id);
        Task SaveChangesAsync();
    }

}
