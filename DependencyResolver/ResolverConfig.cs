using BLL.Interface;
using BLL.Services;
using DAL;
using DAL.Interfaces;
using Ninject;
using Ninject.Web.Common;
using ORM;
using System.Data.Entity;

namespace DependencyResolver
{
    public static class ResolverConfig
    {
        public static void ConfigurateResolverWeb(this IKernel kernel)
        {
            Configure(kernel, true);
        }
        public static void ConfigurateResolverConsole(this IKernel kernel)
        {
            Configure(kernel, false);
        }
        private static void Configure(IKernel kernel, bool isWeb)
        {
            if (isWeb)
            {
                
                kernel.Bind<DbContext>().To<BlogModel>().InRequestScope();
            }
            else
            {
                kernel.Bind<DbContext>().To<BlogModel>().InSingletonScope();
            }

            kernel.Bind<IUSerService>().To<UserService>();
            kernel.Bind<IPostService>().To<PostService>();
            kernel.Bind<IService<Comments>>().To<CommentService>();

            kernel.Bind<IUserRepository>().To<UserRepository>();
            kernel.Bind<IRepository<Roles>>().To<RoleRepository>();
            kernel.Bind<IRepository<Comments>>().To<CommentRepository>();
            kernel.Bind<IRepository<Posts>>().To<PostReposytory>();

        }
    }
}
