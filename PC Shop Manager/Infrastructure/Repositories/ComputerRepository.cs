using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ComputerRepository : IComputerRepository
    {
        private readonly AppDbContext _context;

        public ComputerRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Computer>> GetAllAsync()
        {
            return await _context.Computers.OrderByDescending(c => c.CreatedAt).ToListAsync();
        }

        public async Task AddAsync(Computer computer)
        {
            await _context.Computers.AddAsync(computer);
            await _context.SaveChangesAsync();
        }
    }
}
