using Common.Models.CustomAttribute;

namespace Common.Models.MES;

public class OrdreFabrication : Entity
{
    public long Number { get; set; }

    [ForeignKey(typeof(Article))]
    public string? ArticleId { get; set; }

    public Article? ArticleFabrique { get; set; }

    [ForeignKey(typeof(Gamme))]
    public string GammeId { get; set; }

    public Gamme? Gamme { get; set; }
    public DateTime? DateDebut { get; set; }
    public DateTime? DateFin { get; set; }
    public List<EtapeExecuted>? EtapesExecuted { get; set; }
}

public class EtapeExecuted : Etape
{
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
}