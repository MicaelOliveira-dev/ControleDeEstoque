using System;
using ControlaAiBack.Application.DTOs;
using ControlaAiBack.Application.Interfaces;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace ControlaAiBack.Application.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public void sendEmail(EmailDto request)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_config.GetSection("Email:UserName").Value));
            email.To.Add(MailboxAddress.Parse(request.Para));
            email.Subject = request.Assunto;

            email.Body = new TextPart("plain")
            {
                Text = $"Olá {request.Nome},\n\n" +
                   $"Seja muito bem-vindo(a) ao Controla Aí! Estamos muito felizes em tê-lo(a) conosco.\n\n" +
                   $"Aqui estão suas credenciais de acesso:\n\n" +
                   $"Email: {request.Para}\n" +
                   $"Senha: {request.Senha}\n\n" +
                   "Agora você já pode acessar o nosso serviço e começar a aproveitar todas as funcionalidades que oferecemos. Se precisar de qualquer ajuda, não hesite em nos contatar!\n\n" +
                   "Estamos à disposição e desejamos uma excelente experiência!\n\n" +
                   "Atenciosamente,\n" +
                   "Equipe Controla Aí"
            };

            using (var client = new SmtpClient())
            {
                try
                {
                    client.Connect(_config.GetSection("Email:Host").Value,
                                   int.Parse(_config.GetSection("Email:Port").Value),
                                   SecureSocketOptions.StartTls);

                    client.Authenticate(_config.GetSection("Email:UserName").Value,
                                        _config.GetSection("Email:Password").Value);

                    client.Send(email);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro ao enviar email: {ex.Message}");
                }
                finally
                {
                    client.Disconnect(true);
                }
            }
        }
    }
}
