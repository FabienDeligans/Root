using Common.Models;

namespace Blazor.Pages.TestDragAndDrop
{
    public partial class DragAndDrop
    {
        private List<Item> _items {get; set;}
        private List<string> _zones { get; set; }

        protected override void OnInitialized()
        {
            _items = new List<Item>
            {
                new Item(){ Name = "Item 1", DropZone = "1", Order = 1 },
                new Item(){ Name = "Item 2", DropZone = "1", Order = 2 },
                new Item(){ Name = "Item 3", DropZone = "1", Order = 3 },
                new Item(){ Name = "Item 4", DropZone = "2", Order = 4 },
                new Item(){ Name = "Item 5", DropZone = "2", Order = 5 },
            };

            _zones = _items
                .DistinctBy(v => v.DropZone)
                .Select(v => v.DropZone)
                .Order()
                .ToList(); 
        }

        private void Do()
        {
            //throw new NotImplementedException();
        }
    }


    public class Item : IOrderItem
    {
        public string Name { get; set; }

        public string DropZone { get; set; }
        public int Order { get; set; }
        public string DisplayName
        {
            get => $"{Order} | {Name}";
            set => value = $"{Order} | {Name}";
        }
    }
}
