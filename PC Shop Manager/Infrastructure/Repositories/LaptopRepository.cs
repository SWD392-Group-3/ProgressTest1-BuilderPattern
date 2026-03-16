using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class LaptopRepository : ILaptopRepository
    {
        private readonly AppDbContext _context;

        public LaptopRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Laptop>> GetAllAsync()
        {
            return await _context.Laptops.OrderByDescending(l => l.CreatedAt).ToListAsync();
        }

        public async Task AddAsync(Laptop laptop)
        {
            await _context.Laptops.AddAsync(laptop);
            await _context.SaveChangesAsync();
        }
    }
}
