using Blazor.Provider;
using Library.Models.Business;
using Microsoft.AspNetCore.Components;

namespace Blazor.Pages
{
    public partial class Index
    {
        [Inject]
        public FamilyProvider FamilyProvider { get; set; }

        [Inject]
        public ParentProvider ParentProvider { get; set; }

        [Inject]
        public ChildProvider ChildProvider { get; set; }


        private Family? Family { get; set; }
        private Parent? Parent { get; set; }
        private Child? Child { get; set; }

        private string FamilyId { get; set; }
        private string ParentId { get; set; }
        private string ChildId { get; set; }

        private string Error;
        private async Task GenerateData()
        {
            var random = new Random();

            var nbFamilies = 10;
            var nbParentsByFamily = random.Next(1, 3);
            var nbChildrenByFamily = random.Next(1, 4);

            var families = new List<Family>();
            var parents = new List<Parent>();
            var children = new List<Child>();
            try
            {

                for (var i = 0; i < nbFamilies; i++)
                {
                    var family = new Family()
                    {
                        //Name = $"family_name_{i}"
                    };
                    family = await FamilyProvider.CreateAsync(family).ConfigureAwait(false);
                    families.Add(family);

                    for (var j = 0; j < nbParentsByFamily; j++)
                    {
                        var parent = new Parent
                        {
                            FirstName = $"first_name_parent_{i}",
                            LastName = $"last_name_parent_{i}",
                            Address = $"address_parent_{i}",
                            PostalCode = $"postal_code_parent_{i}",
                            City = $"city_parent_{i}",
                            Phone = $"00000000_{i}",
                            Mail = $"mail@parent.{i}",
                            FamilyId = family.Id
                        };
                        parents.Add(parent);
                        ParentId = parent.Id;
                    }

                    for (var j = 0; j < nbChildrenByFamily; j++)
                    {
                        var child = new Child
                        {
                            FirstName = $"first_name_child_{i}",
                            LastName = $"last_name_child_{i}",
                            BirthDay = new DateTime(1985, 01, 15),
                            FamilyId = family.Id
                        };
                        children.Add(child);
                    }

                }
                var resultParent = await ParentProvider.CreateManyAsync(parents).ConfigureAwait(false);
                parents = resultParent.ToList();

                var resultChild = await ChildProvider.CreateManyAsync(children).ConfigureAwait(false);
                children = resultChild.ToList();

                FamilyId = families.FirstOrDefault().Id;
                ParentId = parents.FirstOrDefault().Id;
                ChildId = children.FirstOrDefault().Id;
            }
            catch (Exception e)
            {
                Error = e.Message;
                await InvokeAsync(StateHasChanged);
            }
        }

        private async Task DropData()
        {
            try
            {
                await FamilyProvider.DropCollectionAsync().ConfigureAwait(false);
                await ParentProvider.DropCollectionAsync().ConfigureAwait(false);
                await ChildProvider.DropCollectionAsync().ConfigureAwait(false);

                Family = null;
                Parent = null;
                Child = null;
                FamilyId = "";
                ParentId = "";
                ChildId = "";
            }
            catch (Exception e)
            {
                Error = e.Message;
                await InvokeAsync(StateHasChanged);
            }
        }

        private async Task GetFullFamily()
        {
            if (!string.IsNullOrWhiteSpace(FamilyId))
            {
                Family = await FamilyProvider.GetOneFullAsync(FamilyId);
                await InvokeAsync(StateHasChanged);
            }
        }

        private async Task GetFullParent()
        {
            if (!string.IsNullOrWhiteSpace(ParentId))
            {
                Parent = await ParentProvider.GetOneFullAsync(ParentId);
                await InvokeAsync(StateHasChanged);
            }
        }

        private async Task GetFullChild()
        {
            if (!string.IsNullOrWhiteSpace(ChildId))
            {
                Child = await ChildProvider.GetOneFullAsync(ChildId);
                await InvokeAsync(StateHasChanged);
            }
        }
    }
}
