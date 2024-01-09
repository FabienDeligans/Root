using Microsoft.AspNetCore.Components;

namespace Blazor.Pages.Cascading
{
    public partial class Level2Cascading
    {
        [CascadingParameter(Name = "ValueCascaded1")]
        public string CascadedValue { get; set; }

        [CascadingParameter(Name = "ValueCascaded2")]
        public string CascadedValue2 { get; set; }
    }
}
