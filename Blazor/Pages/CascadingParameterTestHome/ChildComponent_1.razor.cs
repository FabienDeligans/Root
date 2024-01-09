using Microsoft.AspNetCore.Components;

namespace Blazor.Pages.CascadingParameterTestHome
{
    public partial class ChildComponent_1
    {
        [CascadingParameter(Name = "1_Parameter")]
        public string? Parameter_1 { get; set; }

    }
}
