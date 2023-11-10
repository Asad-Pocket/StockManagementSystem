using Autofac;
using StockManagementSystem.Models;

namespace StockManagementSystem
{
    public class WebModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<RegisterModel>().AsSelf();

            builder.RegisterType<LoginModel>().AsSelf();

            base.Load(builder);
        }
    }
}
