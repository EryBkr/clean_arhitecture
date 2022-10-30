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

        public IResult Add(EmailParameters emailParameters)
        {
            _emailParameterDal.Add(emailParameters);
            return new SuccessResult(EmailParameterMessages.AddedEmail);
        }

        public IResult Delete(EmailParameters emailParameters)
        {
            _emailParameterDal.Delete(emailParameters);
            return new SuccessResult(EmailParameterMessages.DeletedEmail);
        }

        public IDataResult<EmailParameters> GetById(int id)
        {
            return new SuccessDataResult<EmailParameters>(_emailParameterDal.Get(i => i.Id == id));
        }

        public IDataResult<List<EmailParameters>> GetList()
        {
            return new SuccessDataResult<List<EmailParameters>>(_emailParameterDal.GetAll());
        }

        public IResult SendEmail(EmailParameters emailParameters, string body, string subject, string emails)
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
                    smtp.Send(mail);
                }
            }
            return new SuccessResult("Mail başarıyla gönderilmiştir");
        }

        public IResult Update(EmailParameters emailParameters)
        {
            _emailParameterDal.Update(emailParameters);
            return new SuccessResult(EmailParameterMessages.UpdatedEmail);
        }
    }
}
