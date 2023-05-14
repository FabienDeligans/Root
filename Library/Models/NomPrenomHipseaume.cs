namespace Library.Models
{
    public class ListNomPrenomLorraineHipseaume
    {
        public List<NomPrenomHipseaume> Results { get; set; } = new List<NomPrenomHipseaume>();
        public int Total { get; set; }
    }

    public class NomPrenomHipseaume
    {
        public Sexe? Sexe { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
    }

    public enum Sexe
    {
        Masculin = 1, 
        Feminin = 2
    }
}
