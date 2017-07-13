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

namespace TonquishCreek.Messaging.SimpleInjector
{
    internal class SimpleInjectorMessageHandlerFactory : IMessageHandlerFactory
    {
        #region Private Field(s)
        private Container _container;
        #endregion

        #region Constructor(s)
        /// <summary>Initializes a new instance of the <see cref="SimpleInjectorMessageHandlerFactory"/> class using the specified container.</summary>
        /// <param name="container">The <see cref="Container"/> used by the new instance to retrieve handler instances.</param>
        /// <exception cref="ArgumentNullException"><paramref name="container"/> is <c>null</c>.</exception> 
        public SimpleInjectorMessageHandlerFactory(Container container)
        {
            if (container == null)
            {
                throw new ArgumentNullException(nameof(container));
            }

            _container = container;
        }
        #endregion

        #region Public Method(s)
        /// <summary>Returns the message handler used to process the specified message.</summary>
        /// <param name="message">The <see cref="IMessage"/> object to process.</param>
        /// <returns>An object representing the handler that processes the message; otherwise, <c>null</c>.</returns>
        public Object CreateHandlerFor(IMessage message)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            var handlerType = typeof(IMessageHandler<>).MakeGenericType(message.GetType());

            return _container.GetInstance(handlerType);
        }

        /// <summary>Returns the message handler used to process the specified message.</summary>
        /// <typeparam name="TResult">The <see cref="Type"/> of the result returned by the handler after processing the message.</typeparam>
        /// <param name="message">The <see cref="IMessage{TResult}"/> object to process.</param>
        /// <returns>An object representing the handler that processes the message; otherwise, <c>null</c>.</returns>
        public Object CreateHandlerFor<TResult>(IMessage<TResult> message)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            var handlerType = typeof(IMessageHandler<,>).MakeGenericType(message.GetType(), typeof(TResult));

            return _container.GetInstance(handlerType);
        }

        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public void Dispose()
        {
        }

        /// <summary>Releases the specified handler which allows reuse of an existing instance of the handler depending on its lifestyle.</summary>
        /// <param name="handler">The handler to release. </param>
        public void Release(Object handler)
        {
        }
        #endregion
    }
}
