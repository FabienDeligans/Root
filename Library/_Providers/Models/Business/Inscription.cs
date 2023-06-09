﻿using System.ComponentModel.DataAnnotations;
using Library._Providers.CustomAttribute;
using MongoDB.Bson.Serialization.Attributes;

namespace Library._Providers.Models.Business
{
    public class Inscription : Entity
    {
        [Required]
        [ForeignKey(typeof(Child))]
        public string ChildId { get; set; }

        [BsonIgnore]
        public Child Child { get; set; }

        [Required]
        [ForeignKey(typeof(Activity))]
        public string ActivityId { get; set; }

        [BsonIgnore]
        public Activity Activity { get; set; }

        public double? ReductionPercentage { get; set; }

        public List<double>? Payment { get; set; }

        [Required]
        [ForeignKey(typeof(Saison))]
        public string SaisonId { get; set; }

        [Required]
        public Saison Saison { get; set; }
    }
}
