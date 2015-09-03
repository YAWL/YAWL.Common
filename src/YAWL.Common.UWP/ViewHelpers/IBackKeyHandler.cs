// Copyright (c) Massive Pixel.  All Rights Reserved.  Licensed under the MIT License (MIT). See License.txt in the project root for license information.

using System.Threading.Tasks;

namespace YAWL.Common.ViewHelpers
{
    public interface IBackKeyHandler
    {
        Task<bool> HandleBackKey();
    }
}