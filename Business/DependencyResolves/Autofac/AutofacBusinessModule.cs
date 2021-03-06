using Autofac;//Dikkat et 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac.Extras.DynamicProxy;
using Business.Abstract;
using Business.Concrete;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using Core.Utilities.Security.Jwt;
using DataAccess.Abstract;
using DataAccess.Concrete.Dapper;
using DataAccess.Concrete.EntityFramework;


namespace Business.DependencyResolves.Autofac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //Eğer birisi constructer'ında(yapıcı metod) IProductService isterse ona ProductManager vericez
            builder.RegisterType<OperationClaimManager>().As<IOperationClaimService>();
            builder.RegisterType<EfOperationClaimDal>().As<IOperationClaimDal>();

            builder.RegisterType<UserOperationClaimManager>().As<IUserOperationClaimService>();
            builder.RegisterType<EfUserOperationClaimDal>().As<IUserOperationClaimDal>();


            builder.RegisterType<AddressManager>().As<IAddressService>();
            builder.RegisterType<DapperAddressDal>().As<IAddressDal>();


            builder.RegisterType<TelephoneDirectoryManager>().As<ITelephoneDirectoryService>();
            builder.RegisterType<DapperTelephoneDirectoryDal>().As<ITelephoneDirectoryDal>();


            builder.RegisterType<PhoneAddressManager>().As<IPhoneAddressService>();
            builder.RegisterType<EfPhoneAddressDal>().As<IPhoneAddressDal>();



            builder.RegisterType<UserManager>().As<IUserService>();
            builder.RegisterType<EfUserDal>().As<IUserDal>();


            builder.RegisterType<AuthManager>().As<IAuthService>();
            builder.RegisterType<JwtHelper>().As<ITokenHelper>();


            //Yukarıdaki nesneler için bir tane interceptor çalıştırcam
            var assembly = System.Reflection.Assembly.GetExecutingAssembly();//Mevcut assembly'e ulaştım

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces().EnableInterfaceInterceptors(
                new ProxyGenerationOptions()
                {
                    // Araya girecek olan nesneyi belirt
                    Selector = new AspectInterceptorSelector()
                }).SingleInstance(); //Tek bir Instance oluştursun
        }
    }
}
