﻿using Akka.Actor;
using Microsoft.Extensions.DependencyInjection;

namespace Akka.NetCore.WebApi.Extensions
{
    public class ServiceScopeExtension : IExtension
    {
        private IServiceScopeFactory _serviceScopeFactory;

        public void Initialize(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public IServiceScope CreateScope()
        {
            return _serviceScopeFactory.CreateScope();
        }
    }
}
