using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Blazor.Pages.SelectSearch
{
    public partial class InputSelectCustom
    {
        [Parameter]
        public ICollection<ObjectValue> ListOfChoice { get; set; }

        [Parameter]
        public bool MultipleSelection { get; set; }

        [Parameter]
        public bool Search { get; set; }

        [Parameter]
        public bool Dense { get; set; }

        [Parameter]
        public bool SelectAll { get; set; }

        [Parameter]
        public string Label { get; set; }
        
        [Parameter]
        public EventCallback<IEnumerable<ObjectValue>> OnSelectedValuesChanged { get; set; }

        private ICollection<string> _displayValue = []; 
        private IEnumerable<string> _selectedValue = [];

        private IEnumerable<string> SelectedValues
        {
            get => _selectedValue;
            set
            {
                _selectedValue = value;
                CreateObjectValue(SelectedValues); 
            }
        }

        protected override void OnInitialized()
        {
            _displayValue = ListOfChoice.Select(v => v.Title).ToList(); 
        }

        private void CreateObjectValue(IEnumerable<string> selectedValues)
        {
            var result = ListOfChoice.Where(v => selectedValues.Contains(v.Title));
            OnSelectedValuesChanged.InvokeAsync(result); 
        }

    }

    public class ObjectValue
    {
        public string Title { get; set; }
        public object Value { get; set; }

    }
}
