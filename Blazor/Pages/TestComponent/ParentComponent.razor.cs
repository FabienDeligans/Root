using Common.Helper;

namespace Blazor.Pages.TestComponent
{
    public partial class ParentComponent
    {
        protected async Task RefreshMe()
        {
            await OnInitializedAsync(); 
        }

        public string Text { get; set; }

        public List<string>TextList { get; set; }


        protected override void OnInitialized()
        {
            TextList = new List<string>();
            for (var i = 0; i < 10; i++)
            {
                TextList.Add(RandomHelper.GetRandomString(10));
            }
        }


        private void NewItem()
        {
            TextList.Add(Text);
        }
    }
}
