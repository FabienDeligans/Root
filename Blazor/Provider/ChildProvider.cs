﻿using Library._Providers.Models.Business;
using Library.Blazor.BlazorExceptionManager;
using Library.Blazor.CallApiProvider;
using Library.Settings;
using Microsoft.Extensions.Options;

namespace Blazor.Provider
{
    public class ChildProvider : BaseCallApi<Child>
    {
        public ChildProvider(HttpClient client, IOptions<SettingsCallApi> options, BlazorExceptionManager blazorExceptionManager) : base(client, options, blazorExceptionManager)
        {
        }
    }
}
