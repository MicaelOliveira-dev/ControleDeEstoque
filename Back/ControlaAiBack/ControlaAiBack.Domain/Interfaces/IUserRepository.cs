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
        Task<Users> GetByIdAsync(Guid id);
        Task AddAsync(Users user);
        Task UpdateAsync(Users user);
        Task<IEnumerable<Users>> GetAllAsync();
        Task<Users> GetByEmailAsync(string email);
        Task<string> GetCompanyNameByAdminIdAsync(Guid adminId);

    }
}
