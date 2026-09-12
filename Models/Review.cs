using System;
using System.ComponentModel.DataAnnotations;

namespace SouvenirShop.Models{
    public class Review{
        public int? id {get; set; }
        public string? Author_name {get; set; }

        public string? Text {get; set; }

        
        public int? Rating {get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public int? ProductId { get; set; }
        public Souvenir? Sou { get; set; }
    }
}