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
        void sendEmail(EmailDto request);
    }
}
