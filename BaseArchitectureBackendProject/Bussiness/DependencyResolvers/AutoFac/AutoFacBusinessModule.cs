using Autofac;
using Autofac.Extras.DynamicProxy;
using Bussiness.Authentication;
using Bussiness.Repositories.EmailParameterRepository;
using Bussiness.Repositories.OperationClaimRepository;
using Bussiness.Repositories.UserOperationClaimRepository;
using Bussiness.Repositories.UserRepository;
using Bussiness.Utilities.File;
using Core.Utilities.Interceptors;
using Core.Utilities.Security.Jwt;
using DataAccess.Repositories.EmailParameterRepository;
using DataAccess.Repositories.OperationClaimRepository;
using DataAccess.Repositories.UserOperationClaimRepository;
using DataAccess.Repositories.UserRepository;
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

            builder.RegisterType<EmailParameterService>().As<IEmailParameterService>();
            builder.RegisterType<EfEmailParameterDal>().As<IEmailParameterDal>();

            builder.RegisterType<AuthService>().As<IAuthService>();

            builder.RegisterType<TokenHandler>().As<ITokenHandler>();

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
