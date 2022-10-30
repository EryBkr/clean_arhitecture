using Bussiness.Repositories.EmailParameterRepository.Constant;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using Core.Utilities.Results.Concrete.DataResults;
using DataAccess.Repositories.EmailParameterRepository;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness.Repositories.EmailParameterRepository
{
    public class EmailParameterService : IEmailParameterService
    {
        private readonly IEmailParameterDal _emailParameterDal;

        public EmailParameterService(IEmailParameterDal emailParameterDal)
        {
            _emailParameterDal = emailParameterDal;
        }

        public async Task<IResult> AddAsync(EmailParameters emailParameters)
        {
            await _emailParameterDal.AddAsync(emailParameters);
            return new SuccessResult(EmailParameterMessages.AddedEmail);
        }

        public async Task<IResult> DeleteAsync(EmailParameters emailParameters)
        {
            await _emailParameterDal.DeleteAsync(emailParameters);
            return new SuccessResult(EmailParameterMessages.DeletedEmail);
        }

        public async Task<IDataResult<EmailParameters>> GetByIdAsync(int id)
        {
            return new SuccessDataResult<EmailParameters>(await _emailParameterDal.GetAsync(i => i.Id == id));
        }

        public async Task<IDataResult<List<EmailParameters>>> GetListAsync()
        {
            return new SuccessDataResult<List<EmailParameters>>(await _emailParameterDal.GetAllAsync());
        }

        public async Task<IResult> SendEmailAsync(EmailParameters emailParameters, string body, string subject, string emails)
        {
            using (MailMessage mail = new MailMessage())
            {
                string[] setEmails = emails.Split(",");
                mail.From = new MailAddress(emailParameters.Email);

                foreach (var email in setEmails)
                    mail.To.Add(email);

                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = emailParameters.Html;

                using (SmtpClient smtp=new SmtpClient(emailParameters.SMTP))
                {
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential(emailParameters.Email, emailParameters.Password);
                    smtp.EnableSsl = emailParameters.SSL;
                    smtp.Port = emailParameters.Port;
                    await smtp.SendMailAsync(mail);
                }
            }
            return new SuccessResult("Mail başarıyla gönderilmiştir");
        }

        public async Task<IResult> UpdateAsync(EmailParameters emailParameters)
        {
            await _emailParameterDal.UpdateAsync(emailParameters);
            return new SuccessResult(EmailParameterMessages.UpdatedEmail);
        }
    }
}
