namespace Common.Models.MES;

public class OrdreFabrication : Entity
{
    public Article ArticleFabrique { get; set; }
    public Gamme Gamme { get; set; }
    public DateTime DateDebut { get; set; }
    public DateTime DateFin { get; set; }
    public List<Etape> EtapesExecutes { get; set; }
}