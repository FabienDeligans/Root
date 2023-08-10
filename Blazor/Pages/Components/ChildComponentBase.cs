using Microsoft.AspNetCore.Components;

namespace Blazor.Pages.Components
{
    public abstract class ChildComponentBase : ComponentBase
    {
        [Parameter]
        public EventCallback OnChildChange { get; set; }

        protected virtual async Task RefreshParent()
        {
            await OnChildChange.InvokeAsync();
        }
    }
}
