namespace Library._Providers.Models.Business
{
    public class Saison : Entity
    {
        public string SaisonYears { get; set; }

        public List<Activity> Activities { get; set; }
    }
}
