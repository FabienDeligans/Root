using Common.Helper;

namespace Blazor.Pages.CascadingParameterTestHome
{
    public partial class CascadingPageMain
    {
        public string Parameter_1 { get; set; }
        public string Parameter_2 { get; set; }

        protected override void OnInitialized()
        {
            ChangeParameter();
            base.OnInitialized();
        }

        private void ChangeParameter()
        {
            Parameter_1 = RandomHelper.GetRandomString(10);
            Parameter_2 = RandomHelper.GetRandomString(10);
        }

    }
}
