using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;
using Moq;
using Ninject;
//using MASTS.WebUI.Infrastructure.Abstract;
//using MASTS.WebUI.Infrastructure.Concrete;
using MASTS.Domain.Concrete;
using MASTS.Domain.Abstract;

namespace MASTS.WebUI.Infrastructure
{

    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;
        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            kernel.Bind<IApplicationTablesRepository>().To<EFApplicationTableRepository>();

            //kernel.Bind<IAuthProvider>().To<FormsAuthProvider>();
        }
    }
}