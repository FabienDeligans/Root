﻿namespace Library.Models
{
    public class ListNomPrenom
    {
        public List<NomPrenomLorraineHipseaume> Results { get; set; } = new List<NomPrenomLorraineHipseaume>();
        public int Total { get; set; }
    }

    public class NomPrenomLorraineHipseaume
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
