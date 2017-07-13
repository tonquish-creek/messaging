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
using System.Diagnostics.CodeAnalysis;

namespace TonquishCreek.Messaging
{
    /// <summary>Represents a simple command message that does not return data.</summary>
    [SuppressMessage("Microsoft.Design", "CA1040:AvoidEmptyInterfaces", Justification = "This is needed for proper routing and type enforcement. It is empty because there is nothing to put here.")]
    public interface IMessage
    {
    }

    /// <summary>Represents a request message that results in a response containing data of the specified type.</summary>
    /// <typeparam name="TResult">The <see cref="Type"/> of the result(s) returned when the message is processed.</typeparam>
    [SuppressMessage("Microsoft.Design", "CA1040:AvoidEmptyInterfaces", Justification = "This is needed for proper routing and type enforcement. It is empty because there is nothing to put here.")]
    public interface IMessage<TResult>
    {
    }
}
