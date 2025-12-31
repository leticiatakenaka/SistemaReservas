using Microsoft.EntityFrameworkCore;
using SistemaReservas.Domain.Common;
using SistemaReservas.Domain.Entities;
using SistemaReservas.Domain.Enums;
using SistemaReservas.Domain.Interfaces;
using SistemaReservas.Infrastructure.Context;
namespace SistemaReservas.Infrastructure.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly AppDbContext _context;

        public BookingRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Booking booking)
        {
            await _context.Bookings.AddAsync(booking);
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<bool> IsPropertyAvailableAsync(Guid propertyId, DateTime checkIn, DateTime checkOut)
        {
            bool hasOverlap = await _context.Bookings
                .AnyAsync(b =>
                    b.PropertyId == propertyId &&
                    b.Status != BookingStatus.Cancelled &&
                    b.CheckInDate < checkOut &&
                    b.CheckOutDate > checkIn);

            return !hasOverlap;
        }

        public async Task<PagedResult<Booking>> GetPagedBookingAsync(int page, int pageSize, string searchTerm, Guid hostId)
        {
            var query = _context.Bookings
                .Include(b => b.Property)
                .Include(b => b.Guest)
                .AsQueryable();

            query = query.Where(b => b.Property.HostId == hostId);

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(u => u.Property.Title.Contains(searchTerm) ||
                                         u.Property.Description.Contains(searchTerm));
            }

            var totalCount = await query.CountAsync();

            var items = await query
                .OrderByDescending(b => b.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<Booking>(items)
            {
                TotalCount = totalCount,
                Page = page,
                PageSize = pageSize
            };
        }
    }
}