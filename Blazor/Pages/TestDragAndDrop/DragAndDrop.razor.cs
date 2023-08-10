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
            //dropItem.Item.Selector = dropItem.DropzoneIdentifier;

            _dropzoneItems.UpdateOrder(dropItem, item => item.Order);
        }

        private List<DropItem> _dropzoneItems = new();

        private List<DropItem> _serverData = new()
        {
            new DropItem() { Order = 0, Name = "Item 1", Selector = "1" },
            new DropItem() { Order = 1, Name = "Item 2", Selector = "1" },
            new DropItem() { Order = 2, Name = "Item 3", Selector = "1" },
            new DropItem() { Order = 3, Name = "Item 4", Selector = "1" },
            new DropItem() { Order = 4, Name = "Item 5", Selector = "1" },
            new DropItem() { Order = 5, Name = "Item 6", Selector = "1" },
            new DropItem() { Order = 6, Name = "Item 7", Selector = "2" },
            new DropItem() { Order = 7, Name = "Item 8", Selector = "2" },
            new DropItem() { Order = 8, Name = "Item 9", Selector = "2" },
            new DropItem() { Order = 9, Name = "Item 10", Selector = "2" },
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
                    Selector = item.Selector
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
                    Selector = item.Selector
                })
                .ToList();

        private void Reset()
        {
            _dropzoneItems = new();
            _serverData = new()
            {
                new DropItem() { Order = 0, Name = "Item 1", Selector = "1" },
                new DropItem() { Order = 1, Name = "Item 2", Selector = "1" },
                new DropItem() { Order = 2, Name = "Item 3", Selector = "1" },
                new DropItem() { Order = 3, Name = "Item 4", Selector = "1" },
                new DropItem() { Order = 4, Name = "Item 5", Selector = "1" },
                new DropItem() { Order = 5, Name = "Item 6", Selector = "1" },
                new DropItem() { Order = 6, Name = "Item 7", Selector = "2" },
                new DropItem() { Order = 7, Name = "Item 8", Selector = "2" },
                new DropItem() { Order = 8, Name = "Item 9", Selector = "2" },
                new DropItem() { Order = 9, Name = "Item 10", Selector = "2" }
            };

            RefreshContainer();
        }

        public class DropItem
        {
            public string Name { get; init; }
            public string Selector { get; set; }
            public int Order { get; set; }
        }

        protected override void OnInitialized()
        {
            var a = 1; 
        }
    }
}
