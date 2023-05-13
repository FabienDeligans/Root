using Blazor.Controller.Modal;
using Blazor.Pages.PagesEnfants;
using Blazor.Pages.PagesParents;
using Blazor.Provider;
using Blazored.Modal;
using Library.Models.Business;
using Microsoft.AspNetCore.Components;

namespace Blazor.Pages.PagesFamilies
{
    public partial class FamillePage
    {
        [Inject]
        public FamilyProvider? FamilyProvider { get; set; }

        [Inject]
        public ParentProvider? ParentProvider { get; set; }

        [Inject]
        public ChildProvider? ChildProvider { get; set; }

        [Inject]
        public ModalController? ModalController { get; set; }

        private IEnumerable<Family>? Families { get; set; }
        private IEnumerable<Parent>? Parents { get; set; }
        private IEnumerable<Child>? Children { get; set; }

        private Family Family { get; set; }

        private bool ShowAll { get; set; }

        private bool Loaded { get; set; }
    
        protected override async Task OnInitializedAsync()
        {
            await ModalController.ShowSpinner(async () =>
            {
                await InvokeAsync(async () =>
                {
                    var famillesTask = FamilyProvider!.GetAllFilteredByPropertyEqualAsync(nameof(Family.IsDisabled), "false");
                    var parentTask = ParentProvider!.GetAllFilteredByPropertyEqualAsync(nameof(Parent.IsDisabled), "false");
                    var enfantTask = ChildProvider!.GetAllFilteredByPropertyEqualAsync(nameof(Child.IsDisabled), "false");

                    await Task.WhenAll(famillesTask, parentTask, enfantTask);

                    Families = await famillesTask;
                    Parents = await parentTask;
                    Children = await enfantTask;

                    ShowAll = false;
                    Loaded = true;
                });
            });
        }

        private async Task AllLoad()
        {
            Loaded = false;
            await InvokeAsync(StateHasChanged);

            var famillesTask = FamilyProvider!.GetAllAsync();
            var parentTask = ParentProvider!.GetAllAsync();
            var enfantTask = ChildProvider!.GetAllAsync();

            await Task.WhenAll(famillesTask, parentTask, enfantTask);

            Families = await famillesTask;
            Parents = await parentTask;
            Children = await enfantTask;

            ShowAll = !ShowAll;
            Loaded = true;
        }

        private async Task PartialLoad()
        {
            Loaded = false;
            await InvokeAsync(StateHasChanged);

            var famillesTask = FamilyProvider!.GetAllFilteredByPropertyEqualAsync(nameof(Family.IsDisabled), "false");
            var parentTask = ParentProvider!.GetAllFilteredByPropertyEqualAsync(nameof(Parent.IsDisabled), "false");
            var enfantTask = ChildProvider!.GetAllFilteredByPropertyEqualAsync(nameof(Child.IsDisabled), "false");

            await Task.WhenAll(famillesTask, parentTask, enfantTask);

            Families = await famillesTask;
            Parents = await parentTask;
            Children = await enfantTask;

            ShowAll = !ShowAll;
            Loaded = true;
        }

        private async Task ToggleShowAll()
        {
            if (!ShowAll)
            {
                await AllLoad();
            }
            else
            {
                await PartialLoad();
            }
            await InvokeAsync(StateHasChanged);
        }

        private async Task LoadFamily(ChangeEventArgs obj)
        {
            if (!String.IsNullOrEmpty(obj.Value!.ToString()))
            {
                var id = obj.Value!.ToString();
                Family = (await FamilyProvider!.GetOneFullAsync(id))!;
                await InvokeAsync(StateHasChanged);
            }
            else
            {
                Family = null;
            }
        }

        private async Task UpdateFamily(string? familyId)
        {
            var parameters = new ModalParameters()
            {
                {nameof(FamilleModalPage.FamilyId), familyId}
            };
            var result = (Family)await ModalController.ShowModal<FamilleModalPage>("Modifier une famille", parameters);

            await InvokeAsync(async () =>
            {
                if (result != null)
                {
                    await PartialLoad();
                    await LoadFamily(new ChangeEventArgs() { Value = result.Id });
                    await ModalController.ShowModalAlert($"Modification OK pour : {result.Name}", 1000, Alert.Success);
                }
                else
                {
                    await ModalController.ShowModalAlert($"Action annullée", 1000, Alert.Danger);
                }
            });
        }

        private async Task CreateFamily()
        {
            var result = (Family)await ModalController.ShowModal<FamilleModalPage>("Ajouter une famille");

            await InvokeAsync(async () =>
            {
                if (result != null)
                {
                    await PartialLoad();
                    await LoadFamily(new ChangeEventArgs() { Value = result.Id });
                    await ModalController.ShowModalAlert($"Modification OK pour : {result.Name}", 1000, Alert.Success);
                }
                else
                {
                    await ModalController.ShowModalAlert($"Action annullée", 1000, Alert.Danger);
                }
            });
        }

        private async Task UpdateParent(string? parentId)
        {
            var parameters = new ModalParameters()
            {
                {nameof(ParentModalPage.ModelId), parentId}
            };
            var result = (Parent)await ModalController.ShowModal<ParentModalPage>("Modifier un parent", parameters);

            await InvokeAsync(async () =>
            {
                if (result != null)
                {
                    await PartialLoad();
                    await LoadFamily(new ChangeEventArgs() { Value = result.FamilyId });
                    await ModalController.ShowModalAlert($"Modification OK pour : {result.FirstName} {result.LastName}", 1000, Alert.Success);
                }
                else
                {
                    await ModalController.ShowModalAlert($"Action annullée", 1000, Alert.Danger);
                }
            });
        }

        private async Task CreateParent()
        {
            var result = (Parent)await ModalController.ShowModal<ParentModalPage>("Ajouter un parent");

            await InvokeAsync(async () =>
            {
                if (result != null)
                {
                    await PartialLoad();
                    await LoadFamily(new ChangeEventArgs() { Value = result.FamilyId });
                    await ModalController.ShowModalAlert($"Enregistrement OK pour : {result.FirstName} {result.LastName}", 1000, Alert.Success);
                }
                else
                {
                    await ModalController.ShowModalAlert($"Action annullée", 1000, Alert.Danger);
                }
            });
        }

        private async Task UpdateEnfant(string? enfantId)
        {
            var parameters = new ModalParameters()
            {
                {nameof(ParentModalPage.ModelId), enfantId}
            };
            var result = (Child)await ModalController.ShowModal<EnfantModalPage>("Modifier un enfant", parameters);

            await InvokeAsync(async () =>
            {
                if (result != null)
                {
                    await PartialLoad();
                    await LoadFamily(new ChangeEventArgs() { Value = result.FamilyId });
                    await ModalController.ShowModalAlert($"Modification OK pour : {result.FirstName} {result.LastName}", 1000, Alert.Success);
                }
                else
                {
                    await ModalController.ShowModalAlert($"Action annullée", 1000, Alert.Danger);
                }
            });
        }

        private async Task CreateEnfant()
        {
            var result = (Child)await ModalController.ShowModal<EnfantModalPage>("Ajouter un enfant");

            await InvokeAsync(async () =>
            {
                if (result != null)
                {
                    await PartialLoad();
                    await LoadFamily(new ChangeEventArgs() { Value = result.FamilyId });
                    await ModalController.ShowModalAlert($"Enregistrement OK pour : {result.FirstName} {result.LastName}", 1000, Alert.Success);
                }
                else
                {
                    await ModalController.ShowModalAlert($"Action annullée", 1000, Alert.Danger);
                }
            });
        }
    }
}
