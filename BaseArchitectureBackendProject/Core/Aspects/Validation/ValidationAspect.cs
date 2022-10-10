using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Interceptors;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Aspects.Validation
{
    //Validasyon için AOP
    public class ValidationAspect : MethodInterception
    {
        //typeof kısmından bize gönderilecek Type'ı handle etmemiz gerekiyor
        private readonly Type _type;

        public ValidationAspect(Type valiatorType)
        {
            _type = valiatorType;
        }

        //Validasyon onBefore olarak çalışsa bizim için yeterli
        protected override void OnBefore(IInvocation invocation)
        {
            var validator = (IValidator)Activator.CreateInstance(_type);

            //AbstractValidator generic bir sınıftı, onun içerisinden verilen generic tipi handle ediyoruz
            var entityType = _type.BaseType.GetGenericArguments()[0];

            //Tipler eşleşiyor ise (AbstractValidator<TEntity> ve method parametresinde ki parametre tipi)
            var entities = invocation.Arguments.Where(t => t.GetType() == entityType);

            //Validasyon işlemine tabii tutuyorum
            foreach (var entity in entities)
            {
                ValidationTool.Validate(validator, entity);
            }
        }
    }
}
