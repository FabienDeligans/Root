using Common.Models.CustomAttribute;
using Common.Models.MES.Article;
using MongoDB.Bson.Serialization.Attributes;

namespace Common.Models.MES;

public class Ope : Entity
{
    public int Step { get; set; }

    public string Name { get; set; }

    public string? Description { get; set; }

    public bool Done { get; set; }

    public Dictionary<string, int> ArticlesUsedIdQuantity { get; set; }
}