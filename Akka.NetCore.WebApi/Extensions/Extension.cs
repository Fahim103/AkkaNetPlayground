using Akka.Actor;
using Microsoft.Extensions.DependencyInjection;

namespace Akka.NetCore.WebApi.Extensions
{
    public static class Extension
    {
        public static void AddServiceScopeFactory(this ActorSystem actorSystem, IServiceScopeFactory serviceScopeFactory)
        {
            actorSystem.RegisterExtension(ServiceScopeExtensionIdProvider.Instance);
            ServiceScopeExtensionIdProvider.Instance.Get(actorSystem).Initialize(serviceScopeFactory);
        }

        public static IServiceScope CreateScope(this IActorContext context)
        {
            return ServiceScopeExtensionIdProvider.Instance.Get(context.System).CreateScope();
        }
    }
}
