using Core.Aspects.Validation;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete.DataResults;
using Core.Utilities.Results.Concrete;
using DataAccess.Repositories.UserOperationClaimRepository;
using Entities.Concrete;
using Bussiness.Repositories.UserOperationClaimRepository.Validation.FluentValidation;
using Bussiness.Repositories.UserOperationClaimRepository.Constant;
using Core.Utilities.Business;
using Bussiness.Repositories.OperationClaimRepository;
using Bussiness.Repositories.UserRepository;
using System.Security.Claims;
using Bussiness.Repositories.OperationClaimRepository.Constant;
using Bussiness.Aspects.Security;

namespace Bussiness.Repositories.UserOperationClaimRepository
{
    public class UserOperationClaimService : IUserOperationClaimService
    {
        private readonly IUserOperationClaimDal _userOperationClaimDal;
        private readonly IOperationClaimService _operationClaimService;
        private readonly IUserService _userService;

        public UserOperationClaimService(IUserOperationClaimDal userOperationClaimDal, IOperationClaimService operationClaimService, IUserService userService)
        {
            _userOperationClaimDal = userOperationClaimDal;
            _operationClaimService = operationClaimService;
            _userService = userService;
        }

        [ValidationAspect(typeof(UserOperationClaimValidator))]
        public async Task<IResult> AddAsync(UserOperationClaim claim)
        {
            var result = BusinessRules.Run
                (
                    await IsUserExistAsync(claim.UserId),
                    await IsOperationSetAvaibleForAddAsync(claim),
                    await IsOperationClaimExistAsync(claim.OperationClaimId)
                );

            if (result != null)
                return new ErrorResult(result.Message);

            await _userOperationClaimDal.AddAsync(claim);
            return new SuccessResult(UserOperationClaimMessages.Added);
        }

        public async Task<IResult> DeleteAsync(UserOperationClaim claim)
        {
            await _userOperationClaimDal.DeleteAsync(claim);
            return new SuccessResult(UserOperationClaimMessages.Deleted);
        }

        public async Task<IDataResult<UserOperationClaim>> GetByIdAsync(int id)
        {
            var claim =await _userOperationClaimDal.GetAsync(i => i.Id == id);
            return new SuccessDataResult<UserOperationClaim>(claim);
        }

        [SecuredAspect("User")]
        public async Task<IDataResult<List<UserOperationClaim>>> GetListAsync()
        {
            var claims =await _userOperationClaimDal.GetAllAsync();
            return new SuccessDataResult<List<UserOperationClaim>>(claims);
        }

        [ValidationAspect(typeof(UserOperationClaimValidator))]
        public async Task<IResult> UpdateAsync(UserOperationClaim claim)
        {
            var result = BusinessRules.Run
                (
                    await IsUserExistAsync(claim.UserId),
                    await IsOperationSetAvaibleForUpdateAsync(claim),
                    await IsOperationClaimExistAsync(claim.OperationClaimId)
                );

            if (result != null)
                return new ErrorResult(result.Message);

            await _userOperationClaimDal.UpdateAsync(claim);
            return new SuccessResult(UserOperationClaimMessages.Updated);
        }

        private async Task<IResult> IsUserExistAsync(int userId)
        {
            var response =await _userService.GetByIdAsync(userId);
            var result = response.Data;
            return result == null ? new ErrorResult(UserOperationClaimMessages.UserNotExist) : new SuccessResult();
        }

        private async Task<IResult> IsOperationSetAvaibleForAddAsync(UserOperationClaim claim)
        {
            var result =await _userOperationClaimDal.GetAsync(i => i.Id == claim.Id && i.OperationClaimId == claim.OperationClaimId);
            return result != null ? new ErrorResult(UserOperationClaimMessages.IsOperationSetAvaible) : new SuccessResult();
        }

        private async Task<IResult> IsOperationSetAvaibleForUpdateAsync(UserOperationClaim claim)
        {
            var currentOperationClaim =await _userOperationClaimDal.GetAsync(i => i.Id == claim.Id);
            if (currentOperationClaim.UserId != claim.UserId || currentOperationClaim.OperationClaimId != claim.OperationClaimId)
            {
                var result =await _userOperationClaimDal.GetAsync(i => i.Id == claim.Id && i.OperationClaimId == claim.OperationClaimId);
                return result != null ? new ErrorResult(UserOperationClaimMessages.IsOperationSetAvaible) : new SuccessResult();
            }
            return new SuccessResult();
        }

        private async Task<IResult> IsOperationClaimExistAsync(int operationClaimId)
        {
            var response =await _operationClaimService.GetByIdAsync(operationClaimId);
            var result = response.Data;
            return result == null ? new ErrorResult(UserOperationClaimMessages.OperationClaimExist) : new SuccessResult();
        }

        
    }
}
