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
        public IResult Add(UserOperationClaim claim)
        {
            var result = BusinessRules.Run
                (
                    IsUserExist(claim.UserId),
                    IsOperationSetAvaibleForAdd(claim), 
                    IsOperationClaimExist(claim.OperationClaimId)
                );

            if (result != null)
                return new ErrorResult(result.Message);

            _userOperationClaimDal.Add(claim);
            return new SuccessResult(UserOperationClaimMessages.Added);
        }

        public IResult Delete(UserOperationClaim claim)
        {
            _userOperationClaimDal.Delete(claim);
            return new SuccessResult(UserOperationClaimMessages.Deleted);
        }

        public IDataResult<UserOperationClaim> GetById(int id)
        {
            var claim = _userOperationClaimDal.Get(i => i.Id == id);
            return new SuccessDataResult<UserOperationClaim>(claim);
        }

        public IDataResult<List<UserOperationClaim>> GetList()
        {
            var claims = _userOperationClaimDal.GetAll();
            return new SuccessDataResult<List<UserOperationClaim>>(claims);
        }

        [ValidationAspect(typeof(UserOperationClaimValidator))]
        public IResult Update(UserOperationClaim claim)
        {
            var result = BusinessRules.Run
                (
                    IsUserExist(claim.UserId),
                    IsOperationSetAvaibleForUpdate(claim),
                    IsOperationClaimExist(claim.OperationClaimId)
                );

            if (result != null)
                return new ErrorResult(result.Message);

            _userOperationClaimDal.Update(claim);
            return new SuccessResult(UserOperationClaimMessages.Updated);
        }

        private IResult IsUserExist(int userId)
        {
            var result = _userService.GetById(userId).Data;
            return result == null ? new ErrorResult(UserOperationClaimMessages.UserNotExist) : new SuccessResult();
        }

        private IResult IsOperationSetAvaibleForAdd(UserOperationClaim claim)
        {
            var result = _userOperationClaimDal.Get(i => i.Id == claim.Id && i.OperationClaimId == claim.OperationClaimId);
            return result != null ? new ErrorResult(UserOperationClaimMessages.IsOperationSetAvaible) : new SuccessResult();
        }

        private IResult IsOperationSetAvaibleForUpdate(UserOperationClaim claim)
        {
            var currentOperationClaim = _userOperationClaimDal.Get(i => i.Id == claim.Id);
            if (currentOperationClaim.UserId != claim.UserId || currentOperationClaim.OperationClaimId != claim.OperationClaimId)
            {
                var result = _userOperationClaimDal.Get(i => i.Id == claim.Id && i.OperationClaimId == claim.OperationClaimId);
                return result != null ? new ErrorResult(UserOperationClaimMessages.IsOperationSetAvaible) : new SuccessResult();
            }
            return new SuccessResult();
        }

        private IResult IsOperationClaimExist(int operationClaimId)
        {
            var result = _operationClaimService.GetById(operationClaimId).Data;
            return result == null ? new ErrorResult(UserOperationClaimMessages.OperationClaimExist) : new SuccessResult();
        }

        
    }
}
