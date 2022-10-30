using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness.Repositories.EmailParameterRepository
{
    public interface IEmailParameterService
    {
        Task<IResult> AddAsync(EmailParameters emailParameters);
        Task<IResult> UpdateAsync(EmailParameters emailParameters);
        Task<IResult> DeleteAsync(EmailParameters emailParameters);
        Task<IDataResult<List<EmailParameters>>> GetListAsync();
        Task<IDataResult<EmailParameters>> GetByIdAsync(int id);
        Task<IResult> SendEmailAsync(EmailParameters emailParameters, string body, string subject, string emails);
    }
}
