using ControlaAiBack.Domain.Entities;
using ControlaAiBack.Domain.Interfaces;
using ControlaAiBack.Infra.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlaAiBack.Infra.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
            return await _context.Users
                .AsNoTracking() 
                .FirstOrDefaultAsync(u => u.Id == id); 
        }

        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users
                .Where(u => !u.IsDeleted) 
                .ToListAsync();
        }
    }
}
