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
    /// <summary>Abstract base class for all message handlers.</summary>
    public abstract class MessageHandlerBase : IDisposable
    {
        #region Private Variable(s)
        /// <summary>Indicates if the object has been disposed. Used to block redundant calls.</summary>
        [NonSerialized()]
        private volatile Boolean _isDisposed;
        #endregion

        #region Constructor(s)
        /// <summary>Creates a new instance of the <see cref="MessageHandlerBase" /> class.</summary>
        protected MessageHandlerBase()
            : base()
        {
        }
        #endregion

        #region Destructor(s)
        /// <summary>Finalizes the current instance of the <see cref="MessageHandlerBase" /> class.</summary>
        /// <remarks>
        /// <para>
        /// The destructor will be called by the Garbage Collector if the object has not been explicitly disposed.
        /// This means that the instance will remain for an additional cycle before the memory can be reclaimed by the GC.
        /// </para>
        /// <para>
        /// If the object has been explicitly disposed, by calling the public Dispose() method, the destructor will NOT be called.
        /// </para>
        /// <para>
        /// The boolean argument to the protected Dispose(bool) method indicates whether the call is coming from the public Dispose() method or the destructor.
        /// This ensures that we don't try to make changes twice should the destructor be called after the object has been explicitly disposed.
        /// </para>
        /// </remarks>
        ~MessageHandlerBase()
        {
            Dispose(false);
        }
        #endregion

        #region Public Method(s)
        /// <summary>Performs application-defined tasks associated with freeing, releasing or resetting unmanaged resources.</summary>
        public void Dispose()
        {
            if (!_isDisposed)
            {
                // Do not change this code. Put cleanup code in Dispose(System.Boolean disposing) method instead.
                Dispose(true);
                GC.SuppressFinalize(this);
            }
        }
        #endregion

        #region Protected Method(s)
        /// <summary>Releases the managed resources used by the object and optionally releases the unmanaged resources.</summary>
        /// <param name="disposing"><c>true</c> to release managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        /// <remarks>
        /// A good reference source covering the Disposable pattern and how it's used can be found at: http://msdn.microsoft.com/msdnmag/issues/07/07/CLRInsideOut/.
        /// Pattern guidelines can be found at: http://www.bluebytesoftware.com/blog/PermaLink.aspx?guid=88e62cdf-5919-4ac7-bc33-20c06ae539ae.
        /// </remarks>
        protected virtual void Dispose(Boolean disposing)
        {
            _isDisposed = true;
        }

        /// <summary>Throws an <see cref="ObjectDisposedException"/> if the current instance has already been disposed.</summary>
        protected internal void ThrowIfDisposed()
        {
            if (_isDisposed)
            {
                throw new ObjectDisposedException(GetType().Name);
            }
        }
        #endregion
    }

    /// <summary>Abstract base class for all message handlers.</summary>
    /// <typeparam name="TMessage">The <see cref="Type"/> of the message handled by the implementing class.</typeparam>
    public abstract class MessageHandlerBase<TMessage> : MessageHandlerBase, IMessageHandler<TMessage>
        where TMessage : class, IMessage
    {
        #region Constructor(s)
        /// <summary>Creates a new instance of the <see cref="MessageHandlerBase{TMessage}" /> class.</summary>
        protected MessageHandlerBase()
            : base()
        {
        }
        #endregion

        #region Public Method(s)
        /// <summary>Handles the specified message.</summary>
        /// <param name="message">The <see cref="IMessage"/> to handle.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to monitor for cancellation requests.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation containing the data returned from the handler.</returns>
        public abstract Task HandleAsync(TMessage message, CancellationToken cancellationToken);
        #endregion
    }

    /// <summary>Abstract base class for all message handlers.</summary>
    /// <typeparam name="TMessage">The <see cref="Type"/> of the message handled by the implementing class.</typeparam>
    /// <typeparam name="TResult">The <see cref="Type"/> of the result(s) returned by the handler.</typeparam>
    public abstract class MessageHandlerBase<TMessage, TResult> : MessageHandlerBase, IMessageHandler<TMessage, TResult>
        where TMessage : class, IMessage<TResult>
    {
        #region Constructor(s)
        /// <summary>Creates a new instance of the <see cref="MessageHandlerBase{TMessage, TResult}" /> class.</summary>
        protected MessageHandlerBase()
            : base()
        {
        }
        #endregion

        #region Public Method(s)
        /// <summary>Handles the specified message.</summary>
        /// <param name="message">The <see cref="IMessage{TResult}"/> to handle.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to monitor for cancellation requests.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation containing the data returned from the handler.</returns>
        public abstract Task<TResult> HandleAsync(TMessage message, CancellationToken cancellationToken);
        #endregion
    }
}
