// |-----------------------------------------------------------------------------------------------------|
// <copyright file="IMapFrom.cs" company="Roomed">
// Copyright (c) Roomed. All Rights Reserved.
// Licensed under the GPLv3 license. See LICENSE file in the project root for full license information.
// </copyright>
// |-----------------------------------------------------------------------------------------------------|

namespace Roomed.Services.Mapping
{
    /// <summary>
    /// This interface is used to mark that the implementing class maps from <typeparamref name="TClass"/>.
    /// </summary>
    /// <typeparam name="TClass">The class that the implemented one maps from.</typeparam>
    public interface IMapFrom<TClass>
    {
    }
}
