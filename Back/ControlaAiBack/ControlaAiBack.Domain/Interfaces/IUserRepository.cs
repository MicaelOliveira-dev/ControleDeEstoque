using ControlaAiBack.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlaAiBack.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetByIdAsync(Guid id);
        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task<IEnumerable<User>> GetAllAsync();
    }
}
