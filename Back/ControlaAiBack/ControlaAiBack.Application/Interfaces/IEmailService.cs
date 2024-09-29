using ControlaAiBack.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlaAiBack.Application.Interfaces
{
    public interface IEmailService
    {
        Task sendEmail(EmailDto request);
        Task<bool> IsValidEmail(string email);
    }
}
