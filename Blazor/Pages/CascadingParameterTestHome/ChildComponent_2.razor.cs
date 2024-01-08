using Microsoft.AspNetCore.Components;

namespace Blazor.Pages.CascadingParameterTestHome
{
    public partial class ChildComponent_2
    {
        [CascadingParameter(Name = "2_Parameter")]
        public string Parameter_2 { get; set; }
    }
}
