namespace Library.Models
{
    public class ListNomPrenom
    {
        public List<NomPrenomHipseaume> Results { get; set; } = new List<NomPrenomHipseaume>();
        public int Total { get; set; }
    }

    public class NomPrenomHipseaume
    {
        public Genre? Sexe { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
    }

    public enum Genre
    {
        Masculin = 1, 
        Feminin = 2
    }
}
