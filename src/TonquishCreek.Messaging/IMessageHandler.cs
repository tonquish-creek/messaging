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
using System.Threading;
using System.Threading.Tasks;

namespace TonquishCreek.Messaging
{
    /// <summary>Represents a message handler.</summary>
    /// <typeparam name="TMessage">The <see cref="Type"/> of the message handled by the implementing class.</typeparam>
    public interface IMessageHandler<TMessage>
        where TMessage : IMessage
    {
        #region Method(s)
        /// <summary>Handles the specified message.</summary>
        /// <param name="message">The <see cref="IMessage"/> to handle.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to monitor for cancellation requests.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation containing the data returned from the handler.</returns>
        Task HandleAsync(TMessage message, CancellationToken cancellationToken);
        #endregion
    }

    /// <summary>Represents a message handler.</summary>
    /// <typeparam name="TMessage">The <see cref="Type"/> of the message handled by the implementing class.</typeparam>
    /// <typeparam name="TResult">The <see cref="Type"/> of the result(s) returned by the handler.</typeparam>
    public interface IMessageHandler<TMessage, TResult>
        where TMessage : IMessage<TResult>
    {
        #region Method(s)
        /// <summary>Handles the specified message.</summary>
        /// <param name="message">The <see cref="IMessage{TResult}"/> to handle.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to monitor for cancellation requests.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation containing the data returned from the handler.</returns>
        Task<TResult> HandleAsync(TMessage message, CancellationToken cancellationToken);
        #endregion
    }
}
