using Common.Helper;

namespace Blazor.Pages.Cascading
{
    public partial class TestCascading
    {
        public string CascadedValue1 { get; set; } = RandomHelper.GetRandomString(10);
        public string CascadedValue2 { get; set; } = RandomHelper.GetRandomString(10);
    }
}
