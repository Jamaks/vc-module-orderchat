using Jamak.OrderChatModule.Web.Migrations;
using Jamak.OrderChatModule.Web.Services;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtoCommerce.Platform.Core.Modularity;
using VirtoCommerce.Platform.Data.Infrastructure;
using VirtoCommerce.Platform.Data.Infrastructure.Interceptors;

namespace Jamak.OrderChatModule.Web
{
    public class Module : ModuleBase
    {
        private const string _connectionStringName = "VirtoCommerce";
        private readonly IUnityContainer _container;

        public Module(IUnityContainer container)
        {
            _container = container;
        }
        public override void SetupDatabase()
        {
            using (var db = new OrderChatRepository(_connectionStringName, _container.Resolve<AuditableInterceptor>()))
            {
                var initializer = new SetupDatabaseInitializer<OrderChatRepository, Configuration>();
                initializer.InitializeDatabase(db);
            }
        }
        public override void Initialize()
        {
            base.Initialize();
            _container.RegisterType<IOrderChatRepository>(new InjectionFactory(c => new OrderChatRepository(_connectionStringName, _container.Resolve<AuditableInterceptor>(), new EntityPrimaryKeyGeneratorInterceptor())));
            _container.RegisterType<IOrderChatService, OrderChatService>();
        }
        public override void PostInitialize()
        {
            base.PostInitialize();
        }
    }
}
