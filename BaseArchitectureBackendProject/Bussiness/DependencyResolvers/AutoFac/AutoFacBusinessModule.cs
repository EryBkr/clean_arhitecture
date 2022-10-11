using Autofac;
using Autofac.Extras.DynamicProxy;
using Bussiness.Abstract;
using Bussiness.Concrete;
using Core.Utilities.Interceptors;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness.DependencyResolvers.AutoFac
{
    //Module bize AutoFac Library'sinden gelmektedir
    public class AutoFacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //DI Tanımlamalarımı AutoFac ile yapıyorum
            builder.RegisterType<OperationClaimService>().As<IOperationClaimService>();
            builder.RegisterType<EfOperationClaimDal>().As<IOperationClaimDal>();

            builder.RegisterType<UserService>().As<IUserService>();
            builder.RegisterType<EfUserDal>().As<IUserDal>();

            builder.RegisterType<UserOperationClaimService>().As<IUserOperationClaimService>();
            builder.RegisterType<EfUserOperationClaimDal>().As<IUserOperationClaimDal>();

            builder.RegisterType<AuthService>().As<IAuthService>();

            builder.RegisterType<FileService>().As<IFileService>().SingleInstance();

            //AOP için ekledik
            var assembly = System.Reflection.Assembly.GetExecutingAssembly();
            builder.RegisterAssemblyTypes(assemblies: assembly)
                .AsImplementedInterfaces()
                .EnableInterfaceInterceptors(
                new Castle.DynamicProxy.ProxyGenerationOptions() 
                {
                    Selector=new AspectInterceptorSelector()
                }).SingleInstance();
        }
    }
}
