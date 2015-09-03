// Copyright (c) Massive Pixel.  All Rights Reserved.  Licensed under the MIT License (MIT). See License.txt in the project root for license information.

using GalaSoft.MvvmLight.Ioc;

namespace YAWL.Common.Extensions
{
    public static class SimpleIocExtensions
    {
        public static TService TryGetInstance<TService>(this SimpleIoc simpleIoc)
        {
            return simpleIoc.IsRegistered<TService>()
                ? simpleIoc.GetInstance<TService>() 
                : default(TService);
        }
    }
}
