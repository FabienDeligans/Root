namespace Blazor.Pages.ExpandablePanel
{
    public partial class ExpandPanelPage
    {
        private void Callback()
        {
            IsOpen = !IsOpen;
            InvokeAsync(StateHasChanged); 
        }

        private bool IsOpen { get; set; }
    }
}
