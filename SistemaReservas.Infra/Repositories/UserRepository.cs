using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SistemaReservas.Domain.Entities;
using SistemaReservas.Domain.Interfaces; 
using SistemaReservas.Infrastructure.Context;

namespace SistemaReservas.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserRepository(AppDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _context.Users.AnyAsync(u => u.Id == id);
        }

        public async Task<ApplicationUser?> GetByIdAsync(Guid id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<ApplicationUser?> GetByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }
    }
}