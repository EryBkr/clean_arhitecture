using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using System.Transactions;

namespace Core.Aspects.Transaction
{
    public class TransactionAspect:MethodInterception
    {
        //Transaction'ı intercept olarak tanımlama nedenimiz bu işlem aksiyon esnasında gerçekleşecektir
        public override void Intercept(IInvocation invocation)
        {
            using (TransactionScope transactionScope=new TransactionScope())
            {
                try
                {
                    //Çalışmayı sürdür
                    invocation.Proceed();
                    transactionScope.Complete();
                }
                catch (Exception exception)
                {
                    transactionScope.Dispose();
                    throw;
                }
            }
        }
    }
}
