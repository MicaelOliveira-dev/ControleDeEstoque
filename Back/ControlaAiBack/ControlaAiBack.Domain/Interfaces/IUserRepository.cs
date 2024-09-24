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
        Task AddAsync(User user);
    }
}
