using Blazor.Controller.Modal;
using Blazor.Provider;
using Library.Blazor.CallApiAddressProvider;
using Library.Blazor.CallApiLoraineProvider;
using Library.Models;
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

        [Inject]
        public LorraineHipseaumeProvider LorraineHipseaume { get; set; }

        [Inject]
        public LorraineIpsumProvider LorraineIpsum { get; set; }

        [Inject]
        public ApiAddressProvider ApiAddressProvider { get; set; }

        [Inject]
        public ModalController ModalController { get; set; }


        private Family? Family { get; set; }
        private Parent? Parent { get; set; }
        private Child? Child { get; set; }

        private string FamilyId { get; set; }
        private string ParentId { get; set; }
        private string ChildId { get; set; }

        private string Error;

        private async Task GenerateData()
        {
            try
            {
                await InvokeAsync(async () =>
                {
                    await ModalController.ShowSpinner(async () =>
                    {
                        var random = new Random();

                        var nbFamilies = 50;

                        var families = new List<Family>();
                        var parents = new List<Parent>();
                        var children = new List<Child>();

                        var addresses = await ApiAddressProvider.GetListAddress("Brindas", nbFamilies + 1);
                        foreach (var address in addresses.Features)
                        {
                            address.Properties.housenumber = random.Next(1, 50).ToString();
                        }

                        var lorraineIpsum = new List<NomPrenomIpsum>();

                        for (var i = 0; i < nbFamilies; i++)
                        {
                            var family = new Family()
                            {
                                Name = $"Famille - {i}",
                                IsDisabled = false,
                            };
                            family = await FamilyProvider.CreateAsync(family).ConfigureAwait(false);
                            families.Add(family);

                            for (var j = 0; j < random.Next(1, 3); j++)
                            {
                                var result = await LorraineIpsum.GetListRandomName();
                                lorraineIpsum = result.ToList();

                                var rand = random.Next(0, lorraineIpsum.Count);
                                var parent = new Parent
                                {
                                    IsDisabled = false,
                                    FirstName = lorraineIpsum[rand].Prenom,
                                    LastName = lorraineIpsum[rand].Nom,
                                    Address = $"{addresses.Features[i + 1].Properties.housenumber} {addresses.Features[i + 1].Properties.street}",
                                    PostalCode = $"{addresses.Features[i].Properties.postcode}",
                                    City = $"{addresses.Features[i].Properties.city}",
                                    Phone = $"0000000000",
                                    Mail = $"{lorraineIpsum[rand].Prenom}.{lorraineIpsum[rand].Nom}@parent.com",
                                    FamilyId = family.Id
                                };
                                parents.Add(parent);
                                ParentId = parent.Id;
                                lorraineIpsum = new List<NomPrenomIpsum>();
                            }

                            for (var j = 0; j < random.Next(1, 4); j++)
                            {
                                var result = await LorraineIpsum.GetListRandomName();
                                lorraineIpsum = result.ToList();

                                var rand = random.Next(0, lorraineIpsum.Count);
                                var child = new Child
                                {
                                    IsDisabled = false,
                                    FirstName = $"{lorraineIpsum[rand].Prenom}",
                                    LastName = $"{lorraineIpsum[rand].Nom}",
                                    BirthDay = new DateTime(1985, 01, 15),
                                    FamilyId = family.Id
                                };
                                children.Add(child);
                                lorraineIpsum = new List<NomPrenomIpsum>();
                            }
                        }

                        await ParentProvider.CreateManyAsync(parents).ConfigureAwait(false);
                        await ChildProvider.CreateManyAsync(children).ConfigureAwait(false);

                        foreach (var family in families)
                        {
                            var rand = new Random();
                            if (rand.Next(0, 4) == 0)
                            {
                                family.IsDisabled = true;
                                await FamilyProvider.UpdateAsync(family);
                            }
                        }
                    });
                });
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
                await InvokeAsync(async () =>
                {
                    await ModalController.ShowSpinner(async () =>
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
                    });
                });
            }
            catch (Exception e)
            {
                Error = e.Message;
                await InvokeAsync(StateHasChanged);
            }
        }

    }
}
