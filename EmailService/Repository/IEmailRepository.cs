using EmailService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailService.Repository
{
    public interface IEmailRepository
    {
        Task SendEmailAsync(EmailObjectModel email);
    }
}
