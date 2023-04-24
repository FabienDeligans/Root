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

        private string FamilyId { get; set; }
        private string ParentId { get; set; }
        private async Task GenerateData()
        {
            var random = new Random();

            var nbFamilies = 10;
            var nbParentsByFamily = random.Next(1, 3);
            var nbChildrenByFamily = random.Next(1, 4);

            var families = new List<Family>();
            var parents = new List<Parent>();

            for (var i = 0; i < nbFamilies; i++)
            {
                var family = new Family()
                {
                    Name = $"family_name_{i}"
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

                var children = new List<Child>();
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
                await ChildProvider.CreateManyAsync(children).ConfigureAwait(false);
            }
            var result = await ParentProvider.CreateManyAsync(parents).ConfigureAwait(false);
            parents = result.ToList(); 

            FamilyId = families.FirstOrDefault().Id;
            ParentId = parents.FirstOrDefault().Id; 
        }

        private async Task DropData()
        {
            await FamilyProvider.DropCollectionAsync().ConfigureAwait(false);
            await ParentProvider.DropCollectionAsync().ConfigureAwait(false);
            await ChildProvider.DropCollectionAsync().ConfigureAwait(false);
        }

        private async Task GetFullFamily()
        {
            Family = await FamilyProvider.GetOneFullAsync(FamilyId);
            await InvokeAsync(StateHasChanged);
        }

        private async Task GetFullParent()
        {
            Parent = await ParentProvider.GetOneFullAsync(ParentId);
            await InvokeAsync(StateHasChanged);
        }
    }
}
