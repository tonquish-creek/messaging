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

namespace TonquishCreek.Messaging
{
    /// <summary>Provides method for creating new message handlers.</summary>
    public interface IMessageHandlerFactory : IDisposable
    {
        #region Method(s)
        /// <summary>Returns the message handler used to process the specified message.</summary>
        /// <param name="message">The <see cref="IMessage"/> object to process.</param>
        /// <returns>An object representing the handler that processes the message; otherwise, <c>null</c>.</returns>
        Object CreateHandlerFor(IMessage message);

        /// <summary>Returns the message handler used to process the specified message.</summary>
        /// <typeparam name="TResult">The <see cref="Type"/> of the result returned by the handler after processing the message.</typeparam>
        /// <param name="message">The <see cref="IMessage{TResult}"/> object to process.</param>
        /// <returns>An object representing the handler that processes the message; otherwise, <c>null</c>.</returns>
        Object CreateHandlerFor<TResult>(IMessage<TResult> message);

        /// <summary>Releases the specified handler which allows reuse of an existing instance of the handler depending on its lifestyle.</summary>
        /// <param name="handler">The handler to release. </param>
        void Release(Object handler);
        #endregion
    }
}
