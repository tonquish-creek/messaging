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

using Autofac;

namespace TonquishCreek.Messaging.Autofac
{
    /// <summary>Registers messaging types and services with the Autofac container.</summary>
    public sealed class AutofacModule : Module
    {
        #region Constructor(s)
        /// <summary>Initializes a new instance of the <see cref="AutofacModule"/> class.</summary>
        public AutofacModule()
            : base()
        {
        }
        #endregion

        #region Protected Method(s)
        /// <summary>Called to add registrations to the container.</summary>
        /// <param name="builder">The builder through which components can be registered.</param>
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);
        }
        #endregion
    }
}
