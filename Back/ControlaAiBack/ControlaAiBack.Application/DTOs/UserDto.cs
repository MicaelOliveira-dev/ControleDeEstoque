﻿using ControlaAiBack.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlaAiBack.Application.DTOs
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string NomeEmpresa { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public Users.UserType Permissao { get; set; }
    }

}
