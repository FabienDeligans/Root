﻿using Library._Providers.Models.Business;
using Library.Blazor.BlazorExceptionManager;
using Library.Blazor.CallApiProvider;
using Library.Settings;
using Microsoft.Extensions.Options;

namespace Blazor.Provider
{
    public class ParentProvider : BaseCallApi<Parent>
    {
        public ParentProvider(HttpClient client, IOptions<SettingsCallApi> options, BlazorExceptionManager blazorExceptionManager) : base(client, options, blazorExceptionManager)
        {
        }
    }
}
