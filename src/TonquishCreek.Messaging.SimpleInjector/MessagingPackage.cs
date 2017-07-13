#region Copyright Tonquish Creek Software
/*
 * Framework for implementing simple messaging in .NET applications.
 *
 * Copyright (c) 2017 Tonquish Creek Software. All rights reserved.
 * 
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.
 * 
 * See the GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program. If not, see <http://www.gnu.org/licenses/>.
 */
#endregion

using System;
using SimpleInjector;
using SimpleInjector.Packaging;

namespace TonquishCreek.Messaging.SimpleInjector
{
    /// <summary>Registers messaging types and services with the Simple Injector container.</summary>
    public sealed class MessagingPackage : IPackage
    {
        #region Constructor(s)
        /// <summary>Initializes a new instance of the <see cref="MessagingPackage"/> class.</summary>
        public MessagingPackage()
        {
        }
        #endregion

        #region Public Method(s)
        /// <summary>Registers the set of services in the specified container.</summary>
        /// <param name="container">The container into which the set of services is registered.</param>
        /// <exception cref="ArgumentNullException"><paramref name="container"/> is <c>null</c>.</exception> 
        public void RegisterServices(Container container)
        {
            if (container == null)
            {
                throw new ArgumentNullException(nameof(container));
            }

            container.RegisterSingleton<IMessageSender, MessageSender>();
            container.RegisterSingleton<IMessageHandlerFactory, SimpleInjectorMessageHandlerFactory>();

            // Register handlers
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            container.Register(typeof(IMessageHandler<>), assemblies, Lifestyle.Scoped);
            container.Register(typeof(IMessageHandler<,>), assemblies, Lifestyle.Scoped);
        }
        #endregion
    }
}
