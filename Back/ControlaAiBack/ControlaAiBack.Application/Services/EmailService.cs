using ControlaAiBack.Application.DTOs;
using ControlaAiBack.Application.Exceptions;
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

        public async Task sendEmail(EmailDto request)
        {
            if (string.IsNullOrEmpty(request.Para) || !await IsValidEmail(request.Para))
            {
                throw new EmailSendingException(request.Para, new Exception("O endereço de e-mail não é válido."));
            }

            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_config.GetSection("Email:UserName").Value));
            email.To.Add(MailboxAddress.Parse(request.Para));
            email.Subject = request.Assunto;

            var body = $@"
                <!DOCTYPE html>
                <html lang='pt-BR'>
                <head>
                    <meta charset='UTF-8'>
                    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                    <style>
                        body {{
                            font-family: 'Segoe UI', Arial, sans-serif;
                            background-color: #f4f4f4;
                            margin: 0;
                            padding: 0;
                        }}
                        .container {{
                            max-width: 600px;
                            margin: 0 auto;
                            background-color: #ffffff;
                            padding: 30px;
                            border-radius: 10px;
                            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
                        }}
                        .header {{
                            background-color: #512DA8;
                            padding: 20px;
                            text-align: center;
                            border-top-left-radius: 10px;
                            border-top-right-radius: 10px;
                        }}
                        .header img {{
                            width: 150px;
                            margin-bottom: 10px;
                        }}
                        h1 {{
                            color: #512DA8;
                            font-size: 24px;
                            margin-bottom: 20px;
                        }}
                        p {{
                            color: #333333;
                            font-size: 16px;
                            line-height: 1.5;
                            margin-bottom: 20px;
                        }}
                        .credentials {{
                            background-color: #f8f8f8;
                            padding: 15px;
                            border-radius: 5px;
                            margin: 20px 0;
                        }}
                        .credentials p {{
                            font-size: 16px;
                            color: #333;
                        }}
                        .credentials strong {{
                            color: #512DA8;
                        }}
                        .footer {{
                            margin-top: 30px;
                            text-align: center;
                            color: #888888;
                            font-size: 12px;
                        }}
                        .footer p {{
                            margin-bottom: 5px;
                        }}
                    </style>
                </head>
                <body>
                    <div class='container'>
                        <div class='header'>
                            <img src='https://i.ibb.co/ftkd1sZ/logo.png' alt='Controla Aí'>
                        </div>
                        <h1>Bem-vindo ao Controla Aí!</h1>
                        <p>Você se inscreveu para receber doses irregulares do Controla Aí direto na sua caixa de entrada. Estamos muito felizes em ter você a bordo.</p>
                        <p>Aqui estão suas credenciais de acesso:</p>
                        <div class='credentials'>
                            <p><strong>Email:</strong> {request.Para}</p>
                            <p><strong>Senha:</strong> {request.Senha}</p>
                        </div>
                        <p>Agora você já pode acessar o nosso serviço e começar a aproveitar todas as funcionalidades que oferecemos. Se precisar de qualquer ajuda, não hesite em nos contatar!</p>
                        <div class='footer'>
                            <p>© 2024 Controla Aí</p>
                        </div>
                    </div>
                </body>
                </html>";

            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = body
            };

            email.Body = bodyBuilder.ToMessageBody();

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
                    throw new EmailSendingException($"Erro ao enviar email para: {ex.Message}", ex);
                }
                finally
                {
                    client.Disconnect(true);
                }
            }
        }

        public async Task<bool> IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
