namespace Library.Models
{
    public class ListNomPrenom
    {
        public List<NomPrenomLorraineHipseaume> Results { get; set; } = new List<NomPrenomLorraineHipseaume>();
        public int Total { get; set; }
    }

    public class NomPrenomLorraineHipseaume
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
