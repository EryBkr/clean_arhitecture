using Bussiness.Repositories.OperationClaimRepository.Constant;
using Bussiness.Repositories.OperationClaimRepository.Validation.FluentValidation;
using Core.Aspects.Performance;
using Core.Aspects.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using Core.Utilities.Results.Concrete.DataResults;
using DataAccess.Repositories.OperationClaimRepository;
using Entities.Concrete;
using System.Xml.Linq;

namespace Bussiness.Repositories.OperationClaimRepository
{
    public class OperationClaimService : IOperationClaimService
    {
        private readonly IOperationClaimDal _operationClaimDal;

        public OperationClaimService(IOperationClaimDal operationClaimDal)
        {
            _operationClaimDal = operationClaimDal;
        }

        [ValidationAspect(typeof(OperationClaimValidator))]
        public async Task<IResult> AddAsync(OperationClaim claim)
        {
            IResult result = BusinessRules.Run(await IsNameAvaibleForAddAsync(claim.Name));

            if (result != null)
                return new ErrorResult(result.Message);

            await _operationClaimDal.AddAsync(claim);
            return new SuccessResult(OperationClaimMessages.Added);
        }

        public async Task<IResult> DeleteAsync(OperationClaim claim)
        {
            await _operationClaimDal.DeleteAsync(claim);
            return new SuccessResult(OperationClaimMessages.Deleted);
        }

        public async Task<IDataResult<OperationClaim>> GetByIdAsync(int id)
        {
            var claim = await _operationClaimDal.GetAsync(i => i.Id == id);
            return new SuccessDataResult<OperationClaim>(claim);
        }

        [PerformanceAspect(1)]
        public async Task<IDataResult<List<OperationClaim>>> GetListAsync()
        {
            var claims = await _operationClaimDal.GetAllAsync();
            return new SuccessDataResult<List<OperationClaim>>(claims);
        }

        [ValidationAspect(typeof(OperationClaimValidator))]
        public async Task<IResult> UpdateAsync(OperationClaim claim)
        {
            IResult result = BusinessRules.Run(await IsNameAvaibleForUpdateAsync(claim));

            if (result != null)
                return new ErrorResult(result.Message);


            await _operationClaimDal.UpdateAsync(claim);
            return new SuccessResult(OperationClaimMessages.Updated);
        }

        //İsim daha önce kullanılmış mı? (Ekleme operasyonu için)
        private async Task<IResult> IsNameAvaibleForAddAsync(string name)
        {
            var result = await _operationClaimDal.GetAsync(i => i.Name == name);
            return result != null ? new ErrorResult(OperationClaimMessages.NameIsNotAvaible) : new SuccessResult();
        }

        //İsim daha önce kullanılmış mı? (Güncelleme operasyonu için)
        private async Task<IResult> IsNameAvaibleForUpdateAsync(OperationClaim claim)
        {
            var currentOperationClaim = await _operationClaimDal.GetAsync(i => i.Id == claim.Id);
            if (currentOperationClaim.Name != claim.Name)
            {
                var currentResult = await _operationClaimDal.GetAsync(i => i.Name == claim.Name);
                return currentResult != null ? new ErrorResult(OperationClaimMessages.NameIsNotAvaible) : new SuccessResult();
            }
            return new SuccessResult();
        }
    }
}
