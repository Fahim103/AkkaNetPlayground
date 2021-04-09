using Akka.Actor;

namespace Akka.NetCore.WebApi.Extensions
{
    public class ServiceScopeExtensionIdProvider : ExtensionIdProvider<ServiceScopeExtension>
    {
        public override ServiceScopeExtension CreateExtension(ExtendedActorSystem system)
        {
            return new ServiceScopeExtension();
        }

        public static ServiceScopeExtensionIdProvider Instance = new ServiceScopeExtensionIdProvider();
    }
}
