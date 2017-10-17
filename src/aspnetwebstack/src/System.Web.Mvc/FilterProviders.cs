// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.

namespace System.Web.Mvc
{
    /// <summary>
    /// 提供Filter AOP讀取的位置
    /// </summary>
    public static class FilterProviders
    {
        static FilterProviders()
        {
            Providers = new FilterProviderCollection();
            Providers.Add(GlobalFilters.Filters);
            Providers.Add(new FilterAttributeFilterProvider());
            Providers.Add(new ControllerInstanceFilterProvider());
        }

        public static FilterProviderCollection Providers { get; private set; }
    }
}
