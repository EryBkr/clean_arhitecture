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
        public IResult Add(OperationClaim claim)
        {
            IResult result = BusinessRules.Run(IsNameAvaibleForAdd(claim.Name));

            if (result != null)
                return new ErrorResult(result.Message);

            _operationClaimDal.Add(claim);
            return new SuccessResult(OperationClaimMessages.Added);
        }

        public IResult Delete(OperationClaim claim)
        {
            _operationClaimDal.Delete(claim);
            return new SuccessResult(OperationClaimMessages.Deleted);
        }

        public IDataResult<OperationClaim> GetById(int id)
        {
            var claim = _operationClaimDal.Get(i => i.Id == id);
            return new SuccessDataResult<OperationClaim>(claim);
        }

        [PerformanceAspect(1)]
        public IDataResult<List<OperationClaim>> GetList()
        {
            var claims = _operationClaimDal.GetAll();
            return new SuccessDataResult<List<OperationClaim>>(claims);
        }

        [ValidationAspect(typeof(OperationClaimValidator))]
        public IResult Update(OperationClaim claim)
        {
            IResult result = BusinessRules.Run(IsNameAvaibleForUpdate(claim));

            if (result != null)
                return new ErrorResult(result.Message);


            _operationClaimDal.Update(claim);
            return new SuccessResult(OperationClaimMessages.Updated);
        }

        //İsim daha önce kullanılmış mı? (Ekleme operasyonu için)
        private IResult IsNameAvaibleForAdd(string name)
        {
            var result = _operationClaimDal.Get(i => i.Name == name);
            return result != null ? new ErrorResult(OperationClaimMessages.NameIsNotAvaible) : new SuccessResult();
        }

        //İsim daha önce kullanılmış mı? (Güncelleme operasyonu için)
        private IResult IsNameAvaibleForUpdate(OperationClaim claim)
        {
            var currentOperationClaim = _operationClaimDal.Get(i => i.Id == claim.Id);
            if (currentOperationClaim.Name != claim.Name)
            {
                var currentResult = _operationClaimDal.Get(i => i.Name == claim.Name);
                return currentResult != null ? new ErrorResult(OperationClaimMessages.NameIsNotAvaible) : new SuccessResult();
            }
            return new SuccessResult();
        }
    }
}
