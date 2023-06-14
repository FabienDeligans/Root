namespace Blazor.Provider.LoraineProvider.Models
{
    public class NomPrenomLorraineIpsum
    {
        public string id_name { get; set; }
        public string name_part { get; set; }
        public string id_word { get; set; }
        public string word_part { get; set; }
        public string start_count { get; set; }
        public string name_count { get; set; }
        public string word_count { get; set; }
        public string name_ratio { get; set; }
        public string word_ratio { get; set; }
    }

    public class NomPrenomIpsum
    {
        public string Nom { get; set; }
        public string Prenom { get; set; }
    }
}
