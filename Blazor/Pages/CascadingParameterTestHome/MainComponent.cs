using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Blazor.Pages.CascadingParameterTestHome
{
    public class MainComponent : ComponentBase
    {
        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            base.BuildRenderTree(builder);

            builder.OpenComponent<CascadingValue<string>>(0);
            builder.AddComponentParameter(1, "Value", "ae"); // Exemple : valeur transmise
            builder.AddComponentParameter(2, "Name", "1_Parameter");
            builder.AddComponentParameter(3, "IsFixed", false);
            builder.AddComponentParameter(4, "ChildContent", ChildContent);
            builder.CloseComponent();

        }

    }

}
