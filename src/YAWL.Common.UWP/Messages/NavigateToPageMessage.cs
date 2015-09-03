// Copyright (c) Massive Pixel.  All Rights Reserved.  Licensed under the MIT License (MIT). See License.txt in the project root for license information.

using Windows.UI.Xaml.Controls;
using GalaSoft.MvvmLight.Messaging;

namespace YAWL.Common.Messages
{
    public class NavigateToPageMessage : MessageBase
    {
        public Page TargetPage { get; private set; }
        public object Parameter { get; private set; }

        public NavigateToPageMessage(Page targetPage, object parameter = null)
        {
            TargetPage = targetPage;
            Parameter = parameter;
        }
    }
}
