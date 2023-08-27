using MudBlazor.Utilities;
using MudBlazor;

namespace Blazor.Pages
{
    public partial class Test
    {
        private void ItemUpdated(MudItemDropInfo<DropItem> dropItem)
        {
            dropItem.Item.Zone = dropItem.DropzoneIdentifier;


            int indexOffset;
            indexOffset = 0;
            var distinctDropZone = _items.DistinctBy(v => v.Zone).Select(v => v.Zone).Order();

            if (dropItem.DropzoneIdentifier != distinctDropZone.First())
            {
                indexOffset = _items.Count(x => x.Zone == distinctDropZone.First());
            }

            _items.UpdateOrder(dropItem, item => item.Order, indexOffset);
        }

        private List<DropItem> _items = new()
        {
            new DropItem(){ Name = "Item 1", Zone = "1", Order = 1 },
            new DropItem(){ Name = "Item 2", Zone = "1", Order = 2 },
            new DropItem(){ Name = "Item 3", Zone = "1", Order = 3 },
            new DropItem(){ Name = "Item 4", Zone = "2", Order = 4 },
            new DropItem(){ Name = "Item 5", Zone = "2", Order = 5 },
        };

        public class DropItem
        {
            public string Name { get; init; }
            public string Zone { get; set; }
            public int Order { get; set; }
        }
    }
}
