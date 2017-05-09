using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtoCommerce.Platform.Core.Modularity;

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
            //TODO: Init database
        }
        public override void Initialize()
        {
            //TODO: Initialize Repository
        }
        public override void PostInitialize()
        {
            //TODO: PostInitialize service
        }
    }
}
