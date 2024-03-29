﻿using MongoDB.Bson.Serialization.Attributes;

namespace Common.Models.MES;

public class Etape : Entity, IOrderItem
{
    public string Nom { get; set; }

    public string? Commentaire { get; set; }

    public List<ArticleConsome>? ArticlesConsommes { get; set; }

    public TimeSpan? DureePrevue { get; set; }
    
    [BsonIgnore]
    public List<Gamme>? GammeFabrications { get; set;}

    public string DropZone { get; set; } = "1";
    public int Order { get; set; }
    public string DisplayName
    {
        get => $"{Order} | {Nom}";
        set => value = $"{Order} | {Nom}" ;
    }
}

public class ArticleConsome
{
    public string ArticleId { get; set; }
    public string ArticleNom { get; set; }
    public int QuantityToUse { get; set; }
    public int? QuantityUsed { get; set; }
    public DateTime? DateUsed { get; set; }
}