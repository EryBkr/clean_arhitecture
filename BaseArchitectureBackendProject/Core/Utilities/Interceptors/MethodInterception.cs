using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Interceptors
{
    public class MethodInterception : MethodInterceptionBaseAttribute
    {
        //Invocation method hakkında ki herşeydir , gelen giden verilerde dahildir
        //Biz burada metoda gelen isteklerin öncesi ve sonrası gibi her aşamasına karşılık bir metot oluşturduk
        protected virtual void OnBefore(IInvocation invocation) { }
        protected virtual void OnAfter(IInvocation invocation) { }
        protected virtual void OnException(IInvocation invocation, Exception exception) { }
        protected virtual void OnSuccess(IInvocation invocation) { }

        //Metodun komple yaşam döngüsü
        public override void Intercept(IInvocation invocation)
        {
            var isSucess = true;
            OnBefore(invocation);
            try
            {
                //Method devam etsin
                invocation.Proceed();
            }
            catch (Exception exception)
            {
                isSucess = false;
                OnException(invocation,exception);
                throw;
            }
            finally
            {
                if (isSucess)
                    OnSuccess(invocation);
            }
            OnAfter(invocation);
        }
    }
}
