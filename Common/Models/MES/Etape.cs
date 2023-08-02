﻿using MongoDB.Bson.Serialization.Attributes;

namespace Common.Models.MES;

public class Etape : Entity
{
    public int NumeroEtape { get; set; } 
    
    public string Nom { get; set; }
    
    public List<ArticleConsome>? ArticlesConsommes { get; set; }

    [BsonIgnore]
    public List<Gamme>? GammeFabrications { get; set;}
}

public class ArticleConsome
{
    public string ArticleId { get; set; }
    public string ArticleNom { get; set; }
    public int QuantityToUse { get; set; }
}