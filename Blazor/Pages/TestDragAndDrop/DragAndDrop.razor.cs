using Common.Helper;
using Common.Models.TestDragAndDrop;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;
using MudBlazor.Utilities;

namespace Blazor.Pages.TestDragAndDrop
{
    public partial class DragAndDrop
    {
        private MudDropContainer<DropItem> _container;

        private void ItemUpdated(MudItemDropInfo<DropItem> dropItem)
        {
            dropItem.Item.DropZone = dropItem.DropzoneIdentifier;

            var indexOffset = dropItem.DropzoneIdentifier switch
            {
                "2" => _dropzoneItems.Count(x => x.DropZone == "1"),
                _ => 0
            };

            _dropzoneItems.UpdateOrder(dropItem, item => item.Order, indexOffset);
        }

        private List<DropItem> _dropzoneItems = new();

        private List<DropItem> _serverData = new()
        {
            new DropItem() { Order = 0, Name = "Item 1", DropZone = "1" },
            new DropItem() { Order = 1, Name = "Item 2", DropZone = "1" },
            new DropItem() { Order = 2, Name = "Item 3", DropZone = "1" },
            new DropItem() { Order = 3, Name = "Item 4", DropZone = "1" },
            new DropItem() { Order = 4, Name = "Item 5", DropZone = "1" },
            new DropItem() { Order = 5, Name = "Item 6", DropZone = "1" },
            new DropItem() { Order = 6, Name = "Item 7", DropZone = "2" },
            new DropItem() { Order = 7, Name = "Item 8", DropZone = "2" },
            new DropItem() { Order = 8, Name = "Item 9", DropZone = "2" },
            new DropItem() { Order = 9, Name = "Item 10", DropZone = "2" },
        };

        private void RefreshContainer()
        {
            //update the binding to the container
            StateHasChanged();

            //the container refreshes the internal state
            _container.Refresh();
        }

        private void LoadServerData()
        {
            _dropzoneItems = _serverData
                .OrderBy(x => x.Order)
                .Select(item => new DropItem
                {
                    Order = item.Order,
                    Name = item.Name,
                    DropZone = item.DropZone
                })
                .ToList();
            RefreshContainer();
        }

        private void SaveData()
            => _serverData = _dropzoneItems
                .OrderBy(x => x.Order)
                .Select(item => new DropItem
                {
                    Order = item.Order,
                    Name = item.Name,
                    DropZone = item.DropZone
                })
                .ToList();

        private void Reset()
        {
            _dropzoneItems = new();
            _serverData = new()
            {
                new DropItem() { Order = 0, Name = "Item 1", DropZone = "1" },
                new DropItem() { Order = 1, Name = "Item 2", DropZone = "1" },
                new DropItem() { Order = 2, Name = "Item 3", DropZone = "1" },
                new DropItem() { Order = 3, Name = "Item 4", DropZone = "1" },
                new DropItem() { Order = 4, Name = "Item 5", DropZone = "1" },
                new DropItem() { Order = 5, Name = "Item 6", DropZone = "1" },
                new DropItem() { Order = 6, Name = "Item 7", DropZone = "2" },
                new DropItem() { Order = 7, Name = "Item 8", DropZone = "2" },
                new DropItem() { Order = 8, Name = "Item 9", DropZone = "2" },
                new DropItem() { Order = 9, Name = "Item 10", DropZone = "2" }
            };

            RefreshContainer();
        }

        public class DropItem
        {
            public string Name { get; init; }
            public string DropZone { get; set; }
            public int Order { get; set; }
        }



    }
}
