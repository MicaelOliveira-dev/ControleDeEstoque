using ControlaAiBack.Domain.Entities;
using ControlaAiBack.Domain.Interfaces;
using ControlaAiBack.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace ControlaAiBack.Infra.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Users> GetByIdAsync(Guid id)
        {
            return await _context.Users
                .AsNoTracking() 
                .FirstOrDefaultAsync(u => u.Id == id); 
        }

        public async Task<Users> GetByEmailAsync(string email)
        {
            return await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == email);  
        }

        public async Task AddAsync(Users user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Users user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Users>> GetAllAsync()
        {
            return await _context.Users
                .Where(u => !u.IsDeleted) 
                .ToListAsync();
        }

        public async Task<string> GetCompanyNameByAdminIdAsync(Guid adminId)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == adminId && !u.IsDeleted);

            return user?.NomeEmpresa; 
        }

    }
}
