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
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace TonquishCreek.Messaging
{
    /// <summary>Default component that sends messages to the appropriate handler.</summary>
    internal sealed class MessageSender : IMessageSender
    {
        #region Constructor(s)
        /// <summary>Initializes a new instance of the <see cref="MessageSender"/> class.</summary>
        /// <param name="factory">The <see cref="IMessageHandlerFactory"/> object used by the new instance to create message handlers.</param>
        /// <exception cref="ArgumentNullException"><paramref name="factory"/> is <c>null</c>.</exception> 
        public MessageSender(IMessageHandlerFactory factory)
        {
            if (factory == null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            Factory = factory;
        }
        #endregion

        #region Public Properties
        /// <summary>Gets the <see cref="IMessageHandlerFactory"/> object used by the current instance to create new message handlers.</summary>
        /// <returns>The <see cref="IMessageHandlerFactory"/> used by the current instance.</returns>
        public IMessageHandlerFactory Factory { get; private set; }
        #endregion

        #region Public Method(s)
        /// <summary>Sends the specified message asynchronously to the appropriate handler.</summary>
        /// <param name="message">The <see cref="IMessage"/> object to send.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="message"/> is <c>null</c>.</exception> 
        /// <exception cref="InvalidOperationException">A handler could not be found for the specified message type.</exception> 
        public Task SendAsync(IMessage message)
        {
            return SendAsync(message, CancellationToken.None);
        }

        /// <summary>Sends the specified message asynchronously to the appropriate handler.</summary>
        /// <param name="message">The <see cref="IMessage"/> object to send.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to monitor for cancellation requests.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="message"/> is <c>null</c>.</exception> 
        /// <exception cref="InvalidOperationException">A handler could not be found for the specified message type.</exception> 
        public Task SendAsync(IMessage message, CancellationToken cancellationToken)
        {
            var handler = Factory.CreateHandlerFor(message);

            try
            {
                return (Task)Invoke(handler, message, cancellationToken);
            }
            finally
            {
                Factory.Release(handler);
            }
        }

        /// <summary>Sends the specified message asynchronously to the appropriate handler and returns the result of the operation.</summary>
        /// <typeparam name="TResult">The <see cref="Type"/> of the result(s) to return.</typeparam>
        /// <param name="message">The <see cref="IMessage{TResult}"/> object to send.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation containing the data returned from the handler.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="message"/> is <c>null</c>.</exception> 
        /// <exception cref="InvalidOperationException">A handler could not be found for the specified message type.</exception> 
        public Task<TResult> SendAsync<TResult>(IMessage<TResult> message)
        {
            return SendAsync(message, CancellationToken.None);
        }

        /// <summary>Sends the specified message asynchronously to the appropriate handler and returns the result of the operation.</summary>
        /// <typeparam name="TResult">The <see cref="Type"/> of the result(s) to return.</typeparam>
        /// <param name="message">The <see cref="IMessage{TResult}"/> object to send.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to monitor for cancellation requests.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation containing the data returned from the handler.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="message"/> is <c>null</c>.</exception> 
        /// <exception cref="InvalidOperationException">A handler could not be found for the specified message type.</exception> 
        public Task<TResult> SendAsync<TResult>(IMessage<TResult> message, CancellationToken cancellationToken)
        {
            var handler = Factory.CreateHandlerFor(message);

            try
            {
                return (Task<TResult>)Invoke(handler, message, cancellationToken);
            }
            finally
            {
                Factory.Release(handler);
            }
        }
        #endregion

        #region Private Method(s)
        private Object Invoke(Object handler, Object message, CancellationToken cancellationToken)
        {
            try
            {
                // We have to do this because we aren't returning a strongly-typed interface
                var method = handler.GetType().GetRuntimeMethod("HandleAsync", new[] { message.GetType(), typeof(CancellationToken) });

                return method.Invoke(handler, new Object[] { message, cancellationToken });
            }
            finally
            {
                Factory.Release(handler);
            }
        }
        #endregion
    }
}
