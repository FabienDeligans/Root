using Common.Helper;
using Common.Models.TestDragAndDrop;
using Microsoft.AspNetCore.Components.Web;

namespace Blazor.Pages.TestDragAndDrop
{
    public partial class DragAndDrop
    {
        private List<DragAndDropItem> Items { get; set; }
        private DragAndDropItem ItemToMove { get; set; }


        protected override void OnInitialized()
        {
            Items = new List<DragAndDropItem>();

            for (var i = 0; i < 10; i++)
            {
                var item = new DragAndDropItem
                {
                    Position = 0,
                    Texte = RandomHelper.GetRandomString(10)
                };
                Items.Add(item);
            }
        }

        private void HandleDrop(DragAndDropItem landingModel)
        {
            // landing model -> where the drop happened
            if (ItemToMove is null) return;

            // keep the original order for later
            var originalOrderLanding = landingModel.Position;

            // increase model under landing one by 1
            Items.Where(x => x.Position >= landingModel.Position).ToList().ForEach(x => x.Position++);

            // replace landing model
            ItemToMove.Position = originalOrderLanding;

            var i = 0;
            foreach (var model in Items.OrderBy(x => x.Position).ToList())
            {
                // keep the numbers from 0 to size-1
                model.Position = i++;

                // remove drag over. 
                model.IsDragOver = false;
            }
        }
    }
}
