using ControlaAiBack.Domain.Entities;
using ControlaAiBack.Domain.Interfaces;
using ControlaAiBack.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace ControlaAiBack.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _dbContext;

        public UserRepository(AppDbContext dbContext) 
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(User user)
        {
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
        }

        
    }
}
