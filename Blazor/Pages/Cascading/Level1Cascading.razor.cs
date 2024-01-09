using Microsoft.AspNetCore.Components;

namespace Blazor.Pages.Cascading
{
    public partial class Level1Cascading
    {
        [CascadingParameter(Name = "ValueCascaded1")]
        public string CascadedValue { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }
    }
}
