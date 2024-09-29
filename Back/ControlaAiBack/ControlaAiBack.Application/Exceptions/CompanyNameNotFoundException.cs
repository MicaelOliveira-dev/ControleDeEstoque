using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlaAiBack.Application.Exceptions
{
    public class CompanyNameNotFoundException : Exception
    {
        public CompanyNameNotFoundException(Guid adminId)
            : base($"Nome da empresa não encontrado para o adminId {adminId}.")
        {
        }
    }
}
