using Common.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using MudBlazor.Utilities;

namespace Blazor.Pages.Components
{
    public partial class DragAndDropComponent<T> : ChildComponentBase where T : IOrderItem
    {
        [CascadingParameter]
        public List<T> OrderItems { get; set; }

        [Parameter]
        public string Title { get; set; }

        [Parameter]
        public List<string> Zones { get; set; }

        private async Task ItemUpdated(MudItemDropInfo<T> dropItem)
        {
            dropItem.Item.DropZone = dropItem.DropzoneIdentifier;

            int indexOffset;
            indexOffset = 0;
            var distinctDropZone = OrderItems.DistinctBy(v => v.DropZone).Select(v => v.DropZone).Order();

            if (dropItem.DropzoneIdentifier != distinctDropZone.First())
            {
                indexOffset = OrderItems.Count(x => x.DropZone == distinctDropZone.First());
            }
            OrderItems.UpdateOrder(dropItem, item => item.Order, indexOffset);

            await RefreshParent();
        }
    }
}
