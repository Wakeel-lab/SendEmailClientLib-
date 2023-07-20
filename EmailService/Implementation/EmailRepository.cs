using EmailService.Model;
using EmailService.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EmailService.Implementation
{
    public class EmailRepository : IEmailRepository
    {
        public async Task SendEmailAsync(EmailObjectModel email)
        {

            try
            {
                EmailConfiguration emailConfig = new();
                using (SmtpClient smtpClient = new SmtpClient(emailConfig.HOST, emailConfig.PORT))
                {
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = new NetworkCredential
                        (emailConfig.USERNAME, emailConfig.PASSWORD);
                    smtpClient.EnableSsl = true;
                    bool send = false;
                    string emailTemplate = File.ReadAllText(email.TEMPLATECONFIG);
                    emailTemplate = emailTemplate.Replace("{RECIPIENT_BODY_CONTENT}", email.CONTENT);
                    using (var stream = new MemoryStream())
                    using (var writer = new StreamWriter(stream))
                    using (MailMessage mailMessage = new MailMessage())
                    {
                        mailMessage.From = new MailAddress(emailConfig.ADDRESS, emailConfig.DISPLAYNAME);
                        mailMessage.IsBodyHtml = true;
                        mailMessage.Subject = email.SUBJECT;

                        foreach (var recipient in email.CC)
                        {
                            if (!string.IsNullOrEmpty(recipient))
                            {
                                mailMessage.CC.Add(new MailAddress(recipient));
                                send = true;
                            }

                        }
                        foreach (var recipient in email.BCC)
                        {
                            if (!string.IsNullOrEmpty(recipient))
                            {
                                mailMessage.Bcc.Add(new MailAddress(recipient));
                                send = true;
                            }

                        }

                        mailMessage.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                        mailMessage.SubjectEncoding = Encoding.UTF8;
                        mailMessage.Body = emailTemplate;
                        mailMessage.BodyEncoding = Encoding.UTF8;

                        foreach (var recipient in email.TO)
                        {

                            mailMessage.To.Add(new MailAddress(recipient));

                        }
                        
                        if (email.ATTACHMENTS != null)
                        {
                            foreach (var file in email.ATTACHMENTS)
                            {
                                file.CopyTo(stream);
                                writer.Flush();
                                stream.Position = 0;

                                mailMessage.Attachments.Add(new Attachment(file.OpenReadStream(), file.FileName, MediaTypeNames.Application.Octet));
                            }
                        }
                        await smtpClient.SendMailAsync(mailMessage);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("SendMailAsync exception: " + ex);
            }
            finally
            {
                Console.WriteLine("SendMailAsync done");
            }
        }
    }
}
