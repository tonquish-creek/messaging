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
    /// <summary>Represents the object that sends messages to the appropriate handler.</summary>
    public interface IMessageSender
    {
        #region Method(s)
        /// <summary>Sends the specified message asynchronously to the appropriate handler.</summary>
        /// <param name="message">The <see cref="IMessage"/> object to send.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task SendAsync(IMessage message);

        /// <summary>Sends the specified message asynchronously to the appropriate handler.</summary>
        /// <param name="message">The <see cref="IMessage"/> object to send.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to monitor for cancellation requests.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task SendAsync(IMessage message, CancellationToken cancellationToken);


        /// <summary>Sends the specified message asynchronously to the appropriate handler and returns the result of the operation.</summary>
        /// <typeparam name="TResult">The <see cref="Type"/> of the result(s) to return.</typeparam>
        /// <param name="message">The <see cref="IMessage{TResult}"/> object to send.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation containing the data returned from the handler.</returns>
        Task<TResult> SendAsync<TResult>(IMessage<TResult> message);

        /// <summary>Sends the specified message asynchronously to the appropriate handler and returns the result of the operation.</summary>
        /// <typeparam name="TResult">The <see cref="Type"/> of the result(s) to return.</typeparam>
        /// <param name="message">The <see cref="IMessage{TResult}"/> object to send.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to monitor for cancellation requests.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation containing the data returned from the handler.</returns>
        Task<TResult> SendAsync<TResult>(IMessage<TResult> message, CancellationToken cancellationToken);
        #endregion
    }
}
