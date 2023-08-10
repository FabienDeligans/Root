using Blazor.Pages.Components;
using Microsoft.AspNetCore.Components;

namespace Blazor.Pages.TestComponent
{
    public partial class ChildComponent : ChildComponentBase
    {
        [Parameter]
        public List<string> TextList { get; set; }

        protected override Task RefreshParent()
        {
            return base.RefreshParent();
        }
    }
}
