using Blazor.Controller.Modal;
using Blazor.Provider;
using Blazor.Provider.CallApiAddressProvider;
using Blazor.Provider.CallApiLoraineProvider;
using Blazor.Provider.CallApiLoraineProvider.Models;
using Library._Providers.Models.Business;
using Library.Blazor.CallApiProvider;
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

        private async Task GenerateData()
        {
            try
            {
                await InvokeAsync(async () =>
                {
                    await ModalController.ShowSpinner(async () =>
                    {
                        var random = new Random();

                        var nbFamilies = 10;

                        var families = new List<Family>();
                        var parents = new List<Parent>();
                        var children = new List<Child>();

                        var addresses = await ApiAddressProvider.GetListAddress("Brindas", 100);
                        foreach (var address in addresses.Features)
                        {
                            address.Properties.housenumber = random.Next(1, 50).ToString();
                        }

                        var nomPrenomIpsums = new List<NomPrenomIpsum>();

                        for (var i = 0; i < nbFamilies; i++)
                        {
                            var family = new Family()
                            {
                                Name = $"Famille - {i}",
                                IsDisabled = false,
                            };
                            family = await FamilyProvider.CreateAsync(family).ConfigureAwait(false);
                            families.Add(family);

                            var rand = random.Next(1, 100);
                            for (var j = 0; j < random.Next(1, 3); j++)
                            {
                                var result = await LorraineIpsum.GetListRandomName();
                                nomPrenomIpsums = result.ToList();

                                var randName = random.Next(1, nomPrenomIpsums.Count);
                                var parent = new Parent
                                {
                                    IsDisabled = false,
                                    FirstName = nomPrenomIpsums[randName].Prenom,
                                    LastName = nomPrenomIpsums[randName].Nom,
                                    Address = $"{addresses.Features[rand].Properties.housenumber} {addresses.Features[rand].Properties.street}",
                                    PostalCode = $"{addresses.Features[j].Properties.postcode}",
                                    City = $"{addresses.Features[j].Properties.city}",
                                    Phone = $"0000000000",
                                    Mail = $"{nomPrenomIpsums[randName].Prenom}.{nomPrenomIpsums[randName].Nom}@parent.com",
                                    FamilyId = family.Id
                                };
                                parents.Add(parent);
                                ParentId = parent.Id;
                                nomPrenomIpsums = new List<NomPrenomIpsum>();
                            }

                            for (var j = 0; j < random.Next(1, 4); j++)
                            {
                                var result = await LorraineIpsum.GetListRandomName();
                                nomPrenomIpsums = result.ToList();

                                var randName = random.Next(1, nomPrenomIpsums.Count);
                                var child = new Child
                                {
                                    IsDisabled = false,
                                    FirstName = $"{nomPrenomIpsums[randName].Prenom}",
                                    LastName = $"{nomPrenomIpsums[randName].Nom}",
                                    BirthDay = new DateTime(1985, 01, 15),
                                    FamilyId = family.Id
                                };
                                children.Add(child);
                                nomPrenomIpsums = new List<NomPrenomIpsum>();
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

                await ModalController.ShowModalAlert($"Génération des datas terminées", 2000, Alert.Success);

            }
            catch (Exception e)
            {
                await ModalController.ShowModalAlert($"{e.Message}", 2000, Alert.Danger);

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

                await ModalController.ShowModalAlert($"Effacement de la base de données effectuée", 2000, Alert.Success);
            }
            catch (Exception e)
            {
                await ModalController.ShowModalAlert($"{e.Message}", 2000, Alert.Danger);

                await InvokeAsync(StateHasChanged);
            }
        }

    }
}
